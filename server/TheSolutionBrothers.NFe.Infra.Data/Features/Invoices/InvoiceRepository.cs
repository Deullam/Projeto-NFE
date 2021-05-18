using TheSolutionBrothers.Nfe.Infra;
using TheSolutionBrothers.Nfe.Infra.Features.CNPJs;
using TheSolutionBrothers.Nfe.Infra.Features.CPFs;
using TheSolutionBrothers.Nfe.Infra.Features.KeysAccess;
using TheSolutionBrothers.NFe.Domain.Base;
using TheSolutionBrothers.NFe.Domain.Exceptions;
using TheSolutionBrothers.NFe.Domain.Features.Addresses;
using TheSolutionBrothers.NFe.Domain.Features.Carriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceCarriers;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceReceivers;
using TheSolutionBrothers.NFe.Domain.Features.Invoices;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceSenders;
using TheSolutionBrothers.NFe.Domain.Features.InvoiceTaxes;
using TheSolutionBrothers.NFe.Domain.Features.Receivers;
using TheSolutionBrothers.NFe.Domain.Features.Senders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolutionBrothers.NFe.Infra.Data.Contexts;
using System.Data.Entity;

namespace TheSolutionBrothers.NFe.Infra.Data.Features.Invoices
{
	public class InvoiceRepository : IInvoiceRepository
	{
		#region Querys
		private const string _insert = "INSERT INTO [dbo].[invoice] (nature_operation, key_access, number, status, entry_date, issue_date, carrier_id, receiver_id, sender_id) VALUES (@nature_operation, @key_access, @number, @status, @entry_date, @issue_date, @carrier_id, @receiver_id, @sender_id)";

		private const string _update = "UPDATE [dbo].[invoice] SET nature_operation = @nature_operation, key_access = @key_access, number = @number, status = @status, entry_date = @entry_date, issue_date = @issue_date, carrier_id = @carrier_id, receiver_id = @receiver_id, sender_id = @sender_id WHERE id_invoice = @id_invoice";
		//private const string _get = "SELECT DISTINCT I.id_invoice AS invoice_id_invoice, I.nature_operation AS invoice_nature_operation , I.key_access AS invoice_key_access,"
		//                            + "I.number AS invoice_number , I.status AS invoice_status, I.entry_date AS invoice_entry_date, i.issue_date AS invoice_issue_date, "
		//                            + "I.carrier_id AS invoice_carrier_id,"
		//                            + "I.receiver_id AS invoice_receiver_id, I.sender_id AS invoice_sender_id,"

		//                            + "c.name AS carrier_name, c.company_name AS carrier_company_name, c.cpf AS carrier_cpf, c.cnpj AS carrier_cnpj, c.state_registration AS carrier_state_registration, c.freight_responsability AS carrier_freight_responsability,c.person_type AS carrier_person_type, c.address_id AS carrier_address_id,"
		//                            + "AddCarrier.street_name AS carrier_street_name, AddCarrier.number AS carrier_number, AddCarrier.neighborhood AS carrier_neighborhood, AddCarrier.city AS carrier_city, AddCarrier.state AS carrier_state,AddCarrier.country AS carrier_country,"

		//                            + "r.name AS receiver_name, r.company_name AS receiver_company_name, r.cpf AS receiver_cpf, r.cnpj AS receiver_cnpj, r.state_registration AS receiver_state_registration, r.type AS receiver_type, r.address_id AS receiver_address_id,"
		//                            + "AddReceiver.street_name AS receiver_street_name, AddReceiver.number AS receiver_number, AddReceiver.neighborhood AS receiver_neighborhood, AddReceiver.city AS receiver_city, AddReceiver.state AS receiver_state,"
		//                            + "AddReceiver.country AS receiver_country,"

		//                            + "s.fancy_name AS sender_fancy_name, s.company_name AS sender_company_name, s.cnpj AS sender_cnpj, s.state_registration AS sender_state_registration, s.municipal_registration AS sender_municipal_registration, s.address_id AS sender_address_id,"
		//                            + "AddSender.street_name AS sender_street_name, AddSender.number AS sender_number, AddSender.neighborhood AS sender_neighborhood, AddSender.city AS sender_city, AddSender.state AS sender_state, AddSender.country AS sender_country,"

		//                            + "Icarrier.carrier_name AS invoice_carrier_name, Icarrier.carrier_company_name AS invoice_carrier_company_name, Icarrier.carrier_cpf AS invoice_carrier_cpf, Icarrier.carrier_cnpj AS invoice_carrier_cnpj,"
		//                            + "Icarrier.carrier_state_registration AS invoice_carrier_state_registration, Icarrier.carrier_freight_responsability AS invoice_carrier_freight_responsability, Icarrier.carrier_person_type AS invoice_carrier_person_type,"
		//                            + "Icarrier.carrier_address_street_name AS invoice_carrier_address_street_name, Icarrier.carrier_address_number AS invoice_carrier_address_number, Icarrier.carrier_address_neighborhood AS invoice_carrier_address_neighborhood,"
		//                            + "Icarrier.carrier_address_city AS invoice_carrier_address_city, Icarrier.carrier_address_state AS invoice_carrier_address_state, Icarrier.carrier_address_country AS invoice_carrier_address_country,"

		//                            + "Ireceiver.receiver_name AS invoice_receiver_name, Ireceiver.receiver_company_name AS invoice_receiver_company_name, Ireceiver.receiver_cpf AS invoice_receiver_cpf, Ireceiver.receiver_cnpj AS invoice_receiver_cnpj,"
		//                            + "Ireceiver.receiver_state_registration AS invoice_receiver_state_registration, Ireceiver.receiver_type AS invoice_receiver_type,"
		//                            + "Ireceiver.receiver_address_street_name AS invoice_receiver_address_street_name, Ireceiver.receiver_address_number AS invoice_receiver_address_number, Ireceiver.receiver_address_neighborhood AS invoice_receiver_address_neighborhood,"
		//                            + "Ireceiver.receiver_address_city AS invoice_receiver_address_city, Ireceiver.receiver_address_state AS invoice_receiver_address_state, Ireceiver.receiver_address_country AS invoice_receiver_address_country,"

		//                            + "Isender.sender_fancy_name AS invoice_sender_fancy_name, Isender.sender_company_name AS invoice_sender_company_name, Isender.sender_cnpj AS invoice_sender_cnpj, Isender.sender_state_registration AS invoice_sender_state_registration, Isender.sender_municipal_registration AS invoice_sender_municipal_registration,"
		//                            + "Isender.sender_address_street_name AS invoice_sender_address_street_name, Isender.sender_address_number AS invoice_sender_address_number, Isender.sender_address_neighborhood AS invoice_sender_address_neighborhood,"
		//                            + "Isender.sender_address_city AS invoice_sender_address_city, Isender.sender_address_state AS invoice_sender_address_state,"
		//                            + "Isender.sender_address_country AS invoice_sender_address_country,"

		//                            + "ITax.icms_value, ITax.freight, ITax.ipi_value, ITax.total_value_products, ITax.total_value_invoice "
		//                            + "FROM invoice AS I "
		//                            + "LEFT JOIN carrier AS c ON id_carrier = carrier_id  LEFT JOIN address AS AddCarrier ON AddCarrier.id_address = c.address_id "
		//                            + "INNER JOIN receiver AS r ON id_receiver = receiver_id  INNER JOIN address AS AddReceiver ON AddReceiver.id_address = r.address_id "
		//                            + "INNER JOIN sender AS s ON id_sender = sender_id INNER JOIN address AS AddSender ON AddSender.id_address = s.address_id "
		//                            + "LEFT JOIN invoice_carrier AS Icarrier ON id_carrier = carrier_id "
		//                            + "LEFT JOIN invoice_receiver AS Ireceiver ON id_receiver = receiver_id "
		//                            + "LEFT JOIN invoice_sender AS Isender ON id_sender = sender_id "
		//                            + "LEFT JOIN invoice_tax AS ITax ON id_invoice = ITax.invoice_id "
		//                            + "WHERE id_invoice = @id_invoice";

		private const string _get = "SELECT DISTINCT I.id_invoice AS invoice_id_invoice, I.nature_operation AS invoice_nature_operation , I.key_access AS invoice_key_access,"
									+ "I.number AS invoice_number , I.status AS invoice_status, I.entry_date AS invoice_entry_date, i.issue_date AS invoice_issue_date, "
									+ "I.carrier_id AS carrier_id,"
									+ "I.receiver_id AS receiver_id, I.sender_id AS sender_id,"

									+ "c.name AS carrier_name, c.company_name AS carrier_company_name, c.cpf AS carrier_cpf, c.cnpj AS carrier_cnpj, c.state_registration AS carrier_state_registration, c.freight_responsability AS carrier_freight_responsability,c.person_type AS carrier_person_type, c.address_id AS carrier_address_id,"
									+ "AddCarrier.street_name AS carrier_street_name, AddCarrier.number AS carrier_number, AddCarrier.neighborhood AS carrier_neighborhood, AddCarrier.city AS carrier_city, AddCarrier.state AS carrier_state,AddCarrier.country AS carrier_country,"

									+ "r.name AS receiver_name, r.company_name AS receiver_company_name, r.cpf AS receiver_cpf, r.cnpj AS receiver_cnpj, r.state_registration AS receiver_state_registration, r.type AS receiver_type, r.address_id AS receiver_address_id,"
									+ "AddReceiver.street_name AS receiver_street_name, AddReceiver.number AS receiver_number, AddReceiver.neighborhood AS receiver_neighborhood, AddReceiver.city AS receiver_city, AddReceiver.state AS receiver_state,"
									+ "AddReceiver.country AS receiver_country,"

									+ "s.fancy_name AS sender_fancy_name, s.company_name AS sender_company_name, s.cnpj AS sender_cnpj, s.state_registration AS sender_state_registration, s.municipal_registration AS sender_municipal_registration, s.address_id AS sender_address_id,"
									+ "AddSender.street_name AS sender_street_name, AddSender.number AS sender_number, AddSender.neighborhood AS sender_neighborhood, AddSender.city AS sender_city, AddSender.state AS sender_state, AddSender.country AS sender_country,"

									+ "Icarrier.id_invoice_carrier AS invoice_carrier_id, Icarrier.carrier_name AS invoice_carrier_name, Icarrier.carrier_company_name AS invoice_carrier_company_name, Icarrier.carrier_cpf AS invoice_carrier_cpf, Icarrier.carrier_cnpj AS invoice_carrier_cnpj,"
									+ "Icarrier.carrier_state_registration AS invoice_carrier_state_registration, Icarrier.carrier_freight_responsability AS invoice_carrier_freight_responsability, Icarrier.carrier_person_type AS invoice_carrier_person_type,"
									+ "Icarrier.carrier_address_street_name AS invoice_carrier_address_street_name, Icarrier.carrier_address_number AS invoice_carrier_address_number, Icarrier.carrier_address_neighborhood AS invoice_carrier_address_neighborhood,"
									+ "Icarrier.carrier_address_city AS invoice_carrier_address_city, Icarrier.carrier_address_state AS invoice_carrier_address_state, Icarrier.carrier_address_country AS invoice_carrier_address_country,"

									+ "Ireceiver.id_invoice_receiver AS invoice_receiver_id, Ireceiver.receiver_name AS invoice_receiver_name, Ireceiver.receiver_company_name AS invoice_receiver_company_name, Ireceiver.receiver_cpf AS invoice_receiver_cpf, Ireceiver.receiver_cnpj AS invoice_receiver_cnpj,"
									+ "Ireceiver.receiver_state_registration AS invoice_receiver_state_registration, Ireceiver.receiver_type AS invoice_receiver_type,"
									+ "Ireceiver.receiver_address_street_name AS invoice_receiver_address_street_name, Ireceiver.receiver_address_number AS invoice_receiver_address_number, Ireceiver.receiver_address_neighborhood AS invoice_receiver_address_neighborhood,"
									+ "Ireceiver.receiver_address_city AS invoice_receiver_address_city, Ireceiver.receiver_address_state AS invoice_receiver_address_state, Ireceiver.receiver_address_country AS invoice_receiver_address_country,"

									+ "Isender.id_invoice_sender AS invoice_sender_id, Isender.sender_fancy_name AS invoice_sender_fancy_name, Isender.sender_company_name AS invoice_sender_company_name, Isender.sender_cnpj AS invoice_sender_cnpj, Isender.sender_state_registration AS invoice_sender_state_registration, Isender.sender_municipal_registration AS invoice_sender_municipal_registration,"
									+ "Isender.sender_address_street_name AS invoice_sender_address_street_name, Isender.sender_address_number AS invoice_sender_address_number, Isender.sender_address_neighborhood AS invoice_sender_address_neighborhood,"
									+ "Isender.sender_address_city AS invoice_sender_address_city, Isender.sender_address_state AS invoice_sender_address_state,"
									+ "Isender.sender_address_country AS invoice_sender_address_country,"

									+ "ITax.icms_value, ITax.freight, ITax.ipi_value, ITax.total_value_products, ITax.total_value_invoice "
									+ "FROM invoice AS I "
									+ "LEFT JOIN carrier AS c ON c.id_carrier = I.carrier_id  LEFT JOIN address AS AddCarrier ON AddCarrier.id_address = c.address_id "
									+ "INNER JOIN receiver AS r ON r.id_receiver = I.receiver_id  INNER JOIN address AS AddReceiver ON AddReceiver.id_address = r.address_id "
									+ "INNER JOIN sender AS s ON s.id_sender = I.sender_id INNER JOIN address AS AddSender ON AddSender.id_address = s.address_id "
									+ "LEFT JOIN invoice_carrier AS Icarrier ON (Icarrier.invoice_id = I.id_invoice) "
									+ "LEFT JOIN invoice_receiver AS Ireceiver ON (Ireceiver.invoice_id = I.id_invoice) "
									+ "LEFT JOIN invoice_sender AS Isender ON (Isender.invoice_id = I.id_invoice) "
									+ "LEFT JOIN invoice_tax AS ITax ON I.id_invoice = ITax.invoice_id "
									+ "WHERE I.id_invoice = @id_invoice";

		private const string _getAll = @"SELECT * FROM (
	                                SELECT I.id_invoice AS invoice_id_invoice, I.nature_operation AS invoice_nature_operation , I.key_access AS invoice_key_access,
                                    I.number AS invoice_number , I.status AS invoice_status, I.entry_date AS invoice_entry_date, i.issue_date AS invoice_issue_date, 
                                    I.carrier_id,
                                    I.receiver_id, I.sender_id,

                                    c.name AS carrier_name, c.company_name AS carrier_company_name, c.cpf AS carrier_cpf, c.cnpj AS carrier_cnpj, c.state_registration AS carrier_state_registration, c.freight_responsability AS carrier_freight_responsability,c.person_type AS carrier_person_type, c.address_id AS carrier_address_id,
                                    AddCarrier.street_name AS carrier_street_name, AddCarrier.number AS carrier_number, AddCarrier.neighborhood AS carrier_neighborhood, AddCarrier.city AS carrier_city, AddCarrier.state AS carrier_state,AddCarrier.country AS carrier_country,

                                    r.name AS receiver_name, r.company_name AS receiver_company_name, r.cpf AS receiver_cpf, r.cnpj AS receiver_cnpj, r.state_registration AS receiver_state_registration, r.type AS receiver_type, r.address_id AS receiver_address_id,
                                    AddReceiver.street_name AS receiver_street_name, AddReceiver.number AS receiver_number, AddReceiver.neighborhood AS receiver_neighborhood, AddReceiver.city AS receiver_city, AddReceiver.state AS receiver_state,
                                    AddReceiver.country AS receiver_country,

                                    s.fancy_name AS sender_fancy_name, s.company_name AS sender_company_name, s.cnpj AS sender_cnpj, s.state_registration AS sender_state_registration, s.municipal_registration AS sender_municipal_registration, s.address_id AS sender_address_id,
                                    AddSender.street_name AS sender_street_name, AddSender.number AS sender_number, AddSender.neighborhood AS sender_neighborhood, AddSender.city AS sender_city, AddSender.state AS sender_state, AddSender.country AS sender_country,

                                    Icarrier.id_invoice_carrier AS invoice_carrier_id, Icarrier.carrier_name AS invoice_carrier_name, Icarrier.carrier_company_name AS invoice_carrier_company_name, Icarrier.carrier_cpf AS invoice_carrier_cpf, Icarrier.carrier_cnpj AS invoice_carrier_cnpj,
                                    Icarrier.carrier_state_registration AS invoice_carrier_state_registration, Icarrier.carrier_freight_responsability AS invoice_carrier_freight_responsability, Icarrier.carrier_person_type AS invoice_carrier_person_type,
                                    Icarrier.carrier_address_street_name AS invoice_carrier_address_street_name, Icarrier.carrier_address_number AS invoice_carrier_address_number, Icarrier.carrier_address_neighborhood AS invoice_carrier_address_neighborhood,
                                    Icarrier.carrier_address_city AS invoice_carrier_address_city, Icarrier.carrier_address_state AS invoice_carrier_address_state, Icarrier.carrier_address_country AS invoice_carrier_address_country,

                                    Ireceiver.id_invoice_receiver AS invoice_receiver_id, Ireceiver.receiver_name AS invoice_receiver_name, Ireceiver.receiver_company_name AS invoice_receiver_company_name, Ireceiver.receiver_cpf AS invoice_receiver_cpf, Ireceiver.receiver_cnpj AS invoice_receiver_cnpj,
                                    Ireceiver.receiver_state_registration AS invoice_receiver_state_registration, Ireceiver.receiver_type AS invoice_receiver_type,
                                    Ireceiver.receiver_address_street_name AS invoice_receiver_address_street_name, Ireceiver.receiver_address_number AS invoice_receiver_address_number, Ireceiver.receiver_address_neighborhood AS invoice_receiver_address_neighborhood,
                                    Ireceiver.receiver_address_city AS invoice_receiver_address_city, Ireceiver.receiver_address_state AS invoice_receiver_address_state, Ireceiver.receiver_address_country AS invoice_receiver_address_country,

                                    Isender.id_invoice_sender AS invoice_sender_id, Isender.sender_fancy_name AS invoice_sender_fancy_name, Isender.sender_company_name AS invoice_sender_company_name, Isender.sender_cnpj AS invoice_sender_cnpj, Isender.sender_state_registration AS invoice_sender_state_registration, Isender.sender_municipal_registration AS invoice_sender_municipal_registration,
                                    Isender.sender_address_street_name AS invoice_sender_address_street_name, Isender.sender_address_number AS invoice_sender_address_number, Isender.sender_address_neighborhood AS invoice_sender_address_neighborhood,
                                    Isender.sender_address_city AS invoice_sender_address_city, Isender.sender_address_state AS invoice_sender_address_state,
                                    Isender.sender_address_country AS invoice_sender_address_country,

                                    ITax.icms_value, ITax.freight, ITax.ipi_value, ITax.total_value_products, ITax.total_value_invoice,
									ROW_NUMBER() OVER(PARTITION BY I.number ORDER BY I.id_invoice DESC) rn 
                                    FROM invoice AS I 
                                    LEFT JOIN carrier AS c ON c.id_carrier = I.carrier_id  LEFT JOIN address AS AddCarrier ON AddCarrier.id_address = c.address_id 
                                    INNER JOIN receiver AS r ON r.id_receiver = I.receiver_id  INNER JOIN address AS AddReceiver ON AddReceiver.id_address = r.address_id 
                                    INNER JOIN sender AS s ON s.id_sender = I.sender_id INNER JOIN address AS AddSender ON AddSender.id_address = s.address_id 
                                    LEFT JOIN invoice_carrier AS Icarrier ON (Icarrier.invoice_id = I.id_invoice) 
                                    LEFT JOIN invoice_receiver AS Ireceiver ON (Ireceiver.invoice_id = I.id_invoice) 
                                    LEFT JOIN invoice_sender AS Isender ON (Isender.invoice_id = I.id_invoice) 
                                    LEFT JOIN invoice_tax AS ITax ON I.id_invoice = ITax.invoice_id 
                                    ) a
                                    WHERE rn = 1";



		private const string _delete = "DELETE from invoice where id_invoice = @id_invoice";

		private const string _getByCarrier = @"SELECT * FROM (

                                    SELECT I.id_invoice AS invoice_id_invoice, I.nature_operation AS invoice_nature_operation , I.key_access AS invoice_key_access,
                                    I.number AS invoice_number , I.status AS invoice_status, I.entry_date AS invoice_entry_date, i.issue_date AS invoice_issue_date,
                                    I.carrier_id,
                                    I.receiver_id, I.sender_id,

                                    c.name AS carrier_name, c.company_name AS carrier_company_name, c.cpf AS carrier_cpf, c.cnpj AS carrier_cnpj, c.state_registration AS carrier_state_registration, c.freight_responsability AS carrier_freight_responsability, c.person_type AS carrier_person_type, c.address_id AS carrier_address_id,
                                    AddCarrier.street_name AS carrier_street_name, AddCarrier.number AS carrier_number, AddCarrier.neighborhood AS carrier_neighborhood, AddCarrier.city AS carrier_city, AddCarrier.state AS carrier_state, AddCarrier.country AS carrier_country,

                                    r.name AS receiver_name, r.company_name AS receiver_company_name, r.cpf AS receiver_cpf, r.cnpj AS receiver_cnpj, r.state_registration AS receiver_state_registration, r.type AS receiver_type, r.address_id AS receiver_address_id,
                                    AddReceiver.street_name AS receiver_street_name, AddReceiver.number AS receiver_number, AddReceiver.neighborhood AS receiver_neighborhood, AddReceiver.city AS receiver_city, AddReceiver.state AS receiver_state,
                                    AddReceiver.country AS receiver_country,

                                    s.fancy_name AS sender_fancy_name, s.company_name AS sender_company_name, s.cnpj AS sender_cnpj, s.state_registration AS sender_state_registration, s.municipal_registration AS sender_municipal_registration, s.address_id AS sender_address_id,
                                    AddSender.street_name AS sender_street_name, AddSender.number AS sender_number, AddSender.neighborhood AS sender_neighborhood, AddSender.city AS sender_city, AddSender.state AS sender_state, AddSender.country AS sender_country,

                                    Icarrier.id_invoice_carrier AS invoice_carrier_id, Icarrier.carrier_name AS invoice_carrier_name, Icarrier.carrier_company_name AS invoice_carrier_company_name, Icarrier.carrier_cpf AS invoice_carrier_cpf, Icarrier.carrier_cnpj AS invoice_carrier_cnpj,
                                    Icarrier.carrier_state_registration AS invoice_carrier_state_registration, Icarrier.carrier_freight_responsability AS invoice_carrier_freight_responsability, Icarrier.carrier_person_type AS invoice_carrier_person_type,
                                    Icarrier.carrier_address_street_name AS invoice_carrier_address_street_name, Icarrier.carrier_address_number AS invoice_carrier_address_number, Icarrier.carrier_address_neighborhood AS invoice_carrier_address_neighborhood,
                                    Icarrier.carrier_address_city AS invoice_carrier_address_city, Icarrier.carrier_address_state AS invoice_carrier_address_state, Icarrier.carrier_address_country AS invoice_carrier_address_country,

                                    Ireceiver.id_invoice_receiver AS invoice_receiver_id, Ireceiver.receiver_name AS invoice_receiver_name, Ireceiver.receiver_company_name AS invoice_receiver_company_name, Ireceiver.receiver_cpf AS invoice_receiver_cpf, Ireceiver.receiver_cnpj AS invoice_receiver_cnpj,
                                    Ireceiver.receiver_state_registration AS invoice_receiver_state_registration, Ireceiver.receiver_type AS invoice_receiver_type,
                                    Ireceiver.receiver_address_street_name AS invoice_receiver_address_street_name, Ireceiver.receiver_address_number AS invoice_receiver_address_number, Ireceiver.receiver_address_neighborhood AS invoice_receiver_address_neighborhood,
                                    Ireceiver.receiver_address_city AS invoice_receiver_address_city, Ireceiver.receiver_address_state AS invoice_receiver_address_state, Ireceiver.receiver_address_country AS invoice_receiver_address_country,

                                    Isender.id_invoice_sender AS invoice_sender_id, Isender.sender_fancy_name AS invoice_sender_fancy_name, Isender.sender_company_name AS invoice_sender_company_name, Isender.sender_cnpj AS invoice_sender_cnpj, Isender.sender_state_registration AS invoice_sender_state_registration, Isender.sender_municipal_registration AS invoice_sender_municipal_registration,
                                    Isender.sender_address_street_name AS invoice_sender_address_street_name, Isender.sender_address_number AS invoice_sender_address_number, Isender.sender_address_neighborhood AS invoice_sender_address_neighborhood,
                                    Isender.sender_address_city AS invoice_sender_address_city, Isender.sender_address_state AS invoice_sender_address_state,
                                    Isender.sender_address_country AS invoice_sender_address_country,

                                    ITax.icms_value, ITax.freight, ITax.ipi_value, ITax.total_value_products, ITax.total_value_invoice,
                                    ROW_NUMBER() OVER(PARTITION BY I.number ORDER BY I.id_invoice DESC) rn
                                    FROM invoice AS I
                                    LEFT JOIN carrier AS c ON c.id_carrier = I.carrier_id LEFT JOIN address AS AddCarrier ON AddCarrier.id_address = c.address_id
                                    INNER JOIN receiver AS r ON r.id_receiver = I.receiver_id  INNER JOIN address AS AddReceiver ON AddReceiver.id_address = r.address_id
                                    INNER JOIN sender AS s ON s.id_sender = I.sender_id INNER JOIN address AS AddSender ON AddSender.id_address = s.address_id
                                    LEFT JOIN invoice_carrier AS Icarrier ON (Icarrier.invoice_id = I.id_invoice)
                                    LEFT JOIN invoice_receiver AS Ireceiver ON(Ireceiver.invoice_id = I.id_invoice)
                                    LEFT JOIN invoice_sender AS Isender ON(Isender.invoice_id = I.id_invoice)
                                    LEFT JOIN invoice_tax AS ITax ON I.id_invoice = ITax.invoice_id 
                                    WHERE carrier_id = @carrier_id) a
                                    WHERE rn = 1";

		private const string _getByReceiver = @"SELECT * FROM (
	                                SELECT I.id_invoice AS invoice_id_invoice, I.nature_operation AS invoice_nature_operation , I.key_access AS invoice_key_access,
                                    I.number AS invoice_number , I.status AS invoice_status, I.entry_date AS invoice_entry_date, i.issue_date AS invoice_issue_date, 
                                    I.carrier_id,
                                    I.receiver_id, I.sender_id,

                                    c.name AS carrier_name, c.company_name AS carrier_company_name, c.cpf AS carrier_cpf, c.cnpj AS carrier_cnpj, c.state_registration AS carrier_state_registration, c.freight_responsability AS carrier_freight_responsability,c.person_type AS carrier_person_type, c.address_id AS carrier_address_id,
                                    AddCarrier.street_name AS carrier_street_name, AddCarrier.number AS carrier_number, AddCarrier.neighborhood AS carrier_neighborhood, AddCarrier.city AS carrier_city, AddCarrier.state AS carrier_state,AddCarrier.country AS carrier_country,

                                    r.name AS receiver_name, r.company_name AS receiver_company_name, r.cpf AS receiver_cpf, r.cnpj AS receiver_cnpj, r.state_registration AS receiver_state_registration, r.type AS receiver_type, r.address_id AS receiver_address_id,
                                    AddReceiver.street_name AS receiver_street_name, AddReceiver.number AS receiver_number, AddReceiver.neighborhood AS receiver_neighborhood, AddReceiver.city AS receiver_city, AddReceiver.state AS receiver_state,
                                    AddReceiver.country AS receiver_country,

                                    s.fancy_name AS sender_fancy_name, s.company_name AS sender_company_name, s.cnpj AS sender_cnpj, s.state_registration AS sender_state_registration, s.municipal_registration AS sender_municipal_registration, s.address_id AS sender_address_id,
                                    AddSender.street_name AS sender_street_name, AddSender.number AS sender_number, AddSender.neighborhood AS sender_neighborhood, AddSender.city AS sender_city, AddSender.state AS sender_state, AddSender.country AS sender_country,

                                    Icarrier.id_invoice_carrier AS invoice_carrier_id, Icarrier.carrier_name AS invoice_carrier_name, Icarrier.carrier_company_name AS invoice_carrier_company_name, Icarrier.carrier_cpf AS invoice_carrier_cpf, Icarrier.carrier_cnpj AS invoice_carrier_cnpj,
                                    Icarrier.carrier_state_registration AS invoice_carrier_state_registration, Icarrier.carrier_freight_responsability AS invoice_carrier_freight_responsability, Icarrier.carrier_person_type AS invoice_carrier_person_type,
                                    Icarrier.carrier_address_street_name AS invoice_carrier_address_street_name, Icarrier.carrier_address_number AS invoice_carrier_address_number, Icarrier.carrier_address_neighborhood AS invoice_carrier_address_neighborhood,
                                    Icarrier.carrier_address_city AS invoice_carrier_address_city, Icarrier.carrier_address_state AS invoice_carrier_address_state, Icarrier.carrier_address_country AS invoice_carrier_address_country,

                                    Ireceiver.id_invoice_receiver AS invoice_receiver_id, Ireceiver.receiver_name AS invoice_receiver_name, Ireceiver.receiver_company_name AS invoice_receiver_company_name, Ireceiver.receiver_cpf AS invoice_receiver_cpf, Ireceiver.receiver_cnpj AS invoice_receiver_cnpj,
                                    Ireceiver.receiver_state_registration AS invoice_receiver_state_registration, Ireceiver.receiver_type AS invoice_receiver_type,
                                    Ireceiver.receiver_address_street_name AS invoice_receiver_address_street_name, Ireceiver.receiver_address_number AS invoice_receiver_address_number, Ireceiver.receiver_address_neighborhood AS invoice_receiver_address_neighborhood,
                                    Ireceiver.receiver_address_city AS invoice_receiver_address_city, Ireceiver.receiver_address_state AS invoice_receiver_address_state, Ireceiver.receiver_address_country AS invoice_receiver_address_country,

                                    Isender.id_invoice_sender AS invoice_sender_id, Isender.sender_fancy_name AS invoice_sender_fancy_name, Isender.sender_company_name AS invoice_sender_company_name, Isender.sender_cnpj AS invoice_sender_cnpj, Isender.sender_state_registration AS invoice_sender_state_registration, Isender.sender_municipal_registration AS invoice_sender_municipal_registration,
                                    Isender.sender_address_street_name AS invoice_sender_address_street_name, Isender.sender_address_number AS invoice_sender_address_number, Isender.sender_address_neighborhood AS invoice_sender_address_neighborhood,
                                    Isender.sender_address_city AS invoice_sender_address_city, Isender.sender_address_state AS invoice_sender_address_state,
                                    Isender.sender_address_country AS invoice_sender_address_country,

                                    ITax.icms_value, ITax.freight, ITax.ipi_value, ITax.total_value_products, ITax.total_value_invoice,
									ROW_NUMBER() OVER(PARTITION BY I.number ORDER BY I.id_invoice DESC) rn 
                                    FROM invoice AS I 
                                    LEFT JOIN carrier AS c ON c.id_carrier = I.carrier_id  LEFT JOIN address AS AddCarrier ON AddCarrier.id_address = c.address_id 
                                    INNER JOIN receiver AS r ON r.id_receiver = I.receiver_id  INNER JOIN address AS AddReceiver ON AddReceiver.id_address = r.address_id 
                                    INNER JOIN sender AS s ON s.id_sender = I.sender_id INNER JOIN address AS AddSender ON AddSender.id_address = s.address_id 
                                    LEFT JOIN invoice_carrier AS Icarrier ON (Icarrier.invoice_id = I.id_invoice) 
                                    LEFT JOIN invoice_receiver AS Ireceiver ON (Ireceiver.invoice_id = I.id_invoice) 
                                    LEFT JOIN invoice_sender AS Isender ON (Isender.invoice_id = I.id_invoice) 
                                    LEFT JOIN invoice_tax AS ITax ON I.id_invoice = ITax.invoice_id 
                                    WHERE receiver_id = @receiver_id) a
                                    WHERE rn = 1";


		private const string _getBySender = @"SELECT * FROM (

                                    SELECT I.id_invoice AS invoice_id_invoice, I.nature_operation AS invoice_nature_operation , I.key_access AS invoice_key_access,
                                    I.number AS invoice_number , I.status AS invoice_status, I.entry_date AS invoice_entry_date, i.issue_date AS invoice_issue_date,
                                    I.carrier_id,
                                    I.receiver_id, I.sender_id,

                                    c.name AS carrier_name, c.company_name AS carrier_company_name, c.cpf AS carrier_cpf, c.cnpj AS carrier_cnpj, c.state_registration AS carrier_state_registration, c.freight_responsability AS carrier_freight_responsability, c.person_type AS carrier_person_type, c.address_id AS carrier_address_id,
                                    AddCarrier.street_name AS carrier_street_name, AddCarrier.number AS carrier_number, AddCarrier.neighborhood AS carrier_neighborhood, AddCarrier.city AS carrier_city, AddCarrier.state AS carrier_state, AddCarrier.country AS carrier_country,

                                    r.name AS receiver_name, r.company_name AS receiver_company_name, r.cpf AS receiver_cpf, r.cnpj AS receiver_cnpj, r.state_registration AS receiver_state_registration, r.type AS receiver_type, r.address_id AS receiver_address_id,
                                    AddReceiver.street_name AS receiver_street_name, AddReceiver.number AS receiver_number, AddReceiver.neighborhood AS receiver_neighborhood, AddReceiver.city AS receiver_city, AddReceiver.state AS receiver_state,
                                    AddReceiver.country AS receiver_country,

                                    s.fancy_name AS sender_fancy_name, s.company_name AS sender_company_name, s.cnpj AS sender_cnpj, s.state_registration AS sender_state_registration, s.municipal_registration AS sender_municipal_registration, s.address_id AS sender_address_id,
                                    AddSender.street_name AS sender_street_name, AddSender.number AS sender_number, AddSender.neighborhood AS sender_neighborhood, AddSender.city AS sender_city, AddSender.state AS sender_state, AddSender.country AS sender_country,

                                    Icarrier.id_invoice_carrier AS invoice_carrier_id, Icarrier.carrier_name AS invoice_carrier_name, Icarrier.carrier_company_name AS invoice_carrier_company_name, Icarrier.carrier_cpf AS invoice_carrier_cpf, Icarrier.carrier_cnpj AS invoice_carrier_cnpj,
                                    Icarrier.carrier_state_registration AS invoice_carrier_state_registration, Icarrier.carrier_freight_responsability AS invoice_carrier_freight_responsability, Icarrier.carrier_person_type AS invoice_carrier_person_type,
                                    Icarrier.carrier_address_street_name AS invoice_carrier_address_street_name, Icarrier.carrier_address_number AS invoice_carrier_address_number, Icarrier.carrier_address_neighborhood AS invoice_carrier_address_neighborhood,
                                    Icarrier.carrier_address_city AS invoice_carrier_address_city, Icarrier.carrier_address_state AS invoice_carrier_address_state, Icarrier.carrier_address_country AS invoice_carrier_address_country,

                                    Ireceiver.id_invoice_receiver AS invoice_receiver_id, Ireceiver.receiver_name AS invoice_receiver_name, Ireceiver.receiver_company_name AS invoice_receiver_company_name, Ireceiver.receiver_cpf AS invoice_receiver_cpf, Ireceiver.receiver_cnpj AS invoice_receiver_cnpj,
                                    Ireceiver.receiver_state_registration AS invoice_receiver_state_registration, Ireceiver.receiver_type AS invoice_receiver_type,
                                    Ireceiver.receiver_address_street_name AS invoice_receiver_address_street_name, Ireceiver.receiver_address_number AS invoice_receiver_address_number, Ireceiver.receiver_address_neighborhood AS invoice_receiver_address_neighborhood,
                                    Ireceiver.receiver_address_city AS invoice_receiver_address_city, Ireceiver.receiver_address_state AS invoice_receiver_address_state, Ireceiver.receiver_address_country AS invoice_receiver_address_country,

                                    Isender.id_invoice_sender AS invoice_sender_id, Isender.sender_fancy_name AS invoice_sender_fancy_name, Isender.sender_company_name AS invoice_sender_company_name, Isender.sender_cnpj AS invoice_sender_cnpj, Isender.sender_state_registration AS invoice_sender_state_registration, Isender.sender_municipal_registration AS invoice_sender_municipal_registration,
                                    Isender.sender_address_street_name AS invoice_sender_address_street_name, Isender.sender_address_number AS invoice_sender_address_number, Isender.sender_address_neighborhood AS invoice_sender_address_neighborhood,
                                    Isender.sender_address_city AS invoice_sender_address_city, Isender.sender_address_state AS invoice_sender_address_state,
                                    Isender.sender_address_country AS invoice_sender_address_country,

                                    ITax.icms_value, ITax.freight, ITax.ipi_value, ITax.total_value_products, ITax.total_value_invoice,
                                    ROW_NUMBER() OVER(PARTITION BY I.number ORDER BY I.id_invoice DESC) rn
                                    FROM invoice AS I
                                    LEFT JOIN carrier AS c ON c.id_carrier = I.carrier_id LEFT JOIN address AS AddCarrier ON AddCarrier.id_address = c.address_id
                                    INNER JOIN receiver AS r ON r.id_receiver = I.receiver_id  INNER JOIN address AS AddReceiver ON AddReceiver.id_address = r.address_id
                                    INNER JOIN sender AS s ON s.id_sender = I.sender_id INNER JOIN address AS AddSender ON AddSender.id_address = s.address_id
                                    LEFT JOIN invoice_carrier AS Icarrier ON (Icarrier.invoice_id = I.id_invoice)
                                    LEFT JOIN invoice_receiver AS Ireceiver ON(Ireceiver.invoice_id = I.id_invoice)
                                    LEFT JOIN invoice_sender AS Isender ON(Isender.invoice_id = I.id_invoice)
                                    LEFT JOIN invoice_tax AS ITax ON I.id_invoice = ITax.invoice_id 
                                    WHERE sender_id = @sender_id) a
                                    WHERE rn = 1";

		private const string _getByKeyAccess = @"SELECT * FROM (

                                    SELECT I.id_invoice AS invoice_id_invoice, I.nature_operation AS invoice_nature_operation , I.key_access AS invoice_key_access,
                                    I.number AS invoice_number , I.status AS invoice_status, I.entry_date AS invoice_entry_date, i.issue_date AS invoice_issue_date,
                                    I.carrier_id,
                                    I.receiver_id, I.sender_id,

                                    c.name AS carrier_name, c.company_name AS carrier_company_name, c.cpf AS carrier_cpf, c.cnpj AS carrier_cnpj, c.state_registration AS carrier_state_registration, c.freight_responsability AS carrier_freight_responsability, c.person_type AS carrier_person_type, c.address_id AS carrier_address_id,
                                    AddCarrier.street_name AS carrier_street_name, AddCarrier.number AS carrier_number, AddCarrier.neighborhood AS carrier_neighborhood, AddCarrier.city AS carrier_city, AddCarrier.state AS carrier_state, AddCarrier.country AS carrier_country,

                                    r.name AS receiver_name, r.company_name AS receiver_company_name, r.cpf AS receiver_cpf, r.cnpj AS receiver_cnpj, r.state_registration AS receiver_state_registration, r.type AS receiver_type, r.address_id AS receiver_address_id,
                                    AddReceiver.street_name AS receiver_street_name, AddReceiver.number AS receiver_number, AddReceiver.neighborhood AS receiver_neighborhood, AddReceiver.city AS receiver_city, AddReceiver.state AS receiver_state,
                                    AddReceiver.country AS receiver_country,

                                    s.fancy_name AS sender_fancy_name, s.company_name AS sender_company_name, s.cnpj AS sender_cnpj, s.state_registration AS sender_state_registration, s.municipal_registration AS sender_municipal_registration, s.address_id AS sender_address_id,
                                    AddSender.street_name AS sender_street_name, AddSender.number AS sender_number, AddSender.neighborhood AS sender_neighborhood, AddSender.city AS sender_city, AddSender.state AS sender_state, AddSender.country AS sender_country,

                                    Icarrier.id_invoice_carrier AS invoice_carrier_id, Icarrier.carrier_name AS invoice_carrier_name, Icarrier.carrier_company_name AS invoice_carrier_company_name, Icarrier.carrier_cpf AS invoice_carrier_cpf, Icarrier.carrier_cnpj AS invoice_carrier_cnpj,
                                    Icarrier.carrier_state_registration AS invoice_carrier_state_registration, Icarrier.carrier_freight_responsability AS invoice_carrier_freight_responsability, Icarrier.carrier_person_type AS invoice_carrier_person_type,
                                    Icarrier.carrier_address_street_name AS invoice_carrier_address_street_name, Icarrier.carrier_address_number AS invoice_carrier_address_number, Icarrier.carrier_address_neighborhood AS invoice_carrier_address_neighborhood,
                                    Icarrier.carrier_address_city AS invoice_carrier_address_city, Icarrier.carrier_address_state AS invoice_carrier_address_state, Icarrier.carrier_address_country AS invoice_carrier_address_country,

                                    Ireceiver.id_invoice_receiver AS invoice_receiver_id, Ireceiver.receiver_name AS invoice_receiver_name, Ireceiver.receiver_company_name AS invoice_receiver_company_name, Ireceiver.receiver_cpf AS invoice_receiver_cpf, Ireceiver.receiver_cnpj AS invoice_receiver_cnpj,
                                    Ireceiver.receiver_state_registration AS invoice_receiver_state_registration, Ireceiver.receiver_type AS invoice_receiver_type,
                                    Ireceiver.receiver_address_street_name AS invoice_receiver_address_street_name, Ireceiver.receiver_address_number AS invoice_receiver_address_number, Ireceiver.receiver_address_neighborhood AS invoice_receiver_address_neighborhood,
                                    Ireceiver.receiver_address_city AS invoice_receiver_address_city, Ireceiver.receiver_address_state AS invoice_receiver_address_state, Ireceiver.receiver_address_country AS invoice_receiver_address_country,

                                    Isender.id_invoice_sender AS invoice_sender_id, Isender.sender_fancy_name AS invoice_sender_fancy_name, Isender.sender_company_name AS invoice_sender_company_name, Isender.sender_cnpj AS invoice_sender_cnpj, Isender.sender_state_registration AS invoice_sender_state_registration, Isender.sender_municipal_registration AS invoice_sender_municipal_registration,
                                    Isender.sender_address_street_name AS invoice_sender_address_street_name, Isender.sender_address_number AS invoice_sender_address_number, Isender.sender_address_neighborhood AS invoice_sender_address_neighborhood,
                                    Isender.sender_address_city AS invoice_sender_address_city, Isender.sender_address_state AS invoice_sender_address_state,
                                    Isender.sender_address_country AS invoice_sender_address_country,

                                    ITax.icms_value, ITax.freight, ITax.ipi_value, ITax.total_value_products, ITax.total_value_invoice,
                                    ROW_NUMBER() OVER(PARTITION BY I.number ORDER BY I.id_invoice DESC) rn
                                    FROM invoice AS I
                                    LEFT JOIN carrier AS c ON c.id_carrier = I.carrier_id LEFT JOIN address AS AddCarrier ON AddCarrier.id_address = c.address_id
                                    INNER JOIN receiver AS r ON r.id_receiver = I.receiver_id  INNER JOIN address AS AddReceiver ON AddReceiver.id_address = r.address_id
                                    INNER JOIN sender AS s ON s.id_sender = I.sender_id INNER JOIN address AS AddSender ON AddSender.id_address = s.address_id
                                    LEFT JOIN invoice_carrier AS Icarrier ON (Icarrier.invoice_id = I.id_invoice)
                                    LEFT JOIN invoice_receiver AS Ireceiver ON(Ireceiver.invoice_id = I.id_invoice)
                                    LEFT JOIN invoice_sender AS Isender ON(Isender.invoice_id = I.id_invoice)
                                    LEFT JOIN invoice_tax AS ITax ON I.id_invoice = ITax.invoice_id 
                                    WHERE key_access = @key_access) a
                                    WHERE rn = 1";


		private const string _getByNumber = @"SELECT * FROM (
	                                SELECT I.id_invoice AS invoice_id_invoice, I.nature_operation AS invoice_nature_operation , I.key_access AS invoice_key_access,
                                    I.number AS invoice_number , I.status AS invoice_status, I.entry_date AS invoice_entry_date, i.issue_date AS invoice_issue_date, 
                                    I.carrier_id,
                                    I.receiver_id, I.sender_id,

                                    c.name AS carrier_name, c.company_name AS carrier_company_name, c.cpf AS carrier_cpf, c.cnpj AS carrier_cnpj, c.state_registration AS carrier_state_registration, c.freight_responsability AS carrier_freight_responsability,c.person_type AS carrier_person_type, c.address_id AS carrier_address_id,
                                    AddCarrier.street_name AS carrier_street_name, AddCarrier.number AS carrier_number, AddCarrier.neighborhood AS carrier_neighborhood, AddCarrier.city AS carrier_city, AddCarrier.state AS carrier_state,AddCarrier.country AS carrier_country,

                                    r.name AS receiver_name, r.company_name AS receiver_company_name, r.cpf AS receiver_cpf, r.cnpj AS receiver_cnpj, r.state_registration AS receiver_state_registration, r.type AS receiver_type, r.address_id AS receiver_address_id,
                                    AddReceiver.street_name AS receiver_street_name, AddReceiver.number AS receiver_number, AddReceiver.neighborhood AS receiver_neighborhood, AddReceiver.city AS receiver_city, AddReceiver.state AS receiver_state,
                                    AddReceiver.country AS receiver_country,

                                    s.fancy_name AS sender_fancy_name, s.company_name AS sender_company_name, s.cnpj AS sender_cnpj, s.state_registration AS sender_state_registration, s.municipal_registration AS sender_municipal_registration, s.address_id AS sender_address_id,
                                    AddSender.street_name AS sender_street_name, AddSender.number AS sender_number, AddSender.neighborhood AS sender_neighborhood, AddSender.city AS sender_city, AddSender.state AS sender_state, AddSender.country AS sender_country,

                                    Icarrier.id_invoice_carrier AS invoice_carrier_id, Icarrier.carrier_name AS invoice_carrier_name, Icarrier.carrier_company_name AS invoice_carrier_company_name, Icarrier.carrier_cpf AS invoice_carrier_cpf, Icarrier.carrier_cnpj AS invoice_carrier_cnpj,
                                    Icarrier.carrier_state_registration AS invoice_carrier_state_registration, Icarrier.carrier_freight_responsability AS invoice_carrier_freight_responsability, Icarrier.carrier_person_type AS invoice_carrier_person_type,
                                    Icarrier.carrier_address_street_name AS invoice_carrier_address_street_name, Icarrier.carrier_address_number AS invoice_carrier_address_number, Icarrier.carrier_address_neighborhood AS invoice_carrier_address_neighborhood,
                                    Icarrier.carrier_address_city AS invoice_carrier_address_city, Icarrier.carrier_address_state AS invoice_carrier_address_state, Icarrier.carrier_address_country AS invoice_carrier_address_country,

                                    Ireceiver.id_invoice_receiver AS invoice_receiver_id, Ireceiver.receiver_name AS invoice_receiver_name, Ireceiver.receiver_company_name AS invoice_receiver_company_name, Ireceiver.receiver_cpf AS invoice_receiver_cpf, Ireceiver.receiver_cnpj AS invoice_receiver_cnpj,
                                    Ireceiver.receiver_state_registration AS invoice_receiver_state_registration, Ireceiver.receiver_type AS invoice_receiver_type,
                                    Ireceiver.receiver_address_street_name AS invoice_receiver_address_street_name, Ireceiver.receiver_address_number AS invoice_receiver_address_number, Ireceiver.receiver_address_neighborhood AS invoice_receiver_address_neighborhood,
                                    Ireceiver.receiver_address_city AS invoice_receiver_address_city, Ireceiver.receiver_address_state AS invoice_receiver_address_state, Ireceiver.receiver_address_country AS invoice_receiver_address_country,

                                    Isender.id_invoice_sender AS invoice_sender_id, Isender.sender_fancy_name AS invoice_sender_fancy_name, Isender.sender_company_name AS invoice_sender_company_name, Isender.sender_cnpj AS invoice_sender_cnpj, Isender.sender_state_registration AS invoice_sender_state_registration, Isender.sender_municipal_registration AS invoice_sender_municipal_registration,
                                    Isender.sender_address_street_name AS invoice_sender_address_street_name, Isender.sender_address_number AS invoice_sender_address_number, Isender.sender_address_neighborhood AS invoice_sender_address_neighborhood,
                                    Isender.sender_address_city AS invoice_sender_address_city, Isender.sender_address_state AS invoice_sender_address_state,
                                    Isender.sender_address_country AS invoice_sender_address_country,

                                    ITax.icms_value, ITax.freight, ITax.ipi_value, ITax.total_value_products, ITax.total_value_invoice,
									ROW_NUMBER() OVER(PARTITION BY I.number ORDER BY I.id_invoice DESC) rn 
                                    FROM invoice AS I 
                                    LEFT JOIN carrier AS c ON c.id_carrier = I.carrier_id  LEFT JOIN address AS AddCarrier ON AddCarrier.id_address = c.address_id 
                                    INNER JOIN receiver AS r ON r.id_receiver = I.receiver_id  INNER JOIN address AS AddReceiver ON AddReceiver.id_address = r.address_id 
                                    INNER JOIN sender AS s ON s.id_sender = I.sender_id INNER JOIN address AS AddSender ON AddSender.id_address = s.address_id 
                                    LEFT JOIN invoice_carrier AS Icarrier ON (Icarrier.invoice_id = I.id_invoice) 
                                    LEFT JOIN invoice_receiver AS Ireceiver ON (Ireceiver.invoice_id = I.id_invoice) 
                                    LEFT JOIN invoice_sender AS Isender ON (Isender.invoice_id = I.id_invoice) 
                                    LEFT JOIN invoice_tax AS ITax ON I.id_invoice = ITax.invoice_id 
                                    WHERE I.number = @number) a
                                    WHERE rn = 1";

		#endregion

		private ContextNfe _context;

		public InvoiceRepository(ContextNfe context)
		{
			_context = context;
		}

		#region DELETE

		public bool Remove(long id)
		{
			var entity = _context.Invoices.FirstOrDefault(p => p.Id == id);
			if (entity == null)
				throw new NotFoundException();

            entity.InvoiceItems.ToList().ForEach(x => _context.Entry(x).State = EntityState.Deleted);


			_context.Entry(entity).State = EntityState.Deleted;
			return _context.SaveChanges() > 0;
		}

		#endregion

		#region GET

		public Invoice GetById(long invoiceId)
		{
            return _context.Invoices
                                .Include("Sender")
                                .Include("Receiver")
                                .Include("Carrier")
                                .Include("InvoiceItems")
								.Include("InvoiceItems.Product")
								.FirstOrDefault(c => c.Id == invoiceId);
		}

		public Invoice GetByNumber(int number)
		{
			if (number <= 0)
				throw new IdentifierUndefinedException();
            return _context.Invoices
                                .Include("Sender")
                                .Include("Receiver")
                                .Include("Carrier")
                                .FirstOrDefault(c => c.Number == number);

        }

        public IQueryable<Invoice> GetAll(int size)
		{
			return this._context.Invoices
                                    .Include("Sender")
                                    .Include("Receiver")
                                    .Include("Carrier")
                                    .Include("InvoiceItems")
									.Include("InvoiceItems.Product")
									.Take(size);
		}

		public IQueryable<Invoice> GetAll()
		{
			return this._context.Invoices
                                    .Include("Sender")
                                    .Include("Receiver")
                                    .Include("Carrier")
                                    .Include("InvoiceItems")
									.Include("InvoiceItems.Product");
		}

		public IList<Invoice> GetByCarrier(long idCarrier)
		{
			if (idCarrier <= 0)
				throw new IdentifierUndefinedException();
            return _context.Invoices.Include("Sender").Include("Receiver").Include("Carrier").Where(c => c.CarrierId == idCarrier).ToList();
        }

        public IList<Invoice> GetByReceiver(long idReceiver)
		{
			if (idReceiver <= 0)
				throw new IdentifierUndefinedException();
            return _context.Invoices.Include("Sender").Include("Receiver").Include("Carrier").Where(c => c.ReceiverId == idReceiver).ToList();
        }

        public IList<Invoice> GetBySender(long idSender)
		{
			if (idSender <= 0)
				throw new IdentifierUndefinedException();
            return _context.Invoices.Include("Sender").Include("Receiver").Include("Carrier").Where(c => c.SenderId == idSender).ToList();
        }

        public Invoice GetByKeyAccess(string keyAccess)
		{
			if (String.IsNullOrEmpty(keyAccess))
				throw new IdentifierUndefinedException();
			return Db.Get(_getByKeyAccess, Make, new object[] { "@key_access ", keyAccess });
		}


		#endregion

		#region ADD

		public Invoice Add(Invoice invoice)
		{
			var newInvoice = _context.Invoices.Add(invoice);
			_context.SaveChanges();
			return newInvoice;
		}

		#endregion

		#region UPDATE

		public bool Update(Invoice invoice)
		{
			_context.Entry(invoice).State = EntityState.Modified;
			return _context.SaveChanges() > 0;
		}

		#endregion

		private static Func<IDataReader, Invoice> Make = reader =>
		   new Invoice
		   {
			   Id = Convert.ToInt64(reader["invoice_id_invoice"]),
			   NatureOperation = Convert.ToString(reader["invoice_nature_operation"]),
			   KeyAccess = reader["invoice_key_access"] == DBNull.Value ? null : new KeyAccess { Value = Convert.ToString(reader["invoice_key_access"]) },
			   Number = Convert.ToInt32(reader["invoice_number"]),
			   Status = (InvoiceStatus)Enum.Parse(typeof(InvoiceStatus), reader["invoice_status"].ToString()),
			   EntryDate = Convert.ToDateTime(reader["invoice_entry_date"]),
			   IssueDate = reader["invoice_issue_date"] == DBNull.Value ? new Nullable<DateTime>() : Convert.ToDateTime(reader["invoice_issue_date"]),
			   Carrier = reader["carrier_id"] == DBNull.Value ? null : new Carrier
			   {
				   Id = Convert.ToInt64(reader["carrier_id"]),
				   Name = Convert.ToString(reader["carrier_name"]),
				   CompanyName = Convert.ToString(reader["carrier_company_name"]),
				   CPF = reader["carrier_cpf"] == DBNull.Value ? null : new CPF() { Value = Convert.ToString(reader["carrier_cpf"]) },
				   CNPJ = reader["carrier_cnpj"] == DBNull.Value ? null : new CNPJ() { Value = Convert.ToString(reader["carrier_cnpj"]) },
				   StateRegistration = Convert.ToString(reader["carrier_state_registration"]),
				   FreightResponsability = (FreightResponsability)Enum.Parse(typeof(FreightResponsability), reader["carrier_freight_responsability"].ToString()),
				   PersonType = (PersonType)Enum.Parse(typeof(PersonType), reader["carrier_person_type"].ToString()),
				   Address = new Address
				   {
					   Id = Convert.ToInt64(reader["carrier_address_id"]),
					   StreetName = Convert.ToString(reader["carrier_street_name"]),
					   Number = Convert.ToInt32(reader["carrier_number"]),
					   Neighborhood = Convert.ToString(reader["carrier_neighborhood"]),
					   City = Convert.ToString(reader["carrier_city"]),
					   State = Convert.ToString(reader["carrier_state"]),
					   Country = Convert.ToString(reader["carrier_country"])
				   }
			   },

			   Receiver = new Receiver
			   {
				   Id = Convert.ToInt64(reader["receiver_id"]),
				   Name = reader["receiver_name"].ToString(),
				   CompanyName = reader["receiver_company_name"].ToString(),
				   StateRegistration = reader["receiver_state_registration"].ToString(),
				   Cpf = reader["receiver_cpf"] == DBNull.Value ? null : new CPF() { Value = Convert.ToString(reader["receiver_cpf"]) },
				   Cnpj = reader["receiver_cnpj"] == DBNull.Value ? null : new CNPJ() { Value = Convert.ToString(reader["receiver_cnpj"]) },
				   Type = (PersonType)Enum.Parse(typeof(PersonType), reader["receiver_type"].ToString()),
				   Address = new Address()
				   {
					   Id = Convert.ToInt64(reader["receiver_address_id"]),
					   StreetName = reader["receiver_street_name"].ToString(),
					   Number = Convert.ToInt32(reader["receiver_number"].ToString()),
					   Neighborhood = reader["receiver_neighborhood"].ToString(),
					   City = reader["receiver_city"].ToString(),
					   State = reader["receiver_state"].ToString(),
					   Country = reader["receiver_country"].ToString()
				   },
			   },
			   Sender = new Sender
			   {
				   Id = Convert.ToInt64(reader["sender_id"]),
				   CompanyName = Convert.ToString(reader["sender_company_name"]),
				   FancyName = Convert.ToString(reader["sender_fancy_name"]),
				   Cnpj = new CNPJ()
				   {
					   Value = Convert.ToString(reader["sender_cnpj"])
				   },
				   StateRegistration = Convert.ToString(reader["sender_state_registration"]),
				   MunicipalRegistration = Convert.ToString(reader["sender_municipal_registration"]),
				   Address = new Address
				   {
					   Id = Convert.ToInt64(reader["sender_address_id"]),
					   City = Convert.ToString(reader["sender_city"]),
					   Country = Convert.ToString(reader["sender_country"]),
					   State = Convert.ToString(reader["sender_state"]),
					   Neighborhood = Convert.ToString(reader["sender_neighborhood"]),
					   Number = Convert.ToInt32(reader["sender_number"]),
					   StreetName = Convert.ToString(reader["sender_street_name"])
				   }
			   },

			   InvoiceCarrier = reader["invoice_carrier_id"] == DBNull.Value ? null : ReadInvoiceCarrier(reader),
               InvoiceReceiver = reader["invoice_receiver_id"] == DBNull.Value ? null : ReadInvoiceReceiver(reader),
               InvoiceSender = reader["invoice_sender_id"] == DBNull.Value ? null : ReadInvoiceSender(reader),
               InvoiceTax = reader["invoice_status"].Equals(0) ? null : ReadInvoiceTax(reader),

		   };

		private static InvoiceCarrier ReadInvoiceCarrier(IDataReader reader)
		{
			return new InvoiceCarrier
			{
                Invoice = new Invoice() { Id = Convert.ToInt64(reader["invoice_id_invoice"]) },
				Carrier = new Carrier()
				{
					Id = Convert.ToInt64(reader["invoice_carrier_id"]),
					Name = Convert.ToString(reader["invoice_carrier_name"]),
					CompanyName = Convert.ToString(reader["invoice_carrier_company_name"]),
					CPF = reader["invoice_carrier_cpf"] == DBNull.Value ? null : new CPF() { Value = Convert.ToString(reader["invoice_carrier_cpf"]) },
					CNPJ = reader["invoice_carrier_cnpj"] == DBNull.Value ? null : new CNPJ { Value = Convert.ToString(reader["invoice_carrier_cnpj"]) },
					StateRegistration = Convert.ToString(reader["carrier_state_registration"]),
					FreightResponsability = (FreightResponsability)Enum.Parse(typeof(FreightResponsability), reader["invoice_carrier_freight_responsability"].ToString()),
					PersonType = (PersonType)Enum.Parse(typeof(PersonType), reader["invoice_carrier_person_type"].ToString()),
					Address = new Address
					{
						City = Convert.ToString(reader["invoice_carrier_address_city"]),
						Country = Convert.ToString(reader["invoice_carrier_address_country"]),
						State = Convert.ToString(reader["invoice_carrier_address_state"]),
						Neighborhood = Convert.ToString(reader["invoice_carrier_address_neighborhood"]),
						Number = Convert.ToInt32(reader["invoice_carrier_address_number"]),
						StreetName = Convert.ToString(reader["invoice_carrier_address_street_name"])
					}
				},
			};
		}

		private static InvoiceReceiver ReadInvoiceReceiver(IDataReader reader)
		{
			return new InvoiceReceiver
			{
                Invoice = new Invoice() { Id = Convert.ToInt64(reader["invoice_id_invoice"]) },
                Receiver = new Receiver()
				{
					Id = Convert.ToInt64(reader["invoice_receiver_id"]),
					Name = reader["invoice_receiver_name"].ToString(),
					CompanyName = reader["invoice_receiver_company_name"].ToString(),
					Cpf = reader["invoice_receiver_cpf"] == DBNull.Value ? null : new CPF() { Value = Convert.ToString(reader["invoice_receiver_cpf"]) },
					Cnpj = reader["invoice_receiver_cnpj"] == DBNull.Value ? null : new CNPJ { Value = Convert.ToString(reader["invoice_receiver_cnpj"]) },

					StateRegistration = reader["invoice_receiver_state_registration"].ToString(),
					Type = (PersonType)Enum.Parse(typeof(PersonType), reader["invoice_receiver_type"].ToString()),
					Address = new Address()
					{
						Number = Convert.ToInt32(reader["invoice_receiver_address_number"].ToString()),
						StreetName = reader["invoice_receiver_address_street_name"].ToString(),
						Neighborhood = reader["invoice_receiver_address_neighborhood"].ToString(),
						City = reader["invoice_receiver_address_city"].ToString(),
						State = reader["invoice_receiver_address_state"].ToString(),
						Country = reader["invoice_receiver_address_country"].ToString()
					},

				},

			};
		}

		private static InvoiceSender ReadInvoiceSender(IDataReader reader)
		{
			return new InvoiceSender
			{
                Invoice = new Invoice() { Id = Convert.ToInt64(reader["invoice_id_invoice"]) },
                Sender = new Sender
				{
					Id = Convert.ToInt64(reader["invoice_sender_id"]),
					CompanyName = Convert.ToString(reader["invoice_sender_company_name"]),
					FancyName = Convert.ToString(reader["invoice_sender_fancy_name"]),
					MunicipalRegistration = Convert.ToString(reader["invoice_sender_municipal_registration"]),
					StateRegistration = Convert.ToString(reader["invoice_sender_state_registration"]),
					Cnpj = new CNPJ { Value = Convert.ToString(reader["invoice_sender_cnpj"]) },
					Address = new Address
					{

						StreetName = Convert.ToString(reader["invoice_sender_address_street_name"]),
						Number = Convert.ToInt32(reader["invoice_sender_address_number"]),
						City = Convert.ToString(reader["invoice_sender_address_city"]),
						Country = Convert.ToString(reader["invoice_sender_address_country"]),
						State = Convert.ToString(reader["invoice_sender_address_state"]),
						Neighborhood = Convert.ToString(reader["invoice_sender_address_neighborhood"])
					}
				},
			};
		}

		private static InvoiceTax ReadInvoiceTax(IDataReader reader)
		{
			return new InvoiceTax
			{
                Invoice = new Invoice() { Id = Convert.ToInt64(reader["invoice_id_invoice"]) },
                IcmsValue = reader["icms_value"] == DBNull.Value ? 0 : Convert.ToDouble(reader["icms_value"]),
				Freight = reader["freight"] == DBNull.Value ? 0 : Convert.ToDouble(reader["freight"]),
				IpiValue = reader["ipi_value"] == DBNull.Value ? 0 : Convert.ToDouble(reader["ipi_value"]),
				TotalValueProducts = reader["total_value_products"] == DBNull.Value ? 0 : Convert.ToDouble(reader["total_value_products"]),

			};
		}

		private object[] Take(Invoice invoice)
		{
			return new object[]
			{
				 "@id_invoice", invoice.Id,
				 "@nature_operation", invoice.NatureOperation,
				 "@key_access", invoice.KeyAccess == null?Convert.DBNull:invoice.KeyAccess.Value,
				 "@number",invoice.Number,
				 "@status",invoice.Status,
				 "@entry_date",invoice.EntryDate,
				 "@issue_date",invoice.IssueDate,
				 "@carrier_id",invoice.Carrier == null?Convert.DBNull:invoice.Carrier.Id,
				 "@receiver_id",invoice.Receiver.Id,
				 "@sender_id",invoice.Sender.Id,
			};


		}


	}
}
