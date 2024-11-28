﻿using Project_Assistant_Server.Models;
using System.ComponentModel.Design.Serialization;

namespace Project_Assistant_Server.Dto
{
	[Serializable]
	public class ItemPushDto:SessionData
	{
		public List<ItemPush> Items { get; set; } = new List<ItemPush>();
	}
}
