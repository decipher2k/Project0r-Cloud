using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project_Assistant_Server.Dto;
using Project_Assistant_Server.Models;
using System.Linq;

namespace Project_Assistant_Server.Controllers.API
{
	[Route("/api/Project")]
	public class ProjectController : ControllerBase
	{
		DatabaseContext context;
		public ProjectController(DatabaseContext _context)
		{
			context = _context;
		}

		// GET: ProjectController
		[HttpGet("GetAll/{session}")]
		public ActionResult Index(String session)
		{
			UserDto userDto = new UserDto();
			
			if (context.users.Where(a => a.CurrentSession == session && a.IsAdmin==false).Any())
			{
				//if(context.users.Where(a => a.CurrentSession == session && a.IsAdmin==false).First().Projects.Any())
				User user = context.users.Where(a => a.CurrentSession == session && a.IsAdmin==false)
					.Include(a => a.Projects.Where(a=>a.IsInvited==false)).ThenInclude(a => a.Calendars)
					.Include(a => a.Projects.Where(a => a.IsInvited == false)).ThenInclude(a => a.Files)
					.Include(a => a.Projects.Where(a => a.IsInvited == false)).ThenInclude(a => a.Logs)
					.Include(a => a.Projects.Where(a => a.IsInvited == false)).ThenInclude(a => a.Notes)
					.Include(a => a.Projects.Where(a => a.IsInvited == false)).ThenInclude(a => a.Programs)
					.Include(a => a.Projects.Where(a => a.IsInvited == false)).ThenInclude(a => a.ToDo).First();
				String newSession = new Session(context).newSession(session);
				userDto.session = newSession;
				userDto.projects = new List<Project>();
				foreach(Project p in user.Projects)
					userDto.projects.Add(p);

				return Ok(userDto);
			}
			else
			{
				return Unauthorized();
			}
		}

		// POST: ProjectController/Create
		[HttpPost("FetchUsers")]
		public IActionResult FetchUsers(IFormCollection collection)
		{
			UserDto userDto = new UserDto();

			if (context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).Count() > 0)
			{

				String sProjectName = collection["project"];

				if (context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString().ToString()).Include(a => a.Projects.Where(a => a.IsInvited == false).Where(a=>a.Name==sProjectName)).Any())
				{
					List<UserDataDto.UserData> userDatas = new List<UserDataDto.UserData>();
					foreach (User projectUser in context.users.Where(a => a.Projects.Where(a => a.Name == sProjectName).Any()).ToList())
					{
						userDatas.Add(new UserDataDto.UserData() { FullName = projectUser.Fullname, Id=projectUser.Id });
					}

					String newSession = new Session(context).newSession(collection["session"].ToString().ToString());
					UserDataDto userDataDto = new UserDataDto();
					userDataDto.Data = userDatas;
					userDataDto.session= newSession;

					return Ok(userDataDto);
				}
				else
				{
					return BadRequest();
				}
			}
			else
			{
				return Unauthorized();
			}
		}

		// POST: ProjectController/Create
		[HttpPost("FetchProjectUsers")]
		public IActionResult FetchProjectUsers(IFormCollection collection)
		{
			UserDto userDto = new UserDto();

			if (context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).Count() > 0)
			{

				

			
				{
					List<UserDataDto.UserData> userDatas = new List<UserDataDto.UserData>();
					foreach (User projectUser in context.users.ToList())
					{
						userDatas.Add(new UserDataDto.UserData() { FullName = projectUser.Fullname, Id = projectUser.Id });
					}

					String newSession = new Session(context).newSession(collection["session"].ToString().ToString());
					UserDataDto userDataDto = new UserDataDto();
					userDataDto.Data = userDatas;
					userDataDto.session = newSession;

					return Ok(userDataDto);
				}
				
			}
			else
			{
				return Unauthorized();
			}
		}


		// POST: ProjectController/Create
		[HttpPost("Create")]
		public IActionResult Create(IFormCollection collection)
		{
			UserDto userDto = new UserDto();

			if (context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).Count() > 0)
			{
				
				String sProjectName = collection["ItemData"];

				if (!context.projects.Where(a => a.Name == sProjectName).Any())
				{
					Project project = new Project();
					project.Name = sProjectName;
					project.IsOwner= true;
					
					User user = context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString().ToString()).Include(a => a.Projects).First();
					user.Projects.Add(project);
					context.Update(user);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"].ToString().ToString());
					SessionData sessionData = new SessionData();
					sessionData.session = newSession;
					return Ok(sessionData);
				}
				else
				{
					return BadRequest();
				}				
			}
			else
			{
				return Unauthorized();
			}
		}

		// POST: ProjectController/Create
		[HttpPost("Invite")]
		public IActionResult Invite(IFormCollection collection)
		{
			UserDto userDto = new UserDto();

			if (context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).Count() > 0)
			{

				String sProjectName = collection["project"];
				int idUserToInvite = int.Parse(collection["ItemData"].ToString());

				if (context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString().ToString()).Include(a => a.Projects).First().Projects.Where(a => a.Name == sProjectName && a.IsOwner).Any())
				{
					Project project = new Project();
					project.Name = sProjectName;
					project.IsInvited = true;

					User user = context.users.Where(a => a.Id == idUserToInvite).Include(a => a.Projects).First();
					user.Projects.Add(project);
					context.Update(user);

					ItemPush itemPush = new ItemPush();
					itemPush.Project = sProjectName;
					itemPush.SenderName = context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString().ToString()).First().Fullname;
					itemPush.Title = sProjectName;
					itemPush.ItemId = -1;
					itemPush.ReceiverId = idUserToInvite;					
					itemPush.Type=ItemPush.ItemType.Project;

					context.itemPush.Add(itemPush);

					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"].ToString().ToString());
					ItemDto projectData = new ItemDto();
					projectData.session = newSession;
					projectData.item = JsonConvert.SerializeObject(project);
					return Ok(projectData);
				}
				else
				{
					return BadRequest();
				}
			}
			else
			{
				return Unauthorized();
			}
		}

		// POST: ProjectController/Edit/5
		[HttpPost("AcceptDenyInvite")]
		public ActionResult AcceptDenyInvite(IFormCollection collection)
		{
			UserDto userDto = new UserDto();

			if (context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).Count() > 0)
			{
				String sProject = collection["project"];
				String sProjectData = collection["ItemData"];
				String session = collection["session"];
				

				if (!context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).First().Projects.Where(a=>a.Name==sProject && a.IsInvited==false).Any())
				{
					User receiver = context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).First();
					Project p = context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).First().Projects.Where(a => a.Name == sProject && a.IsInvited == true).First();
					if (bool.Parse(sProjectData) == true)
					{
						p.IsInvited = false;
						context.projects.Update(p);						
					}
					else
					{
						context.Remove(p);
					}
					ItemPush itemPush = context.itemPush.Where(a => a.Project == sProject && a.ReceiverId == receiver.Id).FirstOrDefault();
					context.Remove(itemPush);


					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"].ToString().ToString());
					SessionData sessionData = new SessionData();
					sessionData.session = newSession;
					return Ok(sessionData);
				}
				else
				{
					return BadRequest();
				}
			}
			else
			{
				return Unauthorized();
			}
		}

		// POST: ProjectController/Edit/5
		[HttpPost("Edit")]
		public ActionResult Edit(IFormCollection collection)
		{
			UserDto userDto = new UserDto();

			if (context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).Count() > 0)
			{
				String sProjectData = collection["project"];
				String newProjectName = collection["ItemName"];

				if (!context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).First().Projects.Where(a => a.Name == newProjectName && a.IsOwner).Any())
				{
					Project p = context.projects.Where(a=> a.Name== sProjectData).First();
					p.Name = newProjectName;
					context.projects.Update(p);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"].ToString().ToString());
					SessionData sessionData = new SessionData();
					sessionData.session = newSession;
					return Ok(sessionData);
				}
				else
				{ 
					return BadRequest(); 
				}
			}
			else
			{
				return Unauthorized();
			}
		}		

		// POST: ProjectController/Delete/5
		[HttpPost("Delete")]
		public ActionResult Delete(IFormCollection collection)
		{
			UserDto userDto = new UserDto();

			if (context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).Count() > 0)
			{				
				
				String sProjectName = collection["project"];
				String session = collection["session"].ToString();

				if (context.users.Where(a => a.IsAdmin==false && a.CurrentSession == collection["session"].ToString()).Include(a => a.Projects).First().Projects.Where(a => a.Name==sProjectName).Any()) 
				{
					Project project = context.users.Where(a=>a.CurrentSession==session).First().Projects.Where(a => a.Name==sProjectName).First();
					context.projects.Remove(project);
					context.SaveChanges();

					String newSession = new Session(context).newSession(collection["session"].ToString());
					SessionData sessionData = new SessionData();
					sessionData.session = newSession;
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
	}
}
