CREATE TABLE [dbo].[invoice_receiver]
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
)
