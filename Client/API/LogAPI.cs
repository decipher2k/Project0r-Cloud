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
		public bool Create(Log item)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostCreate(sItem, "/api/Log/Create");

		}

		public Log Read(int Id)
		{
			String sItem = PostRead(Id, "/api/Log/Log");
			return JsonConvert.DeserializeObject<Log>(sItem);
		}

		public bool Update(Log item)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostCreate(sItem, "/api/Log/Update");
		}

		public bool Delete(int Id)
		{
			return PostDelete(Id, "/api/Log/Delete");
		}
	}
}
