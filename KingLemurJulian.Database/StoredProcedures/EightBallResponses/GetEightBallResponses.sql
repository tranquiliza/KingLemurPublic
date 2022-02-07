CREATE PROCEDURE [Core].[GetEightBallResponses]
AS
BEGIN
	SELECT [Response] FROM [Core].[EightBallResponses]
END