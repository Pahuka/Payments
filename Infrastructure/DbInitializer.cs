using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DbInitializer
{
	public static void Initialize(AppDbContext appDbContext)
	{
		appDbContext.Database.EnsureCreated();
	}
}