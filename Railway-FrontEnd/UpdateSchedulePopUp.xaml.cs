using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Railway_FrontEnd.domain.timings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Railway_FrontEnd
{
	/// <summary>
	/// Interaction logic for UpdateSchedulePopUp.xaml
	/// </summary>
	public partial class UpdateSchedulePopUp : Window
	{
		RestClient restClient;
		public UpdateSchedulePopUp()
		{
			InitializeComponent();
			restClient = new RestClient();
		}

		private async void UpdateScheduleBtn_Click(object sender, RoutedEventArgs e)
		{
			var depart =DepartureTimeCbx.Text;
			var arrival = ArrivalTimeCbx.Text;
	        var depDate = DepartureDate.Text;  
			var trainNum = int.Parse(TrainNumberTxt.Text);
			var seatsBooked = int.Parse(SeatsBookedTxt.Text);
			//var depLocation = DepartureLocationCbx.Text;
			//var arrivalLocation = ArrivalLocationCbx.Text;

			DateTime date = (DateTime)DepartureDate.SelectedDate;
			string departDateCreate = date.ToString("yyyy-MM-dd");


			NewSchedule schedule = new NewSchedule(trainNum, seatsBooked, departDateCreate, depart, arrival);

			await restClient.MakeRequest("UPDATE", "schedule/update", schedule);
			Close();
		}
	}
}
