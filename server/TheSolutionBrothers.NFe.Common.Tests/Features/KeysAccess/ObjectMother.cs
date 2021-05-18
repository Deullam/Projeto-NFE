using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.KeysAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Common.Tests.Features.ObjectMothers
{
    public static partial class ObjectMother
    {

        public static KeyAccess GetValidKeyAccess()
        {
            return new KeyAccess()
            {
                Value = "12341234123412341234123412341234123412341234"
            };
        }

        public static KeyAccess GetInvalidKeyAccessWithUninformedValue()
        {
            return new KeyAccess()
            {
                Value = ""
            };
        }

        public static KeyAccess GetInvalidKeyAccessWithValueLengthDifferentThan44()
        {
            return new KeyAccess()
            {
                Value = "9432843283234978324978243834"
            };
        }

    }
}
