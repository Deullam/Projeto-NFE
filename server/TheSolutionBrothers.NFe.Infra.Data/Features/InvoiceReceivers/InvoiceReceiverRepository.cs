using TheSolutionBrothers.Nfe.Infra;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Domain.Base;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using System;
using System.Data;


namespace TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceReceivers
{
    public class InvoiceReceiverRepository : IInvoiceReceiverRepository
    {
		#region
		private const string _insert = @"INSERT INTO invoice_receiver (
                                            receiver_name, 
                                            receiver_company_name, 
                                            receiver_cpf, 
                                            receiver_cnpj, 
                                            receiver_state_registration, 
                                            receiver_type,
											receiver_address_street_name,
                                            receiver_address_number, 
                                            receiver_address_neighborhood,
											receiver_address_city,
											receiver_address_state,
											receiver_address_country,
											invoice_id
                                        ) VALUES (
                                            @receiver_name, 
                                            @receiver_company_name, 
                                            @receiver_cpf, 
                                            @receiver_cnpj, 
                                            @receiver_state_registration, 
                                            @receiver_type,
											@receiver_address_street_name, 
                                            @receiver_address_number, 
                                            @receiver_address_neighborhood, 
                                            @receiver_address_city, 
                                            @receiver_address_state, 
                                            @receiver_address_country,
											@invoice_id
                                        )";

		private const string _getByInvoice = @"SELECT * FROM invoice_receiver 
											 WHERE invoice_id = @invoice_id";
		#endregion

		public InvoiceReceiver Add(InvoiceReceiver entity)
        {
			entity.Validate();

			entity.Id = Db.Insert(_insert, Take(entity));

			return entity;
		}

        public InvoiceReceiver GetByInvoice(long id)
        {
			if (id <= 0)
			{
				throw new IdentifierUndefinedException();
			}

			return Db.Get(_getByInvoice, Make, new object[] { "@invoice_id ", id });
		}

		private object[] Take(InvoiceReceiver invoiceReceiver)
		{
			return new object[]
			{
				"@id_invoice_receiver", invoiceReceiver.Id,
				"@receiver_name", invoiceReceiver.Receiver.Name,
				"@receiver_company_name", invoiceReceiver.Receiver.CompanyName,
				"@receiver_cpf", invoiceReceiver.Receiver.Cpf != null ? invoiceReceiver.Receiver.Cpf.Value : null,
				"@receiver_cnpj", invoiceReceiver.Receiver.Cnpj != null ? invoiceReceiver.Receiver.Cnpj.Value : null,
				"@receiver_state_registration", invoiceReceiver.Receiver.StateRegistration,
				"@receiver_type", invoiceReceiver.Receiver.Type,
				"@receiver_address_street_name", invoiceReceiver.Receiver.Address.StreetName,
				"@receiver_address_number", invoiceReceiver.Receiver.Address.Number,
				"@receiver_address_neighborhood", invoiceReceiver.Receiver.Address.Neighborhood,
				"@receiver_address_city", invoiceReceiver.Receiver.Address.City,
				"@receiver_address_state", invoiceReceiver.Receiver.Address.State,
				"@receiver_address_country", invoiceReceiver.Receiver.Address.Country,
				"@invoice_id", invoiceReceiver.Invoice.Id
			};
		}

		private static Func<IDataReader, InvoiceReceiver> Make = reader =>
		   new InvoiceReceiver
		   {
			   Receiver = new Receiver()
			   {
				   Name = reader["receiver_name"].ToString(),
				   CompanyName = reader["receiver_company_name"].ToString(),
				   StateRegistration = reader["receiver_state_registration"].ToString(),
				   Type = (PersonType)Enum.Parse(typeof(PersonType), reader["receiver_type"].ToString()),
				   Address = new Address()
				   {
					   Number = Convert.ToInt32(reader["receiver_address_number"].ToString()),
					   StreetName = reader["receiver_address_street_name"].ToString(),
					   Neighborhood = reader["receiver_address_neighborhood"].ToString(),
					   City = reader["receiver_address_city"].ToString(),
					   State = reader["receiver_address_state"].ToString(),
					   Country = reader["receiver_address_country"].ToString()
				   },
				   Cpf = new CPF()
				   {
					   Value = Convert.ToString(reader["receiver_cpf"])
				   },
				   Cnpj = new CNPJ()
				   {
					   Value = Convert.ToString(reader["receiver_cnpj"]),
				   }

			   },
			   Invoice = new Invoice()
			   {
				   Id = Convert.ToInt64(reader["invoice_id"]),
			   }
		   };

	}
}
