using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Wrappers;
using Application.Interfaces.Services;
using Hangfire;
using MediatR;

namespace Application.Features.Jobs.Commands
{
    public class DelayedJobCommand : IRequest<Response<string>>
    {
    }

    public class DelayedJobCommandHandler : IRequestHandler<DelayedJobCommand, Response<string>>
    {
        private readonly IJobService _jobService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        public DelayedJobCommandHandler(IJobService jobService, IBackgroundJobClient backgroundJobClient)
        {
            _jobService = jobService;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<Response<string>> Handle(DelayedJobCommand request, CancellationToken cancellationToken)
        {
            _backgroundJobClient.Schedule(() => _jobService.DelayedJob(), TimeSpan.FromSeconds(60));
            return new Response<string>("success");
        }
    }
}
