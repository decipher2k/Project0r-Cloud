using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Project_Assistant_Server.Dto;


namespace Project_Assistant
{
	/// <summary>
	/// Interaktionslogik für ConnectServerWindow.xaml
	/// </summary>
	public partial class ConnectServerWindow : Window
	{
		public bool success = false;
		public ConnectServerWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (cbSSO.IsChecked == false)
				{					
					String ret = new WebClient().DownloadString(tbServer.Text + "/api/Session/Login/" + HttpUtility.UrlEncode(tbUsername.Text) + "/" + HttpUtility.UrlEncode(tbPassword.Text));
					SessionData sessionData = Newtonsoft.Json.JsonConvert.DeserializeObject<SessionData>(ret);
					if (sessionData != null)
					{
						Globals.session = sessionData.session;
						success = true;
						this.Close();
					}
				}				
			}
			catch (Exception ex)
			{
				MessageBox.Show("Login error.","Error");
			}
        }

		private void bnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
        }
    }
}
