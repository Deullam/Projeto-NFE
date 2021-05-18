using TheSolutionBrothers.Nfe.Infra.ExtensionMethods.CPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.Nfe.Infra.Features.CPFs
{
    public class CPF
    {
        public CPF()
        {

        }
        public virtual string Value { get; set; }
        public string FormattedValue
        {
            get
            {
                return !String.IsNullOrEmpty(this.Value) ? Value.Format() : "";
            }
        }

        public override bool Equals(object obj)
        {
            var cpf = obj as CPF;
            return cpf != null && Value == cpf.Value;
        }

        public virtual void Validate()
        {
            if (string.IsNullOrEmpty(Value))
            {
                throw new CPFUninformedValueException();
            }

            if (!Value.IsValid())
            {
                throw new CPFInvalidValueException();
            }
        }

    }
}
