CREATE TABLE [dbo].[tm_CompanyType] (
    [CompanyTypeId]   INT            IDENTITY (1, 1) NOT NULL,
    [CompanyTypeName] NVARCHAR (100) NOT NULL,
    [IsActive]        BIT            CONSTRAINT [DF_tm_CompanyType_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tm_CompanyType] PRIMARY KEY CLUSTERED ([CompanyTypeId] ASC)
);

