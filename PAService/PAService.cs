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
using System.Runtime.CompilerServices;

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

		private Data data=new Data();

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
		[MethodImpl(MethodImplOptions.NoOptimization)]
		public PAService()
		{
			InitializeComponent();
		}
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private String Read(NamedPipeServerStream server)
		{
			String ret = "";
			int b;
			int count = 0;
			char[] buffer = new char[255];
			while (server.ReadByte() <= 0) ;
			do
			{
				b = server.ReadByte();
				buffer[count] = (char)b;
				count++;
				//replace "$" with \0 as a stop sign
			} while (b > 0 && ((char)b)!='$' && count<250);
			return new String(buffer).Substring(0,count-1);
		}
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private void Write(String value, NamedPipeServerStream server)
		{
			server.Write(Encoding.ASCII.GetBytes("_" + value + "$"), 0, ("_" + value + "$").Length);
		}
		[MethodImpl(MethodImplOptions.NoOptimization)]
		protected override void OnStart(string[] args)
		{
			
			LoadData();
			Task.Factory.StartNew(() =>
			{
				
				while (running)
				{
					
					
					SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);
					PipeAccessRule access = new PipeAccessRule(sid, PipeAccessRights.ReadWrite, AccessControlType.Allow);
					PipeSecurity pipeSecurity = new PipeSecurity();
					pipeSecurity.AddAccessRule(access);
					var server = new NamedPipeServerStream("PAServiceNamedPipe",PipeDirection.InOut,10,PipeTransmissionMode.Byte,PipeOptions.None,5,5, pipeSecurity);
					server.WaitForConnection();
				
					try
					{
						
						//while (reader.Peek() > 0) ;
						{
							String line = "";
						

							// Read the incoming message	
							line = Read(server);
							if (line==("CHANGEDATA"))
							{
								uint processId = getNamedPipeClientProcID((NamedPipeServerStream)server);
								if (checkMD5(Process.GetProcessById((int)processId).MainModule.FileName))
								{
									Write("SENDUSER",server);
									data.username = Read(server);
									Write("SENDPASS", server);
									data.password = Read(server);
									Write("SENDSERVER", server); 
									data.server =  Read(server);
									Write("DONE", server);
									
									SaveData();
								}
								else
								{
									Write("ERROR", server);
								}
							}
							else if (line.Contains("INIT"))	
							{
								
								if (data.FileMD5 == "")
								{
									Write("SENDMASTERPASS",server);
									String mpass = Read(server);
									data.masterPass = mpass;

									uint processId = getNamedPipeClientProcID(server);
									using (var md5 = MD5.Create())
									{
										using (var stream = File.OpenRead(Process.GetProcessById((int)processId).MainModule.FileName))
										{
											data.FileMD5 = Base64(md5.ComputeHash(stream));
										}
									}
									Write("DONE",server);
									SaveData();
								}
							}
							else if (line.Contains("CHANGEMASTERPASS"))
							{
								Write("SENDPASS",server);

								String oldMasterPass = Read(server);
								if (oldMasterPass == data.masterPass)
								{
									Write("SENDNEWPASS",server);
									data.masterPass = Read(server);

									Write("DONE",server);		
									SaveData();
								}
							}
							else if (data.masterPass.Contains(line) && data.masterPass != "")
							{

								uint processId = getNamedPipeClientProcID((NamedPipeServerStream)server);
								using (var md5 = MD5.Create())
								{
									using (var stream = File.OpenRead(Process.GetProcessById((int)processId).MainModule.FileName))
									{
										data.FileMD5 = Base64(md5.ComputeHash(stream));
									}
								}

								Write("DONE",server);
						
								SaveData();

							}
							else if (line==("GETAUTH"))
							{
								uint processId = getNamedPipeClientProcID((NamedPipeServerStream)server);

								if (checkMD5(Process.GetProcessById((int)processId).MainModule.FileName))
								{
									Write(data.username + ";" + data.password + ";" + data.server,server);
								
								}
								else
								{
									Write("ERROR",server);
						
								}

							}
						}
					} catch (Exception ex) 
					{
						server.Close();
					}



					server.Close();

				}
			});
		}
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private void LoadData()
		{
			if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Project Assistant\\service.dat"))
			{
				using (Stream stream = File.Open(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Project Assistant\\service.dat", FileMode.Open))
				{
					var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
					data = (Data)binaryFormatter.Deserialize(stream);
				}
			}
		}
		[MethodImpl(MethodImplOptions.NoOptimization)]
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

		[MethodImpl(MethodImplOptions.NoOptimization)]
		private bool checkMD5(String path)
		{
			using (var md5 = MD5.Create())
			{
				using (var stream = File.OpenRead(path))
				{
					String base64 = Base64(md5.ComputeHash(stream));
					return base64==data.FileMD5;
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
