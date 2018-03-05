CREATE TABLE [dbo].[tm_ContactType] (
    [ContactTypeId]   INT            IDENTITY (1, 1) NOT NULL,
    [ContactTypeName] NVARCHAR (100) NOT NULL,
    [IsActive]        BIT            CONSTRAINT [DF_tm_ContactType_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tm_ContactType] PRIMARY KEY CLUSTERED ([ContactTypeId] ASC)
);

