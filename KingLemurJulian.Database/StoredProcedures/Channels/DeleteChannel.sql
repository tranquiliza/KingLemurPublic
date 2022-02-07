CREATE PROCEDURE [Core].[DeleteChannel]
	@channel NVARCHAR(200)
AS
BEGIN
	DELETE FROM [Core].[Channels] WHERE [Channel] = @channel
END
