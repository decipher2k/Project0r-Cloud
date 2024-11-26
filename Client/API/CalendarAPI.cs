using Newtonsoft.Json;
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
		public bool Create(Calendar item, String project)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostCreate(sItem, "/api/Calendar/Create", project);

		}

		public ToDo Read(int Id, String project)
		{
			String sToDo = PostRead(Id, "/api/Calendar/Read", project);
			return JsonConvert.DeserializeObject<ToDo>(sToDo);
		}

		public bool Update(ToDo item, String project)
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
