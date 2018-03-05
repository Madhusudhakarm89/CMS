CREATE TABLE [dbo].[tm_TaxCategory] (
    [TaxCategoryId]  INT            IDENTITY (1, 1) NOT NULL,
    [TaxName]        NVARCHAR (50)  NOT NULL,
    [TaxDescription] NVARCHAR (MAX) NULL,
    [IsActive]       BIT            CONSTRAINT [DF_tm_TaxCategory_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tm_TaxCategory] PRIMARY KEY CLUSTERED ([TaxCategoryId] ASC)
);




