using System.Collections.Generic;
using System.Linq;

namespace TheSolutionBrothers.NFe.Domain.Features.Products
{
    public interface IProductRepository
    {

        Product Add(Product entity);
        bool Update(Product entity);
        Product GetById(long id);
        IQueryable<Product> GetAll();
        IQueryable<Product> GetAll(int size);
        bool Remove(long entityId);

    }
}
