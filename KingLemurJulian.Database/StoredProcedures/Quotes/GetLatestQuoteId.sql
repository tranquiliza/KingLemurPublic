CREATE PROCEDURE [Core].[GetLatestQuoteId]
AS
BEGIN
	SELECT [Id] 
	FROM [Core].[Quotes] 
	ORDER BY [Id] DESC
END
