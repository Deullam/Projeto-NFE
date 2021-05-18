CREATE TABLE [dbo].[product]
(
	[id_product] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [code] VARCHAR(14) NOT NULL, 
    [description] VARCHAR(60) NOT NULL, 
    [current_value] DECIMAL(14, 2) NOT NULL
)
