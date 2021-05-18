CREATE TABLE [dbo].[sender]
(
	[id_sender] BIGINT NOT NULL IDENTITY(1,1), 
    [fancy_name] VARCHAR(60) NOT NULL, 
    [company_name] VARCHAR(60) NOT NULL, 
    [cnpj] VARCHAR(14) NOT NULL, 
    [state_registration] VARCHAR(15) NOT NULL, 
    [municipal_registration] VARCHAR(15) NOT NULL, 
    [address_id] BIGINT NOT NULL, 
    CONSTRAINT [FK_sender_ToAddress] FOREIGN KEY ([address_id]) REFERENCES [address]([id_address]),
	CONSTRAINT [PK_Sender] PRIMARY KEY CLUSTERED

(
	[id_sender] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
