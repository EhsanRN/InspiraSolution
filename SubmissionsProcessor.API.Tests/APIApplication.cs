using InpiraProject;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MinimalApiPlayground.Tests;

internal class APIApplication : WebApplicationFactory<Program>
{
    private readonly string _environment;

    public APIApplication(string environment = "Development")
    {
        _environment = environment;
    }

    //protected override IHost CreateHost(IHostBuilder builder)
    //{
    //    builder.UseEnvironment(_environment);

    //    // Add mock/test services to the builder here
    //    builder.ConfigureServices(services =>
    //    {
    //        services.AddScoped(sp =>
    //        {
    //
    //        });
    //    });

    //    return base.CreateHost(builder);
    //}
}
