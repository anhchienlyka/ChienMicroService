using MediatR;
using Ordering.Application.Common.Models;
using Shared.SeedWord;

namespace Ordering.Application.Features.V1.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<ApiResult<OrderDto>>
    {
        public int Id { get; set; }

        public GetOrderByIdQuery(int id)
        {
            Id = id;
        }
    }
}