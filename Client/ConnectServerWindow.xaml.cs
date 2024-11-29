using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.IO;
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
using Project_Assistant.Dto;



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

					String ret = new WebClient().DownloadString(tbServer.Text + "/api/Session/Login/" + HttpUtility.UrlEncode(tbUsername.Text) + "/" + HttpUtility.UrlEncode(tbPassword.Text));
					SessionData sessionData = Newtonsoft.Json.JsonConvert.DeserializeObject<SessionData>(ret);
					if (sessionData != null)
					{
						Globals.session = sessionData.session;
						Globals.ServerAddress = tbServer.Text;
						Globals.isMultiuser = true;
						this.DialogResult = true;
						success = true;

						if (cbSaveLogin.IsChecked == true)
						{
							SetAuthData(tbUsername.Text, tbPassword.Text, tbServer.Text);
						}
						this.Close();
					}

			}
			catch (Exception ex)
			{
				MessageBox.Show("Login error.","Error");
			}
        }

		private void SetAuthData(string username, string password, string server)
		{
			try
			{
				var client = new NamedPipeClientStream("PAServiceNamedPipe");
				client.Connect(2000);
				StreamReader reader = new StreamReader(client);
				StreamWriter writer = new StreamWriter(client);

				writer.WriteLine("CHANGEDATA");
				writer.Flush();
				if (reader.ReadLine() == "SENDUSER")
				{
					writer.WriteLine(username);
					writer.Flush();
				}
				if (reader.ReadLine() == "SENDPASS")
				{
					writer.WriteLine(password);
					writer.Flush();
				}
				if (reader.ReadLine() == "SENDSERVER")
				{
					writer.WriteLine(server);
					writer.Flush();
				}
				if (reader.ReadLine() != "DONE")
				{
					MessageBox.Show("Error saving login data.\nPlease contact your administrator.");
				}
			}catch(Exception ex)
			{
				MessageBox.Show("Error saving login data.\nPlease contact your administrator.");
			}
		}

		private void bnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
        }
    }
}
