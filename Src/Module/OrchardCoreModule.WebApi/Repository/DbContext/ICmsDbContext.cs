using System.Collections.Generic;

namespace OrchardCoreModule.WebApi.Repository.DbContext
{
	internal interface ICmsDbContext
	{
		IList<int> GetStuff();
	}
}