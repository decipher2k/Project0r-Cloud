using Project_Assistant_Server.Models;

namespace Project_Assistant_Server
{
	public class Session
	{
		public String verifySession(String session, DatabaseContext context)
		{
			if (context.users.Where(a => a.CurrentSession == session).Count() > 0)
			{
				User user = context.users.Where(a => a.CurrentSession == session).First();
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
