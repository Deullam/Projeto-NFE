using TheSolutionBrothers.Nfe.Infra;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Infra.Data.Contexts;
using System.Data.Entity;

namespace TheSolutionBrothers.NFe.Infra.Data.Features.Addresses
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ContextNfe _context;

        public AddressRepository(ContextNfe context)
        {
            _context = context;
        }


        public bool Remove(long id)
        {
            var address = _context.Addresses.FirstOrDefault(p => p.Id == id);
            if (address == null)
                throw new NotFoundException();
            _context.Entry(address).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }

        public Address Get(long id)
        {
            return _context.Addresses.FirstOrDefault(c => c.Id == id);

        }

        public IQueryable<Address> GetAll()
        {
            return this._context.Addresses;
        }

        public Address Save(Address entity)
        {
            var newAddress = _context.Addresses.Add(entity);
            _context.SaveChanges();
            return newAddress;
        }

        public bool Update(Address entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }
    }
}
