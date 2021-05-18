using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers
{
    public static partial class ObjectMother
    {

        public static CPF GetValidCPF()
        {
            return new CPF()
            {
                Value = "00805289020"
            };
        }

        public static CPF GetInvalidCPFWithUninformedValue()
        {
            return new CPF()
            {
                Value = ""
            };
        }

        public static CPF GetInvalidCPFWithInvalidValue()
        {
            return new CPF()
            {
                Value = "00805289021"
            };
        }

    }
}
