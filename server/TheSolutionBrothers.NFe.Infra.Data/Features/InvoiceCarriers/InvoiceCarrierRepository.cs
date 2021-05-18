using TheSolutionBrothers.Nfe.Infra;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.NFe.Domain.Base;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceCarriers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSolutionBrothers.NFe.Infra.Data.Features.InvoiceCarriers
{
    public class InvoiceCarrierRepository : IInvoiceCarrierRepository
    {
        #region Querys
        private const string _insert = "INSERT INTO invoice_carrier ( carrier_name, carrier_company_name, carrier_cpf, carrier_cnpj, "
        + " carrier_state_registration, carrier_freight_responsability, carrier_person_type, carrier_address_street_name, carrier_address_number, "
        + "carrier_address_neighborhood, carrier_address_city,  carrier_address_state, carrier_address_country, invoice_id)"
        + "VALUES (@carrier_name,  @carrier_company_name,  @carrier_cpf,  @carrier_cnpj,  @carrier_state_registration,  "
        + "@carrier_freight_responsability,  @carrier_person_type,  @carrier_address_street_name,  @carrier_address_number,  @carrier_address_neighborhood,  "
        + "@carrier_address_city,  @carrier_address_state,  @carrier_address_country,  @invoice_id)";


        private const string _getByInvoice = "SELECT * FROM invoice_carrier WHERE invoice_id = @id_invoice";
        #endregion
        public InvoiceCarrier Add(InvoiceCarrier entity)
        {
            entity.Validate();
            entity.Id = Db.Insert(_insert, Take(entity));
            return entity;
        }

        public InvoiceCarrier GetByInvoice(long id)
        {
            if (id <= 0)
                throw new IdentifierUndefinedException();
            return Db.Get(_getByInvoice, Make, new object[] { "@id_invoice ", id });
        }


        private static Func<IDataReader, InvoiceCarrier> Make = reader =>
        new InvoiceCarrier
        {
            Carrier = new Carrier()
            {
                Name = Convert.ToString(reader["carrier_name"]),
                CompanyName = Convert.ToString(reader["carrier_company_name"]),
                CPF = new CPF { Value = reader["carrier_cpf"] == DBNull.Value ? "" : Convert.ToString(reader["carrier_cpf"]) },
                CNPJ = new CNPJ { Value = reader["carrier_cnpj"] == DBNull.Value ? "" : Convert.ToString(reader["carrier_cnpj"]) },
                StateRegistration = Convert.ToString(reader["carrier_state_registration"]),
                FreightResponsability = (FreightResponsability)Enum.Parse(typeof(FreightResponsability), reader["carrier_freight_responsability"].ToString()),
                PersonType = (PersonType)Enum.Parse(typeof(PersonType), reader["carrier_person_type"].ToString()),
                Address = new Address
                {
                    City = Convert.ToString(reader["carrier_address_city"]),
                    Country = Convert.ToString(reader["carrier_address_country"]),
                    State = Convert.ToString(reader["carrier_address_state"]),
                    Neighborhood = Convert.ToString(reader["carrier_address_neighborhood"]),
                    Number = Convert.ToInt32(reader["carrier_address_number"]),
                    StreetName = Convert.ToString(reader["carrier_address_street_name"])
                }
            },
            Invoice = new Invoice
            {
                Id = Convert.ToInt64(reader["invoice_id"]),
            }

        };



        private object[] Take(InvoiceCarrier invoiceCarrier)
        {

            return new object[]
            {
                    "@carrier_name", invoiceCarrier.Carrier.Name,
                    "@carrier_company_name", invoiceCarrier.Carrier.CompanyName,
                    "@carrier_cpf", invoiceCarrier.Carrier.CPF != null ? invoiceCarrier.Carrier.CPF.Value : null,
                    "@carrier_cnpj", invoiceCarrier.Carrier.CNPJ != null ? invoiceCarrier.Carrier.CNPJ.Value : null,
                    "@carrier_state_registration", invoiceCarrier.Carrier.StateRegistration,
                    "@carrier_freight_responsability",invoiceCarrier.Carrier.FreightResponsability,
                    "@carrier_person_type", invoiceCarrier.Carrier.PersonType,
                    "@carrier_address_number", invoiceCarrier.Carrier.Address.Number,
                    "@carrier_address_street_name", invoiceCarrier.Carrier.Address.StreetName,
                    "@carrier_address_neighborhood", invoiceCarrier.Carrier.Address.Neighborhood,
                    "@carrier_address_city", invoiceCarrier.Carrier.Address.City,
                    "@carrier_address_state", invoiceCarrier.Carrier.Address.State,
                    "@carrier_address_country", invoiceCarrier.Carrier.Address.Country,
                    "@invoice_id", invoiceCarrier.Invoice.Id
            };
        }


    }
}
