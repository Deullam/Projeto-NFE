using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Domain.Features.Receivers
{
	public interface IReceiverRepository
	{
        IQueryable<Receiver> GetAll();
        IQueryable<Receiver> GetAll(int size);
        Receiver Add(Receiver product);
        bool Update(Receiver product);
        Receiver GetById(long productId);
        bool Remove(long productId);
    }
}
