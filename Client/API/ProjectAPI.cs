using Newtonsoft.Json;
using Project_Assistant.Dto;
using ProjectOrganizer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Project_Assistant.API
{
	internal class ProjectAPI : APIBase
	{
		public void FetchAll()
		{			
			Projects.Instance=new Projects();
			UserDto userDto = JsonConvert.DeserializeObject<UserDto>(PostFetchAll("/api/Project/GetAll/" + Globals.session));
			Globals.session = userDto.session;
			List<Project> projects = userDto.projects;
			foreach (var item in projects)
			{
				for (int i = 0; i < item.Programs.Count; i++)
				{
					Program program = item.Programs[i];
					System.Drawing.Icon result = (System.Drawing.Icon)null;

					result = System.Drawing.Icon.ExtractAssociatedIcon(program.executaleFile);
					if (result != null)
					{
						ImageSource img = ToImageSource(result);
						program.picture = img;
						item.Programs[i] = program;
					}
					
				}

				for (int i = 0; i < item.Files.Count; i++)
				{
					File file = item.Files[i];
					System.Drawing.Icon result = (System.Drawing.Icon)null;

					result = System.Drawing.Icon.ExtractAssociatedIcon(file.fileName);
					if (result != null)
					{
						ImageSource img = ToImageSource(result);
						file.picture = img;
						item.Files[i] = file;
					}

				}

				Data data = new Data();
				data.Calendar = new ObservableCollection<Calendar>(item.Calendars);
				data.ToDo = new ObservableCollection<ToDo>(item.ToDo);
				data.Apps = new ObservableCollection<Program>(item.Programs);
				data.Notes = new ObservableCollection<Note>(item.Notes);
				data.Files = new ObservableCollection<File>(item.Files);
				data.Log = new ObservableCollection<Log>(item.Logs);

				Projects.Instance.Project.Add(item.Name, data);
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
		public List<UserDataDto.UserData> FetchUsers(String project)
		{
			Projects.Instance.Project.Clear();
			UserDataDto userDataDto = JsonConvert.DeserializeObject<UserDataDto>(PostFetchProjectUsers("/api/Project/FetchUsers",project));
			Globals.session = userDataDto.session;
			return userDataDto.Data;
		}

		public List<UserDataDto.UserData> FetchAllUsers()
		{
			Projects.Instance.Project.Clear();
			UserDataDto userDataDto = JsonConvert.DeserializeObject<UserDataDto>(PostFetchAllUsers("/api/Project/FetchProjectUsers"));
			Globals.session = userDataDto.session;
			return userDataDto.Data;
		}


		public long Create(String item)
		{		
			IdSessionDto idSessionDto = PostCreate( item, "/api/Project/Create"); return idSessionDto.Id;
		}

		public String Read(String name, String project)
		{
			String sItem = PostRead(name, "/api/Project/Read", project);
			return sItem;
		}

		public bool Update(String item, String project)
		{
			return PostUpdate(item, "/api/Project/Update", project);
		}

		public bool Delete(String name, String project)
		{
			return PostDelete(name, "/api/Project/Delete", project);
		}

		public bool InviteUser(int idUser, String project)
		{
			return PostCreate(idUser.ToString(), "/api/Project/Invite",project)!=null;
		}

		public bool AccepDenyInvite(bool accept, String project)
		{			
			return PostUpdate(accept.ToString(), "/api/Project/AcceptDenyInvite",project)!=null;
		}
	}
}
