﻿using AutoMapper;
using EventBus.Messages.IntegrationEvents.Events;
using MassTransit;
using MassTransit.Mediator;
using Ordering.Application.Features.V1.Orders;
using ILogger = Serilog.ILogger;

namespace Ordering.API.Application.IntegrationEvents.EventsHandler;

public class BasketCheckoutEventHandler : IConsumer<BasketCheckoutEvent>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public BasketCheckoutEventHandler(IMediator mediator, IMapper mapper, ILogger logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        var command = _mapper.Map<CreateOrderCommand>(context.Message);
        //var result = await _mediator.Send(command);
        await _mediator.Send<CreateOrderCommand>(command);
        _logger.Information("BasketCheckoutEvent consumed succesfully. Order is created with Id: {newOrder}", command);
    }
}
