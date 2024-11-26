using Newtonsoft.Json;
using ProjectOrganizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Assistant.API
{
	public class LogAPI : APIBase
	{
		public long Create(Log item, String project)
		{
			String sItem = JsonConvert.SerializeObject(item);
			IdSessionDto idSessionDto = PostCreate( sItem, "/api/Log/Create", project); return idSessionDto.Id;

		}

		public Log Read(int Id, String project)
		{
			String sItem = PostRead(Id, "/api/Log/Read", project	);
			return JsonConvert.DeserializeObject<Log>(sItem);
		}

		public bool Update(Log item, String project)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostUpdate(sItem, "/api/Log/Update", project);
		}

		public bool Delete(int Id, String project)
		{
			return PostDelete(Id, "/api/Log/Delete", project);
		}
	}
}
