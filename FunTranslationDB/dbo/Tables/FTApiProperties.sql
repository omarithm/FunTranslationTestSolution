CREATE TABLE [dbo].[FTApiProperties]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	FunTranslationsApiBaseUrl NVARCHAR(255) NOT NULL,
	TranslationEndpoint NVARCHAR(255) NOT NULL,
	FunTranslationsApiKey NVARCHAR(255) NULL,

)
