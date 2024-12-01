using Microsoft.AspNetCore.Mvc;
using Project_Assistant_Server.Models;
using System.Security.Cryptography;
using System.Text;

namespace Project_Assistant_Server.Controllers.API
{
	[Route("/api/Install")]
	public class InstallController : ControllerBase
	{
		DatabaseContext DatabaseContext { get; set; }
		public InstallController(DatabaseContext _context)
		{
			DatabaseContext = _context;
		}

		public IActionResult Index()
		{
			
			User user = new User();
			user.Email = "admin@example.com";
			user.Salt = "12345";
			user.Password = GenSha512("change12345");
			user.Name = "admin";
			user.Fullname = "Administrator";
			user.CurrentSession = "";
			user.IsAdmin= true;
			DatabaseContext.users.Add(user);
			DatabaseContext.SaveChanges();
			return Ok("Done");

		}

		private String GenSha512(System.String password)
		{
			using (SHA512 shaM = new SHA512Managed())
			{
				return Encoding.ASCII.GetString(shaM.ComputeHash(Encoding.ASCII.GetBytes(password)));
			}
		}
	}
}
