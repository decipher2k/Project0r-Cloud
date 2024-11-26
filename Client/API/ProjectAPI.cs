using Newtonsoft.Json;
using Project_Assistant.Dto;
using ProjectOrganizer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Project_Assistant.API
{
	internal class ProjectAPI : APIBase
	{
		public void FetchAll()
		{
			Projects.Instance.Project.Clear();
			List<Project> projects = JsonConvert.DeserializeObject<List<Project>>(PostFetchAll(Globals.session));
			foreach (var item in projects)
			{
				Data data = new Data();
				data.Calendar = (ObservableCollection<Calendar>) item.Calendars.AsEnumerable(); 
				data.ToDo = (ObservableCollection<ToDo>)item.ToDo.AsEnumerable();
				data.Apps = (ObservableCollection<Program>)item.Programs.AsEnumerable();
				data.Notes = (ObservableCollection<Note>)item.Notes.AsEnumerable();
				data.Files = (ObservableCollection<File>)item.Files.AsEnumerable();

				Projects.Instance.Project.Add(item.Name, data);
			}
		}

		public long Create(String item)
		{		
			IdSessionDto idSessionDto = PostCreate( item, "/api/Project/Create"); return idSessionDto.Id;
		}

		public String Read(int Id, String project)
		{
			String sItem = PostRead(Id, "/api/Project/Read", project);
			return sItem;
		}

		public bool Update(String item, String project)
		{
			return PostUpdate(item, "/api/Project/Update", project);
		}

		public bool Delete(int Id, String project)
		{
			return PostDelete(Id, "/api/Project/Delete", project);
		}
	}
}
