using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Win32;
using Project_Assistant;
using Project_Assistant.API;
using Project_Assistant.Dto;
using Project_Assistant_Server.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.WebRequestMethods;

namespace ProjectOrganizer
{

    /// <summary>
    /// Interaktionslogik für MainControl.xaml
    /// </summary>
    public partial class MainControl : UserControl
    {
   
        String project;
        public MainControl(String _project)
        {
            project = _project;
            InitializeComponent();
			lbTodo.LayoutUpdated += LbTodo_LayoutUpdated;
			lbTodo.TargetUpdated += LbTodo_TargetUpdated;
            reloadItems();
   
        }

		private void LbTodo_TargetUpdated(object sender, DataTransferEventArgs e)
		{
			lbApps.Dispatcher.Invoke(new Action(() =>
			{
				if (Projects.Instance.Project.ContainsKey(project))
				{
					foreach (Program program in Projects.Instance.Project[project].Apps)
					{
						bool found = false;
						for (int i = 0; i < lbApps.Items.Count; i++)
						{
							Program todo = (Program)lbApps.Items[i];
							if (program.Id == todo.Id)
							{
								found = true;
							}

						}
						if (!found)
							lbApps.Items.Add(program);
					}

					if (Projects.Instance.Project.ContainsKey(project))
					{
						for (int i = 0; i < lbApps.Items.Count; i++)
						{
							Program item = (Program)lbApps.Items[i];
							if (!Projects.Instance.Project[project].Apps.Where(a => a.Id == item.Id).Any())
							{
								lbApps.Items.Remove(item);
								i--;
							}
						}
					}


				}
			}));

			lbFiles.Dispatcher.Invoke(new Action(() =>
			{
				if (Projects.Instance.Project.ContainsKey(project))
				{
					foreach (File program in Projects.Instance.Project[project].Files)
					{
						bool found = false;
						for (int i = 0; i < lbFiles.Items.Count; i++)
						{
							File todo = (File)lbFiles.Items[i];
							if (program.Id == todo.Id)
							{
								found = true;
							}

						}
						if (!found)
							lbFiles.Items.Add(program);
					}

					if (Projects.Instance.Project.ContainsKey(project))
					{
						for (int i = 0; i < lbFiles.Items.Count; i++)
						{
							File item = (File)lbFiles.Items[i];
							if (!Projects.Instance.Project[project].Files.Where(a => a.Id == item.Id).Any())
							{
								lbFiles.Items.Remove(item);
								i--;
							}
						}
					}


				}
			}));

			lbTodo.Dispatcher.Invoke(new Action(() =>
			{
				if (Projects.Instance.Project.ContainsKey(project))
				{
					foreach (ToDo program in Projects.Instance.Project[project].ToDo)
					{
						bool found = false;
						for (int i = 0; i < lbTodo.Items.Count; i++)
						{
							ToDo todo = (ToDo)lbTodo.Items[i];
							if (program.Id == todo.Id)
							{
								found = true;
							}

						}
						if (!found)
							lbTodo.Items.Add(program);
					}

					if (Projects.Instance.Project.ContainsKey(project))
					{
						for (int i = 0; i < lbTodo.Items.Count; i++)
						{
							ToDo item = (ToDo)lbTodo.Items[i];
							if (!Projects.Instance.Project[project].ToDo.Where(a => a.Id == item.Id).Any())
							{
								lbTodo.Items.Remove(item);
								i--;
							}
						}
					}

				}
			}));



			lbNotes.Dispatcher.Invoke(new Action(() =>
			{
				if (Projects.Instance.Project.ContainsKey(project))
				{
					foreach (Note program in Projects.Instance.Project[project].Notes)
					{
						bool found = false;
						for (int i = 0; i < lbNotes.Items.Count; i++)
						{
							Note todo = (Note)lbNotes.Items[i];
							if (program.Id == todo.Id)
							{
								found = true;
							}

						}
						if (!found)
							lbNotes.Items.Add(program);
					}

					if (Projects.Instance.Project.ContainsKey(project))
					{
						for (int i = 0; i < lbNotes.Items.Count; i++)
						{
							Note item = (Note)lbNotes.Items[i];
							if (!Projects.Instance.Project[project].Notes.Where(a => a.Id == item.Id).Any())
							{
								lbNotes.Items.Remove(item);
								i--;
							}
						}
					}


				}
			}));


			lbCalendar.Dispatcher.Invoke(new Action(() =>
			{
				if (Projects.Instance.Project.ContainsKey(project))
				{
					foreach (Calendar program in Projects.Instance.Project[project].Calendar)
					{
						bool found = false;
						for (int i = 0; i < lbCalendar.Items.Count; i++)
						{
							Calendar todo = (Calendar)lbCalendar.Items[i];
							if (program.Id == todo.Id)
							{
								found = true;
							}

						}
						if (!found)
							lbCalendar.Items.Add(program);
					}

					if (Projects.Instance.Project.ContainsKey(project))
					{
						for (int i = 0; i < lbCalendar.Items.Count; i++)
						{
							Calendar item = (Calendar)lbCalendar.Items[i];
							if (!Projects.Instance.Project[project].Calendar.Where(a => a.Id == item.Id).Any())
							{
								lbCalendar.Items.Remove(item);
								i--;
							}
						}
					}


				}
			}));



			lbLog.Dispatcher.Invoke(new Action(() =>
			{
				if (Projects.Instance.Project.ContainsKey(project))
				{
					foreach (Log program in Projects.Instance.Project[project].Log)
					{
						bool found = false;
						for (int i = 0; i < lbLog.Items.Count; i++)
						{
							Log todo = (Log)lbLog.Items[i];
							if (program.Id == todo.Id)
							{
								found = true;
							}

						}
						if (!found)
							lbLog.Items.Add(program);
					}

					if (Projects.Instance.Project.ContainsKey(project))
					{
						for (int i = 0; i < lbLog.Items.Count; i++)
						{
							Log item = (Log)lbLog.Items[i];
							if (!Projects.Instance.Project[project].Log.Where(a => a.Id == item.Id).Any())
							{
								lbLog.Items.Remove(item);
								i--;
							}
						}
					}


				}
			}));
		}

		private void LbTodo_LayoutUpdated(object sender, EventArgs e)
		{
			lbApps.Dispatcher.Invoke(new Action(() =>
			{
				if (Projects.Instance.Project.ContainsKey(project))
				{
					foreach (Program program in Projects.Instance.Project[project].Apps)
					{
						bool found = false;
						for (int i = 0; i < lbApps.Items.Count; i++)
						{
							Program todo = (Program)lbApps.Items[i];
							if (program.Id == todo.Id)
							{
								found = true;
							}

						}
						if (!found)
							lbApps.Items.Add(program);
					}

					if (Projects.Instance.Project.ContainsKey(project))
					{
						for (int i = 0; i < lbApps.Items.Count; i++)
						{
							Program item = (Program)lbApps.Items[i];
							if (!Projects.Instance.Project[project].Apps.Where(a => a.Id == item.Id).Any())
							{
								lbApps.Items.Remove(item);
								i--;
							}
						}
					}

				
				}
			}));

			lbFiles.Dispatcher.Invoke(new Action(() =>
			{
				if (Projects.Instance.Project.ContainsKey(project))
				{
					foreach (File program in Projects.Instance.Project[project].Files)
					{
						bool found = false;
						for (int i = 0; i < lbFiles.Items.Count; i++)
						{
							File todo = (File)lbFiles.Items[i];
							if (program.Id == todo.Id)
							{
								found = true;
							}

						}
						if (!found)
							lbFiles.Items.Add(program);
					}

					if (Projects.Instance.Project.ContainsKey(project))
					{
						for (int i = 0; i < lbFiles.Items.Count; i++)
						{
							File item = (File)lbFiles.Items[i];
							if (!Projects.Instance.Project[project].Files.Where(a => a.Id == item.Id).Any())
							{
								lbFiles.Items.Remove(item);
								i--;
							}
						}
					}

				
				}
			}));

			lbTodo.Dispatcher.Invoke(new Action(() =>
			{
				if (Projects.Instance.Project.ContainsKey(project))
				{
					foreach (ToDo program in Projects.Instance.Project[project].ToDo)
					{
						bool found = false;
						for (int i = 0; i < lbTodo.Items.Count; i++)
						{
							ToDo todo = (ToDo)lbTodo.Items[i];
							if (program.Id == todo.Id)
							{
								found = true;
							}

						}
						if (!found)
							lbTodo.Items.Add(program);
					}

					if (Projects.Instance.Project.ContainsKey(project))
					{
						for (int i = 0; i < lbTodo.Items.Count; i++)
						{
							ToDo item = (ToDo)lbTodo.Items[i];
							if (!Projects.Instance.Project[project].ToDo.Where(a => a.Id == item.Id).Any())
							{
								lbTodo.Items.Remove(item);
								i--;
							}
						}
					}

				}
			}));



			lbNotes.Dispatcher.Invoke(new Action(() =>
			{
				if (Projects.Instance.Project.ContainsKey(project))
				{
					foreach (Note program in Projects.Instance.Project[project].Notes)
					{
						bool found = false;
						for (int i = 0; i < lbNotes.Items.Count; i++)
						{
							Note todo = (Note)lbNotes.Items[i];
							if (program.Id == todo.Id)
							{
								found = true;
							}

						}
						if (!found)
							lbNotes.Items.Add(program);
					}

					if (Projects.Instance.Project.ContainsKey(project))
					{
						for (int i = 0; i < lbNotes.Items.Count; i++)
						{
							Note item = (Note)lbNotes.Items[i];
							if (!Projects.Instance.Project[project].Notes.Where(a => a.Id == item.Id).Any())
							{
								lbNotes.Items.Remove(item);
								i--;
							}
						}
					}

				
				}
			}));


			lbCalendar.Dispatcher.Invoke(new Action(() =>
			{
				if (Projects.Instance.Project.ContainsKey(project))
				{
					foreach (Calendar program in Projects.Instance.Project[project].Calendar)
					{
						bool found = false;
						for (int i = 0; i < lbCalendar.Items.Count; i++)
						{
							Calendar todo = (Calendar)lbCalendar.Items[i];
							if (program.Id == todo.Id)
							{
								found = true;
							}

						}
						if (!found)
							lbCalendar.Items.Add(program);
					}

					if (Projects.Instance.Project.ContainsKey(project))
					{
						for (int i = 0; i < lbCalendar.Items.Count; i++)
						{
							Calendar item = (Calendar)lbCalendar.Items[i];
							if (!Projects.Instance.Project[project].Calendar.Where(a => a.Id == item.Id).Any())
							{
								lbCalendar.Items.Remove(item);
								i--;
							}
						}
					}

					
				}
			}));



			lbLog.Dispatcher.Invoke(new Action(() =>
			{
				if (Projects.Instance.Project.ContainsKey(project))
				{
					foreach (Log program in Projects.Instance.Project[project].Log)
					{
						bool found = false;
						for (int i = 0; i < lbLog.Items.Count; i++)
						{
							Log todo = (Log)lbLog.Items[i];
							if (program.Id == todo.Id)
							{
								found = true;
							}

						}
						if (!found)
							lbLog.Items.Add(program);
					}

					if (Projects.Instance.Project.ContainsKey(project))
					{
						for (int i = 0; i < lbLog.Items.Count; i++)
						{
							Log item = (Log)lbLog.Items[i];
							if (!Projects.Instance.Project[project].Log.Where(a => a.Id == item.Id).Any())
							{
								lbLog.Items.Remove(item);
								i--;
							}
						}
					}

					
				}
			}));

			/*            if (Projects.Instance.Project.ContainsKey(project))
						{
							for (int i = 0; i < lbTodo.Items.Count; i++)
							{
								ToDo item = (ToDo)lbTodo.Items[i];
								if (!Projects.Instance.Project[project].ToDo.Where(a => a.Id == item.Id).Any())
								{
									lbTodo.Items.Remove(item);
									i--;
								}
							}
						}*/
		}

		private void reloadItems()
        {

			lbApps.Dispatcher.Invoke(new Action(delegate ()
			{
				if (Projects.Instance.Project.ContainsKey(project))
				{
					foreach (Program program in Projects.Instance.Project[project].Apps)
					{
						bool found = false;
						for (int i = 0; i < lbApps.Items.Count; i++)
						{
							Program todo = (Program)lbApps.Items[i];
							if (program.Id == todo.Id)
							{
								found = true;
							}

						}
						if (!found)
							lbApps.Items.Add(program);
					}

					if (Projects.Instance.Project.ContainsKey(project))
					{
						for (int i = 0; i < lbApps.Items.Count; i++)
						{
							Program item = (Program)lbApps.Items[i];
							if (!Projects.Instance.Project[project].Apps.Where(a => a.Id == item.Id).Any())
							{
								lbApps.Items.Remove(item);
								i--;
							}
						}
					}

					lbApps.UpdateLayout();
				}
			}));

			lbFiles.Dispatcher.Invoke(new Action(delegate ()
			{
				if (Projects.Instance.Project.ContainsKey(project))
				{
					foreach (File program in Projects.Instance.Project[project].Files)
					{
						bool found = false;
						for (int i = 0; i < lbFiles.Items.Count; i++)
						{
							File todo = (File)lbFiles.Items[i];
							if (program.Id == todo.Id)
							{
								found = true;
							}

						}
						if (!found)
							lbFiles.Items.Add(program);
					}

					if (Projects.Instance.Project.ContainsKey(project))
					{
						for (int i = 0; i < lbFiles.Items.Count; i++)
						{
							File item = (File)lbFiles.Items[i];
							if (!Projects.Instance.Project[project].Files.Where(a => a.Id == item.Id).Any())
							{
								lbFiles.Items.Remove(item);
								i--;
							}
						}
					}

					lbFiles.UpdateLayout();
				}
			}));
		
			lbTodo.Dispatcher.Invoke(new Action(delegate ()
			{
                if (Projects.Instance.Project.ContainsKey(project))
                {
                    foreach (ToDo program in Projects.Instance.Project[project].ToDo)
                    {
                        bool found = false;
                        for (int i = 0; i < lbTodo.Items.Count; i++)
                        {
                            ToDo todo = (ToDo)lbTodo.Items[i];
                            if (program.Id == todo.Id)
                            {
                                found = true;
                            }

                        }
                        if (!found)
                            lbTodo.Items.Add(program);
                    }

					if (Projects.Instance.Project.ContainsKey(project))
					{
						for (int i = 0; i < lbTodo.Items.Count; i++)
						{
							ToDo item = (ToDo)lbTodo.Items[i];
							if (!Projects.Instance.Project[project].ToDo.Where(a => a.Id == item.Id).Any())
							{
								lbTodo.Items.Remove(item);
								i--;
							}
						}
					}

					lbTodo.UpdateLayout();
                }
            }));



			lbNotes.Dispatcher.Invoke(new Action(delegate ()
			{
				if (Projects.Instance.Project.ContainsKey(project))
				{
					foreach (Note program in Projects.Instance.Project[project].Notes)
					{
						bool found = false;
						for (int i = 0; i < lbNotes.Items.Count; i++)
						{
							Note todo = (Note)lbNotes.Items[i];
							if (program.Id == todo.Id)
							{
								found = true;
							}

						}
						if (!found)
							lbNotes.Items.Add(program);
					}

					if (Projects.Instance.Project.ContainsKey(project))
					{
						for (int i = 0; i < lbNotes.Items.Count; i++)
						{
							Note item = (Note)lbNotes.Items[i];
							if (!Projects.Instance.Project[project].Notes.Where(a => a.Id == item.Id).Any())
							{
								lbNotes.Items.Remove(item);
								i--;
							}
						}
					}

					lbNotes.UpdateLayout();
				}
			}));


			lbCalendar.Dispatcher.Invoke(new Action(delegate ()
			{
				if (Projects.Instance.Project.ContainsKey(project))
				{
					foreach (Calendar program in Projects.Instance.Project[project].Calendar)
					{
						bool found = false;
						for (int i = 0; i < lbCalendar.Items.Count; i++)
						{
							Calendar todo = (Calendar)lbCalendar.Items[i];
							if (program.Id == todo.Id)
							{
								found = true;
							}

						}
						if (!found)
							lbCalendar.Items.Add(program);
					}

					if (Projects.Instance.Project.ContainsKey(project))
					{
						for (int i = 0; i < lbCalendar.Items.Count; i++)
						{
							Calendar item = (Calendar)lbCalendar.Items[i];
							if (!Projects.Instance.Project[project].Calendar.Where(a => a.Id == item.Id).Any())
							{
								lbCalendar.Items.Remove(item);
								i--;
							}
						}
					}

					lbCalendar.UpdateLayout();
				}
			}));



			lbLog.Dispatcher.Invoke(new Action(delegate ()
			{
				if (Projects.Instance.Project.ContainsKey(project))
				{
					foreach (Log program in Projects.Instance.Project[project].Log)
					{
						bool found = false;
						for (int i = 0; i < lbLog.Items.Count; i++)
						{
							Log todo = (Log)lbLog.Items[i];
							if (program.Id == todo.Id)
							{
								found = true;
							}

						}
						if (!found)
							lbLog.Items.Add(program);
					}

					if (Projects.Instance.Project.ContainsKey(project))
					{
						for (int i = 0; i < lbLog.Items.Count; i++)
						{
							Log item = (Log)lbLog.Items[i];
							if (!Projects.Instance.Project[project].Log.Where(a => a.Id == item.Id).Any())
							{
								lbLog.Items.Remove(item);
								i--;
							}
						}
					}

					lbLog.UpdateLayout();
				}
			}));
		
		}

        public static class WindowHelper
        {
            public static void BringProcessToFront(Process process)
            {
                IntPtr handle = process.MainWindowHandle;
                if (IsIconic(handle))
                {
                    ShowWindow(handle, SW_RESTORE);
                }

                SetForegroundWindow(handle);
            }

            const int SW_RESTORE = 9;

            [System.Runtime.InteropServices.DllImport("User32.dll")]
            private static extern bool SetForegroundWindow(IntPtr handle);
            [System.Runtime.InteropServices.DllImport("User32.dll")]
            private static extern bool ShowWindow(IntPtr handle, int nCmdShow);
            [System.Runtime.InteropServices.DllImport("User32.dll")]
            private static extern bool IsIconic(IntPtr handle);
        }
        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {                
                Program p = (Program)((ListBox)sender).SelectedItem;
                if (p.startOnce == true)
                {
                    if (p.process != null && !p.process.HasExited)
                    {
                        WindowHelper.BringProcessToFront((Process)p.process);
                    }
                    else
                    {
                        p.process = Process.Start(p.executaleFile);
                        MainWindow.Instance.Close();
                    }
                }
                else
                {
                    p.process = Process.Start(p.executaleFile);
                    MainWindow.Instance.Close();
                }
            }
            catch (Exception) { }
        }
        HubConnection hubConnection;
        IDisposable handler;
        public void setTab(long idx)
        {
            tabMain.SelectedIndex = (int)idx;
        }
        private async void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabMain.SelectedItem == null)
                tabMain.SelectedIndex = 0;
            MainWindow.Instance.VerticalTab = tabMain.SelectedIndex;
            if(((TabItem)tabMain.SelectedItem).Header.ToString()=="Chat")
            {
                hubConnection = new HubConnectionBuilder().WithUrl(Globals.ServerAddress + "/messagehub").Build();
                

				hubConnection.Closed += async (error) =>
				{
					await Task.Delay(new Random().Next(0, 5) * 1000);
					await hubConnection.StartAsync();
					await hubConnection.SendAsync("SendUser", Globals.session, hubConnection.ConnectionId, project);

				};

                

				handler=hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
				{
					this.Dispatcher.Invoke(() =>
					{
						var newMessage = $"{user}: {message}";
						tbChatHistory.Text += "\r\n\r\n" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + ": " + user + "\r\n" + message;
					});
				});
               
				await hubConnection.StartAsync();
				await hubConnection.SendAsync("SendUser",Globals.session,hubConnection.ConnectionId, project);

			}		
        }


        public static ImageSource ToImageSource(Icon icon)
        {
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        }
        private void bnAddProgram_Click(object sender, RoutedEventArgs e)
        {
            AddEditFile wnd = new AddEditFile();
            wnd.isExe = true;
            if (wnd.ShowDialog()==true)
            {
                if (!System.IO.File.Exists(wnd.file))
                {
                    MessageBox.Show("File not found.");
                }
                else
                {
                    Program p = new Program();
                    p.executaleFile = wnd.file;
                    p.description = wnd.description;
                    p.name = wnd.name;

                    if (!Globals.isMultiuser)
                    {
                        if (Projects.Instance.Project[project].Apps.Count == 0)
                            p.Id = 0;
                        else
                            p.Id = Projects.Instance.Project[project].Apps.Max(a => a.Id) + 1;
                    }
                    else
                    {
                        long Id=new ProgramAPI().Create(p,project);
                        p.Id= Id;
                    }

                    p.startOnce = wnd.startOnce;                   
                    System.Drawing.Icon result = (System.Drawing.Icon)null;

                    result = System.Drawing.Icon.ExtractAssociatedIcon(wnd.file);
                    if (result != null)
                    {
                        ImageSource img = ToImageSource(result);
                        p.picture = img;
                        Projects.Instance.Project[project].Apps.Add(p);                       
                        Projects.Save();
                    }
                }
            }
         
        }

        private void bnAddFile_Click(object sender, RoutedEventArgs e)
        {
            AddEditFile wnd = new AddEditFile();
            wnd.isExe = false;
            if (wnd.ShowDialog() == true)
            {
                File p = new File();
                
                p.fileName = wnd.file;
                p.description = wnd.description;
                p.name = wnd.name;
                p.startOnce = wnd.startOnce;

				if (!Globals.isMultiuser)
				{
					p.Id = Projects.Instance.Project[project].Files.Count == 0 ? 0 : Projects.Instance.Project[project].Files.Max(a => a.Id) + 1;
				}
                else
                {
                    long Id=new FileAPI().Create(p, project);
                    p.Id = Id;
                }

				System.Drawing.Icon result = (System.Drawing.Icon)null;
                ImageSource img;

                try
                {
                    result = System.Drawing.Icon.ExtractAssociatedIcon(wnd.file);
                    img = ToImageSource(result);
                }
                catch
                {
                    img = Imaging.CreateBitmapSourceFromHBitmap(new System.Drawing.Bitmap(System.Drawing.Image.FromFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+ "\\folder.png")).GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
                 
                p.picture = img;
                Projects.Instance.Project[project].Files.Add(p);                
                Projects.Save();
                
            }
        }
     
        
        // Code For OpenWithDialog Box
    

        public const uint SW_NORMAL = 1;

       
       
        private void lbFiles_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                File p = (File)((ListBox)sender).SelectedItem;
                if (p.startOnce == true)
                {
                    if (p.process != null && !p.process.HasExited)
                    {
                        WindowHelper.BringProcessToFront((Process)p.process);
                    }
                    else
                    {                                                                     
                        p.process = Process.Start(p.fileName);                     
                        MainWindow.Instance.Close();
                    }
                }
                else
                {
                    p.process = Process.Start(p.fileName);
                    MainWindow.Instance.Close();
                }
            }
            catch (Exception) { }
        }

        private void bnAddNote_Click(object sender, RoutedEventArgs e)
        {
            
            AddEditNote wnd = new AddEditNote();
            if (wnd.ShowDialog() == true)
            {
                Note note = new Note();
                note.text = wnd.note;
                note.description = wnd.description;
                note.name = wnd.caption;

                if (!Globals.isMultiuser)
                {
                    if (Projects.Instance.Project[project].Notes.Count == 0)
                        note.Id = 0;
                    else
                        note.Id = Projects.Instance.Project[project].Notes.Max(a => a.Id) + 1;
                }
                else
                {
                    long Id=new NoteAPI().Create(note, project);
                    note.Id = Id;
                }

				Projects.Instance.Project[project].Notes.Add(note);
                Projects.Save();
            }
           
        }

        private void bnAddToDo_Click(object sender, RoutedEventArgs e)
        {
            AddEditNote wnd = new AddEditNote();
            if (wnd.ShowDialog() == true)
            {
                ToDo toDo = new ToDo();
                toDo.caption = wnd.caption;                
                toDo.description = wnd.note;

                if (!Globals.isMultiuser)
                {
                    toDo.Id = Projects.Instance.Project[project].ToDo.Count == 0 ? 0 : Projects.Instance.Project[project].ToDo.Max(a => a.Id) + 1;
                }
                else
                {
                    long Id=new ToDoAPI().Create(toDo, project);
                    toDo.Id = Id;
                }

				Projects.Instance.Project[project].ToDo.Add(toDo);
                Projects.Save();

                reloadItems();
                
			};

        }

        private void bnAddLog_Click(object sender, RoutedEventArgs e)
        {
            AddEditNote wnd = new AddEditNote();
            if (wnd.ShowDialog() == true)
            {
                Log log = new Log();
                log.caption = wnd.caption;                
                log.description = wnd.note;
                log.date=DateTime.Now;

                if(!Globals.isMultiuser)
                {
					log.Id = Projects.Instance.Project[project].Log.Count == 0 ? 0 : Projects.Instance.Project[project].Log.Max(a => a.Id) + 1;
				}
                else
                {
                    long Id=new LogAPI().Create(log, project); 
                    log.Id = Id;
                }

                Projects.Instance.Project[project].Log.Insert(0,log);                           
                Projects.Save();
                
            }

        }


        private void lbNotes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Note note=lbNotes.SelectedItem as Note;
            if (note != null)
            {
                AddEditNote addEditNote = new AddEditNote();
                addEditNote.caption = note.name;
                addEditNote.description = note.description;
                addEditNote.note = note.text;

                if(addEditNote.ShowDialog() == true)
                {
                    for (int i = 0; i < Projects.Instance.Project[project].Notes.Count; i++)
                    {
                        if (Projects.Instance.Project[project].Notes[i].name == note.name)
                        {

                            Projects.Instance.Project[project].Notes[i].text = addEditNote.note;
                            Projects.Instance.Project[project].Notes[i].description = addEditNote.description;
                            Projects.Instance.Project[project].Notes[i].name = addEditNote.caption;

                            if (Globals.isMultiuser)
                            {
                                new NoteAPI().Update(Projects.Instance.Project[project].Notes[i], project);
                            }

                            Projects.Save();
                            break;
                        }
                    }
                }
            }
        }

        private void lbTodo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ToDo toDo = lbLog.SelectedItem as ToDo;
            if (toDo != null)
            {
                AddEditNote addEditNote = new AddEditNote();
                addEditNote.caption = toDo.caption;
                addEditNote.note = toDo.description;


                if (addEditNote.ShowDialog() == true)
                {
                    for (int i = 0; i < Projects.Instance.Project[project].ToDo.Count; i++)
                    {
                        if (Projects.Instance.Project[project].ToDo[i].caption == toDo.caption)
                        {                            
                            Projects.Instance.Project[project].ToDo[i].description = addEditNote.note;
                            Projects.Instance.Project[project].ToDo[i].caption = addEditNote.caption;
                            Projects.Save();

                            if (Globals.isMultiuser)
                            {
                                new ToDoAPI().Update(Projects.Instance.Project[project].ToDo[i], project);

							}

                            break;
                        }
                    }
                }
            }
        }

        private void lbLog_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Log log = lbLog.SelectedItem as Log;
            if (log != null)
            {
                AddEditNote addEditNote = new AddEditNote();
                addEditNote.caption = log.caption;
                addEditNote.note = log.description;
                

                if (addEditNote.ShowDialog() == true)
                {
                    for (int i = 0; i < Projects.Instance.Project[project].Log.Count; i++)
                    {
                        if (Projects.Instance.Project[project].Log[i].caption == log.caption)
                        {

                            Projects.Instance.Project[project].Log[i].description = addEditNote.note;
                            Projects.Instance.Project[project].Log[i].caption = addEditNote.caption;                         
                            
                            if(Globals.isMultiuser)
                            {
                                new LogAPI().Update(Projects.Instance.Project[project].Log[i], project);
                            }

                            Projects.Save();
                            break;
                        }
                    }
                }
            }
        }

        private void bnCreateCalendar_Click(object sender, RoutedEventArgs e)
        {

            if (calCalendar.SelectedDate == null)
            {
                MessageBox.Show("Please select a date.");
            }
            else
            {
                try
                {
                    if (bnCreateCalendar.Content.ToString().Equals("Save")==false)
                    {
                        Calendar toDo = new Calendar();
                        
                        toDo.caption = tbCalendarCaption.Text;
                        toDo.text = tbCalendarDetails.Text;
                        toDo.from = DateTime.Parse(tbCalendarFrom.Text);
                        toDo.to = DateTime.Parse(tbCalendarTo.Text);
                        toDo.handled = false;
                        toDo.date = (DateTime)calCalendar.SelectedDate;

                        if (!Globals.isMultiuser)
                        {
                            toDo.Id = Projects.Instance.Project[project].Calendar.Count == 0 ? 0 : Projects.Instance.Project[project].Calendar.Max(a => a.Id) + 1;
                        }
                        else
                        {
                            long Id=new CalendarAPI().Create(toDo, project);
                            toDo.Id=Id;
                        }

                        Projects.Instance.Project[project].Calendar.Add(toDo);
                        Projects.Save();
                        
                        bnAddCalendar_Click(sender, e);
                    }
                    else
                    {
                       
                        Calendar c=(Calendar)lbCalendar.SelectedItem;                        
                        for (int i = 0; i < Projects.Instance.Project[project].Calendar.Count; i++)
                        {
                            
                            if (Projects.Instance.Project[project].Calendar[i].Id == c.Id)
                            {
                                Projects.Instance.Project[project].Calendar[i].from = DateTime.Parse(tbCalendarFrom.Text);
                                Projects.Instance.Project[project].Calendar[i].to = DateTime.Parse(tbCalendarTo.Text);
                                Projects.Instance.Project[project].Calendar[i].text = tbCalendarDetails.Text;
                                Projects.Instance.Project[project].Calendar[i].caption = tbCalendarCaption.Text;

                                if (Globals.isMultiuser)
                                {
                                    new CalendarAPI().Update(Projects.Instance.Project[project].Calendar[i], project);
                                }

                                Projects.Save();
                                reloadItems();
                                break;
                            }
                        }                        

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid time format.");
                }
            }
        }

        private void lbCalendar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Calendar calendar = (Calendar)lbCalendar.SelectedItem;
            if (calendar != null)
            {
                bnCreateCalendar.Content = "Save";
                
                tbCalendarCaption.Text=calendar.caption;
                tbCalendarDetails.Text = calendar.text;
                tbCalendarFrom.Text=calendar.from.ToShortTimeString();
                tbCalendarTo.Text = calendar.to.ToShortTimeString();
                calCalendar.SelectedDate = calendar.date;
            }
            else
            {
                bnCreateCalendar.Content = "Create";
                
                tbCalendarCaption.Text = "";
                tbCalendarFrom.Text = "";
                tbCalendarTo.Text = "";
                tbCalendarDetails.Text = "";
                calCalendar.SelectedDate = null;
            }
            
        }
        Control currentLB=null;
        private void bnAddCalendar_Click(object sender, RoutedEventArgs e)
        {
            lbCalendar.SelectedItem = null;
            
            tbCalendarCaption.Text = "";
            tbCalendarDetails.Text = "";
            tbCalendarFrom.Text = "";
            tbCalendarTo.Text = "";
            calCalendar.SelectedDate = DateTime.Now;

        }

        private void bnDeleteAppFile_Click(object sender, RoutedEventArgs e)
        {
            /*  if(lbApps.IsFocused && lbApps.SelectedItems.Count > 0)
              {
                  Program p=lbApps.SelectedItem as Program;
                  for (int i = 0; i < Projects.Instance.Project[project].Apps.Count; i++)
                  {
                      if (Projects.Instance.Project[project].Apps[i].name == p.name)
                      {
                          Projects.Instance.Project[project].Apps.RemoveAt(i);
                          break;
                      }
                  }

                  Projects.Save();
              }*/

            if (lbApps.SelectedItems.Count > 0 && currentLB==lbApps)
            {
                Program p = lbApps.SelectedItem as Program;

                if(Globals.isMultiuser)
                {
                    new ProgramAPI().Delete((int)p.Id, project);
                }

                Projects.Instance.Project[project].Apps.Remove(p);
                Projects.Save();
            }
            else if(lbFiles.SelectedItems.Count>0 && currentLB==lbFiles)
            {
                File p = lbFiles.SelectedItem as File;

				if (Globals.isMultiuser)
				{
					new FileAPI().Delete((int)p.Id, project);
				}

				Projects.Instance.Project[project].Files.Remove(p);
                Projects.Save();
            }
        }

        private void lbApps_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lbApps.SelectedItems.Count > 0)
                currentLB = lbApps;
        }

        private void lbFiles_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (lbFiles.SelectedItems.Count > 0)
                currentLB = lbFiles;
        }

        private void bnDeleteNote_Click(object sender, RoutedEventArgs e)
        {
            if (lbNotes.SelectedItems.Count > 0)
            {
                Note p = lbNotes.SelectedItem as Note;

				if (Globals.isMultiuser)
				{
					new NoteAPI().Delete((int)p.Id, project);
				}

				Projects.Instance.Project[project].Notes.Remove(p);
                Projects.Save();
            }
        }

        private void bnDeleteToDo_Click(object sender, RoutedEventArgs e)
        {
            if (lbTodo.SelectedItems.Count > 0)
            {
                ToDo p = lbTodo.SelectedItem as ToDo;
                MessageBoxResult result = MessageBox.Show("Move to Log?", "Move to Log", MessageBoxButton.YesNoCancel);

				if (result == MessageBoxResult.Yes)
                {                          
                    Log log = new Log();
                    log.caption = p.caption;                    
                    log.description = p.description;
                    log.date = DateTime.Now;

                    if (!Globals.isMultiuser)
                    {
                        log.Id = Projects.Instance.Project[project].Log.Count == 0 ? 0 : Projects.Instance.Project[project].Log.Max(a => a.Id) + 1;
                    }
                    else
                    {
                        long Id=new LogAPI().Create(log, project);
                        log.Id=Id;
                    }

					Projects.Instance.Project[project].Log.Add(log);                
                }

                if (result != MessageBoxResult.Cancel)
                {

                    if (Globals.isMultiuser)
                    {
                        new ToDoAPI().Delete((int)p.Id, project);
                    }

                    Projects.Instance.Project[project].ToDo.Remove(p);
                    Projects.Save();
                    reloadItems();
                }
            }
        }

        private void bnDeleteLog_Click(object sender, RoutedEventArgs e)
        {
            if (lbLog.SelectedItems.Count > 0)
            {
                Log p = lbLog.SelectedItem as Log;


				if (Globals.isMultiuser)
				{
					new LogAPI().Delete((int)p.Id, project);
				}

				Projects.Instance.Project[project].Log.Remove(p);
                Projects.Save();
            }
        }

        private void bnDeleteCalendar_Click(object sender, RoutedEventArgs e)
        {
            if (lbCalendar.SelectedItems.Count > 0)
            {
                Calendar p = lbCalendar.SelectedItem as Calendar;


				if (Globals.isMultiuser)
				{
					new CalendarAPI().Delete((int)p.Id, project);
				}

				Projects.Instance.Project[project].Calendar.Remove(p);
                Projects.Save();
            }
        }

        private void mnuAddCalendar_Click(object sender, RoutedEventArgs e)
        {
            bnAddCalendar_Click(sender, e);
        }

        private void mnuEditCalendar_Click(object sender, RoutedEventArgs e)
        {
            lbCalendar_SelectionChanged(sender, null);
        }

        private void mnuRemoveCalendar_Click(object sender, RoutedEventArgs e)
        {
            if (lbCalendar.SelectedItems.Count > 0)
            {


				if (Globals.isMultiuser)
				{
					new CalendarAPI().Delete((int)((Calendar)lbCalendar.SelectedItem).Id, project);
				}

				Projects.Instance.Project[project].Calendar.Remove((Calendar)lbCalendar.SelectedItem);
                tbCalendarCaption.Text = string.Empty;
                tbCalendarDetails.Text = string.Empty;
                tbCalendarFrom.Text = string.Empty;
                tbCalendarTo.Text = string.Empty;
                calCalendar.SelectedDate = null;
            }
        }

        private void mnuRemoveToDo_Click(object sender, RoutedEventArgs e)
        {
            bnDeleteToDo_Click(sender, e);
        }

        private void mnuRemoveLog_Click(object sender, RoutedEventArgs e)
        {
            bnDeleteLog_Click(sender, e);
        }

        private void mnuEditToDo_Click(object sender, RoutedEventArgs e)
        {
            ToDo note = lbTodo.SelectedItem as ToDo;
            if (note != null)
            {
                AddEditNote addEditNote = new AddEditNote();
                addEditNote.caption = note.caption;
                addEditNote.note = note.description;


                if (addEditNote.ShowDialog() == true)
                {
                    for (int i = 0; i < Projects.Instance.Project[project].ToDo.Count; i++)
                    {
                        if (Projects.Instance.Project[project].ToDo[i].Id == note.Id)
                        {

                            Projects.Instance.Project[project].ToDo[i].description = addEditNote.note;
                            Projects.Instance.Project[project].ToDo[i].caption = addEditNote.caption;

                            if (Globals.isMultiuser)
                            {
                                new ToDoAPI().Update(Projects.Instance.Project[project].ToDo[i], project);
                            }

                            Projects.Save();
                            reloadItems();
                            break;
                        }
                    }
                }
            }
        }

        private void mnuEditLog_Click(object sender, RoutedEventArgs e)
        {
            Log log = lbLog.SelectedItem as Log;
            if (log != null)
            {
                AddEditNote addEditNote = new AddEditNote();
                addEditNote.caption = log.caption;
                addEditNote.note = log.description;


                if (addEditNote.ShowDialog() == true)
                {
                    for (int i = 0; i < Projects.Instance.Project[project].Log.Count; i++)
                    {
                        if (Projects.Instance.Project[project].Log[i].Id == log.Id)
                        {
                            Log l = Projects.Instance.Project[project].Log[i];
                            l.description = addEditNote.note;
                            l.caption = addEditNote.caption;
                            Projects.Instance.Project[project].Log.RemoveAt(i);
                            Projects.Instance.Project[project].Log.Insert(i, l);


							if (Globals.isMultiuser)
							{
								new LogAPI().Update(Projects.Instance.Project[project].Log[i], project);
							}

							Projects.Save();
                            break;
                        }
                    }
                }
            }
        }

        private void mnuAddToDo_Click(object sender, RoutedEventArgs e)
        {
            bnAddToDo_Click(sender, e);
        }

        private void mnuAddLog_Click(object sender, RoutedEventArgs e)
        {
            bnAddLog_Click(sender, e);
        }

        private void mnuAddNote_Click(object sender, RoutedEventArgs e)
        {
            bnAddNote_Click(sender, e);
        }

        private void mnuEditNote_Click(object sender, RoutedEventArgs e)
        {
            Note note = lbNotes.SelectedItem as Note;
            if (note != null)
            {
                AddEditNote addEditNote = new AddEditNote();
                addEditNote.caption = note.name;
                addEditNote.description = note.description;
                addEditNote.note = note.text;

                if (addEditNote.ShowDialog() == true)
                {
                    for (int i = 0; i < Projects.Instance.Project[project].Notes.Count; i++)
                    {
                        if (Projects.Instance.Project[project].Notes[i].Id == note.Id)
                        {

                            Projects.Instance.Project[project].Notes[i].text = addEditNote.note;
                            Projects.Instance.Project[project].Notes[i].description = addEditNote.description;
                            Projects.Instance.Project[project].Notes[i].name = addEditNote.caption;


							if (Globals.isMultiuser)
							{
								new NoteAPI().Update(Projects.Instance.Project[project].Notes[i], project);
							}

							Projects.Save();
                            reloadItems();
                            break;
                        }
                    }
                }
            }
        }

        private void mnuRemoveNote_Click(object sender, RoutedEventArgs e)
        {
            bnDeleteNote_Click(sender, e);
        }

        private void mnuAddDocument_Click(object sender, RoutedEventArgs e)
        {
            bnAddFile_Click(sender, e);
        }

        private void mnuEditDocument_Click(object sender, RoutedEventArgs e)
        {
            File p=lbFiles.SelectedItem as File;
            if (p != null)
            {
                AddEditFile wnd = new AddEditFile();
                wnd.file = p.fileName;
                wnd.name = p.name;
                wnd.description = p.description;
                wnd.startOnce=p.startOnce;
                wnd.isExe = false;

                if (wnd.ShowDialog() == true)
                {
                    p.fileName = wnd.file;
                    p.description = wnd.description;
                    p.name = wnd.name;
                    p.startOnce = wnd.startOnce;

                    System.Drawing.Icon result = (System.Drawing.Icon)null;

                    result = System.Drawing.Icon.ExtractAssociatedIcon(wnd.file);
                    if (result != null)
                    {
                        ImageSource img = ToImageSource(result);
                        p.picture = img;
                        for (int i = 0; i < Projects.Instance.Project[project].Files.Count; i++)
                        {
                            if (Projects.Instance.Project[project].Files[i] == (File)lbFiles.SelectedItem)
                            {
                                Projects.Instance.Project[project].Files[i] = p;


								if (Globals.isMultiuser)
								{
									new FileAPI().Update(p, project);
								}

								break;
                            }
                        }

                       
                        reloadItems();
                        Projects.Save();
                    }
                }
            }
        }

        private void mnuRemoveDocument_Click(object sender, RoutedEventArgs e)
        {
            bnDeleteAppFile_Click(sender, e);
        }

        private void mnuAddApp_Click(object sender, RoutedEventArgs e)
        {
            bnAddProgram_Click(sender, e);
        }

        private void mnuEditApp_Click(object sender, RoutedEventArgs e)
        {
            Program p = lbApps.SelectedItem as Program;
            if (p != null)
            {
                AddEditFile wnd = new AddEditFile();
                wnd.name = p.name;
                wnd.description = p.description;
                wnd.file = p.executaleFile;
                wnd.startOnce = p.startOnce;
                wnd.isExe = true;

                if (wnd.ShowDialog() == true)
                {
                    if (!System.IO.File.Exists(wnd.file))
                    {
                        MessageBox.Show("File not found.");
                    }
                    else
                    {

                        p.executaleFile = wnd.file;
                        p.description = wnd.description;
                        p.name = wnd.name;
                        p.startOnce = wnd.startOnce;

                        System.Drawing.Icon result = (System.Drawing.Icon)null;

                        result = System.Drawing.Icon.ExtractAssociatedIcon(wnd.file);
                        if (result != null)
                        {
                            ImageSource img = ToImageSource(result);
                            p.picture = img;

                            for (int i = 0; i < Projects.Instance.Project[project].Apps.Count; i++)
                            {
                                if (Projects.Instance.Project[project].Apps[i] == (Program)lbApps.SelectedItem)
                                {
                                    Projects.Instance.Project[project].Apps[i] = p;

									if (Globals.isMultiuser)
									{
										new ProgramAPI().Update(p, project);
									}

									break;
                                }
                            }
                            
                            reloadItems();
                            Projects.Save();
                        }
                    }
                }
            }
        }

        private void mnuRemoveApp_Click(object sender, RoutedEventArgs e)
        {
            bnDeleteAppFile_Click((object)sender, e);
        }

        private void lbApps_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (String file in files)
                {
                    if (file.EndsWith(".exe") || file.EndsWith(".bat") || file.EndsWith(".cmd") || file.EndsWith(".lnk") || file.EndsWith(".ps1"))
                    {
                        Program p = new Program();
                        p.executaleFile = file;
                        p.description = "";
                        MsgBox msgBox = new MsgBox("Please enter a title for " + System.IO.Path.GetFileName(file));
                        if (msgBox.ShowDialog()==true)
                        {
                            p.name = msgBox.ret;
                         

                            p.startOnce = false;
                            System.Drawing.Icon result = (System.Drawing.Icon)null;

                            result = System.Drawing.Icon.ExtractAssociatedIcon(file);
                            if (result != null)
                            {
                                ImageSource img = ToImageSource(result);
                                p.picture = img;

                                if (!Globals.isMultiuser)
                                {
									if (Projects.Instance.Project[project].Apps.Count == 0)
										p.Id = 0;
									else
										p.Id = Projects.Instance.Project[project].Apps.Max(a => a.Id) + 1;
                                }
                                else
                                {
									long Id=new ProgramAPI().Create(p, project);
                                    p.Id = Id;
								}

                                Projects.Instance.Project[project].Apps.Add(p);                       
                                Projects.Save();
                            }
                        }
                    }
                }
            }
        }

        private void lbFiles_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (String file in files)
                {
                    File p = new File();
                    p.Id = Projects.Instance.Project[project].Files.Count == 0 ? 0 : Projects.Instance.Project[project].Files.Max(a => a.Id) + 1;
                    p.fileName = file;
                    p.description = "";
                    MsgBox msgBox = new MsgBox("Please enter a title for " + System.IO.Path.GetFileName(file));
                    if (msgBox.ShowDialog() == true)
                    {
                        p.name = msgBox.ret;

                        p.startOnce = false;
                        System.Drawing.Icon result = (System.Drawing.Icon)null;
                        ImageSource img;

                        try
                        {
                            result = System.Drawing.Icon.ExtractAssociatedIcon(file);
                            img = ToImageSource(result);
                        }
                        catch
                        {
                            img = Imaging.CreateBitmapSourceFromHBitmap(new System.Drawing.Bitmap(System.Drawing.Image.FromFile(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\folder.png")).GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        }

                        p.picture = img;

                        if(!Globals.isMultiuser)
                        {
							p.Id = Projects.Instance.Project[project].Files.Count == 0 ? 0 : Projects.Instance.Project[project].Files.Max(a => a.Id) + 1;
						}
                        else
                        {
                            long Id = new FileAPI().Create(p, project);
                            p.Id = Id;
                        }

						Projects.Instance.Project[project].Files.Add(p);
                        Projects.Save();
                    }
                }
            }
        }

		private void mnuHighToDo_Click(object sender, RoutedEventArgs e)
		{
			ToDo note = lbTodo.SelectedItem as ToDo;
			if (note != null)
			{
					for (int i = 0; i < Projects.Instance.Project[project].ToDo.Count; i++)
					{
						if (Projects.Instance.Project[project].ToDo[i].Id == note.Id)
						{

                            Projects.Instance.Project[project].ToDo[i].priority = 1;

                            if(Globals.isMultiuser)
                            {
                                new ToDoAPI().Update(Projects.Instance.Project[project].ToDo[i], project);
							}

							Projects.Save();
							reloadItems();
							break;
						}
					}
			}
		}

		private void mnuMediumToDo_Click(object sender, RoutedEventArgs e)
		{
			ToDo note = lbTodo.SelectedItem as ToDo;
			if (note != null)
			{
				for (int i = 0; i < Projects.Instance.Project[project].ToDo.Count; i++)
				{
					if (Projects.Instance.Project[project].ToDo[i].Id == note.Id)
					{

						Projects.Instance.Project[project].ToDo[i].priority = 2;


						if (Globals.isMultiuser)
						{
							new ToDoAPI().Update(Projects.Instance.Project[project].ToDo[i], project);
						}

						Projects.Save();
						reloadItems();
						break;
					}
				}
			}
		}

		private void mnuLowToDo_Click(object sender, RoutedEventArgs e)
		{
			ToDo note = lbTodo.SelectedItem as ToDo;
			if (note != null)
			{
				for (int i = 0; i < Projects.Instance.Project[project].ToDo.Count; i++)
				{
					if (Projects.Instance.Project[project].ToDo[i].Id == note.Id)
					{

						Projects.Instance.Project[project].ToDo[i].priority = 3;


						if (Globals.isMultiuser)
						{
							new ToDoAPI().Update(Projects.Instance.Project[project].ToDo[i], project);
						}

						Projects.Save();
						reloadItems();
						break;
					}
				}
			}
		}

		private void mnuSendTodo_Click(object sender, RoutedEventArgs e)
		{
            if (lbTodo.SelectedItem != null)
            {

                ItemSendUserSelect itemSendUserSelect = new ItemSendUserSelect(project);
                if (itemSendUserSelect.ShowDialog() == true)
                {
                    long idUser = itemSendUserSelect.userId;
                    if (lbTodo.SelectedItem != null)
                    {
                        long idItem = ((ToDo)lbTodo.SelectedItem).Id;
                        new ItemPushAPI().AddItem((int)idItem, (int)idUser, project, (int)ItemPush.ItemType.ToDo);
                    }
                }
                else
                {
                    MessageBox.Show("No users selected. Aborting.", "Cancel");
                }
            }
		}

		private void mnuSendDocument_Click(object sender, RoutedEventArgs e)
		{
            if (lbFiles.SelectedItem != null)
            {
                ItemSendUserSelect itemSendUserSelect = new ItemSendUserSelect(project);
                if (itemSendUserSelect.ShowDialog() == true)
                {
                    long idUser = itemSendUserSelect.userId;
                    long idItem = ((File)lbFiles.SelectedItem).Id;
                    new ItemPushAPI().AddItem((int)idItem, (int)idUser, project, (int)ItemPush.ItemType.File);
                }
                else
                {
                    MessageBox.Show("No users selected. Aborting.", "Cancel");
                }
            }
		}

		private void mnuSendNote_Click(object sender, RoutedEventArgs e)
		{
            if (lbNotes.SelectedItem != null)
            {
                ItemSendUserSelect itemSendUserSelect = new ItemSendUserSelect(project);
                if (itemSendUserSelect.ShowDialog() == true)
                {
                    long idUser = itemSendUserSelect.userId;
                    long idItem = ((Note)lbNotes.SelectedItem).Id;
                    new ItemPushAPI().AddItem((int)idItem, (int)idUser, project, (int)ItemPush.ItemType.Note);
                }
                else
                {
                    MessageBox.Show("No users selected. Aborting.", "Cancel");
                }
            }
		}

		private void mnuSendCalendar_Click(object sender, RoutedEventArgs e)
		{
            if (lbCalendar.SelectedItem != null)
            {
                ItemSendUserSelect itemSendUserSelect = new ItemSendUserSelect(project);
                if (itemSendUserSelect.ShowDialog() == true)
                {
                    long idUser = itemSendUserSelect.userId;
                    long idItem = ((Calendar)lbCalendar.SelectedItem).Id;
                    new ItemPushAPI().AddItem((int)idItem, (int)idUser, project, (int)ItemPush.ItemType.Calendar);
                }
                else
                {
                    MessageBox.Show("No users selected. Aborting.", "Cancel");
                }
            }
		}

		private void mnuIdeaToDo_Click(object sender, RoutedEventArgs e)
		{
			ToDo note = lbTodo.SelectedItem as ToDo;
			if (note != null)
			{
				for (int i = 0; i < Projects.Instance.Project[project].ToDo.Count; i++)
				{
					if (Projects.Instance.Project[project].ToDo[i].Id == note.Id)
					{

						Projects.Instance.Project[project].ToDo[i].priority = 4;


						if (Globals.isMultiuser)
						{
							new ToDoAPI().Update(Projects.Instance.Project[project].ToDo[i], project);
						}

						Projects.Save();
						reloadItems();
						break;
					}
				}
			}
		}

		private async void bnSendChat_Click(object sender, RoutedEventArgs e)
		{
            if (tbChatInput.Text != "")
            {
				await hubConnection.InvokeAsync("SendMessage",  Globals.session, project, tbChatInput.Text);
			}
            tbChatInput.Text = "";
		}

        public void startItemUpdateThread()
        {
            new System.Threading.Thread(ItemUpdateThread).Start();

		}

        public void ItemUpdateThread()
        {
            while (FloatingWindow.Instance.running)
            {
                if (Globals.isMultiuser)
                {
                 
                            new ProjectAPI().FetchAll();
							reloadItems();
					
                }
                System.Threading.Thread.Sleep(5000);
            }
        }
	}
}
