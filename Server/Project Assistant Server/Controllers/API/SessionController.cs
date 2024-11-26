﻿using Microsoft.AspNetCore.Mvc;
using Project_Assistant_Server.Dto;
using Project_Assistant_Server.Models;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace Project_Assistant_Server.Controllers.API
{
	public class SessionController : ControllerBase
	{

		private DatabaseContext context;

		public SessionController(DatabaseContext _context)
		{
			context = _context;
		}

		//Change to Post if accessed from web
		[HttpGet(Name ="/Login/{Username}/{Password")]
		public IActionResult Login(String Username, String Password)
		{
			if(context.users.Where(a=>a.Name==Username).Any())
			{
				User user = context.users.Where(a => a.Name == Username).First();
				String salt = user.Salt;
				if (context.users.Where(a => a.Name == Username && a.Password == GenSha512(Password + salt)).Any())
				{
					SessionData sessionData = new SessionData();
					sessionData.session = Session.RandomString(20);

					user.CurrentSession	= sessionData.session;
					context.users.Update(user);
					context.SaveChanges();

					return Ok(sessionData);
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

		//Change to Post if accessed from web
		[HttpPost(Name = "/Register")]
		public IActionResult RegisterUser(IFormCollection collection)
		{
			if (!isValidInput(collection["name"]) || !isValidInput(collection["fullname"]) || !isValidInput(collection["email"]))
					return BadRequest();

			if (context.users.Where(a => a.CurrentSession == collection["session"]).Any())
			{
				User user = new User();
				user.Salt=Session.RandomString(20);
				user.Name = collection["name"];
				user.Fullname = collection["fullname"];
				user.Email = collection["email"];
				user.Password = GenSha512(collection["password"] + user.Salt);

				context.users.Add(user);
				context.SaveChanges();

				String newSession = new Session(context).newSession(collection["session"]);
				SessionData sessionData = new SessionData();
				sessionData.session = newSession;
				return Ok(sessionData);
			}
			else
			{
				return Unauthorized();
			}
		}

		private bool isValidInput(String value)
		{
			value = value.ToLower();
			String chars = "abcdefgijklmnopqrstuvwxyz1234567890@.- ";
			foreach (char c in value)
			{
				if (!chars.ToCharArray().Contains(c))
					return false;
			}
			return true;
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
