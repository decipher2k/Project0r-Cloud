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
		public bool Create(File item, String project)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostCreate(sItem, "/api/File/Create", project);

		}

		public File Read(int Id, String project)
		{
			String sItem = PostRead(Id, "/api/File/Read", project);
			return JsonConvert.DeserializeObject<File>(sItem);
		}

		public bool Update(ToDo item, String project)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostUpdate(sItem, "/api/File/Update", project);
		}

		public bool Delete(int Id, String project)
		{
			return PostDelete(Id, "/api/File/Delete", project);
		}
	}
}
