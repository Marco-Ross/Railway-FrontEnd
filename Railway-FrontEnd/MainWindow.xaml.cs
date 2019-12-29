using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Railway_FrontEnd.domain.actors;
using Railway_FrontEnd.domain.timings;
using Railway_FrontEnd.domain.transit;
using Railway_FrontEnd.domain.util;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Railway_FrontEnd
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		RestClient restClient;
		public MainWindow()
		{
			InitializeComponent();
			restClient = new RestClient();
		}



		//VIEW SCHEDULES TAB//
		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			var result = await restClient.MakeRequest("GETALL", "schedule/getAll");

			ScheduleListBox.Items.Clear();

			for (int i = 0; i < result.Count; i++)
			{
				ScheduleListBox.Items.Add(result[i]);
			}

			// Add columns
			var gridView = new GridView();
			ScheduleListBox.View = gridView;
			gridView.Columns.Add(new GridViewColumn
			{
				Header = "Train Number",
				DisplayMemberBinding = new Binding("trainNumber")
			});
			gridView.Columns.Add(new GridViewColumn
			{
				Header = "Booked Seats",
				DisplayMemberBinding = new Binding("capacity")
			});
			gridView.Columns.Add(new GridViewColumn
			{
				Header = "Departure Date",
				DisplayMemberBinding = new Binding("departureDate")
			});
			gridView.Columns.Add(new GridViewColumn
			{
				Header = "Departure",
				DisplayMemberBinding = new Binding("departure")
			});
			gridView.Columns.Add(new GridViewColumn
			{
				Header = "Arrival",
				DisplayMemberBinding = new Binding("arrival")
			});
			gridView.Columns.Add(new GridViewColumn
			{
				Header = "Dep Loca",
				DisplayMemberBinding = new Binding("departureLocation")
			});
			gridView.Columns.Add(new GridViewColumn
			{
				Header = "Arr Loca",
				DisplayMemberBinding = new Binding("arrivalLocation")
			});
		}

		private async void LoadTrains_Click(object sender, RoutedEventArgs e)
		{
			var result = await restClient.MakeRequest("GETALL", "train/getAll");

			TrainNumberCreateCbx.Items.Clear();
			for (int i = 0; i < result.Count; i++)
			{
				JObject jSchedule = result[i] as JObject;
				Train train = jSchedule.ToObject<Train>();

				TrainNumberCreateCbx.Items.Add(train.getTrainNumber());
			}

			var resultStation = await restClient.MakeRequest("GETALL", "station/getAll");

			DepartureLocationCbx.Items.Clear();
			ArrivalLocationCbx.Items.Clear();
			for (int i = 0; i < resultStation.Count; i++)
			{
				JObject jSchedule = resultStation[i] as JObject;
				Station station = jSchedule.ToObject<Station>();

				DepartureLocationCbx.Items.Add(station.stationName);
				ArrivalLocationCbx.Items.Add(station.stationName);
			}			
		}

		private async void NewScheduleS_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				string departTimeCreate = DepartureTimeCreateCbx.Text;
				string arriveTimeCreate = ArrivalTimeCreateCbx.Text;
				int trainNumberCreate = int.Parse(TrainNumberCreateCbx.Text);
				//string departLocation = DepartureLocationCbx.Text;
				//string arriveLocation = ArrivalLocationCbx.Text;

				DateTime date = (DateTime)DepartureDateCreateDte.SelectedDate;
				string departDateCreate = date.ToString("yyyy-MM-dd");

				var result = await restClient.MakeRequest("POST", "schedule/create", new NewSchedule(trainNumberCreate, 0, departDateCreate, departTimeCreate, arriveTimeCreate));

				if (result.FirstOrDefault() == null)
				{
					MessageBox.Show("You cant create a Schedule this close together");
				}

				Button_Click(sender, e);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Please enter all fields");
			}
			
		}

		private void UpdateScheduleS_Click(object sender, RoutedEventArgs e)
		{
			UpdateSchedulePopUp popUp = new UpdateSchedulePopUp();
			try
			{
				object scheduleSelected = ScheduleListBox.SelectedItem;
				JObject jSchedule = scheduleSelected as JObject;
				NewSchedule schedule = jSchedule.ToObject<NewSchedule>();

				popUp.DepartureTimeCbx.Text = schedule.departure;
				popUp.ArrivalTimeCbx.Text = schedule.arrival;
				popUp.DepartureDate.Text = schedule.departureDate;
				popUp.TrainNumberTxt.Text = schedule.trainNumber.ToString();
				popUp.SeatsBookedTxt.Text = schedule.capacity.ToString();
				//popUp.DepartureLocationCbx.Text = schedule.departureLocation.ToString();
				//popUp.ArrivalLocationCbx.Text = schedule.arrivalLocation.ToString();
				popUp.Show();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Please select a Schedule");
			}
		}

		private async void DeleteScheduleS_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				object scheduleSelected = ScheduleListBox.SelectedItem;
				JObject jSchedule = scheduleSelected as JObject;
				NewSchedule schedule = jSchedule.ToObject<NewSchedule>();

				await restClient.MakeRequest("DELETE", "schedule/delete/" + schedule.trainNumber + "/" + schedule.departureDate + "/" +schedule.departure);

				Button_Click(sender, e);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Please select a Schedule");
			}
			
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private void ScheduleListViewDisplay(object sender, SelectionChangedEventArgs e)
		{

		}

		private async void ReadScheduleByDateBtn_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				DateTime date = (DateTime)DepartureDateReadDte.SelectedDate;
				string departDateCreate = date.ToString("yyyy-MM-dd");
				string departTimeCreate = DepartureTimeReadCbx.Text;

				var result = await restClient.MakeRequest("GETALL", "schedule/readTime/" + departDateCreate + "/" + departTimeCreate);

				ScheduleListBox.Items.Clear();

				for (int i = 0; i < result.Count; i++)
				{
					ScheduleListBox.Items.Add(result[i]);
				}

				// Add columns
				var gridView = new GridView();
				ScheduleListBox.View = gridView;
				gridView.Columns.Add(new GridViewColumn
				{
					Header = "Train Number",
					DisplayMemberBinding = new Binding("trainNumber")
				});
				gridView.Columns.Add(new GridViewColumn
				{
					Header = "Booked Seats",
					DisplayMemberBinding = new Binding("capacity")
				});
				gridView.Columns.Add(new GridViewColumn
				{
					Header = "Departure Date",
					DisplayMemberBinding = new Binding("departureDate")
				});
				gridView.Columns.Add(new GridViewColumn
				{
					Header = "Departure",
					DisplayMemberBinding = new Binding("departure")
				});
				gridView.Columns.Add(new GridViewColumn
				{
					Header = "Arrival",
					DisplayMemberBinding = new Binding("arrival")
				});
				gridView.Columns.Add(new GridViewColumn
				{
					Header = "Dep Loca",
					DisplayMemberBinding = new Binding("departureLocation")
				});
				gridView.Columns.Add(new GridViewColumn
				{
					Header = "Arr Loca",
					DisplayMemberBinding = new Binding("arrivalLocation")
				});
			}
			catch(Exception ex)
			{
				MessageBox.Show("Please insert data");
			}
			
		}

		private void TrainNumberCreateCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}



		//USERS TAB//
		private async void CreateUserButton_Click(object sender, RoutedEventArgs e)
		{
			string userType = UserDescriptionCreateCbx.Text;
			int employeeNumber = int.Parse(EmployeeIdTxt.Text);
			string userName = EmployeeCreateNameTxt.Text;
			string userSurname = EmployeeCreateSurnameTxt.Text;

			if (userType.Equals("Select User") || EmployeeCreateNameTxt.Text.Equals("") || EmployeeCreateSurnameTxt.Text.Equals("") || EmployeeIdTxt.Text.Equals(""))
			{
				MessageBox.Show("Please enter all fields");
			}

			Regex regex = new Regex("[^0-9]+");
				if (regex.IsMatch(EmployeeIdTxt.Text))
				{
					MessageBox.Show("Employee ID must be correct");
				}
			
			var sendingObject = HelperCreate.createObject(userType.ToLower(), employeeNumber, userName, userSurname);

			if (sendingObject.GetType() == typeof(Announcer))
			{
				var obectToSend = (Announcer)sendingObject;
				await restClient.MakeRequest("POST", "actor/" + userType.ToLower() + "/create", obectToSend);
			}
			else if (sendingObject.GetType() == typeof(DoorMan))
			{
				var obectToSend = (DoorMan)sendingObject;
				await restClient.MakeRequest("POST", "actor/" + userType.ToLower() + "/create", obectToSend);
			}
			else if (sendingObject.GetType() == typeof(Driver))
			{
				var obectToSend = (Driver)sendingObject;
				await restClient.MakeRequest("POST", "actor/" + userType.ToLower() + "/create", obectToSend);
			}
			else if (sendingObject.GetType() == typeof(Security))
			{
				var obectToSend = (Security)sendingObject;
				await restClient.MakeRequest("POST", "actor/" + userType.ToLower() + "/create", obectToSend);
			}
			else if (sendingObject.GetType() == typeof(TicketChecker))
			{
				var obectToSend = (TicketChecker)sendingObject;
				await restClient.MakeRequest("POST", "actor/" + userType.ToLower() + "/create", obectToSend);
			}
			else if (sendingObject.GetType() == typeof(TicketClerk))
			{
				var obectToSend = (TicketClerk)sendingObject;
				await restClient.MakeRequest("POST", "actor/" + userType.ToLower() + "/create", obectToSend);
			}
		}

		private async void UserListBtn_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var result = await restClient.MakeRequest("GETALL", "actor/allactors/getAll");

				ListUsersListBox.Items.Clear();
				UserTypeForList.Content = "";

				for (int i = 0; i < result.Count; i++)
				{
					ListUsersListBox.Items.Add(result[i]);
				}

				// Add columns
				var gridView = new GridView();
				ListUsersListBox.View = gridView;
				gridView.Columns.Add(new GridViewColumn
				{
					Header = "Emp Number",
					DisplayMemberBinding = new Binding("employeeNumber")
				});
				gridView.Columns.Add(new GridViewColumn
				{
					Header = "Name",
					DisplayMemberBinding = new Binding("name")
				});
				gridView.Columns.Add(new GridViewColumn
				{
					Header = "Surname",
					DisplayMemberBinding = new Binding("surname")
				});
			}
			catch(Exception ex)
			{
				MessageBox.Show("No Actors!");
			}
			
		}

		private async void ListSpecificUserBtn_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var result = await restClient.MakeRequest("GETALL", "actor/" + UserDescriptionListingCbx.Text.ToLower() + "/getAll");

				ListUsersListBox.Items.Clear();
				UserTypeForList.Content = UserDescriptionListingCbx.Text.ToLower() + "(s)";

				for (int i = 0; i < result.Count; i++)
				{
					ListUsersListBox.Items.Add(result[i]);
				}

				// Add columns
				var gridView = new GridView();
				ListUsersListBox.View = gridView;
				gridView.Columns.Add(new GridViewColumn
				{
					Header = "Emp Number",
					DisplayMemberBinding = new Binding("employeeNumber")
				});
				gridView.Columns.Add(new GridViewColumn
				{
					Header = "Name",
					DisplayMemberBinding = new Binding("name")
				});
				gridView.Columns.Add(new GridViewColumn
				{
					Header = "Surname",
					DisplayMemberBinding = new Binding("surname")
				});
			}
			catch(Exception ex)
			{
				MessageBox.Show("No Actors!");
			}
		}

		private void ActorChange(object sender, SelectionChangedEventArgs e)
		{

		}

		private void EmployeeIdTxt_TextChanged(object sender, TextChangedEventArgs e)
		{

		}

		private void ListUsersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private async void Button_Click_1(object sender, RoutedEventArgs e)
		{
			try
			{
				await restClient.MakeRequest("DELETE", "actor/" + UserDescriptionDeleteCbx.Text.ToLower() + "/delete/" + EmployeeDeleteTxt.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Invalid Employee Number");
			}

			try
			{
				var result = await restClient.MakeRequest("GETALL", "actor/" + UserDescriptionListingCbx.Text.ToLower() + "/getAll");

				ListUsersListBox.Items.Clear();
				UserTypeForList.Content = UserDescriptionDeleteCbx.Text.ToLower() + "(s)";

				for (int i = 0; i < result.Count; i++)
				{
					ListUsersListBox.Items.Add(result[i]);
				}

				// Add columns
				var gridView = new GridView();
				ListUsersListBox.View = gridView;
				gridView.Columns.Add(new GridViewColumn
				{
					Header = "Emp Number",
					DisplayMemberBinding = new Binding("employeeNumber")
				});
				gridView.Columns.Add(new GridViewColumn
				{
					Header = "Name",
					DisplayMemberBinding = new Binding("name")
				});
				gridView.Columns.Add(new GridViewColumn
				{
					Header = "Surname",
					DisplayMemberBinding = new Binding("surname")
				});
			}
			catch(Exception ex)
			{
				MessageBox.Show("Deleted");
			}
			
		}

		private async void Button_Click_2(object sender, RoutedEventArgs e)
		{
			await restClient.MakeRequest("DELETE", "actor/allactors/deleteAll");
			ListUsersListBox.Items.Clear();
		}

		private async void Button_Click_4(object sender, RoutedEventArgs e)
		{
			var result1 = await restClient.MakeRequest("GET", "actor/allactors/getEmp/" + EmployeeUpdateTxt.Text);

			try
			{
				UpdateUserPopUp popUp = new UpdateUserPopUp();
				try
				{
					object scheduleSelected = ListUsersListBox.SelectedItem;
					JObject jSchedule = scheduleSelected as JObject;
					NewSchedule schedule = jSchedule.ToObject<NewSchedule>();

					popUp.UserNamerTxt.Text = schedule.departure;
					popUp.UserSNameTxt.Text = schedule.arrival;
					popUp.Show();
				}
				catch (Exception ex)
				{
					MessageBox.Show("Please select a Schedule");
				}


			}
			catch (Exception ex)
			{
				MessageBox.Show("Please select a Schedule");
			}
		}



		//TRAIN//
		private async void CreateTrainButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				int trainNumber = int.Parse(TrainCreateTxt.Text);

				var result = await restClient.MakeRequest("POST", "train/create", new Train(trainNumber, 0));

				if (result.FirstOrDefault() == null)
				{
					MessageBox.Show("Train already exists");
				}

				TrainCreateTxt.Text = "";
			}
			catch (Exception ex)
			{
				MessageBox.Show("Please enter all fields");
			}
		}

		//private void CreateStationButton_Click(object sender, RoutedEventArgs e)
		//{

		//}

		private async void CreateStationButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				int stationNum = int.Parse(StationNumberTxt.Text);
				string stationName = StationNameTxt.Text;

				var result = await restClient.MakeRequest("POST", "station/create", new Station(stationNum, stationName));

				if (result.FirstOrDefault() == null)
				{
					MessageBox.Show("Station already exists");
				}

				StationNumberTxt.Text = "";
				StationNameTxt.Text = "";
			}
			catch (Exception ex)
			{
				MessageBox.Show("Please enter all fields");
			}
		}
	}
}
