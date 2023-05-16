CREATE PROCEDURE [dbo].[Proc_FTApiProperties_GetAll]
AS
BEGIN
	SELECT 
		[Id], 
		FunTranslationsApiBaseUrl, 
		TranslationEndpoint, 
		FunTranslationsApiKey
	FROM [dbo].[FTApiProperties]
END