using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Microsoft.Win32;
using Project_Assistant;
using System.Globalization;
using Project_Assistant_Server.Dto;
using Project_Assistant.API;
using Project_Assistant_Server.Models;
using System.IO.Pipes;
using System.IO;
using System.Net;
using System.Web;

namespace ProjectOrganizer
{
    /// <summary>
    /// Interaktionslogik für FloatingWindow.xaml
    /// </summary>
    
    public partial class FloatingWindow : Window
    {
        
        public static FloatingWindow Instance;
        bool draged = false;
        bool startDragin=false;
        MainWindow mainWindow=new MainWindow();
        public static String currentProject = "";
        public List<ItemPush> itemPushes = new List<ItemPush>();
        Dictionary<String,Reminder> reminders = new Dictionary<string, Reminder>();
        public bool isItemPushAvailable=false;

        public FloatingWindow()
        {

			string[] args = Environment.GetCommandLineArgs();
            try
            {
                if (args.Length > 1)
                {
                    if (args[1] == "/init" && args.Length == 3)
                    {
                        var client = new NamedPipeClientStream("PAServiceNamedPipe");
                        client.Connect(2000);
                        StreamReader reader = new StreamReader(client);
                        StreamWriter writer = new StreamWriter(client);

                        writer.WriteLine("INIT");
						writer.Flush();
						if (reader.ReadLine() == "SENDMASTERPASS")
                        {
                            writer.WriteLine(args[2]);
							writer.Flush();
						}
                        if (reader.ReadLine() == "DONE")
                        {
                            Console.WriteLine("Success");
                            Application.Current.Shutdown();
                        }
                        else
                        {
                            Application.Current.Shutdown(-1);
                        }

                    }
                    else if (args[1] == "/changepass" && args.Length == 4)
                    {
                        var client = new NamedPipeClientStream("PAServiceNamedPipe");
                        client.Connect(2000);
                        StreamReader reader = new StreamReader(client);
                        StreamWriter writer = new StreamWriter(client);

                        writer.WriteLine("CHANGEMASTERPASS");
                        if (reader.ReadLine() == "SENDPASS")
                        {
                            writer.WriteLine(args[2]);
							writer.Flush();
						}
                        if (reader.ReadLine() == "SENDNEWPASS")
                        {
                            writer.WriteLine(args[3]);
							writer.Flush();
						}
                        if (reader.ReadLine() == "DONE")
                        {
                            Console.WriteLine("Success");
                            Application.Current.Shutdown();
                        }
                        else
                        {
                            Application.Current.Shutdown(-1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {				
			}

            try
            {
                var client = new NamedPipeClientStream("PAServiceNamedPipe");
                client.Connect(2000);
                StreamReader reader = new StreamReader(client);
                StreamWriter writer = new StreamWriter(client);

                writer.WriteLine("GETAUTH");
                writer.Flush();
                String userdata = reader.ReadLine();
                if (userdata != "ERROR")
                {


                    String username = userdata.Split(";".ToCharArray())[0];
                    String password = userdata.Split(";".ToCharArray())[1];
                    String server = userdata.Split(";".ToCharArray())[2];
                    reader.Close();
                    writer.Close();
                    client.Close();

                    String ret = new WebClient().DownloadString(server + "/api/Session/Login/" + HttpUtility.UrlEncode(username) + "/" + HttpUtility.UrlEncode(password));
                    SessionData sessionData = Newtonsoft.Json.JsonConvert.DeserializeObject<SessionData>(ret);
                    if (sessionData != null)
                    {
                        Globals.session = sessionData.session;
                        Globals.ServerAddress = server;
                        Globals.isMultiuser = true;
                        this.DialogResult = true;
                    }
                    else
                    {
                        Projects.Load();
                    }
                }
                else
                {
                    Projects.Load();
				}
            }
            catch (Exception ex)
            {
				Projects.Load();
			}

			Instance = this;
            InitializeComponent();
          //  LicenseCheck licenseCheck = new LicenseCheck();
           // if(licenseCheck.IsInitialized)
             //   licenseCheck.ShowDialog();
            
            Left = Projects.Instance.x;
            Top = Projects.Instance.y;

            new System.Threading.Thread(ReminderThread).Start();
			new System.Threading.Thread(ItemPushThread).Start();
		}
        public bool running = true;
        [STAThread]
        private void ReminderThread()
        {
            while (running)
            {
                foreach (String p in Projects.Instance.Project.Keys)
                {
                    if (Projects.Instance.Project[p].Calendar.Where(a => a.date <= DateTime.Now && a.handled == false).Count() > 0)
                    {
                        if (reminders.ContainsKey(p))
                        {
                            reminders[p].UpdateItems(Projects.Instance.Project[p]);
                        }
                        else
                        {
                            Action<String> action = new Action<string>((s) =>
                            {
                                for (int i = 0; i < Projects.Instance.Project[s].Calendar.Count; i++)
                                {
                                    if (Projects.Instance.Project[s].Calendar[i].date <= DateTime.Now)
                                    {
                                        Projects.Instance.Project[s].Calendar[i].handled = true;
                                        Projects.Save();
                                    }
                                }
                            });
                            
                            Instance.Dispatcher.Invoke(()=> { 
                                Reminder r = new Reminder(Projects.Instance.Project[p], p, action);
                                reminders.Add(p, r);
                                r.Show();
                            });
                            
                            
                        }
                    }
                }
                System.Threading.Thread.Sleep(1000*60);
            }            
        }

        public void StartItemPushThread()
        {
			new System.Threading.Thread(ItemPushThread).Start();

		}

		[STAThread]
		private void ItemPushThread()
		{
			while (running && Globals.isMultiuser)
			{
                bool showWindow = false;
                ItemPushDto itemPushDto = new ItemPushAPI().PollItems();
                foreach (var item in itemPushDto.Items)
                {
                    if (!itemPushes.Where(a => a.Id == item.Id).Any())
                    {
                        showWindow = true;
                        itemPushes.Add(item);
                    }
                }

                if (showWindow)
                {
					Instance.Dispatcher.Invoke(() => {
                        ItemPushWindow i = new ItemPushWindow();
						i.Show();
					});
				}

                if(itemPushes.Any())
                {
                    isItemPushAvailable = true;
                }
                else
                {
                    isItemPushAvailable= false;
                }

				System.Threading.Thread.Sleep(10000);
			}
		}


		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            startDragin = true;
            if (!draged)
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    if (!mainWindow.IsVisible)
                    {

                        mainWindow = new MainWindow();
                        mainWindow.Show();

                    }
                    else
                    {
                        mainWindow.Activate();
                        mainWindow.WindowState = WindowState.Normal;
                        mainWindow.BringIntoView();
                    }
                }
            }
            else
            {
                Projects.Save();
            }
            draged = false;
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            startDragin=false;
            if (!draged)
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    if (!mainWindow.IsVisible)
                    {

                        mainWindow = new MainWindow();
                        mainWindow.Show();

                    }
                    else
                    {
                        mainWindow.Activate();
                        mainWindow.WindowState = WindowState.Normal;
                        mainWindow.BringIntoView();
                    }
                }
            }
            else
            {
                Projects.Save();
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if(Mouse.LeftButton==MouseButtonState.Pressed)
                this.DragMove();
            draged = true;
        }

        private void mnuCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindow.IsVisible)
            {   
                mainWindow.Close();
            }
			running = false;
			Application.Current.Shutdown();
        }

        private void mnuCloseWindow_Click(object sender, MouseButtonEventArgs e)
        {
            if (mainWindow.IsVisible)
            {    
                mainWindow.Close();               
            }
            running = false;
			Application.Current.Shutdown();
        }

        private void closeAllWindows()
        {
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcesses();
            for (int i = 0; i < process.Length; i++)
            {
                try
                {
                    if (process[i].MainWindowHandle != IntPtr.Zero && process[i].Id != Process.GetCurrentProcess().Id && process[i].ProcessName!="olk"&& process[i].ProcessName != "Teams" && !process[i].ProcessName.ToLower().Contains("explorer") && !process[i].ProcessName.Contains("ShellExperienceHost") && !process[i].ProcessName.ToLower().Contains("outlook") && !process[i].ProcessName.ToLower().Contains("teams"))
                    {
                        process[i].Kill();
                    }
                }
                catch (Exception e)
                {
                    
                }
            }
           
        }

        private void mnuCloseAllWindows_Click(object sender, RoutedEventArgs e)
        {
           closeAllWindows();
        }

        private void mnuShow_Click(object sender, RoutedEventArgs e)
        {
            if (!mainWindow.IsVisible)
            {
                mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                mainWindow.Activate();
                mainWindow.WindowState = WindowState.Normal;
                mainWindow.BringIntoView();
            }
        }
    }
}
