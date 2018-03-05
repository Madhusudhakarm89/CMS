CREATE TABLE [dbo].[tx_Mapping_ProviceTaxRates] (
    [TaxRateId]     INT             IDENTITY (1, 1) NOT NULL,
    [ProviceId]     INT             NULL,
    [TaxCategoryId] INT             NULL,
    [Rate]          DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_tx_Mapping_ProviceTaxRates] PRIMARY KEY CLUSTERED ([TaxRateId] ASC),
    CONSTRAINT [FK_tx_Mapping_ProviceTaxRates_tm_TaxCategory] FOREIGN KEY ([ProviceId]) REFERENCES [dbo].[tm_Province] ([ProvinceId]),
    CONSTRAINT [FK_tx_Mapping_ProviceTaxRates_tm_TaxCategory1] FOREIGN KEY ([TaxCategoryId]) REFERENCES [dbo].[tm_TaxCategory] ([TaxCategoryId])
);

