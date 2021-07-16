using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;

        public RequestLogger(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

#pragma warning disable 1998
        public async Task Process(TRequest request, CancellationToken cancellationToken)
#pragma warning restore 1998
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUserService.UserId;
            var userName = _currentUserService.UserName;

            //var logToDatabase = new RequestLoggerEntity
            //{
            //    RequestName = requestName,
            //    UserId = userId,
            //    UserName = userName
            //};

            //await _logToDatabaseService.Save(logToDatabase, cancellationToken);

            _logger.LogInformation("Api Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);
        }
    }
}
