using System;
namespace Project_Assistant.Dto
{
	public class CreateUserDto : SessionData
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
	}
}
