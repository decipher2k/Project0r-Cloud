using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project_Assistant_Server.Controllers.API;
using Project_Assistant_Server.Models;
using static Project_Assistant_Server.Dto.UserDataDto;

namespace Project_Assistant_Server.Controllers
{
	public class UsersController : Controller
	{
		private readonly DatabaseContext _context;

		private readonly IHttpContextAccessor _httpContextAccessor;
	
		public UsersController(DatabaseContext context, IHttpContextAccessor httpContextAccessor)
		{
			_context = context;
			this._httpContextAccessor = httpContextAccessor;
		}

		private bool Authorize()
		{
			String session = _httpContextAccessor.HttpContext.Request.Cookies["SESSION"];
			if (session != null)
			{
				if (_context.users.Where(a => a.CurrentSession == session).Any())
				{
					User user = _context.users.Where(a => a.CurrentSession == session).FirstOrDefault();
					user.CurrentSession=new Session(_context).newSession(session);

					CookieOptions options = new CookieOptions();
					options.Expires = DateTime.Now.AddMinutes(10);			
					options.Secure = true;
					options.HttpOnly = true;
					options.Path = "/Users";

					_httpContextAccessor.HttpContext.Response.Cookies.Append("SESSION", user.CurrentSession, options);

					_context.Update(user);
					_context.SaveChanges();
					return true;
				}
				else
				{ 

					return false;
				}
			}
			else
			{
				return false;
			}

		}

		// GET: Users
		public async Task<IActionResult> Index()
		{
			if (Authorize())
			{
				return View(await _context.users.ToListAsync());
			}
			else
			{
				return Redirect("/Account/Login");
			}
		}

		// GET: Users/Details/5
		public async Task<IActionResult> Details(long? id)
		{
			if (Authorize())
			{
				if (id == null)
				{
					return NotFound();
				}

				var user = await _context.users
					.FirstOrDefaultAsync(m => m.Id == id);
				if (user == null)
				{
					return NotFound();
				}

				return View(user);
			}
			else
			{
				return Redirect("/Account/Login");
			}
		}

		// GET: Users/Create
		public IActionResult Create()
		{
			if (Authorize())
			{
				return View();
			}
			else
			{
				return Redirect("/Account/Login");
			}
		}

		// POST: Users/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Name,Fullname,Email,Password,IsAdmin")] User user)
		{
			if (Authorize())
			{
				if (user.Name != "" && user.Fullname != "" && user.Password != "" && user.Email != ""
					&& user.Name != null && user.Fullname != null && user.Password != null && user.Email != null)
				{
					user.Salt = "";
					user.CurrentSession = "";
					user.Projects = new List<Project>();

					user.Salt = Session.RandomString(20);
					user.Password = GenSha512(user.Password + user.Salt);
					_context.Add(user);
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Index));
				}
				return View(user);
			}
			else
			{
				return Redirect("/Account/Login");
			}
		}

		// GET: Users/Edit/5
		public async Task<IActionResult> Edit(long? id)
		{
			if (Authorize())
			{
				if (id == null)
				{
					return NotFound();
				}

				var user = await _context.users.FindAsync(id);
				if (user == null)
				{
					return NotFound();
				}
				return View(user);
			}
			else
			{
				return Redirect("/Account/Login");
			}
		}

		// POST: Users/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Fullname,Email,Password,Salt,CurrentSession,IsAdmin")] User user)
		{
			if (Authorize())
			{
				if (id != user.Id)
				{
					return NotFound();
				}

				if (ModelState.IsValid)
				{
					try
					{
						_context.Update(user);
						await _context.SaveChangesAsync();
					}
					catch (DbUpdateConcurrencyException)
					{
						if (!UserExists(user.Id))
						{
							return NotFound();
						}
						else
						{
							throw;
						}
					}
					return RedirectToAction(nameof(Index));
				}
				return View(user);
			}
			else
			{
				return Redirect("/Account/Login");
			}
		}

		// GET: Users/Delete/5
		public async Task<IActionResult> Delete(long? id)
		{
			if (Authorize())
			{
				if (id == null)
				{
					return NotFound();
				}

				var user = await _context.users
					.FirstOrDefaultAsync(m => m.Id == id);
				if (user == null)
				{
					return NotFound();
				}

				return View(user);
			}
			else
			{
				return Redirect("/Account/Login");
			}
		}

		// POST: Users/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(long id)
		{
			if (Authorize())
			{
				var user = await _context.users.FindAsync(id);
				if (user != null)
				{
					_context.users.Remove(user);
				}

				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			else
			{
				return Redirect("/Account/Login");
			}
		}

		private bool UserExists(long id)
		{
			if(Authorize())
			{ 
				return _context.users.Any(e => e.Id == id);
			}
			else
			{
				return false;
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
