using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using RestSharp;
using System.Net;

namespace TAEssentials.UI.Extensions
{
    public class RestResponseAssertions : ReferenceTypeAssertions<RestResponse, RestResponseAssertions>
    {
        public RestResponseAssertions(RestResponse instance) : base(instance) {}

        protected override string Identifier => "restResponse";

        public AndConstraint<RestResponseAssertions> HaveStatusCode(HttpStatusCode expectedStatusCode, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .Given(() => Subject)
                .ForCondition(response => response.StatusCode.Equals(expectedStatusCode))
                .FailWith("Expected HttpStatusCode to be {0}{condition}. \nFound {1}. \nResponse URI: {2} \nMethod: {3} \nResponse Content: {4}",
                    _ => expectedStatusCode, 
                    restResponse =>  restResponse.StatusCode,
                    restResponse => restResponse.ResponseUri,
                    restResponse => restResponse.Request.Method,
                    restResponse => restResponse.Content);

            return new AndConstraint<RestResponseAssertions>(this);
        }
    }
}