CREATE TABLE [dbo].[Users]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [EmailAddress] NVARCHAR(256) NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(),

	[CreatedBy] nvarchar(128) NULL,
    [ActivationStatus] bit NOT NULL Default 0,
	[ActivationDate] DATETIME2 NULL,
	[Image] image null,
	[ImageURL] nvarchar (256) null,
	[LastSignIn] DATETIME2 NULL,
	[LastPasswordChange] DATETIME2 NULL,
	[IsBlocked] bit NOT NULL Default 0,
	[BlockDate] DATETIME2 NULL
)
