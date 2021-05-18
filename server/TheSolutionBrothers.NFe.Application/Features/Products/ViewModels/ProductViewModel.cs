using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Application.Features.Products.ViewModels
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public virtual string Code { get; set; }
        public virtual string Description { get; set; }
        public virtual double CurrentValue { get; set; }     
        public virtual double IcmsAliquot { get; set; }
        public virtual double IpiAliquot { get; set; }
    }
}
