using Newtonsoft.Json;
using Project_Assistant.Dto;
using ProjectOrganizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Assistant.API
{
	public class CalendarAPI : APIBase
	{
		public long Create(Calendar item, String project)
		{
			String sItem = JsonConvert.SerializeObject(item);
			IdSessionDto idSessionDto=PostCreate(sItem, "/api/Calendar/Create", project);
			return idSessionDto.Id;
		}

		public Calendar Read(int Id, String project)
		{
			String sToDo = PostRead(Id, "/api/Calendar/Read", project);
			Globals.session = JsonConvert.DeserializeObject<ItemDto>(sToDo).session;
			return JsonConvert.DeserializeObject<Calendar>(sToDo);
		}

		public bool Update(Calendar item, String project)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostUpdate(sItem, "/api/Calendar/Update", project);
		}

		public bool Delete(int Id, String project)
		{			
			return PostDelete(Id, "/api/Calendar/Delete", project);
		}

	}
}
