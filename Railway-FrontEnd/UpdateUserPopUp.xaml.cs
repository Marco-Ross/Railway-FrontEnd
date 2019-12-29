using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for UpdateUserPopUp.xaml
    /// </summary>
    public partial class UpdateUserPopUp : Window
    {
        public UpdateUserPopUp()
        {
            InitializeComponent();
        }

		private void UpdateScheduleBtn_Click(object sender, RoutedEventArgs e)
		{
			var uname = UserNamerTxt.Text;
			var usname = UserSNameTxt.Text;
			
			object user = new object();

			//await restClient.MakeRequest("UPDATE", "schedule/update", schedule);
		}
	}
}
