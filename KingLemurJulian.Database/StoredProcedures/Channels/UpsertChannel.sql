CREATE PROCEDURE [Core].[UpsertChannel]
	@channel NVARCHAR(200)
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM [Core].[Channels] WHERE [Channel] = @channel)
		INSERT INTO [Core].[Channels]([Channel]) 
		VALUES (@channel)
END