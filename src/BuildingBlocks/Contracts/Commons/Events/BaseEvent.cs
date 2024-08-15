using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Commons.Events
{
    public abstract class BaseEvent : INotification
    {
        protected BaseEvent()
        {
        }
    }
}
