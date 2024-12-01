using Project_Assistant.API;
using Project_Assistant.Dto;
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

namespace Project_Assistant
{
	/// <summary>
	/// Interaktionslogik für Window1.xaml
	/// </summary>
	public partial class ItemSendUserSelect : Window
	{
		private String project="";
		public long userId=-1;
		public ItemSendUserSelect(String _project, bool invite = false)
		{
			InitializeComponent();
			this.project = _project;

			if (invite)
			{
				List<UserDataDto.UserData> userDatas = new ProjectAPI().FetchAllUsers();
				foreach (UserDataDto.UserData userData in userDatas)
					lbReminder.Items.Add(userData);

			}
			else
			{
				List<UserDataDto.UserData> userDatas = new ProjectAPI().FetchUsers(project);
				foreach (UserDataDto.UserData userData in userDatas)
					lbReminder.Items.Add(userData);

			}
		}

		private void bnOK_Click(object sender, RoutedEventArgs e)
		{
			if (lbReminder.SelectedItem != null)
			{
				userId=((UserDataDto.UserData) lbReminder.SelectedItem).Id;
				DialogResult = true;
			}
			else
			{
				DialogResult = false;
			}
			this.Close();
		}
	}
}
