namespace Project_Assistant_Server.Dto
{
	public class UserDataDto : SessionData
	{
		public List<UserData> Data { get; set; }
		public class UserData
		{
			public long Id { get; set; }
			public string FullName { get; set; }
		}
	}
}
