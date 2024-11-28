using Newtonsoft.Json;
using Project_Assistant.Dto;
using Project_Assistant_Server.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Project_Assistant.API
{
	public class ItemPushAPI:APIBase
	{
		public ItemPushDto PullItems()
		{
			String sItems = PostFetchAll("/api/ItemPush/PollItems");
			return JsonConvert.DeserializeObject<ItemPushDto>(sItems);
		}	

		public bool AcceptItem(int iItemPushId, String project)
		{
			String sIdSessionDto = PostContextualIDPush(iItemPushId, -1, "/api/ItemPush/AcceptItem", project);
			IdSessionDto idSessionDto = JsonConvert.DeserializeObject<IdSessionDto>(sIdSessionDto);
			Globals.session=idSessionDto.session;
			return true;
		}

		public bool DenyItem(int iItemPushId, String project)
		{
			String sIdSessionDto = PostContextualIDPush(iItemPushId, -1, "/api/ItemPush/DenyItem", project);
			IdSessionDto idSessionDto = JsonConvert.DeserializeObject<IdSessionDto>(sIdSessionDto);
			Globals.session = idSessionDto.session;
			return true;
		}

		public bool AddItem(int iItemPushId, int contextId, String project, int itemType)
		{
			String sIdSessionDto = PostContextualIDPush(iItemPushId, contextId, "/api/ItemPush/PushItem", project, itemType);
			IdSessionDto idSessionDto = JsonConvert.DeserializeObject<IdSessionDto>(sIdSessionDto);
			Globals.session = idSessionDto.session;
			return true;
		}
	}	
}
