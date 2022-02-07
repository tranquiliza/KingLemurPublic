CREATE PROCEDURE [Core].[SaveDynamicCommand]
	@commandIdentifer NVARCHAR(200),
	@data NVARCHAR(MAX)
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM [Core].[DynamicCommands] WHERE [CommandIdentifier] = @commandIdentifer)
		INSERT INTO [Core].[DynamicCommands]([CommandIdentifier], [Data])
		VALUES(@commandIdentifer, @data)
	ELSE
		UPDATE [Core].[DynamicCommands] 
		SET [Data] = @data
		WHERE [CommandIdentifier] = @commandIdentifer
END