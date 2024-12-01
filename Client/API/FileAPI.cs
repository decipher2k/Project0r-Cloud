using Newtonsoft.Json;
using Project_Assistant.Dto;
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
		public long Create(File item, String project)
		{
			String sItem = JsonConvert.SerializeObject(item);
			IdSessionDto idSessionDto = PostCreate( sItem, "/api/File/Create", project); return idSessionDto.Id;

		}

		public File Read(long Id, String project)
		{
			String sItem = PostRead(Id, "/api/File/Read", project);
			Globals.session = JsonConvert.DeserializeObject<ItemDto>(sItem).session;
			return JsonConvert.DeserializeObject<File>(sItem);
		}

		public bool Update(File item, String project)
		{
			String sItem = JsonConvert.SerializeObject(item);
			return PostUpdate(sItem, "/api/File/Update", project);
		}

		public bool Delete(long Id, String project)
		{
			return PostDelete(Id, "/api/File/Delete", project);
		}
	}
}
