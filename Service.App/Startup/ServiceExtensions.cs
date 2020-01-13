// <copyright file="ServiceExtensions.cs" company="Microsoft">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace Service.App.Startup
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceExtensions
    {
        public static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
        }

        public static void AddAppServices(this IServiceCollection services)
        {
            services
                .AddHealthChecks()
                .AddCheck<AcademyHealthChecks>("example_health_check");
        }
    }
}
