using TheSolutionBrothers.Nfe.Infra;

namespace TheSolutionBrothers.NFe.Common.Tests.Base
{
    public static class BaseSqlTest
    {
		
        private const string DROP_TABLE_INVOICE_TAX = "DROP TABLE [dbo].[invoice_tax]";
        private const string CREATE_TABLE_INVOICE_TAX = @"CREATE TABLE [dbo].[invoice_tax]
                                                        (
	                                                        [id_invoice_tax] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
                                                            [icms_value] FLOAT NOT NULL, 
                                                            [freight] FLOAT NOT NULL, 
                                                            [ipi_value] FLOAT NOT NULL, 
                                                            [total_value_products] FLOAT NOT NULL, 
                                                            [total_value_invoice] FLOAT NOT NULL,
	                                                        [invoice_id] BIGINT NOT NULL,
	                                                        CONSTRAINT [FK_invoicetax_invoice] FOREIGN KEY ([invoice_id]) REFERENCES [invoice]([id_invoice])ON DELETE CASCADE
                                                        )";

        private const string DROP_TABLE_INVOICE_RECEIVER = "DROP TABLE [dbo].[invoice_receiver]";
        private const string CREATE_TABLE_INVOICE_RECEIVER = @"CREATE TABLE [dbo].[invoice_receiver]
                                                                    (
	                                                                    [id_invoice_receiver] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
                                                                        [receiver_name] VARCHAR(60) NOT NULL, 
                                                                        [receiver_company_name] VARCHAR(60) NULL, 
                                                                        [receiver_cpf] VARCHAR(11) NULL, 
                                                                        [receiver_cnpj] VARCHAR(14) NULL, 
                                                                        [receiver_state_registration] VARCHAR(15) NULL, 
                                                                        [receiver_type] INT NULL, 
                                                                        [receiver_address_street_name] VARCHAR(50) NOT NULL,
	                                                                    [receiver_address_number] INT NOT NULL, 
                                                                        [receiver_address_neighborhood] VARCHAR(40) NOT NULL, 
                                                                        [receiver_address_city] VARCHAR(50) NOT NULL, 
                                                                        [receiver_address_state] VARCHAR(40) NOT NULL, 
                                                                        [receiver_address_country] VARCHAR(50) NOT NULL, 
                                                                        [invoice_id] BIGINT NOT NULL,
	                                                                    CONSTRAINT [FK_invoicereceiver_invoice] FOREIGN KEY ([invoice_id]) REFERENCES [invoice]([id_invoice])ON DELETE CASCADE
                                                                    )";

        private const string DROP_TABLE_INVOICE_CARRIER = "DROP TABLE [dbo].[invoice_carrier]";
        private const string CREATE_TABLE_INVOICE_CARRIER = @"CREATE TABLE [dbo].[invoice_carrier]
                                                                    (
	                                                                    [id_invoice_carrier] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
                                                                        [carrier_name] VARCHAR(60) NOT NULL, 
                                                                        [carrier_company_name] VARCHAR(60) NULL, 
                                                                        [carrier_cpf] VARCHAR(11) NULL, 
                                                                        [carrier_cnpj] VARCHAR(14) NULL, 
                                                                        [carrier_state_registration] VARCHAR(15) NULL, 
                                                                        [carrier_freight_responsability] INT NOT NULL, 
                                                                        [carrier_person_type] INT NOT NULL, 
                                                                        [carrier_address_street_name] VARCHAR(60) NOT NULL, 
                                                                        [carrier_address_number] INT NOT NULL, 
                                                                        [carrier_address_neighborhood] VARCHAR(40) NOT NULL, 
                                                                        [carrier_address_city] VARCHAR(50) NOT NULL, 
                                                                        [carrier_address_state] VARCHAR(40) NOT NULL, 
                                                                        [carrier_address_country] VARCHAR(50) NOT NULL, 
                                                                        [invoice_id] BIGINT NOT NULL, 
                                                                        CONSTRAINT [FK_invoice_carrier_ToInvoice] FOREIGN KEY ([invoice_id]) REFERENCES [invoice]([id_invoice])ON DELETE CASCADE
                                                                    )";

        private const string DROP_TABLE_INVOICE_SENDER = "DROP TABLE [dbo].[invoice_sender]";
        private const string CREATE_TABLE_INVOICE_SENDER = @"CREATE TABLE[dbo].[invoice_sender]
                                                                    (

                                                                        [id_invoice_sender] BIGINT PRIMARY KEY NOT NULL IDENTITY(1,1), 
                                                                        [sender_fancy_name] VARCHAR(60) NOT NULL,
                                                                        [sender_company_name] VARCHAR(60) NOT NULL,
                                                                        [sender_cnpj] VARCHAR(14) NOT NULL,
                                                                        [sender_state_registration] VARCHAR(15) NOT NULL,
                                                                        [sender_municipal_registration] VARCHAR(15) NOT NULL,
                                                                        [sender_address_street_name] VARCHAR(60) NOT NULL,
                                                                        [sender_address_number] INT NOT NULL, 
                                                                        [sender_address_neighborhood] VARCHAR(40) NOT NULL,
                                                                        [sender_address_city] VARCHAR(50) NOT NULL,
                                                                        [sender_address_state] VARCHAR(40) NOT NULL,
                                                                        [sender_address_country] VARCHAR(50) NOT NULL,

                                                                        [invoice_id] BIGINT NOT NULL,
	                                                                    CONSTRAINT[FK_invoice_sender_Toinvoice] FOREIGN KEY([invoice_id]) REFERENCES[invoice] ([id_invoice]) ON DELETE CASCADE
                                                                    )";

        private const string DROP_TABLE_INVOICE_ITEM = "DROP TABLE [dbo].[invoice_item]";
        private const string CREATE_TABLE_INVOICE_ITEM = @"CREATE TABLE [dbo].[invoice_item]
                                                                (
	                                                                [id_invoice_item] BIGINT NOT NULL PRIMARY KEY IDENTITY,
	                                                                [code] VARCHAR(14) NULL, 
                                                                    [description] VARCHAR(60) NULL, 
                                                                    [amount] INT NOT NULL, 
                                                                    [total_value] DECIMAL(18, 2) NULL, 
                                                                    [unit_value] DECIMAL(18, 2) NULL, 
                                                                    [icms_value] DECIMAL(18, 2) NULL, 
                                                                    [ipi_value] DECIMAL(18, 2) NULL, 
                                                                    [icms_aliquot] DECIMAL(18, 2) NULL, 
                                                                    [ipi_aliquot] DECIMAL(18, 2) NULL, 
                                                                    [invoice_id] BIGINT NOT NULL, 
                                                                    [product_id] BIGINT NOT NULL, 
                                                                    CONSTRAINT [FK_invoice_product_ToInvoice] FOREIGN KEY ([invoice_id]) REFERENCES [invoice]([id_invoice])ON DELETE CASCADE, 
                                                                    CONSTRAINT [FK_invoice_product_ToProduct] FOREIGN KEY ([product_id]) REFERENCES [product]([id_product])ON DELETE CASCADE
                                                                )";

        private const string DROP_TABLE_INVOICE = "DROP TABLE [dbo].[invoice]";
        private const string CREATE_TABLE_INVOICE = @"CREATE TABLE [dbo].[invoice]
                                                            (
	                                                            [id_invoice] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
                                                                [nature_operation] VARCHAR(70) NULL, 
                                                                [key_access] VARCHAR(44) NULL, 
                                                                [number] INT NULL, 
                                                                [status] INT NULL, 
                                                                [entry_date] DATETIME NULL, 
                                                                [issue_date] DATETIME NULL, 
                                                                [carrier_id] BIGINT NULL, 
                                                                [receiver_id] BIGINT NULL, 
                                                                [sender_id] BIGINT NULL, 
                                                                CONSTRAINT [FK_invoice_ToCarrier] FOREIGN KEY ([carrier_id]) REFERENCES [carrier]([id_carrier]), 
                                                                CONSTRAINT [FK_invoice_ToReceiver] FOREIGN KEY ([receiver_id]) REFERENCES [receiver]([id_receiver]),
	                                                            CONSTRAINT [FK_invoice_ToSender] FOREIGN KEY ([sender_id]) REFERENCES [sender]([id_sender]) 
                                                            )";

        private const string DROP_TABLE_RECEIVER = "DROP TABLE [dbo].[receiver]";
        private const string CREATE_TABLE_RECEIVER = @"CREATE TABLE [dbo].[receiver]
                                                            (

                                                                [id_receiver] BIGINT NOT NULL PRIMARY KEY IDENTITY,
                                                                [name] VARCHAR(60) NOT NULL,
                                                                [company_name] VARCHAR(60) NULL, 
                                                                [cpf] VARCHAR(11) NULL, 
                                                                [cnpj] VARCHAR(14) NULL, 
                                                                [state_registration] VARCHAR(15) NULL, 
                                                                [type] INT NULL,
                                                                [address_id] BIGINT NULL,
                                                                CONSTRAINT[FK_receiver_address] FOREIGN KEY([address_id]) REFERENCES[address] ([id_address])

                                                            )";

        private const string DROP_TABLE_CARRIER = "DROP TABLE [dbo].[carrier]";
        private const string CREATE_TABLE_CARRIER = @"CREATE TABLE [dbo].[carrier]
                                                            (
	                                                            [id_carrier] BIGINT NOT NULL PRIMARY KEY IDENTITY(1,1), 
                                                                [name] VARCHAR(60) NOT NULL, 
                                                                [company_name] VARCHAR(60) NULL, 
                                                                [cpf] VARCHAR(11) NULL, 
                                                                [cnpj] VARCHAR(14) NULL, 
                                                                [state_registration] VARCHAR(15) NULL, 
                                                                [freight_responsability] INT NOT NULL, 
                                                                [person_type] INT NOT NULL, 
                                                                [address_id] BIGINT NOT NULL, 
                                                                CONSTRAINT [FK_carrier_ToAddress] FOREIGN KEY ([address_id]) REFERENCES [address]([id_address])
                                                            )";

        private const string DROP_TABLE_SENDER = "DROP TABLE [dbo].[sender]";
        private const string CREATE_TABLE_SENDER = @"CREATE TABLE [dbo].[sender]
                                                    (
	                                                    [id_sender] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
                                                        [fancy_name] VARCHAR(60) NOT NULL, 
                                                        [company_name] VARCHAR(60) NOT NULL, 
                                                        [cnpj] VARCHAR(14) NOT NULL, 
                                                        [state_registration] VARCHAR(15) NOT NULL, 
                                                        [municipal_registration] VARCHAR(15) NOT NULL, 
                                                        [address_id] BIGINT NOT NULL, 
                                                        CONSTRAINT [FK_sender_ToAddress] FOREIGN KEY ([address_id]) REFERENCES [address]([id_address]),
                                                    )";

        private const string DROP_TABLE_ADDRESS = "DROP TABLE [dbo].[address]";
        private const string CREATE_TABLE_ADDRESS = @"CREATE TABLE [dbo].[address]
                                                            (
	                                                            [id_address] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
                                                                [street_name] VARCHAR(60) NOT NULL, 
                                                                [number] INT NOT NULL, 
                                                                [neighborhood] VARCHAR(40) NOT NULL, 
                                                                [city] VARCHAR(50) NOT NULL, 
                                                                [state] VARCHAR(40) NOT NULL, 
                                                                [country] VARCHAR(50) NOT NULL
                                                            )";

        private const string DROP_TABLE_PRODUCT = "DROP TABLE [dbo].[product]";
        private const string CREATE_TABLE_PRODUCT = @"CREATE TABLE [dbo].[product]
                                                            (
	                                                            [id_product] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
                                                                [code] VARCHAR(14) NOT NULL, 
                                                                [description] VARCHAR(60) NOT NULL, 
                                                                [current_value] DECIMAL(14, 2) NOT NULL
                                                            )";


        private const string INSERT_ADDRESS = "INSERT INTO address (street_name, number, neighborhood, city, state, country) VALUES ('Teste rua 1',1,'Teste bairro 1','Teste cidade 1','SC','Teste país 1')";
        private const string INSERT_ADDRESS_WITHOUT_DEPENDENCY = "INSERT INTO address (street_name, number, neighborhood, city, state, country) VALUES ('Teste rua 2',2,'Teste bairro 2','Teste cidade 2','RS','Teste país 2')";
        private const string INSERT_SENDER = "INSERT INTO sender (fancy_name, company_name,cnpj,state_registration,municipal_registration,address_id) VALUES ('Teste nome fantasia 2', 'Teste razão social 1', '60415996000128', '49779902000167', '123456789012',1)";
        private const string INSERT_SENDER_WITHOUT_DEPENDENCY = "INSERT INTO sender (fancy_name, company_name,cnpj,state_registration,municipal_registration,address_id) VALUES ('Teste nome fantasia 2', 'Teste razão social 2', '01227951000160', '123456789012', '123456789012',1)";
        private const string INSERT_SENDER_FOR_INVOICE = "INSERT INTO sender (fancy_name, company_name,cnpj,state_registration,municipal_registration,address_id) VALUES ('Teste nome fantasia 2', 'Teste razão social 1', '07242338930', '49779902000167', '123456789012',1)";

        private const string INSERT_RECEIVER = "INSERT INTO receiver (name, company_name, cpf, cnpj, state_registration, type, address_id) VALUES ('Nome Destinatario', 'Razao Social', '75476205071', '31616805000198', '414332962', 1, 1)";
		private const string INSERT_RECEIVER_WITHOUT_DEPENDENCY = "INSERT INTO receiver (name, company_name, cpf, cnpj, state_registration, type, address_id) VALUES ('Nome Destinatario 2', 'Razao Social 2', '54658618001', '56836505000145', '414332962', 1, 1)";
		private const string INSERT_RECEIVER_FOR_INVOICE = "INSERT INTO receiver (name, company_name, cpf, cnpj, state_registration, type, address_id) VALUES ('Nome Destinatario', 'Razao Social', '26503194021', '47304076000174', '414332962', 1, 1)";


        private const string INSERT_PRODUCT = @"INSERT INTO product (code, description, current_value) VALUES ('654645', 'Teste produto', 230.50)";
        private const string INSERT_PRODUCT_WITHOUT_DEPENDENCY = @"INSERT INTO product (code, description, current_value) VALUES ('98765', 'Teste produto 2', 7678.86)";

        private const string INSERT_CARRIER = "INSERT INTO carrier (name,company_name,cpf,cnpj,state_registration,freight_responsability,person_type, address_id) VALUES ('Solution Brothers', 'Solution Brothers','68165115065','60889136000126', '123456',1, 1, 1)";
        private const string INSERT_CARRIER_WITHOUT_DEPENDENCY = "INSERT INTO carrier (name,company_name,cpf,cnpj,state_registration,freight_responsability,person_type, address_id) VALUES ('Solution Brothers', 'Solution Brothers','20536821089','48311464000145', '123456',1, 1, 1)";
        private const string INSERT_CARRIER_FOR_INVOICE = "INSERT INTO carrier (name,company_name,cpf,cnpj,state_registration,freight_responsability,person_type, address_id) VALUES ('Solution Brothers', 'Solution Brothers','97162112013','31169079000102', '123456',1, 1, 1)";

        private const string INSERT_INVOICE_TESTE = "INSERT INTO[dbo].[invoice] ([nature_operation], [key_access], [number], [status], [entry_date], [issue_date], [carrier_id], [receiver_id], [sender_id]) VALUES(N'44', N'', 2, 0, N'2000-05-05 11:11:11',null, 3, 3, 3)";

        private const string INSERT_INVOICE_OPENED = "INSERT INTO[dbo].[invoice] ([nature_operation], [key_access], [number], [status], [entry_date], [issue_date], [carrier_id], [receiver_id], [sender_id]) VALUES(N'a', N'', 3, 0, N'2000-05-05 11:11:11',null, 3, 3, 3)";
        private const string INSERT_INVOICE_ISSUED = "INSERT INTO[dbo].[invoice] ([nature_operation], [key_access], [number], [status], [entry_date], [issue_date], [carrier_id], [receiver_id], [sender_id]) VALUES(N'a', N'a', 4, 1, N'2000-05-05 11:11:11','2000-05-05 21:11:11', 3, 3, 3)";



        private const string INSERT_INVOICE_RECEIVER = "INSERT INTO invoice_receiver (receiver_name, receiver_company_name, receiver_cpf, receiver_cnpj, receiver_state_registration, receiver_type, receiver_address_street_name, receiver_address_number, receiver_address_neighborhood, receiver_address_city, receiver_address_state, receiver_address_country, invoice_id) VALUES ('Nome Destinatario', 'Razao Social', '90991280024', '60485689000113', '414332962', 1, 'Teste rua 1', 1, 'Teste bairro 1', 'Teste cidade 1', 'Teste estado 1', 'Teste país 1', 4)";
		private const string INSERT_INVOICE_RECEIVER_WITHOUT_DEPENDENCY = "INSERT INTO invoice_receiver (receiver_name, receiver_company_name, receiver_cpf, receiver_cnpj, receiver_state_registration, receiver_type, receiver_address_street_name, receiver_address_number, receiver_address_neighborhood, receiver_address_city, receiver_address_state, receiver_address_country, invoice_id) VALUES ('Nome Destinatario', 'Razao Social', '54476798004', '11037773000169', '414332962', 1, 'Teste rua 1', 1, 'Teste bairro 1', 'Teste cidade 1', 'Teste estado 1', 'Teste país 1', 4)";
        private const string INSERT_INVOICE_CARRIER = "INSERT INTO invoice_carrier (carrier_name, carrier_company_name, carrier_cpf, carrier_cnpj, "
        + " carrier_state_registration, carrier_freight_responsability, carrier_person_type, carrier_address_street_name, carrier_address_number, "
        + "carrier_address_neighborhood, carrier_address_city,  carrier_address_state, carrier_address_country, invoice_id)"
        + "VALUES ('Solution Brothers', 'Solution Brothers','86200764000','85731266000167', '123456',1, 1,'Teste rua 1',1,'Teste bairro 1','Teste cidade 1','Teste estado 1','Teste país 1',4)";

        private const string INSERT_INVOICE_TEST = "INSERT INTO[dbo].[invoice] ([nature_operation], [key_access], [number], [status], [entry_date], [issue_date], [carrier_id], [receiver_id], [sender_id]) VALUES(N'1215455', N'', 1, 0, N'2000-05-06 11:11:11',null, 3, 3, 3)";

        private const string INSERT_INVOICE_SENDER = "INSERT INTO invoice_sender (sender_fancy_name,sender_company_name,sender_cnpj," +
                                                    "sender_state_registration,sender_municipal_registration,sender_address_street_name," +
                                                    "sender_address_number,sender_address_neighborhood,sender_address_city,sender_address_state," +
                                                    "sender_address_country,invoice_id) " +
                                                    "VALUES ('Teste nome fantasia 2', 'Teste razão social 1', '07242338930', '60415996000128', '123456789012','Teste rua 1',1,'Teste bairro 1','Teste cidade 1','Teste estado 1','Teste país 1',4)";

        private const string INSERT_INVOICE_ITEM = "INSERT INTO invoice_item (code,description,amount,total_value,unit_value,icms_value,ipi_value,icms_aliquot,ipi_aliquot,invoice_id,product_id)" +
                                                " VALUES ('654645','Teste producto',1,230.50,230.50,9.22,23.50,0.04,0.1,1,1)";
		private const string INSERT_INVOICE_TAX = "INSERT INTO invoice_tax (icms_value, freight, ipi_value, total_value_products, total_value_invoice, invoice_id) VALUES (10, 10, 10, 10, 10, 1)";
        private const string INSERT_INVOICE_TAX_OF_INVOICE = "INSERT INTO invoice_tax (icms_value, freight, ipi_value, total_value_products, total_value_invoice, invoice_id) VALUES (10, 10, 10, 10, 10, 3)";



        public static void SeedDatabase()
        {
            ResetDatabase();

            Db.Update(INSERT_ADDRESS);
            Db.Update(INSERT_ADDRESS_WITHOUT_DEPENDENCY);

            Db.Update(INSERT_RECEIVER);
			Db.Update(INSERT_RECEIVER_WITHOUT_DEPENDENCY);
            Db.Update(INSERT_RECEIVER_FOR_INVOICE);

            Db.Update(INSERT_CARRIER);
            Db.Update(INSERT_CARRIER_WITHOUT_DEPENDENCY);
            Db.Update(INSERT_CARRIER_FOR_INVOICE);

            Db.Update(INSERT_SENDER);
            Db.Update(INSERT_SENDER_WITHOUT_DEPENDENCY);
            Db.Update(INSERT_SENDER_FOR_INVOICE);

            Db.Update(INSERT_PRODUCT_WITHOUT_DEPENDENCY);
            Db.Update(INSERT_PRODUCT);

            Db.Update(INSERT_INVOICE_TESTE);

            Db.Update(INSERT_INVOICE_TEST);
		

            
            Db.Update(INSERT_INVOICE_TAX);

            Db.Update(INSERT_INVOICE_ITEM);

            Db.Update(INSERT_INVOICE_OPENED);
            Db.Update(INSERT_INVOICE_ISSUED);
            Db.Update(INSERT_INVOICE_TAX_OF_INVOICE);

            Db.Update(INSERT_INVOICE_RECEIVER);
            Db.Update(INSERT_INVOICE_RECEIVER_WITHOUT_DEPENDENCY);
            Db.Update(INSERT_INVOICE_CARRIER);
            Db.Update(INSERT_INVOICE_SENDER);
        }
        
        public static void ResetDatabase()
        {
            Db.Update(DROP_TABLE_INVOICE_TAX);
            Db.Update(DROP_TABLE_INVOICE_RECEIVER);
            Db.Update(DROP_TABLE_INVOICE_SENDER);
            Db.Update(DROP_TABLE_INVOICE_ITEM);
            Db.Update(DROP_TABLE_INVOICE_CARRIER);
            Db.Update(DROP_TABLE_INVOICE);
            Db.Update(DROP_TABLE_CARRIER);
            Db.Update(DROP_TABLE_SENDER);
            Db.Update(DROP_TABLE_RECEIVER);
            Db.Update(DROP_TABLE_ADDRESS);
            Db.Update(DROP_TABLE_PRODUCT);

            Db.Update(CREATE_TABLE_ADDRESS);
            Db.Update(CREATE_TABLE_PRODUCT);
            Db.Update(CREATE_TABLE_CARRIER);
            Db.Update(CREATE_TABLE_SENDER);
            Db.Update(CREATE_TABLE_RECEIVER);
            Db.Update(CREATE_TABLE_INVOICE);
            Db.Update(CREATE_TABLE_INVOICE_TAX);
            Db.Update(CREATE_TABLE_INVOICE_RECEIVER);
            Db.Update(CREATE_TABLE_INVOICE_SENDER);
            Db.Update(CREATE_TABLE_INVOICE_ITEM);
            Db.Update(CREATE_TABLE_INVOICE_CARRIER);
        }

	}
}
