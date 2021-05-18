using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.Nfe.Infra.Features.KeysAccess
{
    public class KeyAccess
    {

        public virtual string Value { get; set; }
        public int Length
        {
            get => 44;
        }

        public virtual void GenerateKeyAccess(int number)
        {
            if (number <= 0)
            {
                throw new KeyAccessNonPositiveNumberException();
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(number);
            builder.Insert(0, "0", (Length - builder.Length));

            Value = builder.ToString();
        }

        public virtual void Validate()
        {
            if (string.IsNullOrEmpty(Value))
            {
                throw new KeyAccessUninformedValueException();
            }

            if (Value.Length != 44)
            {
                throw new KeyAccessValueLengthDifferentThan44Exception();
            }
        }

    }
}
