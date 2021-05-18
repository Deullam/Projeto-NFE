CREATE TABLE [dbo].[invoice_carrier]
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
)