CREATE PROCEDURE [Core].[GetChannels]
AS
BEGIN
	SELECT [Channel] FROM [Core].[Channels]
END