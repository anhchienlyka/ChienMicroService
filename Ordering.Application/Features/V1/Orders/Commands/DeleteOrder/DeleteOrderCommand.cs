using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.V1.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<bool>
    {
        public int Id { get; private set; }
        public DeleteOrderCommand(int id)
        {
            Id = id;
        }
    }
}
