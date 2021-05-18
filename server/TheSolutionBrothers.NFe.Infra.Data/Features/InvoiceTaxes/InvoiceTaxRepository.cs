using TheSolutionBrothers.Nfe.Infra;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;
using System;
using System.Data;


namespace TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceTaxes
{
	public class InvoiceTaxRepository : IInvoiceTaxRepository
	{
		#region
		private const string _insert = @"INSERT INTO invoice_tax (
                                            icms_value, 
                                            freight, 
                                            ipi_value, 
                                            total_value_products, 
                                            total_value_invoice,
											invoice_id
                                        ) VALUES (
                                            @icms_value, 
                                            @freight, 
                                            @ipi_value, 
                                            @total_value_products, 
                                            @total_value_invoice,
											@invoice_id
                                        )";

		private const string _getByInvoice = @"SELECT * FROM invoice_tax 
											 WHERE invoice_id = @invoice_id";
		#endregion

		public InvoiceTax Add(InvoiceTax entity)
		{
			entity.Validate();

			entity.Id = Db.Insert(_insert, Take(entity));

			return entity;
		}

		public InvoiceTax GetByInvoice(long id)
		{
			if (id <= 0)
			{
				throw new IdentifierUndefinedException();
			}

			return Db.Get(_getByInvoice, Make, new object[] { "@invoice_id", id });
		}

		private object[] Take(InvoiceTax invoiceTax)
		{
			return new object[]
			{
				"@id_invoice_tax", invoiceTax.Id,
				"@icms_value", invoiceTax.IcmsValue,
				"@freight", invoiceTax.Freight,
				"@ipi_value", invoiceTax.IpiValue,
				"@total_value_products", invoiceTax.TotalValueProducts,
				"@total_value_invoice", invoiceTax.TotalValueInvoice,
				"@invoice_id", invoiceTax.Invoice.Id
			};
		}

		private static Func<IDataReader, InvoiceTax> Make = reader =>
		   new InvoiceTax
		   {
			   Id = Convert.ToInt64(reader["id_invoice_tax"]),
			   IcmsValue = Convert.ToDouble(reader["icms_value"]),
			   Freight = Convert.ToDouble(reader["freight"]),
			   IpiValue = Convert.ToDouble(reader["ipi_value"]),
			   TotalValueProducts = Convert.ToDouble(reader["total_value_products"]),
			   Invoice = new Invoice()
			   {
					Id = Convert.ToInt64(reader["invoice_id"]),
			   }
		   };

	}
}
