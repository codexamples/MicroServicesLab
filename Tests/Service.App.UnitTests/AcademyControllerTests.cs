// <copyright file="AcademyControllerTests.cs" company="Microsoft">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace Service.App.ComponentTests
{
    using System.Net;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Service.Controllers;
    using Xunit;

    public class AcademyControllerTests
    {
        private const string ApiHeaderName = "api-version";

        private readonly AcademyController _academyController;

        public AcademyControllerTests()
        {
            _academyController = new AcademyController();
        }

        [Fact]
        public void Should_Return_200()
        {
            var response = _academyController.GetGreating();

            response.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }
    }
}
