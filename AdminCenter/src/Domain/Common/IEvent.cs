using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminCenter.Domain.Common;
public interface IEvent
{
    public Guid EventId { get; set; }

    public DateTimeOffset Created { get; set; }
}
