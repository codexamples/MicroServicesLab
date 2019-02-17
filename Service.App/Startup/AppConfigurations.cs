// <copyright file="AppConfigurations.cs" company="Microsoft">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace Service.App.Startup
{
    using System.Net;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Http;

    public static class AppConfigurations
    {
        public static void UseDetailedExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>();
                    var error = exception?.Error;

                    string messageToReturn = $"Request: {context.Request?.Host + context.Request?.Path}";

                    if (error != null)
                    {
                        messageToReturn += System.Environment.NewLine + $"Exception - {error.ToString()}";
                    }
                    else
                    {
                        messageToReturn += System.Environment.NewLine + "Exception - No details.";
                    }

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync(messageToReturn);
                });
            });
        }
    }
}
