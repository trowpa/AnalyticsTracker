using NUnit.Framework;
using Paragon.Analytics.Extensions;

namespace AnalyticsTracker.Tests
{
	[TestFixture]
	public class StringExtensionsTester
	{
		[Test]
		public void Test()
		{
			var safeSubstring = "Mikael".SafeSubstring(3, 10);

			Assert.That(safeSubstring, Is.EqualTo("ael"));
		}
	}
}