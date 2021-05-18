using TheSolutionBrothers.Nfe.Infra;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceItems;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
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

namespace TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceItems
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        #region Scrips
        private const string _insert = "INSERT INTO invoice_item (code,description,amount,total_value,unit_value,icms_value,ipi_value,icms_aliquot,ipi_aliquot,invoice_id,product_id)" +
                                                " VALUES (@code,@description,@amount,@total_value,@unit_value,@icms_value,@ipi_value,@icms_aliquot,@ipi_aliquot,@invoice_id,@product_id)";

        private const string _update = "UPDATE invoice_item SET code = @code, " +
                                                               "amount = @amount," +
                                                               "description = @description," +
                                                               "total_value = @total_value," +
                                                               "unit_value = @unit_value," +
                                                               "icms_value = @icms_value," +
                                                               "ipi_value = @ipi_value," +
                                                               "icms_aliquot = @icms_aliquot," +
                                                               "ipi_aliquot = @ipi_aliquot," +
                                                               "invoice_id = @invoice_id," +
                                                               "product_id = @product_id" +
                                                               " WHERE id_invoice_item = @id_invoice_item";

        private const string _delete = "DELETE FROM invoice_item WHERE id_invoice_item = @id_invoice_item";

        private const string _getById = "SELECT I.*, P.id_product, P.code AS code_product, P.description AS description_product, P.current_value FROM invoice_item AS I " +
                                    "INNER JOIN product AS P ON I.product_id = P.id_product WHERE id_invoice_item = @id_invoice_item";

        private const string _getByInvoice = "SELECT I.*, P.id_product, P.code AS code_product, P.description AS description_product, P.current_value FROM invoice_item AS I " +
                                    "INNER JOIN product AS P ON I.product_id = P.id_product WHERE invoice_id = @id_invoice";

        private const string _getByProduct = "SELECT I.*, P.id_product, P.code AS code_product, P.description AS description_product, P.current_value FROM invoice_item AS I " +
                                    "INNER JOIN product AS P ON I.product_id = P.id_product WHERE product_id = @id_product";

        private const string _getAll = "SELECT I.*, P.id_product, P.code AS code_product, P.description AS description_product, P.current_value FROM invoice_item AS I " +
                                    "INNER JOIN product AS P ON I.product_id = P.id_product";
		#endregion

		private ContextNfe _context;

		public InvoiceItemRepository(ContextNfe context)
		{
			_context = context;
		}

		public bool Remove(long entityId)
		{
			var entity = _context.InvoiceItems.FirstOrDefault(p => p.Id == entityId);
			if (entity == null)
				throw new NotFoundException();
			_context.Entry(entity).State = EntityState.Deleted;
			return _context.SaveChanges() > 0;
		}

		public InvoiceItem GetById(long entityId)
		{
			return _context.InvoiceItems.Include("Product").FirstOrDefault(c => c.Id == entityId);
		}

		public IQueryable<InvoiceItem> GetAll(int size)
		{
			return this._context.InvoiceItems.Include("Product").Take(size);
		}

		public IQueryable<InvoiceItem> GetAll()
		{
			return this._context.InvoiceItems.Include("Product");
		}

		public InvoiceItem Add(InvoiceItem invoiceItem)
		{
			var newInvoiceItem = _context.InvoiceItems.Add(invoiceItem);
			_context.SaveChanges();
			return newInvoiceItem;
		}

		public bool Update(InvoiceItem invoiceItem)
		{
			_context.Entry(invoiceItem).State = EntityState.Modified;
			return _context.SaveChanges() > 0;
		}

		private static Func<IDataReader, InvoiceItem> Make = reader =>
           new InvoiceItem
           {
               Id = Convert.ToInt64(reader["id_invoice_item"]),
               Code = Convert.ToString(reader["code"]),
               Description = Convert.ToString(reader["description"]),
               Amount = Convert.ToInt64(reader["amount"]),
               UnitValue = Convert.ToInt64(reader["unit_value"]),
               IcmsAliquot = Convert.ToDouble(reader["icms_aliquot"]),
               IpiAliquot = Convert.ToDouble(reader["ipi_aliquot"]),
               Product = new Product
               {
                   Id = Convert.ToInt64(reader["product_id"]),
                   Code = Convert.ToString(reader["code_product"]),
                   CurrentValue = Convert.ToDouble(reader["current_value"]),
                   Description = Convert.ToString(reader["description_product"]),
                   TaxProduct = new TaxProduct()
               },
               Invoice = new Invoice
               {
                   Id = Convert.ToInt64(reader["invoice_id"])
               }
           };

        private object[] Take(InvoiceItem invoiceItem)
        {
            return new object[]
            {
                "@id_invoice_item", invoiceItem.Id,
                "@code", invoiceItem.Code,
                "@amount", invoiceItem.Amount,
                "@description", invoiceItem.Description,
                "@total_value", invoiceItem.TotalValue,
                "@unit_value", invoiceItem.UnitValue,
                "@icms_value", invoiceItem.IcmsValue,
                "@ipi_value", invoiceItem.IpiValue,
                "@icms_aliquot", invoiceItem.IcmsAliquot,
                "@ipi_aliquot", invoiceItem.IpiAliquot,
                "@invoice_id", invoiceItem.Invoice.Id,
                "@product_id", invoiceItem.Product.Id
            };
        }

        public IList<InvoiceItem> GetByInvoice(long idInvoice)
        {
            if (idInvoice <= 0)
                throw new IdentifierUndefinedException();

            return Db.GetAll(_getByInvoice, Make, new object[] { "@id_invoice", idInvoice });
        }

        public IList<InvoiceItem> GetByProduct(long idProduct)
        {
            if (idProduct <= 0)
                throw new IdentifierUndefinedException();

            return Db.GetAll(_getByProduct, Make, new object[] { "@id_product", idProduct });
        }
    }
}
