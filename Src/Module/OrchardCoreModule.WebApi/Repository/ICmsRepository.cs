using System.Collections.Generic;

namespace OrchardCoreModule.WebApi.Repository
{
	internal interface ICmsRepository
	{
		IList<int> GetStuff();
	}
}