using System.Web;

namespace Paragon.Analytics.Extensions
{
	public static class ExtensionsToHttpResponse
	{
		
        public static bool IsGTMTrackingEnabled(this HttpResponse response)
        {
            var s = response.Headers["GTMTracker-Enabled"];
           
            if (string.IsNullOrWhiteSpace(s))
                return false;

            bool result;
            bool.TryParse(s, out result);

            return result;
        }
    }
}