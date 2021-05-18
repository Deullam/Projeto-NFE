CREATE TABLE [dbo].[address]
(
	[id_address] BIGINT NOT NULL PRIMARY KEY IDENTITY, 
    [street_name] VARCHAR(60) NOT NULL, 
    [number] INT NOT NULL, 
    [neighborhood] VARCHAR(40) NOT NULL, 
    [city] VARCHAR(50) NOT NULL, 
    [state] VARCHAR(40) NOT NULL, 
    [country] VARCHAR(50) NOT NULL
)
