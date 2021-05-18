using TheSolutionBrothers.Nfe.Infra.ExtensionMethods.CNPJ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.Nfe.Infra.Features.CNPJs
{
    public class CNPJ
    {
        public CNPJ()
        {

        }

        public virtual string Value { get; set; }
        public string FormattedValue
        {
            get
            {
                return !String.IsNullOrEmpty(this.Value)?Value.Format():"";
            }
        }

        public override bool Equals(object obj)
        {
            var cnpj = obj as CNPJ;
            return cnpj != null && Value == cnpj.Value;
        }

        public virtual void Validate()
        {
            if (string.IsNullOrEmpty(Value))
            {
                throw new CNPJUninformedValueException();
            }

            if (!Value.IsValid())
            {
                throw new CNPJInvalidValueException();
            }
        }
        
    }
}
