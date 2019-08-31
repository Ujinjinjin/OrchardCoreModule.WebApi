using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	[TestClass]
	public class DbConnectionTests
	{
		[TestInitialize]
		public void Initialize()
		{
			
		}
		
		[TestMethod]
		public void DbConnectionTest()
		{
			Assert.AreEqual(1, 1);
		}
	}
}