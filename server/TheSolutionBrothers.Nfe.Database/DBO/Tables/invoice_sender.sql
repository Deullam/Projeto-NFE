CREATE TABLE [dbo].[invoice_sender]
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
	CONSTRAINT [FK_invoice_sender_Toinvoice] FOREIGN KEY ([invoice_id]) REFERENCES [invoice]([id_invoice])ON DELETE CASCADE
 )
