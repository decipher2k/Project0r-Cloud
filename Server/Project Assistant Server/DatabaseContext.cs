
using Microsoft.EntityFrameworkCore;
using Project_Assistant_Server.Models;

namespace Project_Assistant_Server
{
	public class DatabaseContext:DbContext
	{	
			// Constructor that accepts DbContextOptions<EFCoreDbContext> as a parameter.
			// The options parameter contains the settings required by EF Core to configure the DbContext,
			// such as the connection string and provider.
			public DatabaseContext(DbContextOptions<DatabaseContext> options)
			: base(options) // The base(options) call passes the options to the base DbContext class constructor.
			{
			}

		public DbSet<Calendar> calendars { get; set; }
		public DbSet<Models.File> files { get; set; }
		public DbSet<Log> logs { get; set; }
		public DbSet<Note> notes { get; set; }
		public DbSet<Program> program { get; set; }
		public DbSet<ToDo> toDo { get; set; }
		public DbSet<User> users { get; set; }
		public DbSet<Project> projects { get; set; }
	}
}
