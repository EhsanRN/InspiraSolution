using Amazon.Runtime.Internal.Transform;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using SubmissionsProcessor.API.Models;
using SubmissionsProcessor.API.Repositories;
using SubmissionsProcessor.API.Services.MongoDB;
using SubmissionsProcessor.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Driver;
using Microsoft.AspNetCore.Builder;
using SubmissionsProcessor.API.Middlewares;
using SubmissionsProcessor.API.Controllers;

namespace SubmissionsProcessor.API.Tests
{
    public class SubmissionControllerIntegrationTests
    {
        private const string _requestSubmissionIdKey = "_submissionIdKey";
        private const string _requestUserIdKey = "_userIdKey";
        
        //TODO: add more tests

        [Fact]
        public async Task TestMiddleware_Return_ShouldBe_Unauthorized()
        {
            //TODO: REFACTOR
            //arrange
            using var host = await new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .ConfigureServices(services =>
                        {
                            services.AddMvc();
                            var mongoClient = new MongoClient("mongodb://localhost:27017");
                            var mongoDatabase = mongoClient.GetDatabase("Avoka");
                            services.AddScoped(provider => mongoDatabase);                            
                            services.AddScoped<ISubmissionPropertiesService, SubmissionPropertiesService>();
                            services.AddScoped<ISubmissionContext, SubmissionContext>();
                            services.AddScoped<ISubmissionRepository, SubmissionRepository>();
                            services.AddScoped<ISSNInternalCheckMockService, SSNInternalCheckMockService>();
                            services.AddControllers();

                            services.AddEndpointsApiExplorer();


                        })
                        .Configure(app =>
                        {
                            //app.UseHttpsRedirection();

                            //app.UseAuthorization();

                            //TODO: add UseAuthentication
                            app.UseWhen(context => context.Request.Path.StartsWithSegments($"/{typeof(SubmissionController).Name}", StringComparison.OrdinalIgnoreCase), appBuilder =>
                            {
                                app.UseMiddleware<SubmissionMiddleware>();

                            });
                        });
                })
                .StartAsync();

            var server = host.GetTestServer();
            server.BaseAddress = new Uri("https://localhost:7035/Submission/GetContactId");

            //act
            var context = await server.SendAsync(c =>
            {
                c.Request.Method = HttpMethods.Post;
                c.Request.HttpContext.Items[_requestUserIdKey] = null;
                c.Request.HttpContext.Items[_requestSubmissionIdKey] = "234423";
                c.Request.Form = new FormCollection(new Dictionary<string, StringValues> { { "ssn", "332234" }, { "role", "owner" } });

            });
            //assert
            context.Response.StatusCode.Should().Be((int)HttpStatusCode.Unauthorized);

        }

    }
}
