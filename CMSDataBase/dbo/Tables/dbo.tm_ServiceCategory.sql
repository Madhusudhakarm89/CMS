CREATE TABLE [dbo].[tm_ServiceCategory] (
    [ServiceCategoryId]   INT            NOT NULL,
    [ServiceCategoryName] NVARCHAR (200) NULL,
    [SortOrder]           INT            NULL,
    [IsActive]            BIT            CONSTRAINT [DF_tm_ServiceCategory_IsActive] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_tm_ServiceCategory] PRIMARY KEY CLUSTERED ([ServiceCategoryId] ASC)
);