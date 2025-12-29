using RestSharp;

namespace TAEssentials.UI.Extensions
{
    public static class RestResponseExtensions
    {
        public static RestResponseAssertions Should(this RestResponse instance)
        {
            return new RestResponseAssertions(instance);
        }
    }
}