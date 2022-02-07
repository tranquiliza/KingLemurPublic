CREATE TABLE [Core].[DynamicCommands]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [CommandIdentifier] NVARCHAR(200) NOT NULL, 
    [Data] NVARCHAR(MAX) NOT NULL
)
