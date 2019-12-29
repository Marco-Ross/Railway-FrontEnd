using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;

namespace Railway_FrontEnd
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
		public Login()
        {
            InitializeComponent();

        }

		private async void LoginBtn_Click(object sender, RoutedEventArgs e)
		{
			HttpClientHandler handler = new HttpClientHandler();
			

			HttpClient httpClient = new HttpClient(handler);

			httpClient.BaseAddress = new Uri("http://localhost:8080/");
			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				//setup login data
				var username = LoginUserName.Text;
				var password = LoginUserPassword.Text;


			handler.Credentials = new NetworkCredential(username, password);

			httpClient.DefaultRequestHeaders.Accept.Clear();
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			//send request
			var responseManager = await httpClient.GetAsync(httpClient.BaseAddress + "railway");			
		
			if (responseManager.StatusCode == HttpStatusCode.OK)
			{
				MessageBox.Show("Loggin In");
				MainWindow main = new MainWindow();				
				main.Show();
				Close();
			}
			else
			{
				MessageBox.Show("Invalid Details");
			}			
		}
	}
}
