CREATE TABLE [dbo].[tm_ServiceItem] (
    [ServiceItemId]          INT             IDENTITY (1, 1) NOT NULL,
    [ClaimId]                INT             NULL,
    [ServiceItemDescription] NVARCHAR (500)  NULL,
    [ServiceItemName]        NVARCHAR (200)  NULL,
    [IsHourBased]            BIT             NULL,
    [DefaultQuantity]        DECIMAL (18, 6) NULL,
    [DefaultFee]             DECIMAL (18, 6) NULL,
    [MinimumFee]             DECIMAL (18, 6) NULL,
    [ServiceCategoryId]      INT             NULL,
    [CreatedDate]            DATETIME        CONSTRAINT [DF_tm_ServiceItem_CreatedDate] DEFAULT (getdate()) NULL,
    [CreatedBy]              NVARCHAR (128)  NULL,
    [LastModifiedDate]       DATETIME        CONSTRAINT [DF_tm_ServiceItem_LastModifiedDate] DEFAULT (getdate()) NULL,
    [LastModifiedBy]         NVARCHAR (128)  NULL,
    [IsActive]               BIT             CONSTRAINT [DF_tm_ServiceItem_IsSActive] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_tm_ServiceItem] PRIMARY KEY CLUSTERED ([ServiceItemId] ASC),
    CONSTRAINT [FK_tm_ServiceItem_tm_ServiceCategory] FOREIGN KEY ([ServiceCategoryId]) REFERENCES [dbo].[tm_ServiceCategory] ([ServiceCategoryId]),
    CONSTRAINT [FK_tm_ServiceItem_tx_Claim] FOREIGN KEY ([ClaimId]) REFERENCES [dbo].[tx_Claim] ([ClaimId])
);

