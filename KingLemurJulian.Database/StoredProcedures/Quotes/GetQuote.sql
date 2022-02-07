CREATE PROCEDURE [Core].[GetQuote]
	@id int
AS
BEGIN
	SELECT TOP(1) [Id], [Channel], [QuoteText], [CreationTime], [CreatedBy], [Deleted] 
	FROM [Core].[Quotes]
	WHERE [Id] = @id
END