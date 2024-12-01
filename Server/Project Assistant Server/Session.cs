using Project_Assistant_Server.Models;

namespace Project_Assistant_Server
{
	public class Session
	{
		private DatabaseContext context;
		public Session(DatabaseContext _context) 
		{
			context = _context;
		}
		public String newSession(String session, bool isAdmin=false)
		{
			if (context.users.Where(a => a.CurrentSession == session && a.IsAdmin==isAdmin).Count() > 0)
			{
				User user = context.users.Where(a => a.CurrentSession == session && a.IsAdmin==false).First();
				String newSession = "";

				do {
					newSession = RandomString(20);
				}while(context.users.Where(a=>a.CurrentSession == newSession).Count() > 0);

				user.CurrentSession = newSession;
				context.users.Update(user);
				context.SaveChanges();
				return newSession;
			}
			else
			{
				throw new Exception();
			}
		}		

		public static string RandomString(int length)
		{
			Random random = new Random();
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}
	}
}
