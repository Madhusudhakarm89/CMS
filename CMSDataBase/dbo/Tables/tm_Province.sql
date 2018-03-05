CREATE TABLE [dbo].[tm_Province] (
    [ProvinceId]   INT            IDENTITY (1, 1) NOT NULL,
    [ProvinceName] NVARCHAR (200) NOT NULL,
    [CountryId]    INT            NOT NULL,
    [IsActive]     BIT            CONSTRAINT [DF_tm_Province_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tm_Province] PRIMARY KEY CLUSTERED ([ProvinceId] ASC),
    CONSTRAINT [FK_tm_Province_tm_Country] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[tm_Country] ([CountryId])
);



