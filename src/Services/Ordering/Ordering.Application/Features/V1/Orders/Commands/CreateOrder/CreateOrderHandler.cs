using AutoMapper;
using Contracts.Services;
using MediatR;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Ordering.Domain.Entities;
using Serilog;
using Shared.SeedWord;
using Shared.Services.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.V1.Orders.Commands.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, ApiResult<OrderDto>>
{
    private readonly IMapper _mapper;
    private readonly IOrderRepository _repository;
    private readonly ILogger _logger;
    private const string MethodName = "CreateOrderHandler";
    private readonly ISmtpEmailService _emailService;
    public CreateOrderHandler(IMapper mapper, 
                              IOrderRepository repository, 
                              ILogger logger,
                              ISmtpEmailService emailService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
    }

    public async Task<ApiResult<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        _logger.Information($"BEGIN: {MethodName}");

        var order = _mapper.Map<Order>(request);
        await _repository.CreateOrder((Order)order);
        
        order.AddedOrder();

        await _repository.SaveChangesAsync();

        //await SendMailAsync(order, cancellationToken);
        _logger.Information($"END: {MethodName}");

        return new ApiSuccessResult<OrderDto>(
            _mapper.Map<OrderDto>(order));
    }
    private async Task SendMailAsync(Order order, CancellationToken cancellationToken)
    {
        var emailRequest = new MailRequest
        {
            ToAddress = order.EmailAddress,
            Body = "Order was created",
            Subject = "Order was created"
        };

        try
        {
            await _emailService.SendEmailAsync(emailRequest, cancellationToken);
            _logger.Information($"Sent created order to email {order.EmailAddress}");

        }
        catch (Exception ex)
        {
            _logger.Error($"Order {order.Id} failed due to an error with the email service: {ex.Message}");
        }
    }
}
