﻿using Newtonsoft.Json;
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
			List<Project> projects = JsonConvert.DeserializeObject<UserDto>(PostFetchAll("/api/Project/GetAll/"+Globals.session)).projects;
			foreach (var item in projects)
			{
				Data data = new Data();
				data.Calendar = new ObservableCollection<Calendar>(item.Calendars);
				data.ToDo = new ObservableCollection<ToDo>(item.ToDo);
				data.Apps = new ObservableCollection<Program>(item.Programs);
				data.Notes = new ObservableCollection<Note>(item.Notes);
				data.Files = new ObservableCollection<File>(item.Files);

				Projects.Instance.Project.Add(item.Name, data);
			}
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
	}
}
