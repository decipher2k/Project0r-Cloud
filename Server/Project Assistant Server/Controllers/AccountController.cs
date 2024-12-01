using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using Project_Assistant_Server.Models;
using System.Security.Cryptography;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Project_Assistant_Server.Controllers
{
	[Route("/Account")]
	public class AccountController : Controller
	{
		private DatabaseContext context;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public AccountController(DatabaseContext _context, IHttpContextAccessor httpContextAccessor) 
		{ 
			context= _context;
			this._httpContextAccessor = httpContextAccessor;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet("Login")]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost("Login")]
		public IActionResult Login(User user)
		{
			if (context.users.Where(a => a.Name == user.Name).Any())
			{
				User userData = context.users.Where(a => a.Name == user.Name && a.IsAdmin==true).FirstOrDefault();
				String salt = userData.Salt;
				String passwordEnc=GenSha512(user.Password+salt);
				if (userData.Password == passwordEnc)
				{
					CookieOptions options = new CookieOptions();
					options.Expires = DateTime.Now.AddMinutes(10);

					String session = "";
					do
					{
						session = Session.RandomString(20);
					} while (context.users.Where(a => a.CurrentSession == session).Count() > 0);
			
					userData.CurrentSession = session;
					context.Update(userData);
					context.SaveChanges();

					_httpContextAccessor.HttpContext.Response.Cookies.Append("SESSION", session, options);
					return View("LoginRedirect");
				}
				else
				{
					return Unauthorized();
				}
			}
			else
			{
				return Unauthorized();
			}
		}

		private string GenSha512(string password)
		{
			using (SHA512 shaM = new SHA512Managed())
			{
				return Encoding.ASCII.GetString(shaM.ComputeHash(Encoding.ASCII.GetBytes(password)));
			}
		}
	}
}
