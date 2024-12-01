using Newtonsoft.Json;
using Project_Assistant.Dto;
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
		public long Create(ToDo item, String project)
		{
			String sItem=JsonConvert.SerializeObject(item);
			IdSessionDto idSessionDto = PostCreate( sItem, "/api/ToDo/Create", project); return idSessionDto.Id;				
		}

		public ToDo Read(long Id, String project)
		{
			String sToDo = PostRead(Id, "/api/ToDo/Read", project);
			Globals.session = JsonConvert.DeserializeObject<ItemDto>(sToDo).session;
			return JsonConvert.DeserializeObject<ToDo>(sToDo);
		}

		public bool Update(ToDo item, String project)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostUpdate(sItem, "/api/ToDo/Update", project);
		}

		public bool Delete(long Id, String project)
		{
			return PostDelete(Id, "/api/ToDo/Delete", project);
		}
	}
}
