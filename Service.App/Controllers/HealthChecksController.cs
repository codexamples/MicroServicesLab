// <copyright file="HealthChecksController.cs" company="Microsoft">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace Service.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// A diagnostic tool which can be used by Kubernetes as a health check to determine whether the service is alive or ready to receive traffic.
    /// </summary>
    [Route("api/academy")]
    public class HealthChecksController : Controller
    {
        /// <summary>
        /// Indicates whether the service is ready to service requests.
        /// The kubelet uses readiness probes to know when a Container is ready to start accepting traffic.
        /// A Pod is considered ready when all of its Containers are ready.
        /// One use of this signal is to control which Pods are used as backends for Services.
        /// When a Pod is not ready, it is removed from Service load balancers.
        /// </summary>
        /// <returns>
        /// NoContent if service is ready.
        /// InternalServerError otherwise with status message.
        /// </returns>
        [HttpGet("readiness")]
        public async Task<ActionResult> GetReadinessAsync()
        {
            return await Task.FromResult(NoContent());
        }

        /// <summary>
        /// Indicates whether the service is running.
        /// If the liveness probe fails, the kubelet kills the Container, and the Container is subjected to its restart policy.
        /// The kubelet uses liveness probes to know when to restart a Container.
        /// For example, liveness probes could catch a deadlock, where an application is running, but unable to make progress.
        /// Restarting a Container in such a state can help to make the application more available despite bugs.
        /// </summary>
        /// <returns>
        /// NoContent if service is alive.
        /// InternalServerError otherwise.
        /// </returns>
        [HttpGet("liveness")]
        public async Task<ActionResult> GetLivenessAsync()
        {
            return await Task.FromResult(NoContent());
        }
    }
}
