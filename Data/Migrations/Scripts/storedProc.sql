
IF object_id('[dbo].[ProcessNotifications]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[ProcessNotifications]
@NotificationId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	insert into UserNotification

	select U.Id,N.NotificationType,0 from [Notification] N
	inner join NotificationPermission NP on N.NotificationType = NP.NotificationType
	inner join RolePermissions RP on RP.ApplicationPermissionId = Np.ApplicationPermissionId
	inner join UserRole UR on UR.RoleId = RP.RoleId
	inner join [User] U on U.Id = UR.UserId 
	where N.Id = @NotificationId and ((N.DebtRecoveryAgentId is not null
                          and ((U.DebtRecoveryAgentId is null and U.Id != 0) or U.DebtRecoveryAgentId = N.DebtRecoveryAgentId))

                       or (N.DebtRecoveryAgentId is null and U.Id != 0)) 



END')
END

IF object_id('[dbo].[GetCurrentLegalSummary]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[GetCurrentLegalSummary]
	@projectId int --GetLegalSummary null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Declare @table table (
	NPLCasesInCourtNo int,
	NPLCasesInCourtValue decimal(18,2),
	RecoverySuitsByBankNo int,
	RecoverySuitsByBankValue int,
	SuitsAgainstBankNo int,
	SuitsAgainstBankValue decimal(18,2),
	FCAALCoDefendantNo int,
	FCAALCoDefendantValue decimal(18,2),
	FCAALJudgementFavourNo int,
	FCAALJudgementFavourValue decimal (18,2),
	CasesByFCAALNo int,
	CasesByFCAALValue decimal(18,2),
	CasesAgainstFCAALNo int,
	CasesAgainstFCAALValue decimal(18,2),
	OutOfCourtSettlementNo int,
	OutOfCourtSettlementValue decimal(18,2),
	CasesInADRNo int,
	CaseInADRValue decimal(18,2),
	CourtType int
	)


	--- insert all court types 
	insert into @table(CourtType) values (1)
	insert into @table(CourtType) values (2)
	insert into @table(CourtType) values (3)
	insert into @table(CourtType) values (4)


	-- cases in court
	
	update a set a.NPLCasesInCourtNo = tb1.NPLCasesInCourtNo,
				 a.NPLCasesInCourtValue = tb1.NPLCasesInCourtValue
	 from @table a inner join
	(select count(o.Id) NPLCasesInCourtNo , sum(convert(decimal, isnull(prevJudge.JudgementSum, o.InformingObligorDateAmount))) NPLCasesInCourtValue, 
	ol.CourtStatus CourtType from Obligor o
	inner join ObligorLitigation ol on o.Id = ol.ObligorId
	left join ObligorLitigation olPrev on olPrev.Id = ol.PreviousLitigationId
	left join Judgement prevJudge on prevJudge.ObligorLitigationId = olPrev.Id
	where o.Litigation = 1 and o.IsDeleted = 0 and ol.IsActive = 1
	group by ol.CourtStatus) tb1 on a.CourtType = tb1.CourtType

--- suits by bank
	
	update a set a.RecoverySuitsByBankNo = tb1.totalNumber,
				 a.RecoverySuitsByBankValue = tb1.Value
	 from @table a inner join
	(select count(o.Id) totalNumber  , sum(convert(decimal,isnull(prevJudge.JudgementSum,o.InformingObligorDateAmount))) Value, 
	ol.CourtStatus CourtType from Obligor o
	inner join ObligorLitigation ol on o.Id = ol.ObligorId
	left join ObligorLitigation olPrev on olPrev.Id = ol.PreviousLitigationId
	left join Judgement prevJudge on prevJudge.ObligorLitigationId = olPrev.Id
	where  o.Litigation = 1 and o.IsDeleted = 0 and ol.SuedBy = 2
	group by ol.CourtStatus) tb1 on a.CourtType = tb1.CourtType


--- suits against bank alone
	
	update a set a.SuitsAgainstBankNo = tb1.totalNumber,
				 a.SuitsAgainstBankValue = tb1.Value
	 from @table a inner join
	(select count(o.Id) totalNumber  , sum(convert(decimal,isnull(prevJudge.JudgementSum,o.InformingObligorDateAmount))) Value, 
	ol.CourtStatus CourtType from Obligor o
	inner join ObligorLitigation ol on o.Id = ol.ObligorId
	left join ObligorLitigation olPrev on olPrev.Id = ol.PreviousLitigationId
	left join Judgement prevJudge on prevJudge.ObligorLitigationId = olPrev.Id
	where  o.Litigation = 1 and o.IsDeleted = 0  and ol.SuedBy = 3 
	and ol.IsFcaalLitigation = 0
	group by ol.CourtStatus) tb1 on a.CourtType = tb1.CourtType


--- fcaal co defendant
	
	update a set a.FCAALCoDefendantNo = tb1.totalNumber,
				 a.FCAALCoDefendantValue = tb1.Value
	 from @table a inner join
	(select count(o.Id) totalNumber  , sum(convert(decimal,isnull(prevJudge.JudgementSum,o.InformingObligorDateAmount))) Value, 
	ol.CourtStatus CourtType from Obligor o
	inner join ObligorLitigation ol on o.Id = ol.ObligorId
	left join ObligorLitigation olPrev on olPrev.Id = ol.PreviousLitigationId
	left join Judgement prevJudge on prevJudge.ObligorLitigationId = olPrev.Id
	where o.Litigation = 1 and o.IsDeleted = 0 and ol.SuedBy = 3 
	and ol.IsFcaalLitigation = 1
	group by ol.CourtStatus) tb1 on a.CourtType = tb1.CourtType


	--- In favour of bank / fcaal
	
	update a set a.FCAALJudgementFavourNo = tb1.totalNumber,
				 a.FCAALJudgementFavourValue = tb1.Value
	 from @table a inner join
	(select count(o.Id) totalNumber,sum(convert(decimal,isnull(prevJudge.JudgementSum,j.JudgementSum))) Value, 
	ol.CourtStatus CourtType from Obligor o
	inner join ObligorLitigation ol on o.Id = ol.ObligorId
	inner join Judgement J on ol.Id = j.ObligorLitigationId
	left join ObligorLitigation olPrev on olPrev.Id = ol.PreviousLitigationId
	left join Judgement prevJudge on prevJudge.ObligorLitigationId = olPrev.Id
	where o.Litigation = 0 and o.IsDeleted = 0 and ol.IsFinalVerdict = 1 and j.JudgmentStatus = 1 and ol.IsActive = 1
	group by ol.CourtStatus) tb1 on a.CourtType = tb1.CourtType


	-- cases by fcaal

	update a set a.CasesByFCAALNo = tb1.totalNumber,
				 a.CasesAgainstFCAALValue = tb1.Value
	 from @table a inner join
	(select count(o.Id) totalNumber  , sum(convert(decimal,isnull(prevJudge.JudgementSum,o.InformingObligorDateAmount))) Value, 
	ol.CourtStatus CourtType from Obligor o
	inner join ObligorLitigation ol on o.Id = ol.ObligorId
	left join ObligorLitigation olPrev on olPrev.Id = ol.PreviousLitigationId
	left join Judgement prevJudge on prevJudge.ObligorLitigationId = olPrev.Id
	where o.Litigation = 1 and o.IsDeleted = 0 and ol.SuedBy = 1
	group by ol.CourtStatus) tb1 on a.CourtType = tb1.CourtType

	

	-- judgements against fcaal /bank

		update a set a.FCAALJudgementFavourNo = tb1.totalNumber,
				 a.FCAALJudgementFavourValue = tb1.Value
	 from @table a inner join
	(select count(o.Id) totalNumber,sum(convert(decimal,isnull(prevJudge.JudgementSum,j.JudgementSum))) Value, 
	ol.CourtStatus CourtType from Obligor o
	inner join ObligorLitigation ol on o.Id = ol.ObligorId
	inner join Judgement J on ol.Id = j.ObligorLitigationId
	left join ObligorLitigation olPrev on olPrev.Id = ol.PreviousLitigationId
	left join Judgement prevJudge on prevJudge.ObligorLitigationId = olPrev.Id
	where o.Litigation = 0 and o.IsDeleted = 0 and ol.IsFinalVerdict = 1 and j.JudgmentStatus = 2 and ol.IsActive = 1
	group by ol.CourtStatus) tb1 on a.CourtType = tb1.CourtType

	-- out of court

		update a set a.OutOfCourtSettlementNo = tb1.totalNumber,
				 a.OutOfCourtSettlementValue = tb1.Value
	 from @table a inner join
	(select count(o.Id) totalNumber,sum(convert(decimal,isnull(prevJudge.JudgementSum,j.JudgementSum))) Value, 
	ol.CourtStatus CourtType from Obligor o
	inner join ObligorLitigation ol on o.Id = ol.ObligorId
	inner join Judgement J on ol.Id = j.ObligorLitigationId
	left join ObligorLitigation olPrev on olPrev.Id = ol.PreviousLitigationId
	left join Judgement prevJudge on prevJudge.ObligorLitigationId = olPrev.Id
	where o.IsDeleted = 0 and ol.IsFinalVerdict = 1 and j.SettleOutOfCourt = 1 and ol.IsActive = 1
	group by ol.CourtStatus) tb1 on a.CourtType = tb1.CourtType


-- cases in adr

		update a set a.CasesInADRNo = tb1.totalNumber,
				 a.CaseInADRValue = tb1.Value
	 from @table a inner join
	(select count(o.Id) totalNumber  , sum(convert(decimal,isnull(prevJudge.JudgementSum,o.InformingObligorDateAmount))) Value, 
	ol.CourtStatus CourtType from Obligor o
	inner join ObligorLitigation ol on o.Id = ol.ObligorId
	left join ObligorLitigation olPrev on olPrev.Id = ol.PreviousLitigationId
	left join Judgement prevJudge on prevJudge.ObligorLitigationId = olPrev.Id
	where  o.Litigation = 1 and o.IsDeleted = 0  and ol.CourtStatus = 4
	group by ol.CourtStatus) tb1 on a.CourtType = tb1.CourtType

	select * from @table

END')
END

IF object_id('[dbo].[GetOperationSummaryReport]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[GetOperationSummaryReport] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		Declare @table table (
	LoanClass int,
	NumberOfObligors int,
	OutstandingBalance decimal(18,2),
	OngoingProposalNo int,
	OngoingProposalProposedAmount decimal(18,2),
	OngoingProposalLoanBalance decimal(18,2),
	OngoingProposalPercentProposalToBalance decimal(18,2),
	ApprovedProposalNo int,
	ApprovedProposalAmount decimal (18,2),
	ApprovedProposalLoanBalance decimal (18,2),
	ApprovedProposalPercentProposalToBalance decimal(18,2),
	CollectionNoOfObligors int,
	CollectionAmountToDate decimal(18,2),
	CollectionApprovedAmount decimal (18,2),
	CollectionWillingObligorNo int,
	CollectionWillingObligorAmount decimal(18,2),
	TotalRecoveries decimal(18,2)
	)

	insert into @table(LoanClass) values (1)
	insert into @table(LoanClass) values (2)
	insert into @table(LoanClass) values (3)
	insert into @table(LoanClass) values (4)
	insert into @table(LoanClass) values (5)
	insert into @table(LoanClass) values (6)

	-- total report

	update a set a.NumberOfObligors = tb1.NoOfObligors,
				 a.OutstandingBalance = tb1.totalInformingObligorDateAmount
	 from @table a inner join
	(select count(o.Id) NoOfObligors , sum(convert(decimal,o.InformingObligorDateAmount)) totalInformingObligorDateAmount,
	c.CollateralClass from Obligor o
	inner join Collateral c on o.Id = c.ObligorId
	where o.IsDeleted = 0 
	group by c.CollateralClass) tb1 on a.LoanClass = tb1.CollateralClass


	-- ongoing proposals

		update a set a.OngoingProposalNo = tb1.NoOfObligors,
				 a.OngoingProposalProposedAmount = tb1.totalProposedSum,
				 a.OngoingProposalLoanBalance = tb1.totalInformObligorDateAmount,
				 a.OngoingProposalPercentProposalToBalance = (tb1.totalProposedSum / tb1.totalInformObligorDateAmount) * 100
	 from @table a inner join
	(select count(o.Id) NoOfObligors,sum(p.TotalProposedAmount) totalProposedSum, sum(convert(decimal,o.InformingObligorDateAmount)) totalInformObligorDateAmount,
	c.CollateralClass from Obligor o
	inner join Collateral c on o.Id = c.ObligorId
	inner join Proposal p on o.id = p.ObligorId and p.DRAProposalWorkFlow < 4 and p.IsActive = 1
	where o.IsDeleted = 0 
	group by c.CollateralClass) tb1 on a.LoanClass = tb1.CollateralClass


	-- approved proposals

	update a set a.ApprovedProposalNo = tb1.NoOfObligors,
				 a.ApprovedProposalAmount = tb1.totalProposedSum,
				 a.ApprovedProposalLoanBalance = tb1.totalInformObligorDateAmount,
				 a.ApprovedProposalPercentProposalToBalance = (tb1.totalProposedSum / tb1.totalInformObligorDateAmount) * 100
	 from @table a inner join
	(select count(o.Id) NoOfObligors , sum(p.TotalProposedAmount) totalProposedSum, sum(convert(decimal,o.InformingObligorDateAmount)) totalInformObligorDateAmount,
	c.CollateralClass from Obligor o
	inner join Collateral c on o.Id = c.ObligorId
	inner join Proposal p on o.id = p.ObligorId and p.DRAProposalWorkFlow = 4 and p.IsActive = 1
	where o.IsDeleted = 0 
	group by c.CollateralClass) tb1 on a.LoanClass = tb1.CollateralClass


	-- collections approval

	update a set a.CollectionNoOfObligors = tb1.NoOfObligors,
				 a.CollectionAmountToDate = tb1.totalPaid,
				 a.CollectionApprovedAmount = (tb1.totalPaid/ tb1.totalProposedAmount)*100
	 from @table a inner join
	(select count(o.Id) NoOfObligors , sum(op.AmountPaid) totalPaid, sum(p.TotalProposedAmount) totalProposedAmount,
	c.CollateralClass from Obligor o
	inner join Collateral c on o.Id = c.ObligorId
	inner join Proposal p on o.id = p.ObligorId and p.DRAProposalWorkFlow = 4 and p.IsActive = 1 
	inner join ObligorPayment op on o.id = op.ObligorId and op.DRARecoveriesWorkflow =  1
	where o.IsDeleted = 0 and o.IsFcaalObligor = 0
	group by c.CollateralClass) tb1 on a.LoanClass = tb1.CollateralClass


	-- collections willing clients

	update a set a.CollectionWillingObligorNo = tb1.NoOfObligors,
				 a.CollectionWillingObligorAmount = tb1.totalPaid
	 from @table a inner join
	(select count(o.Id) NoOfObligors , sum(op.AmountPaid) totalPaid,
	c.CollateralClass from Obligor o
	inner join Collateral c on o.Id = c.ObligorId
	inner join ObligorPayment op on o.id = op.ObligorId and op.DRARecoveriesWorkflow =  1
	where o.IsDeleted = 0 and o.IsFcaalObligor = 1
	group by c.CollateralClass) tb1 on a.LoanClass = tb1.CollateralClass


	select * from @table
	--
END')
END

IF object_id('[dbo].[Sp_GetDRAZONEREPORT]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[Sp_GetDRAZONEREPORT]  ---Sp_GetDRAZONEREPORT null,null
	-- Add the parameters for the stored procedure here
@minRecovery decimal, @maxRecovery decimal
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	select r.Name as RegionName, rs.[State], sum(case when oa.id is null then 0 else 1 end) Allocated,
 sum(case when oa.id is null then 1 else 0 end) UnAllocated from Region r
inner join RegionState rs on r.Id = rs.RegionId
inner join Obligor o on o.[State] = rs.[State]
left join ObligorAssignment oa on o.Id = oa.ObligorId
left join ObligorPayment op on op.ObligorId = o.Id
group by r.Name,rs.[State]

having (@minRecovery is not null and @maxRecovery is not null 
		and sum(isnull(op.AmountPaid,0)) >= @minRecovery and sum(isnull(op.AmountPaid,0)) <= @maxRecovery)

		or(@minRecovery is not null and @maxRecovery is null and sum(isnull(op.AmountPaid,0)) >= @minRecovery)

		or(@maxRecovery is not null and @minRecovery is null and sum(isnull(op.AmountPaid,0)) <= @maxRecovery)

		or (@maxRecovery is null and @minRecovery is null and  1 = 1)


		

END')
END

IF object_id('[dbo].[ProcessObligorBulkUpload]') IS NULL
BEGIN
	EXECUTE('CREATE PROCEDURE [dbo].[ProcessObligorBulkUpload] 
	-- Add the parameters for the stored procedure here
	@BulkUploadId int, @ProjectId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- validate null enums
		
		update o set HasError = 1, 
		Errors = case when [State] is null then ''Invalid State'' else '''' end + 
				case when Currency is null then '',Invalid Currency'' else '''' end + 
				case when FacilityType is null then '',Invalid Facility Type'' else '''' end +
				case when CollateralClass is null then '',Invalid Collateral Type'' else '''' end,
				IsProcessed = 1
				from ObligorBulkUpload o
				inner join BulkUpload b on o.BulkUploadId = b.Id
				where b.Id = @BulkUploadId and 
				([State] is null or Currency is null or FacilityType is null
				 or CollateralClass is null)
		
		
		
	--- validate regions

		---insert regions that do not exist
		insert into Region(Name,CreatedById,LastModifiedById,ModifiedDate,CreatedDate,IsDeleted)

		select distinct Region,b.CreatedById,b.CreatedById,b.CreatedDate,b.CreatedDate,0
		from ObligorBulkUpload a
		left join Region r on R.Name = a.Region
		inner join BulkUpload b on a.BulkUploadId = b.Id
		where r.Id is null and b.Id = @BulkUploadId 

		--update a set a.IsProcessed = 1, a.HasError = 1,
		-- a.Errors = case when a.Errors is null then '' else a.Errors + ',' end + ''Invalid Region''
		--from ObligorBulkUpload a
		--left join Region r on R.Name = a.Region
		--inner join BulkUpload b on a.BulkUploadId = b.Id
		--where r.Id is null and b.Id = @BulkUploadId


	-- validate obligorName exists

		update a set a.IsProcessed = 1, a.HasError = 1,
		 a.Errors = case when a.Errors is null then '' else a.Errors + ',' end + ''Obligor name exists''
		from ObligorBulkUpload a
		left join Obligor o on o.FullName = a.Name and o.AccountNumber = a.AccountNumber
		inner join BulkUpload b on a.BulkUploadId = b.Id
		where b.Id = @BulkUploadId and (o.Id is not null and o.Id != a.AppId)

		
		--- handle updates / first check status of obligor

		Update ob set ob.[AccountNumber] = o.AccountNumber,ob.[Branch] = ob.Branch,
		ob.[AccountManagerName] = o.AccountManagerName,ob.[AccountManagerPhoneNumber] = o.AccountManagerPhoneNumber,
		ob.[CACNumber] = o.CACNumber,ob.[FullName] = o.Name,ob.[Address] = o.[Address],ob.[Remark] = o.Remark,
		ob.[PhoneNumber] = o.PhoneNumber,ob.[Email] = o.Email,ob.[Litigation] = o.Litigation,
		ob.[LastModifiedById] = b.CreatedById,ob.[ModifiedDate] = b.CreatedDate,ob.[BVN] = o.BVN,
		ob.[State] = o.[State], ob.LoanClass = o.CollateralClass,
		 ob.[AmountLoaned] = case when ob.[Status] < 3 then o.AmountGranted else ob.AmountLoaned end, 
		  ob.[Currency] = case when ob.[Status] < 3 then o.Currency else ob.[Currency] end,
		   ob.[ExchangeRate] = case when ob.[Status] < 3 then o.ExchangeRate else ob.[ExchangeRate] end,
		    ob.[NairaEquivalent] = case when ob.[Status] < 3 then o.ExchangeRate * o.AmountGranted else ob.[NairaEquivalent] end,
			 ob.[BankCutOffDateAmount] = case when ob.[Status] < 3 then o.[BankCutOffDateAmount] else ob.[BankCutOffDateAmount] end,
			  ob.[LoanPurchaseDateAmount] = case when ob.[Status] < 3 then o.[LoanPurchaseDateAmount] else ob.[LoanPurchaseDateAmount] end,
			   ob.[InformingObligorDateAmount] = case when ob.[Status] < 3 then o.[InformingObligorDateAmount] else ob.[InformingObligorDateAmount] end,
			    ob.[FormalConcession] = case when ob.[Status] < 3 then o.FormalConcession else ob.FormalConcession end
				
				from 
		ObligorBulkUpload o
		inner join BulkUpload b on o.BulkUploadId = b.Id
		inner join Obligor ob on o.AppId = ob.Id
		where o.IsProcessed = 0 and o.AppId != 0 and b.Id = @BulkUploadId

		--- insert into table

		insert into Obligor ([AccountNumber],[Branch],[AccountManagerName],[AccountManagerPhoneNumber]
      ,[AmountLoaned] ,[CACNumber],[FullName],[Address],[PhoneNumber],[Email],[Status]
      ,[Litigation],[Currency],[ExchangeRate],[NairaEquivalent],[FacilityType]
      ,[State],StateId,[ProjectId],[Remark],[BankCutOffDateAmount],[LoanPurchaseDateAmount],[InformingObligorDateAmount]
      ,[FormalConcession],LoanClass,[CreatedById],[LastModifiedById],[ModifiedDate],[CreatedDate],[BVN],IsDeleted)

		select [AccountNumber],AccountBranch,[AccountManagerName],[AccountManagerPhoneNumber]
      ,[AmountGranted] ,[CACNumber],Name,[Address],[PhoneNumber],[Email],1
      ,[Litigation],[Currency],[ExchangeRate],[NairaEquivalent],[FacilityType]
      ,[State],0,@ProjectId,[Remark],[BankCutOffDateAmount],[LoanPurchaseDateAmount],[InformingObligorDateAmount]
      ,[FormalConcession],CollateralClass, b.CreatedById,b.CreatedById,b.CreatedDate,b.CreatedDate,[BVN],0
	  from 
		ObligorBulkUpload o
		inner join BulkUpload b on o.BulkUploadId = b.Id
		where o.IsProcessed = 0 and o.AppId = 0 and b.Id = @BulkUploadId

		--- get new obligorId to create Collateral

		Update o set o.AppId = ob.Id 
		from ObligorBulkUpload o
		inner join Obligor ob on o.Name = ob.FullName
		inner join BulkUpload b on o.BulkUploadId = b.Id
		where o.IsProcessed = 0 and o.AppId = 0  and b.Id = @BulkUploadId

		-- add status information
		update a set a.HasError = 1,
		a.Errors = case when a.Errors is null then '' else a.Errors + ',' end + ''Obligor has been assigned, Loan details and collateral remain unchanged''
		from ObligorBulkUpload a
		left join Obligor o  on o.Id = a.AppId
		inner join BulkUpload b on a.BulkUploadId = b.Id
		where o.Id is not null and b.Id = @BulkUploadId and o.[Status] >= 3



			-- Handle Collateral Update
		
		Update c set c.[Name] = o.CollateralDetails ,c.[OpenMarketValue] = o.CollateralOpenMarketValue,
		c.[ForceSaleValue] = o.ForcedSaleValue, c.[Comments] = o.CollateralComments,c.[LastModifiedById] = b.CreatedById
       ,c.[ModifiedDate] = b.CreatedDate
			From ObligorBulkUpload o
			inner join BulkUpload b on o.BulkUploadId = b.Id
			inner join Obligor ob on o.AppId = ob.Id
			inner join Collateral c on ob.Id = c.ObligorId
			where o.IsProcessed = 0 and o.AppId != 0 and c.ObligorId is null
			and b.Id = @BulkUploadId and ob.[Status] < 3


		-- Handle Collateral Insert

		Insert into Collateral([Name],[OpenMarketValue],[ForceSaleValue],[Comments],
		[CollateralStatus],[ObligorId],[CreatedById],[LastModifiedById]
       ,[ModifiedDate],[CreatedDate],IsDeleted)

	   Select  o.CollateralDetails, o.CollateralOpenMarketValue, o.ForcedSaleValue,
				o.CollateralComments,1, o.AppId,b.CreatedById,b.CreatedById,
				b.CreatedDate, b.CreatedDate,0
	   From ObligorBulkUpload o
		inner join BulkUpload b on o.BulkUploadId = b.Id
		inner join Obligor ob on o.AppId = ob.Id
		left join Collateral c on ob.Id = c.ObligorId
		where o.IsProcessed = 0 and o.AppId != 0 and c.ObligorId is null and b.Id = @BulkUploadId

		 
		--- Update bulk upload details

		update a set a.IsProcessed = 1,a.Passed = 1
		from ObligorBulkUpload a
		inner join BulkUpload b on a.BulkUploadId = b.Id
		where  b.Id = @BulkUploadId and a.IsProcessed = 0


		Declare @totalRows int = (select count(Id) from ObligorBulkUpload where BulkUploadId = @BulkUploadId);
		Declare @totalFailed int = (select count(Id) from ObligorBulkUpload where BulkUploadId = @BulkUploadId and Passed = 0);

	
		Update BulkUpload set TotalProcessed = @totalRows, TotalFailed = @totalFailed,IsProcessed = 1
		where Id = @BulkUploadId
	
    
END')
END



