using System;
using OrchardCoreModule.WebApi.Repository.DbContext;

namespace OrchardCoreModule.WebApi.Repository
{
	internal class CmsRepository : ICmsRepository
	{
		private readonly ICmsDbContext _dbContext;
		
		public CmsRepository(ICmsDbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
		}
	}
}