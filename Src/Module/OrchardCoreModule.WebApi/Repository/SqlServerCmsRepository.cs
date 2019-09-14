using System.Collections.Generic;
using OrchardCoreModule.WebApi.Repository.DbContext;

namespace OrchardCoreModule.WebApi.Repository
{
	internal class SqlServerCmsRepository : ICmsRepository
	{
		public IList<int> GetStuff()
		{
			using (var dbContext = new SqlServerCmsDbContext())
			{
				return dbContext.GetStuff();	
			}
		}
	}
}