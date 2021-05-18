using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Addresses.ViewModels;

namespace TheSolutionBrothers.NFe.Application.Features.Carriers.ViewModels
{
    public class CarrierViewModel
    {

        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string StateRegistration { get; set; }
        public virtual string CPF { get; set; }
        public virtual string CNPJ { get; set; }
        public virtual string PersonType { get; set; }
        public virtual string FreightResponsability { get; set; }
        public virtual AddessViewModel Address { get; set; }

    }
}
