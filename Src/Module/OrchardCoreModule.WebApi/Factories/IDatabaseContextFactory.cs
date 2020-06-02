using OrchardCoreModule.WebApi.Repository.DbContext;

namespace OrchardCoreModule.WebApi.Factories
{
	internal interface IDatabaseContextFactory
	{
		ICmsDbContext CreateDatabaseContext();
	}
}