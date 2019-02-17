// <copyright file="HealthChecksControllerTests.cs" company="Microsoft">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace Service.App.ComponentTests
{
    using System.Net;
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Service.Controllers;
    using Xunit;

    public class HealthChecksControllerTests
    {
        private const string ApiHeaderName = "api-version";

        private readonly HealthChecksController _healthChecksController;

        public HealthChecksControllerTests()
        {
            _healthChecksController = new HealthChecksController();
        }

        [Fact]
        public async Task Should_Return_NoContent_If_Ready()
        {
            var response = await _healthChecksController.GetReadinessAsync() as StatusCodeResult;
            response.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }

        [Fact]
        public async Task Should_Return_NoContent_If_Alive()
        {
            var response = await _healthChecksController.GetLivenessAsync() as StatusCodeResult;
            response.StatusCode.Should().Be((int)HttpStatusCode.NoContent);
        }
    }
}
