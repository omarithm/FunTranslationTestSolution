CREATE PROCEDURE [dbo].[proc_User_GetById]
	@Id NVARCHAR(128) 
AS
BEGIN
	Set nocount on;

	SELECT Id, FirstName, LastName, EmailAddress, CreatedDate
	from dbo.[Users]
	where Id = @Id;
END
