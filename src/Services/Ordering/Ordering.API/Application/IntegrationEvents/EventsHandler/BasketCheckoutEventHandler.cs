using AutoMapper;
using EventBus.Messages.IntegrationEvents.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Features.V1.Orders.Commands.CreateOrder;
using ILogger = Serilog.ILogger;
namespace Ordering.API.Application.IntegrationEvents.EventsHandler
{
    public class BasketCheckoutEventHandler : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public BasketCheckoutEventHandler(IMediator mediator, IMapper mapper, ILogger logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
           
            var command = _mapper.Map<CreateOrderCommand>(context.Message);

            var result = await _mediator.Send(command);
            _logger.Information("BasketCheckoutEvent consumed succesfully. Order is created with Id: {newOrder}", result.Data);
        }
    }
}
