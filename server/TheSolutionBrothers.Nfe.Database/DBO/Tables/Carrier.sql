CREATE TABLE [dbo].[carrier]
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
)