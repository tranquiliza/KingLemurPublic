CREATE PROCEDURE [Core].[GetDynamicCommandIdentifiers]
AS 
BEGIN
	SELECT [CommandIdentifier] FROM [Core].[DynamicCommands]
END