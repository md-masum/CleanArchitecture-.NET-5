using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Wrappers;
using Application.DTOs.Students;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Hangfire;
using MediatR;

namespace Application.Features.Jobs.Commands
{
    public class FireAndForgetJobCommand : IRequest<Response<string>>
    {
    }

    public class FireAndForgetJobCommandHandler : IRequestHandler<FireAndForgetJobCommand, Response<string>>
    {
        private readonly IJobService _jobService;
        private readonly IBackgroundJobClient _backgroundJobClient;
        public FireAndForgetJobCommandHandler(IJobService jobService, IBackgroundJobClient backgroundJobClient)
        {
            _jobService = jobService;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<Response<string>> Handle(FireAndForgetJobCommand request, CancellationToken cancellationToken)
        {
            _backgroundJobClient.Enqueue(() => _jobService.FireAndForgetJob());
            return new Response<string>("success");
        }
    }
}
