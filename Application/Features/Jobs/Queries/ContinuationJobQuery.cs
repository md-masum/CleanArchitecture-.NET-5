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

namespace Application.Features.Jobs.Queries
{
    public class ContinuationJobQuery : IRequest<Response<string>>
    {
    }

    public class ContinuationJobQueryHandler : IRequestHandler<ContinuationJobQuery, Response<string>>
    {
        private readonly IJobService _jobService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        public ContinuationJobQueryHandler(IJobService jobService, IBackgroundJobClient backgroundJobClient)
        {
            _jobService = jobService;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<Response<string>> Handle(ContinuationJobQuery request, CancellationToken cancellationToken)
        {
            var parentJobId = _backgroundJobClient.Enqueue(() => _jobService.FireAndForgetJob());
            _backgroundJobClient.ContinueJobWith(parentJobId, () => _jobService.ContinuationJob());

            return new Response<string>("success");
        }
    }
}
