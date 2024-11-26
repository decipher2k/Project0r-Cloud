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
		public bool Create(ToDo item)
		{
			String sItem=JsonConvert.SerializeObject(item);
			return PostCreate(sItem, "/api/ToDo/Create");
				
		}

		public ToDo Read(int Id)
		{
			String sToDo = PostRead(Id, "/api/ToDo/Read");
			return JsonConvert.DeserializeObject<ToDo>(sToDo);
		}

		public bool Update(ToDo item)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostCreate(sItem, "/api/ToDo/Update");
		}

		public bool Delete(int Id)
		{
			return PostDelete(Id, "/api/ToDo/Delete");
		}
	}
}
