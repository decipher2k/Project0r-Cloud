﻿using Newtonsoft.Json;
using Project_Assistant.Dto;
using Project_Assistant_Server.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Project_Assistant.API
{
	public class APIBase
	{
		protected IdSessionDto PostCreate(String item, String APIEndpoint, String project="")
		{ 			
			HttpClient client = new HttpClient();
			Dictionary<string, string> values = new Dictionary<string, string>()
			{
				{ "session",Globals.session },
				{ "ItemData", item },
				{ "project", project }
			};

			FormUrlEncodedContent content = new FormUrlEncodedContent(values);

			var response = client.PostAsync(Globals.ServerAddress+APIEndpoint, content).Result;
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var responseString = response.Content.ReadAsStringAsync().Result;
				if(responseString!="")
					Globals.session = JsonConvert.DeserializeObject<IdSessionDto>(responseString).session;
				return JsonConvert.DeserializeObject<IdSessionDto>(responseString);
			}
			else
			{ 
				return null;
			}
		}

		protected String PostFetchAll(String APIEndpoint)
		{
			HttpClient client = new HttpClient();
			Dictionary<string, string> values = new Dictionary<string, string>()
			{
				{ "session",Globals.session }
			};

			FormUrlEncodedContent content = new FormUrlEncodedContent(values);

			var response = client.GetAsync(Globals.ServerAddress + APIEndpoint).Result;
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var responseString = response.Content.ReadAsStringAsync().Result;
				return responseString;
			}
			else
			{
				return "ERROR";
			}
		}

		protected String PostFetchAllUsers(String APIEndpoint)
		{
			HttpClient client = new HttpClient();
			Dictionary<string, string> values = new Dictionary<string, string>()
			{
				{ "session",Globals.session }

			};

			FormUrlEncodedContent content = new FormUrlEncodedContent(values);

			var response = client.PostAsync(Globals.ServerAddress + APIEndpoint,content).Result;
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var responseString = response.Content.ReadAsStringAsync().Result;
				return responseString;
			}
			else
			{
				return "ERROR";
			}
		}

		protected String PostFetchProjectContextual(String APIEndpoint, String project)
		{
			HttpClient client = new HttpClient();
			Dictionary<string, string> values = new Dictionary<string, string>()
			{
				{ "session",Globals.session },
				{ "project",project }

			};

			FormUrlEncodedContent content = new FormUrlEncodedContent(values);

			var response = client.PostAsync(Globals.ServerAddress + APIEndpoint, content).Result;
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var responseString = response.Content.ReadAsStringAsync().Result;
				return responseString;
			}
			else
			{
				return "ERROR";
			}
		}


		protected String PostRead(long itemId, String APIEndpoint, String project)
		{
			HttpClient client = new HttpClient();
			Dictionary<string, string> values = new Dictionary<string, string>()
			{
				{ "session",Globals.session },
				{ "project",project },
				{ "ItemId", itemId.ToString() },
			};

			FormUrlEncodedContent content = new FormUrlEncodedContent(values);

			var response = client.PostAsync(Globals.ServerAddress + APIEndpoint, content).Result;
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var responseString = response.Content.ReadAsStringAsync().Result;
				Globals.session=JsonConvert.DeserializeObject<ItemDto>(responseString).session;
				return responseString;
			}
			else
			{
				return "ERROR";
			}
		}

		protected String PostRead(string itemName, String APIEndpoint, String project)
		{
			HttpClient client = new HttpClient();
			Dictionary<string, string> values = new Dictionary<string, string>()
			{
				{ "session",Globals.session },
				{ "project",project },
				{ "ItemName", itemName.ToString() },
			};

			FormUrlEncodedContent content = new FormUrlEncodedContent(values);

			var response = client.PostAsync(Globals.ServerAddress + APIEndpoint, content).Result;
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var responseString = response.Content.ReadAsStringAsync().Result;
				return responseString;
			}
			else
			{
				return "ERROR";
			}
		}

		protected bool PostUpdate(String item, String APIEndpoint, String project)
		{
			HttpClient client = new HttpClient();
			Dictionary<string, string> values = new Dictionary<string, string>()
			{
				{ "session",Globals.session },
				{ "ItemData", item },
				{ "project", project },
			 };

			FormUrlEncodedContent content = new FormUrlEncodedContent(values);

			var response = client.PostAsync(Globals.ServerAddress + APIEndpoint, content).Result;
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var responseString = response.Content.ReadAsStringAsync().Result;
				Globals.session = JsonConvert.DeserializeObject<SessionData>(responseString).session;
				return true;
			}
			else
			{
				return false;
			}
		}

		protected bool PostDelete(long itemId, String APIEndpoint, String project)
		{
			HttpClient client = new HttpClient();
			Dictionary<string, string> values = new Dictionary<string, string>()
			{
				  { "session",Globals.session },
				  { "ItemData", itemId.ToString() },
				  { "project",project }
			 };

			FormUrlEncodedContent content = new FormUrlEncodedContent(values);

			var response = client.PostAsync(Globals.ServerAddress + APIEndpoint, content).Result;
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var responseString = response.Content.ReadAsStringAsync().Result;
				Globals.session = JsonConvert.DeserializeObject<SessionData>(responseString).session;
				return true;
			}
			else
			{
				return false;
			}
		}

		protected bool PostDelete(String itemName, String APIEndpoint, String project)
		{
			HttpClient client = new HttpClient();
			Dictionary<string, string> values = new Dictionary<string, string>()
			{
				  { "session",Globals.session },
				  { "ItemData", itemName },
				  { "project",project }
			 };

			FormUrlEncodedContent content = new FormUrlEncodedContent(values);

			var response = client.PostAsync(Globals.ServerAddress + APIEndpoint, content).Result;
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var responseString = response.Content.ReadAsStringAsync().Result;
				Globals.session = JsonConvert.DeserializeObject<SessionData>(responseString).session;
				return true;
			}
			else
			{
				return false;
			}
		}

		protected String PostContextualIDPush(long itemId, int contextId,String APIEndpoint, String project, int itemType=-1)
		{
			HttpClient client = new HttpClient();
			Dictionary<string, string> values = new Dictionary<string, string>()
			{
				  { "session",Globals.session },
				  { "itemId", itemId.ToString() },
				  { "itemType", itemType.ToString() },
				  { "contextId", contextId.ToString() },
				  { "project",project }
			 };

			FormUrlEncodedContent content = new FormUrlEncodedContent(values);

			var response = client.PostAsync(Globals.ServerAddress + APIEndpoint, content).Result;
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var responseString = response.Content.ReadAsStringAsync().Result;
				Globals.session = JsonConvert.DeserializeObject<SessionData>(responseString).session;
				return responseString;
			}
			else
			{
				return "ERROR";
			}
		}

		protected bool PostProject(String APIEndpoint, String project)
		{
			HttpClient client = new HttpClient();
			Dictionary<string, string> values = new Dictionary<string, string>()
			{
				  { "session",Globals.session },
				  { "project",project }
			 };

			FormUrlEncodedContent content = new FormUrlEncodedContent(values);

			var response = client.PostAsync(Globals.ServerAddress + APIEndpoint, content).Result;
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				var responseString = response.Content.ReadAsStringAsync().Result;
				Globals.session = JsonConvert.DeserializeObject<SessionData>(responseString).session;
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
