using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Wrappers;
using Application.DTOs.Students;
using Application.Interfaces.Services;
using Domain.Entities;
using Hangfire;
using MediatR;

namespace Application.Features.Jobs.Queries
{
    public class ReccuringJobQuery : IRequest<Response<string>>
    {
    }

    public class ReccuringJobQueryHandler : IRequestHandler<ReccuringJobQuery, Response<string>>
    {
        private readonly IJobService _jobService; 
        private readonly IRecurringJobManager _recurringJobManager;
        public ReccuringJobQueryHandler(IJobService jobService, IRecurringJobManager recurringJobManager)
        {
            _jobService = jobService;
            _recurringJobManager = recurringJobManager;
        }

        public async Task<Response<string>> Handle(ReccuringJobQuery request, CancellationToken cancellationToken)
        {
            _recurringJobManager.AddOrUpdate("jobId", () => _jobService.ReccuringJob(), Cron.Minutely);
            return new Response<string>("success");
        }
    }
}
