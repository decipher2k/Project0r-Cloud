﻿using Newtonsoft.Json;
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
		public ItemPushDto PollItems()
		{
			String sItems = PostFetchProjectContextual("/api/ItemPush/PollItems","");
			if (sItems != "ERROR")
			{
				ItemPushDto itemPushDto = JsonConvert.DeserializeObject<ItemPushDto>(sItems);
				Globals.session = itemPushDto.session;
				return itemPushDto;
			}
			else
			{
				return null;
			}
		}	

		public bool AcceptItem(int iItemPushId, String project)
		{
			String sIdSessionDto = PostContextualIDPush(iItemPushId, -1, "/api/ItemPush/AcceptItem", project);
			
			return true;
		}

		public bool DenyItem(int iItemPushId, String project)
		{
			String sIdSessionDto = PostContextualIDPush(iItemPushId, -1, "/api/ItemPush/DenyItem", project);
			
			return true;
		}

		public bool AddItem(int iItemId, int receiverId, String project, int itemType)
		{
			String sIdSessionDto = PostContextualIDPush(iItemId, receiverId, "/api/ItemPush/PushItem", project, itemType);
			if (sIdSessionDto != "ERROR")
			{
				IdSessionDto idSessionDto = JsonConvert.DeserializeObject<IdSessionDto>(sIdSessionDto);
				Globals.session = idSessionDto.session;
				return true;
			}
			else
			{
				return false;
			}
			
		}
	}	
}
