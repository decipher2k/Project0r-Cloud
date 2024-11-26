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
		public bool Create(Calendar item)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostCreate(sItem, "/api/Calendar/Create");

		}

		public ToDo Read(int Id)
		{
			String sToDo = PostRead(Id, "/api/Calendar/Read");
			return JsonConvert.DeserializeObject<ToDo>(sToDo);
		}

		public bool Update(ToDo item)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostUpdate(sItem, "/api/Calendar/Update");
		}

		public bool Delete(int Id)
		{			
			return PostDelete(Id, "/api/Calendar/Delete");
		}

	}
}
