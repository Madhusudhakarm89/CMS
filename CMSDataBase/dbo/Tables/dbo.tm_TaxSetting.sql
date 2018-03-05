CREATE TABLE [dbo].[tm_TaxSetting] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [TaxRate]          DECIMAL (18, 6) NULL,
    [CountryId]        INT             NULL,
    [ProvinceId]       INT             NULL,
    [IsActive]         BIT             CONSTRAINT [DF_tm_TaxRateSetting_IsActive] DEFAULT ((1)) NULL,
    [CreatedBy]        NVARCHAR (128)  NULL,
    [CreatedDate]      DATETIME        CONSTRAINT [DF_tm_TaxRateSetting_CreatedDate] DEFAULT (getdate()) NULL,
    [LastModifiedBy]   NVARCHAR (128)  NULL,
    [LastModifiedDate] DATETIME        CONSTRAINT [DF_tm_TaxRateSetting_LastModifiedDate] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_tm_TaxRateSetting] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_tm_TaxRateSetting_tm_Country] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[tm_Country] ([CountryId]),
    CONSTRAINT [FK_tm_TaxRateSetting_tm_Province] FOREIGN KEY ([ProvinceId]) REFERENCES [dbo].[tm_Province] ([ProvinceId])
);
