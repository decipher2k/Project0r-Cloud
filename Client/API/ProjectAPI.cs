using Newtonsoft.Json;
using ProjectOrganizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Assistant.API
{
	internal class ProjectAPI : APIBase
	{
		public Dictionary<String,Data> FetchAll()
		{

		}

		public bool Create(String item)
		{		
			return PostCreate(item, "/api/Project/Create");

		}

		public String Read(int Id)
		{
			String sItem = PostRead(Id, "/api/Project/Read");
			return sItem;
		}

		public bool Update(String item)
		{
			return PostCreate(item, "/api/Project/Update");
		}

		public bool Delete(int Id)
		{
			return PostDelete(Id, "/api/Project/Delete");
		}
	}
}
