using Microsoft.EntityFrameworkCore;

namespace SampleLMS.Data
{
	public static class DataExtensions
	{
		public static void InitializeDb(this IServiceProvider serviceProvider)
		{
			using var scope = serviceProvider.CreateScope();
			var dbContext = scope.ServiceProvider.GetRequiredService<CourseDbContext>();
			dbContext.Database.Migrate();
		}
	}
}
