CREATE TABLE [dbo].[invoice_item]
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
)
