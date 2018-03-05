CREATE TABLE [dbo].[tx_TimeLog] (
    [TimeLogId]      INT             IDENTITY (1, 1) NOT NULL,
    [ClaimId]        INT             NOT NULL,
    [CompanyId]      INT             NOT NULL,
    [ServiceItemId]  INT             NOT NULL,
    [HoursSpent]     VARCHAR (50)    NULL,
    [Quantity]       DECIMAL (18, 2) NULL,
    [Comment]        NVARCHAR (MAX)  NULL,
    [TaskDate]       DATETIME        NOT NULL,
    [LoggedOn]       DATETIME        NOT NULL,
    [AdjusterId]     NVARCHAR (128)  NOT NULL,
    [CreatedBy]      NVARCHAR (128)  NOT NULL,
    [CreatedOn]      DATETIME        NOT NULL,
    [LastModifiedBy] NVARCHAR (128)  NOT NULL,
    [LastModifiedOn] DATETIME        NOT NULL,
    [IsActive]       BIT             CONSTRAINT [DF_tx_TimeLog_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tx_TimeLog] PRIMARY KEY CLUSTERED ([TimeLogId] ASC),
    CONSTRAINT [FK_tx_TimeLog_AspNetUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_tx_TimeLog_AspNetUsers_Adjuster] FOREIGN KEY ([AdjusterId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_tx_TimeLog_AspNetUsers1] FOREIGN KEY ([LastModifiedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_tx_TimeLog_tm_ServiceItem] FOREIGN KEY ([ServiceItemId]) REFERENCES [dbo].[tm_ServiceItem] ([ServiceItemId]),
    CONSTRAINT [FK_tx_TimeLog_tx_Account] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[tx_Account] ([AccountId]),
    CONSTRAINT [FK_tx_TimeLog_tx_Claim] FOREIGN KEY ([ClaimId]) REFERENCES [dbo].[tx_Claim] ([ClaimId])
);





