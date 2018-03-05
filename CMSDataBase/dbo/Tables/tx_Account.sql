CREATE TABLE [dbo].[tx_Account] (
    [AccountId]       INT            IDENTITY (1, 1) NOT NULL,
    [CompanyName]     NVARCHAR (200) NULL,
    [Type]            NVARCHAR (200) NULL,
    [Unit]            NVARCHAR (100) NULL,
    [Owner]           NVARCHAR (128) NULL,
    [DefaultAdjuster] NVARCHAR (128) NULL,
    [EmailId]         NVARCHAR (200) NULL,
    [Phone]           NVARCHAR (30)  NULL,
    [AlternatePhone]  NVARCHAR (30)  NULL,
    [Extension]       NVARCHAR (10)  NULL,
    [Fax]             NVARCHAR (30)  NULL,
    [Street]          NVARCHAR (200) NULL,
    [City]            NVARCHAR (200) NULL,
    [ProvinceId]      INT            NOT NULL,
    [CountryId]       INT            NOT NULL,
    [Postal]          NVARCHAR (30)  NULL,
    [KeyContact]      INT            NULL,
    [Status]          NVARCHAR (50)  NULL,
    [IsActive]        BIT            CONSTRAINT [DF_tx_Account_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tx_Account] PRIMARY KEY CLUSTERED ([AccountId] ASC),
    CONSTRAINT [FK_tx_Account_tm_Country] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[tm_Country] ([CountryId]),
    CONSTRAINT [FK_tx_Account_tm_Province] FOREIGN KEY ([ProvinceId]) REFERENCES [dbo].[tm_Province] ([ProvinceId])
);









