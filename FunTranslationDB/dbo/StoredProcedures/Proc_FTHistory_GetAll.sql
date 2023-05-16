CREATE PROCEDURE [dbo].[Proc_FTHistory_GetAll]
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
END
