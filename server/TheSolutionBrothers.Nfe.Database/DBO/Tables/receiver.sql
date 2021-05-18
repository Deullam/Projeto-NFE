CREATE TABLE [dbo].[receiver]
(
	[id_receiver] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [name] VARCHAR(60) NOT NULL, 
    [company_name] VARCHAR(60) NULL, 
    [cpf] VARCHAR(11) NULL, 
    [cnpj] VARCHAR(14) NULL, 
    [state_registration] VARCHAR(15) NULL, 
    [type] INT NULL, 
    [address_id] BIGINT NULL, 
	CONSTRAINT [FK_receiver_address] FOREIGN KEY ([address_id]) REFERENCES [address]([id_address])

)
