using System.Collections.Generic;

namespace OrchardCoreModule.WebApi.Repository
{
	public interface ICmsRepository
	{
		IList<int> GetStuff();
	}
}