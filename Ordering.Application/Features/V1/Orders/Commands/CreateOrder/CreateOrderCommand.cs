using AutoMapper;
using MediatR;
using Ordering.Application.Common.Mappings;
using Ordering.Application.Common.Models;
using Ordering.Application.Features.V1.Orders.Common;
using Ordering.Domain.Entities;
using Shared.DTOs.Order;
using Shared.SeedWord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.V1.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : CreateOrUpdateCommand, IRequest<ApiResult<OrderDto>>, IMapFrom<Order>
        //,IMapFrom<BasketCheckoutEvent>
    {
        public string Username { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateOrderDto, CreateOrderCommand>();
            profile.CreateMap<CreateOrderCommand, Order>();
            //profile.CreateMap<BasketCheckoutEvent, CreateOrderCommand>();
        }
    }
}
