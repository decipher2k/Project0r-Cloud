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
using System.Security.AccessControl;
using System.Security.Principal;

namespace PAService
{
	public partial class PAService : ServiceBase
	{
		bool running=true;

		[Serializable]
		class Data
		{
			public String masterPass="";
			public String FileMD5  = "";
			public String username="";
			public String password="";
			public String server = "";
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
			LoadData();
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
								writer.WriteLine("SENDSERVER");
								writer.Flush();
								data.server = reader.ReadLine();
								writer.WriteLine("DONE");
								writer.Flush();
								SaveData();
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
								SaveData();
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
								SaveData();
							}
						}
						else if(line== data.masterPass && data.masterPass!="")
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
							SaveData();

						}
						else if (line == "GETAUTH")
						{
							uint processId = getNamedPipeClientProcID((NamedPipeServerStream)server);							

							if (checkMD5(Process.GetProcessById((int)processId).MainModule.FileName))
							{
								writer.WriteLine(data.username + ";" + data.password+";"+data.server);
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
			using (Stream stream = File.Open(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Project Assistant\\service.dat", FileMode.Open))
			{
				var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

				data = (Data)binaryFormatter.Deserialize(stream);
			}
		}

		private void SaveData()
		{
			if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Project Assistant"))
			{
				Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Project Assistant");
			}



			using (Stream stream = File.Open(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Project Assistant\\service.dat", FileMode.Create))
			{
				var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				binaryFormatter.Serialize(stream, data);
				stream.Close();

				var fileSecurity = File.GetAccessControl(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Project Assistant\\service.dat");
				foreach (FileSystemAccessRule accessRule in fileSecurity.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount)))
					fileSecurity.RemoveAccessRule(accessRule);

				foreach (FileSystemAccessRule accessRule in fileSecurity.GetAccessRules(true, true, typeof(System.Security.Principal.WindowsIdentity)))
					fileSecurity.RemoveAccessRule(accessRule);

				var fileAccessRule = new FileSystemAccessRule(new NTAccount("", "SYSTEM"),
					FileSystemRights.FullControl,
					AccessControlType.Allow);

				fileSecurity.SetAccessRule(fileAccessRule);

				File.SetAccessControl(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Project Assistant\\service.dat", fileSecurity);
			}

		}

		private bool checkMD5(String path)
		{
			using (var md5 = MD5.Create())
			{
				using (var stream = File.OpenRead(path))
				{
					return Base64(md5.ComputeHash(stream))==data.FileMD5;
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
