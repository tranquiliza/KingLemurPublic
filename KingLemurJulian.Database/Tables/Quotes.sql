CREATE TABLE [Core].[Quotes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Channel] NVARCHAR(200) NOT NULL, 
    [QuoteText] NVARCHAR(1000) NOT NULL, 
    [CreationTime] DATETIME2(0) NOT NULL, 
    [CreatedBy] NVARCHAR(200) NOT NULL ,
    [Deleted] BIT NOT NULL 
)
