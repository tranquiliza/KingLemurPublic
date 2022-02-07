CREATE PROCEDURE [Core].[GetQuotes]
AS
BEGIN
	SELECT [Id], [Channel], [QuoteText], [CreationTime], [CreatedBy], [Deleted] 
	FROM [Core].[Quotes]
END