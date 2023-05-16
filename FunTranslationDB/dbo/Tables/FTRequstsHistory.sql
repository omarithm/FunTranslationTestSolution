CREATE TABLE [dbo].[FTRequstsHistory]
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	CreatedById NVARCHAR(128) NOT NULL,
	CreatedDate DateTime2 NOT NULL DEFAULT getutcdate(),
	RequestText NVARCHAR(MAX) NOT NULL,
	Response NVARCHAR(MAX) NULL,
	BaseApiUsed NVARCHAR(255),
	EndpointUsed NVARCHAR(255)
)
