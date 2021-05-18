using TheSolutionBrothers.NFe.Domain.Features.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Application.Features.Products.Commands;
using TheSolutionBrothers.NFe.Application.Features.Products.Queries;
using TheSolutionBrothers.NFe.Application.Features.Products.ViewModels;

namespace TheSolutionBrothers.NFe.Application.Features.Products
{
    public interface IProductService
    {
        long Add(ProductRegisterCommand sender);
        bool Update(ProductUpdateCommand sender);
        ProductViewModel GetById(long id);
        IQueryable<Product> GetAll();
        IQueryable<Product> GetAll(ProductGetAllQuery query);
        bool Remove(ProductDeleteCommand sender);

    }
}
