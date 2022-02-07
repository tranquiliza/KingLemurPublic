CREATE PROCEDURE [Core].[GetDynamicCommand]
	@commandIdentifier NVARCHAR(200)
AS
BEGIN
	SELECT [Data] FROM [Core].[DynamicCommands]
	WHERE [CommandIdentifier] = @commandIdentifier
END