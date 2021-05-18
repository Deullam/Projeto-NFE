using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Infra.XML.Features.Invoices.Mappers
{
    public interface IMapper<E, S>
    {

        S Map(E entity);

    }
}
