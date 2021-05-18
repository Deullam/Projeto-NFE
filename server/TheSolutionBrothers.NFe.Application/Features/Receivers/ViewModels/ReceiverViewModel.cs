using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Addresses.ViewModels;

namespace TheSolutionBrothers.NFe.Application.Features.Receivers.ViewModels
{
    public class ReceiverViewModel
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string StateRegistration { get; set; }
        public virtual string Cpf { get; set; }
        public virtual string Cnpj { get; set; }
        public virtual string PersonType { get; set; }
        public virtual AddessViewModel Address { get; set; }
    }
}
