using MediatR;
using Shared.SeedWord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.V1.Orders.Commands.DeleteOrderByDocumentNo
{
    public class DeleteOrderByDocumentNoCommand : IRequest<ApiSuccessResult<bool>>
    {
        public string DocumentNo { get; private set; }
        public DeleteOrderByDocumentNoCommand(string documentNo)
        {
            DocumentNo = documentNo;
        }
    }
}
