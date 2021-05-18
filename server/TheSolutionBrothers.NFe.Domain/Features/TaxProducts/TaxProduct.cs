using TheSolutionBrothers.NFe.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.TaxProducts
{
    public class TaxProduct 
    {
        public virtual long Id { get; set; }

        public virtual double IcmsAliquot
        {
            get => 0.04;
        }
        public virtual double IpiAliquot
        {
            get => 0.1;
        }

    }
}
