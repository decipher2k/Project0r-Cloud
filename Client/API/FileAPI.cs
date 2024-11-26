using Newtonsoft.Json;
using ProjectOrganizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Assistant.API
{
	public class FileAPI : APIBase
	{
		public bool Create(File item)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostCreate(sItem, "/api/File/Create");

		}

		public File Read(int Id)
		{
			String sItem = PostRead(Id, "/api/File/Read");
			return JsonConvert.DeserializeObject<File>(sItem);
		}

		public bool Update(ToDo item)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostCreate(sItem, "/api/File/Update");
		}

		public bool Delete(int Id)
		{
			return PostDelete(Id, "/api/File/Delete");
		}
	}
}
