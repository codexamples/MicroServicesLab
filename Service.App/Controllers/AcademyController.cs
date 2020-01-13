// <copyright file="AcademyController.cs" company="Microsoft">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{
    [Route("api/academy")]
    [ApiController]
    public class AcademyController : ControllerBase
    {
        [HttpGet("greeting")]
        public IActionResult GetGreating()
        {
            return Ok("HI");
		}
    }
}
