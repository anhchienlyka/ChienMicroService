using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Ordering.Application.Common.Behaviours
{
    public class PerformanceBehaviour<TRequest, TResponse>
      : IPipelineBehavior<TRequest, TResponse>
      where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehaviour(Stopwatch timer, ILogger<TRequest> logger)
        {
            _timer = timer;
            _logger = logger;
        }

   

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();
            var reponse = await next();
            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds; // thời gian request thực hiện mất bao lâu

            if (elapsedMilliseconds <= 500)
                return reponse;

            var requestName = typeof(TRequest).Name;

            _logger.LogWarning("Application Long Running Request: {Name} ({ElapsedMilliseconds} miliseconds) {@Request}", requestName, elapsedMilliseconds, request);

            return reponse;
        }
    }
}