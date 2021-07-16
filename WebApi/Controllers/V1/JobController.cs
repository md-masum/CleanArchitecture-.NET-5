using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Features.Jobs.Commands;
using Application.Features.Jobs.Queries;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize]
    public class JobController : BaseApiController
    {
        [HttpPost("FireAndForgetJob")]
        [AllowAnonymous]
        public async Task<IActionResult> FireAndForgetJob()
        {

            return Ok(await Mediator.Send(new FireAndForgetJobCommand()));
        }

        [HttpPost("DelayedJob")]
        public async Task<IActionResult> DelayedJob()
        {

            return Ok(await Mediator.Send(new DelayedJobCommand()));
        }

        [HttpGet("ReccuringJob")]
        public async Task<IActionResult> ReccuringJob()
        {

            return Ok(await Mediator.Send(new ReccuringJobQuery()));
        }

        [HttpGet("ContinuationJob")]
        public async Task<IActionResult> ContinuationJob()
        {

            return Ok(await Mediator.Send(new ContinuationJobQuery()));
        }
    }
}
