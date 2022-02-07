CREATE PROCEDURE [Core].[GetIamListeningResponses]
AS
BEGIN
	SELECT [Response] FROM [Core].[IAmListeningResponses]
END