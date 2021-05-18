CREATE TABLE [dbo].[invoice_tax]
(
	[id_invoice_tax] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [icms_value] FLOAT NOT NULL, 
    [freight] FLOAT NOT NULL, 
    [ipi_value] FLOAT NOT NULL, 
    [total_value_products] FLOAT NOT NULL, 
    [total_value_invoice] FLOAT NOT NULL,
	[invoice_id] BIGINT NOT NULL,
	CONSTRAINT [FK_invoicetax_invoice] FOREIGN KEY ([invoice_id]) REFERENCES [invoice]([id_invoice])ON DELETE CASCADE
)
