CREATE PROCEDURE [dbo].[Proc_FTApiProperties_UpdateById]
	@Id int,
	@FunTranslationsApiBaseUrl nvarchar(255),
	@TranslationEndpoint nvarchar(255),
	@FunTranslationsApiKey nvarchar(255)

AS
begin
	set nocount on;

	Update [dbo].[FTApiProperties]
	Set FunTranslationsApiBaseUrl = @FunTranslationsApiBaseUrl,
	TranslationEndpoint = @TranslationEndpoint,
	FunTranslationsApiKey = @FunTranslationsApiKey
	Where Id = @Id;

end