CREATE PROCEDURE [Core].[InsertQuote]
	@channel NVARCHAR(200),
	@quoteText NVARCHAR(1000),
	@creationTime DATETIME2(0),
	@createdBy NVARCHAR(200),
	@deleted BIT
AS
BEGIN
	INSERT INTO [Core].[Quotes]([Channel],[QuoteText],[CreationTime],[CreatedBy],[Deleted])
	VALUES (@channel, @quoteText, @creationTime, @createdBy, @deleted)
END