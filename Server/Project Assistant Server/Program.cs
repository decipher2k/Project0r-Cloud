using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Pomelo.EntityFrameworkCore.MySql;
using Project_Assistant_Server.Models;
using static Project_Assistant_Server.Program.MessageHub;

namespace Project_Assistant_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //var connectionstring = "Connection string";
            var connectionstring = System.IO.File.ReadAllText(".\\db.conf");


			builder.Services.AddDbContext<DatabaseContext>(options =>
				options.UseMySql(connectionstring,ServerVersion.AutoDetect(connectionstring)));
			builder.Services.AddHttpContextAccessor();

         

			builder.Services.Configure<CookiePolicyOptions>(options =>
			{
				options.CheckConsentNeeded = context => false; // consent required
				options.MinimumSameSitePolicy = SameSiteMode.Strict;
			});

			builder.Services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(10);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});

			builder.Services.AddSignalR();

			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

			app.UseSession();

			app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapHub<MessageHub>("/messagehub");
			});

			
			app.Run();
        }

		public class MessageHub : Hub<ITypedHubClient>
		{
			DatabaseContext databaseContext;
			public MessageHub(DatabaseContext _context)
			{
				databaseContext = _context;
			}

			public override Task OnConnectedAsync()
			{
				return base.OnConnectedAsync();
			}

			private static Dictionary <string,Tuple<string,string>> users = new Dictionary<string,Tuple<string,string>>();
			public interface ITypedHubClient
			{
				Task ReceiveMessage(string name, string message);
			}
			public async Task SendMessage(string session, string project, string message)
			{
				await Task.Run(() =>
				{
					if (databaseContext.users.Where(a => a.CurrentSession == session).Include(a => a.Projects).First().Projects.Where(a => a.Name == project).Any())
					{
						String username = databaseContext.users.Where(a => a.CurrentSession == session).First().Name;
						foreach (var user in databaseContext.users.Include(a => a.Projects).ToList())
						{
							if (user.Projects.Where(a => a.Name == project).Any())
							{
								if (users.ContainsKey(user.Name))
								{
									Clients.Clients(users[user.Name].Item1).ReceiveMessage(username, message);
								}
							}
						}
					}
				});
			}


			public async Task SendUser(string session, string connectionId, string project)
			{
				await Task.Run(() =>
				{
					if (databaseContext.users.Where(a => a.CurrentSession == session).Include(a => a.Projects).First().Projects.Where(a => a.Name == project).Any())
					{
						String username = databaseContext.users.Where(a => a.CurrentSession == session).First().Name;
						Tuple<string, string> userdata = new Tuple<string, string>(connectionId, project);
						if (users.ContainsKey(username))
						{
							users[username] = userdata;
						}
						else
						{
							users.Add(username, userdata);
						}
					}
				});
			}
		}
	}
}
