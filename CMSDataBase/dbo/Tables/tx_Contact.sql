CREATE TABLE [dbo].[tx_Contact] (
    [ContactId]      INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]      NVARCHAR (200) NOT NULL,
    [LastName]       NVARCHAR (200) NULL,
    [CompanyId]      INT            NOT NULL,
    [ContactTypeId]  INT            NULL,
    [OwnerId]        NVARCHAR (128) NULL,
    [EmailId]        NVARCHAR (200) NOT NULL,
    [Phone]          NVARCHAR (30)  NOT NULL,
    [Cell]           NVARCHAR (30)  NULL,
    [IsKeyContact]   BIT            CONSTRAINT [DF_tx_Contact_IsKeyContact] DEFAULT ((0)) NOT NULL,
    [Street]         NVARCHAR (50)  NOT NULL,
    [City]           NVARCHAR (50)  NOT NULL,
    [ProvinceId]     INT            NOT NULL,
    [CountryId]      INT            NOT NULL,
    [Postal]         NVARCHAR (50)  NOT NULL,
    [CreatedBy]      NVARCHAR (128) NULL,
    [CreatedOn]      DATETIME       CONSTRAINT [DF_tx_Contact_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [LastModifiedBy] NVARCHAR (128) NULL,
    [LastModifiedOn] DATETIME       CONSTRAINT [DF_tx_Contact_LastModifiedOn] DEFAULT (getdate()) NOT NULL,
    [IsActive]       BIT            CONSTRAINT [DF_tx_Contact_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tx_Contact] PRIMARY KEY CLUSTERED ([ContactId] ASC),
    CONSTRAINT [FK_tx_Contact_Owner] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_tx_Contact_tm_ContactType] FOREIGN KEY ([ContactTypeId]) REFERENCES [dbo].[tm_ContactType] ([ContactTypeId]),
    CONSTRAINT [FK_tx_Contact_tm_Country] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[tm_Country] ([CountryId]),
    CONSTRAINT [FK_tx_Contact_tm_Province] FOREIGN KEY ([ProvinceId]) REFERENCES [dbo].[tm_Province] ([ProvinceId]),
    CONSTRAINT [FK_tx_Contact_tx_Account] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[tx_Account] ([AccountId])
);









