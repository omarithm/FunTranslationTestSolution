CREATE PROCEDURE [dbo].[Proc_FTHistory_GetById]
	@UserId int
AS
BEGIN
	SELECT 
		[Id], 
		CreatedById, 
		CreatedDate, 
		RequestText,
		Response,
		BaseApiUsed,
		EndpointUsed
	FROM [dbo].[FTRequstsHistory]
	WHERE CreatedById = @UserId
END