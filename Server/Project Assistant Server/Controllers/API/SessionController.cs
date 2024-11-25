using Microsoft.AspNetCore.Mvc;

namespace Project_Assistant_Server.Controllers.API
{
	public class SessionController : ControllerBase
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
