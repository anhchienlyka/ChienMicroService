﻿using AutoMapper;
using MediatR;
using Ordering.Application.Common.Exceptions;
using Ordering.Application.Common.Interfaces;
using Ordering.Application.Common.Models;
using Ordering.Domain.Entities;
using Serilog;
using Shared.SeedWord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.V1.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, ApiResult<OrderDto>>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repository;
        private readonly ILogger _logger;
        private const string MethodName = "UpdateOrderHandler";

        public UpdateOrderHandler(IMapper mapper, IOrderRepository repository, ILogger logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ApiResult<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = await _repository.GetByIdAsync(request.Id);
            if (orderEntity is null)
                throw new NotFoundException(nameof(Order), request.Id);
            _logger.Information($"BEGIN: {MethodName}");

            //var order = _mapper.Map<Order>(request);
            orderEntity = _mapper.Map(request, orderEntity);
            await _repository.UpdateOrder(orderEntity);
            await _repository.SaveAsync();

            _logger.Information($"END: {MethodName}");

            return new ApiSuccessResult<OrderDto>(
                _mapper.Map<OrderDto>(orderEntity));
        }
    }
}
