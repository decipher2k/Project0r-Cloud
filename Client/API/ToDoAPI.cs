using Newtonsoft.Json;
using ProjectOrganizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_Assistant.API
{
	public  class ToDoAPI : APIBase
	{
		public bool Create(ToDo item, String project)
		{
			String sItem=JsonConvert.SerializeObject(item);
			return PostCreate(sItem, "/api/ToDo/Create", project);				
		}

		public ToDo Read(int Id, String project)
		{
			String sToDo = PostRead(Id, "/api/ToDo/Read", project);
			return JsonConvert.DeserializeObject<ToDo>(sToDo);
		}

		public bool Update(ToDo item, String project)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostUpdate(sItem, "/api/ToDo/Update", project);
		}

		public bool Delete(int Id, String project)
		{
			return PostDelete(Id, "/api/ToDo/Delete", project);
		}
	}
}
