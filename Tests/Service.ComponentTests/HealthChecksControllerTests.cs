// <copyright file="HealthChecksControllerTests.cs" company="Microsoft">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace Service.App.ComponentTests
{
    using System.Threading.Tasks;
    using FluentAssertions;
    using Xunit;
    using static System.Net.HttpStatusCode;

    public class HealthChecksControllerTests : ComponentTest
    {
        private const string ApiHeaderName = "api-version";

        public HealthChecksControllerTests()
        {
            _httpClient.DefaultRequestHeaders.Add(ApiHeaderName, "1.0");
        }

        [Fact]
        public async Task Should_Return_NoContent_If_Ready()
        {
            var response = await _httpClient.GetAsync("/api/healthChecks/readiness");
            response.StatusCode.Should().Be(NoContent);
        }

        [Fact]
        public async Task Should_Return_NoContent_If_Alive()
        {
            var response = await _httpClient.GetAsync("/api/healthChecks/liveness");
            response.StatusCode.Should().Be(NoContent);
        }
    }
}
