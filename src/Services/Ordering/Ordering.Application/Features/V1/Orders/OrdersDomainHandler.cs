﻿using Contracts.Services;
using MediatR;
using Ordering.Domain.Entities;
using Ordering.Domain.OrderAggregate.Events;
using Serilog;
using Shared.Services.Email;

namespace Ordering.Application.Features.V1.Orders
{
    public class OrdersDomainHandler : INotificationHandler<OrderCreatedEvent>,
        INotificationHandler<OrderDeletedEvent>
    {
        private readonly ILogger _logger;
        private readonly ISmtpEmailService _emailService;
        public OrdersDomainHandler(ILogger logger, ISmtpEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.Information("Ordering Domain Event: {DomainEvent}", notification.GetType().Name);
            var emailRequest = new MailRequest
            {
                ToAddress = notification.EmailAddress,
                Body = "Your order detail",
                Subject = "Order was created"
            };

            try
            {
                await _emailService.SendEmailAsync(emailRequest, cancellationToken);
                _logger.Information($"Sent created order to email {notification.EmailAddress}");

            }
            catch (Exception ex)
            {
                _logger.Error($"Order {notification.Id} failed due to an error with the email service: {ex.Message}");
            }
            //return Task.CompletedTask;
        }
        public Task Handle(OrderDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.Information("Ordering Domain Event: {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }

       
    }
}
