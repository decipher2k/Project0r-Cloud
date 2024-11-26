using Newtonsoft.Json;
using ProjectOrganizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Assistant.API
{
	public class ProgramAPI : APIBase
	{
		public bool Create(Program item)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostCreate(sItem, "/api/Program/Create");

		}

		public Program Read(int Id)
		{
			String sItem = PostRead(Id, "/api/Program/Read");
			return JsonConvert.DeserializeObject<Program>(sItem);
		}

		public bool Update(Program item)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostCreate(sItem, "/api/Program/Update");
		}

		public bool Delete(int Id)
		{
			return PostDelete(Id, "/api/Program/Delete");
		}
	}
}
