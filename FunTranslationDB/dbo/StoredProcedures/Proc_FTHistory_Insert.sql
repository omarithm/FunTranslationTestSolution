CREATE PROCEDURE [dbo].[Proc_FTHistory_Insert]
	@Id int,
	@CreatedById NVARCHAR(128),
	@CreatedDate DateTime2,
	@RequestText NVARCHAR(MAX),
	@Response NVARCHAR(MAX),
	@BaseApiUsed NVARCHAR(255),
	@EndpointUsed NVARCHAR(255)

AS
begin
	set nocount on;

	insert into [dbo].[FTRequstsHistory](CreatedById,CreatedDate,RequestText,Response,BaseApiUsed,EndpointUsed)
	values (@CreatedById,@CreatedDate,@RequestText,@Response,@BaseApiUsed,@EndpointUsed);

	select @Id = Scope_Identity(); -- Scope_Identity() will get last Id been created for this proc
end