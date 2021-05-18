using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Base
{
    [ExcludeFromCodeCoverage]
    public abstract class Entity 
    {

        public virtual long Id { get; set; }

        public override bool Equals(object obj)
        {
            var entity = obj as Entity;
            return entity != null && Id == entity.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + Id.GetHashCode();
        }

        public abstract void Validate();


    }
}
