// <copyright file="ComponentTest.cs" company="Microsoft">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace Service.App.ComponentTests
{
    using System.Net.Http;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Service.App.Startup;

    public abstract class ComponentTest
    {
        protected readonly HttpClient _httpClient;

        protected ComponentTest()
        {
            // Create the default builder in order to load all configurations similar to our real builder
            var builder = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .ConfigureTestServices(OverrideServices);

            var testServer = new TestServer(builder);

            _httpClient = testServer.CreateClient();
        }

        protected virtual void OverrideServices(IServiceCollection services)
        {
        }
    }
}
