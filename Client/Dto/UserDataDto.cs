using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Assistant.Dto
{
	public class UserDataDto:SessionData
	{
		public List<UserData> Data { get; set; }
		public class UserData
		{
			public int Id { get; set; }
			public string FullName { get; set; }

			public override String ToString()
			{
				return FullName;
			}
		}
	}
}
