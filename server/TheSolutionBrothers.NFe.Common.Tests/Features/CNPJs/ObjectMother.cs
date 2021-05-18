using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers
{
    public static partial class ObjectMother
    {

        public static CNPJ GetValidCNPJ()
        {
            return new CNPJ()
            {
                Value = "25103755000142"
            };
        }

        public static CNPJ GetInvalidCNPJWithUninformedValue()
        {
            return new CNPJ()
            {
                Value = ""
            };
        }

        public static CNPJ GetInvalidCNPJWithInvalidValue()
        {
            return new CNPJ()
            {
                Value = "25103755000146"
            };
        }

    }
}
