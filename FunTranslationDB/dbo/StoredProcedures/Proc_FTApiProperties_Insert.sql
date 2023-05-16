CREATE PROCEDURE [dbo].[Proc_FTApiProperties_Insert]
	@Id int,
	@FunTranslationsApiBaseUrl nvarchar(255),
	@TranslationEndpoint nvarchar(255),
	@FunTranslationsApiKey nvarchar(255)

AS
begin
	set nocount on;

	insert into [dbo].[FTApiProperties](FunTranslationsApiBaseUrl,TranslationEndpoint,FunTranslationsApiKey)
	values (@FunTranslationsApiBaseUrl,@TranslationEndpoint,@FunTranslationsApiKey);

	select @Id = Scope_Identity(); -- Scope_Identity() will get last Id been created for this proc
end