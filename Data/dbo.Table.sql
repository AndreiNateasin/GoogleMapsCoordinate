CREATE TABLE [dbo].[Routes]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [fromCoordonateId] NVARCHAR(50) NULL, 
    [toCoordonateId] NVARCHAR(50) NULL
)
