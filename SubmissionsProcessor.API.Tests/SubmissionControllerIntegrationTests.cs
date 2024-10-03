using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using MinimalApiPlayground.Tests;

namespace SubmissionsProcessor.API.Tests
{
    public class SubmissionControllerIntegrationTests
    {
        private const string _requestSubmissionIdKey = "_submissionIdKey";
        private const string _requestUserIdKey = "_userIdKey";
        
        //TODO: add more tests

        [Fact]
        public async Task TestMiddleware_Return_ShouldBe_200()
        {
            //TODO: REFACTOR
            //arrange
            var server = new APIApplication().Server;
            server.BaseAddress = new Uri("https://localhost:7242");

            //act
            var context = await server.SendAsync(c =>
            {
                c.Request.Method = HttpMethods.Post;
                c.Request.HttpContext.Items[_requestUserIdKey] = "11111";
                c.Request.HttpContext.Items[_requestSubmissionIdKey] = "123456789";
                c.Request.Path = "/Submission/GetContactId";

                c.Request.Form = new FormCollection(new Dictionary<string, StringValues> { { "ssn", "123456789" }, { "role", "owner" } });

            });
            //assert
            context.Response.StatusCode.Should().Be(200);

        }

    }
}
