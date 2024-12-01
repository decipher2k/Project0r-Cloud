using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

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
            var connectionstring = File.ReadAllText(".\\db.conf");


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

            app.Run();
        }
    }
}
