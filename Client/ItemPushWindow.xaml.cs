using Project_Assistant.API;
using Project_Assistant_Server.Dto;
using Project_Assistant_Server.Models;
using ProjectOrganizer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
			public long Id { get; set; }
			public String Project { get; set; }
			public ItemPush.ItemType ItemType { get;set; }
		}
		public ItemPushWindow()
		{
			InitializeComponent();


			ItemPushDto itemPushDto=new ItemPushAPI().PollItems();
			Globals.session = itemPushDto.session;
			foreach(ItemPush item in itemPushDto.Items)
			{
				Item newItem = new Item();
				newItem.Type = item.Type.ToString();
				newItem.Task = item.Title;
				newItem.Sender = item.SenderName;
				newItem.Id = item.Id;
				newItem.ItemType = item.Type;
				newItem.Project = item.Project;

				lbReminder.Items.Add(newItem);
			}
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
			var curItem = (Item)((ListBoxItem)lbReminder.ContainerFromElement((Button)sender)).Content;
			FloatingWindow.Instance.itemPushes.Where(a => a.Id == curItem.Id && a.Type == curItem.ItemType).First();
			for (int i = 0; i < FloatingWindow.Instance.itemPushes.Count; i++)
			{
				if(FloatingWindow.Instance.itemPushes[i].Id == curItem.Id)
				{
					FloatingWindow.Instance.itemPushes.Remove(FloatingWindow.Instance.itemPushes.Where(a => a.Id == curItem.Id).First());
					i--;
				}
			}

			if (curItem.ItemType == ItemPush.ItemType.Project)
			{
				new ProjectAPI().AccepDenyInvite(true, curItem.Project);
			}
			else 
			{
				new ItemPushAPI().AcceptItem((int)curItem.Id, curItem.Project);
			}

			lbReminder.Items.Remove(curItem);
		}

		private void ButtonDeny_Click(object sender, RoutedEventArgs e)
		{
			var curItem = (Item)((ListBoxItem)lbReminder.ContainerFromElement((Button)sender)).Content;
			FloatingWindow.Instance.itemPushes.Where(a => a.Id == curItem.Id && a.Type == curItem.ItemType).First();
			for (int i = 0; i < FloatingWindow.Instance.itemPushes.Count; i++)
			{
				if (FloatingWindow.Instance.itemPushes[i].Id == curItem.Id)
				{
					FloatingWindow.Instance.itemPushes.Remove(FloatingWindow.Instance.itemPushes.Where(a => a.Id == curItem.Id).First());
					i--;
				}
			}
			if (curItem.ItemType == ItemPush.ItemType.Project)
			{
				new ProjectAPI().AccepDenyInvite(false, curItem.Project);
			}
			else
			{
				new ItemPushAPI().DenyItem((int)curItem.Id, curItem.Project);
			}

			lbReminder.Items.Remove(curItem);
		}
	}
}
