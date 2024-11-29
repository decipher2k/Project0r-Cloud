using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO.Pipes;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Resources;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;

namespace PAService
{
	public partial class PAService : ServiceBase
	{
		bool running=true;
		class Data
		{
			public String masterPass;
			public String FileMD5 = "";
			public String username;
			public String password;
		}

		private Data data;

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetNamedPipeClientProcessId(IntPtr Pipe, out uint ClientProcessId);
		public static uint getNamedPipeClientProcID(NamedPipeServerStream pipeServer)
		{
			UInt32 nProcID;
			IntPtr hPipe = pipeServer.SafePipeHandle.DangerousGetHandle();
			if (GetNamedPipeClientProcessId(hPipe, out nProcID))
				return nProcID;
			return 0;
		}

		public PAService()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			Task.Factory.StartNew(() =>
			{
				while (running)
				{
					
					var server = new NamedPipeServerStream("PAServiceNamedPipe");
					server.WaitForConnection();
					StreamReader reader = new StreamReader(server);
					StreamWriter writer = new StreamWriter(server);

					try
					{
						var line = reader.ReadLine();
						if(line=="CHANGEDATA")
						{
							uint processId = getNamedPipeClientProcID((NamedPipeServerStream)server);
							if (checkMD5(Process.GetProcessById((int)processId).MainModule.FileName))
							{
								writer.WriteLine("SENDUSER");
								writer.Flush();
								data.username = reader.ReadLine();
								writer.WriteLine("SENDPASS");
								writer.Flush();
								data.password = reader.ReadLine();
								writer.WriteLine("DONE");
								writer.Flush();
							}
						}
						if(line=="INIT")
						{
							if(data.FileMD5 == "")
							{
								writer.WriteLine("SENDMASTERPASS");
								writer.Flush();
								String mpass = reader.ReadLine();
								data.masterPass = mpass;

								uint processId = getNamedPipeClientProcID((NamedPipeServerStream)server);
								using (var md5 = MD5.Create())
								{
									using (var stream = File.OpenRead(Process.GetProcessById((int)processId).MainModule.FileName))
									{
										data.FileMD5=Base64(md5.ComputeHash(stream));
									}
								}
								writer.WriteLine("DONE");
								writer.Flush();
							}							
						}
						else if(line=="CHANGEMASTERPASS")
						{
							writer.WriteLine("SENDPASS");
							writer.Flush();
							String oldMasterPass=reader.ReadLine();
							if(oldMasterPass== data.masterPass)
							{
								writer.WriteLine("SENDNEWPASS");
								writer.Flush();
								data.masterPass = reader.ReadLine();

								writer.WriteLine("DONE");
								writer.Flush();
							}
						}
						else if(line== data.masterPass)
						{
							if (data.FileMD5 == "")
							{
								uint processId = getNamedPipeClientProcID((NamedPipeServerStream)server);
								using (var md5 = MD5.Create())
								{
									using (var stream = File.OpenRead(Process.GetProcessById((int)processId).MainModule.FileName))
									{
										data.FileMD5 = Base64(md5.ComputeHash(stream));
									}
								}

								writer.WriteLine("DONE");
								writer.Flush();
							}
						}
						else if (line == "GETAUTH")
						{
							uint processId = getNamedPipeClientProcID((NamedPipeServerStream)server);							

							if (checkMD5(Process.GetProcessById((int)processId).MainModule.FileName))
							{
								writer.WriteLine(data.username + ";" + data.password);
								writer.Flush();
							}
						}
					} catch (Exception ex) 
					{					
						reader.Close();
						writer.Close();
						server.Close();
					}


					writer.Close();
					reader.Close();
					server.Close();

				}
			});
		}

		private void LoadData()
		{

		}

		private void SaveData()
		{

		}

		private bool checkMD5(String path)
		{
			using (var md5 = MD5.Create())
			{
				using (var stream = File.OpenRead(path))
				{
					return Base64(md5.ComputeHash(stream))==FileMD5;
				}
			}
		}

		private String Base64(byte[] input)
		{
			return System.Convert.ToBase64String(input);
		}

		protected override void OnStop()
		{
			running = false;
			base.OnStop();
		}
	}
}
