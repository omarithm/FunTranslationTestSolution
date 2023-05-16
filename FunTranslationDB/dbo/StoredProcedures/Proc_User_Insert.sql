CREATE PROCEDURE [dbo].[Proc_User_Insert]
	@Id NVARCHAR(128),
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@EmailAddress NVARCHAR(265),
	@CreatedDate DateTime2

AS
begin
	set nocount on;

	insert into [dbo].[Users](FirstName,LastName,EmailAddress)
	values (@FirstName,@LastName,@EmailAddress);

	select @Id = Scope_Identity(); -- Scope_Identity() will get last Id been created for this proc
end