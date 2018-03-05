USE [ClaimManagementSystem]
GO
/****** Object:  StoredProcedure [dbo].[usp_GlobalSearch_Claim]    Script Date: 04-09-2016 16:47:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_GlobalSearch_Claim]  --'1214232'
	-- Add the parameters for the stored procedure here
	@searchKeyWord nvarchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @companyId varchar(Max),@contactId varchar(Max),@cliamId varchar(Max);


select @companyId =coalesce(@companyId + ', ', '') + cast(AccountId as nvarchar(10))
from tx_Account 
where CompanyName like '%'+@searchKeyWord+'%' 
or KeyContact in  (select coalesce(@contactId + ', ', '') + cast(ContactId as nvarchar(10))
from tx_Contact where
FirstName like '%'+@searchKeyWord+'%' or 
LastName like '%'+@searchKeyWord+'%' )



select @contactId =coalesce(@contactId + ', ', '') + cast(ContactId as nvarchar(5))
from tx_Contact 
where
FirstName like '%'+@searchKeyWord+'%' or 
LastName like '%'+@searchKeyWord+'%' or 
CompanyId in (select * from SplitString(@companyId,','))



select @cliamId =coalesce(@cliamId + ', ', '') + cast(ClaimId as nvarchar(30))
from tx_Claim where FileNo like '%'+@searchKeyWord+'%' or ClaimNo like '%'+@searchKeyWord+'%' or ContactId in (select * from SplitString(@contactId,',')) 
or CompanyId in (select * from SplitString(@companyId,','))

select * from tx_Claim 
where IsActive = 1
		AND ( ClaimId in (select * from SplitString(@cliamId,','))
				OR CompanyId in (select * from SplitString(@companyId,','))
				OR ContactId in (select * from SplitString(@contactId,',')) )
    

--select * from tx_Account where AccountId in
--(select * from SplitString(@companyId,','))

--select * from tx_Contact where ContactId in
--(select * from SplitString(@contactId,','))




END
