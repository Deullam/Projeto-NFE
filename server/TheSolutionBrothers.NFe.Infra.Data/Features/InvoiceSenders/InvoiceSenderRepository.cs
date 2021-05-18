using TheSolutionBrothers.Nfe.Infra;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceSenders;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceSenders
{
    public class InvoiceSenderRepository : IInvoiceSenderRepository
    {
        private const string _insert = "INSERT INTO invoice_sender (sender_fancy_name,sender_company_name,sender_cnpj," +
                                                    "sender_state_registration,sender_municipal_registration,sender_address_street_name," +
                                                    "sender_address_number,sender_address_neighborhood,sender_address_city,sender_address_state," +
                                                    "sender_address_country,invoice_id) " +
                                                    "VALUES (@sender_fancy_name, @sender_company_name,@sender_cnpj, @sender_state_registration,@sender_municipal_registration," +
                                                    "@sender_address_street_name, @sender_address_number, @sender_address_neighborhood,@sender_address_city, @sender_address_state," +
                                                    "@sender_address_country,@invoice_id)";

        private const string _getByInvoice = "SELECT * FROM invoice_sender WHERE invoice_id = @id_invoice";

        public InvoiceSender Add(InvoiceSender entity)
        {
            entity.Validate();
            entity.Id = Db.Insert(_insert, Take(entity));
            return entity;
        }

        public InvoiceSender GetByInvoice(long id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();
            return Db.Get(_getByInvoice, Make, new object[] { "@id_invoice ", id });
        }

        private static Func<IDataReader, InvoiceSender> Make = reader =>
           new InvoiceSender
           {
               Id = Convert.ToInt64(reader["id_invoice_sender"]),
               Sender = new Sender
               {
                   CompanyName = Convert.ToString(reader["sender_company_name"]),
                   FancyName = Convert.ToString(reader["sender_fancy_name"]),
                   MunicipalRegistration = Convert.ToString(reader["sender_municipal_registration"]),
                   StateRegistration = Convert.ToString(reader["sender_state_registration"]),
                   Address = new Address
                   {
                       City = Convert.ToString(reader["sender_address_city"]),
                       Country = Convert.ToString(reader["sender_address_country"]),
                       State = Convert.ToString(reader["sender_address_state"]),
                       Neighborhood = Convert.ToString(reader["sender_address_neighborhood"]),
                       Number = Convert.ToInt32(reader["sender_address_number"]),
                       StreetName = Convert.ToString(reader["sender_address_street_name"])
                   },
                   Cnpj = new CNPJ
                   {
                       Value = Convert.ToString(reader["sender_cnpj"])
                   }
               },
               Invoice = new Invoice
               {
                   Id = Convert.ToInt64(reader["invoice_id"])
               }
           };

        private object[] Take(InvoiceSender invoiceSender)
        {
            return new object[]
            {
                "@id_invoice_sender", invoiceSender.Id,
                "@sender_fancy_name", invoiceSender.Sender.FancyName,
                "@sender_company_name", invoiceSender.Sender.CompanyName,
                "@sender_cnpj", invoiceSender.Sender.Cnpj.Value,
                "@sender_state_registration", invoiceSender.Sender.StateRegistration,
                "@sender_municipal_registration", invoiceSender.Sender.MunicipalRegistration,
                "@sender_address_street_name", invoiceSender.Sender.Address.StreetName,
                "@sender_address_number", invoiceSender.Sender.Address.Number,
                "@sender_address_neighborhood", invoiceSender.Sender.Address.Neighborhood,
                "@sender_address_city", invoiceSender.Sender.Address.City,
                "@sender_address_state", invoiceSender.Sender.Address.State,
                "@sender_address_country", invoiceSender.Sender.Address.Country,
                "@invoice_id", invoiceSender.Invoice.Id
            };
        }
    }
}
