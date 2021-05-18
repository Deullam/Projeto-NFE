CREATE TABLE [dbo].[invoice]
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
)
