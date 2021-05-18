
using TheSolutionBrothers.Nfe.Infra;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Products;
using TheSolutionBrothers.NFe.Domain.Features.TaxProducts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Infra.Data.Contexts;
using System.Data.Entity;

namespace TheSolutionBrothers.NFe.Infra.Data.Features.Products
{
    public class ProductRepository : IProductRepository
    {

        private ContextNfe _context;

        public ProductRepository(ContextNfe context)
        {
            _context = context;
        }

        #region ADD

        public Product Add(Product product)
        {
            var newProduct = _context.Products.Add(product);
            _context.SaveChanges();
            return newProduct;
        }

        #endregion

        #region GET
        public IQueryable<Product> GetAll(int size)
        {
            return this._context.Products.Take(size);
        }

        public IQueryable<Product> GetAll()
        {
            return this._context.Products;
        }

        public Product GetById(long productId)
        {
            return _context.Products.FirstOrDefault(c => c.Id == productId);
        }
        #endregion

        #region REMOVE
        public bool Remove(long productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                throw new NotFoundException();
            _context.Entry(product).State = EntityState.Deleted;
            return _context.SaveChanges() > 0;
        }

        #endregion

        #region UPDATE

        public bool Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            return _context.SaveChanges() > 0;
        }

        #endregion
    }
}
