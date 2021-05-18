using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Addresses
{
    public interface IAddressRepository
    {

        Address Save(Address entity);
        bool Update(Address entity);
        Address Get(long id);
        IQueryable<Address> GetAll();
        bool Remove(long id);

    }
}
