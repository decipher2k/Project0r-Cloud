using Project_Assistant_Server.Models;
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
	public partial class ItemPushWindow : Window
	{
		class Item
		{
			public String Type { get; set; }
			public String Task { get; set; }
			public String Sender { get; set; }
			public ItemPush.ItemType ItemType { get;set; }
		}
		public ItemPushWindow()
		{
			InitializeComponent();
			Item i1 = new Item();
			i1.Type = "Type1";
			i1.Task = "Task1";
			i1.Sender = "Sender1";

			Item i2 = new Item();
			i2.Type = "Type2";
			i2.Task = "Task2";
			i2.Sender = "Sender2";

			lbReminder.Items.Add(i1);
			lbReminder.Items.Add(i2);
		}

		private void bnOK_Click(object sender, RoutedEventArgs e)
		{		
			this.Close();
		}

		private void bnAccept_Click(object sender, RoutedEventArgs e)
		{
		}

		private void bnReject_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

        }

		private void ButtonAccept_Click(object sender, RoutedEventArgs e)
		{
			var curItem = ((ListBoxItem)lbReminder.ContainerFromElement((Button)sender)).Content;

		}

		private void ButtonDeny_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
