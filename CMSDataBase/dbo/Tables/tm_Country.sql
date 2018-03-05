CREATE TABLE [dbo].[tm_Country] (
    [CountryId]   INT            IDENTITY (1, 1) NOT NULL,
    [CountryName] NVARCHAR (200) NOT NULL,
    [IsActive]    BIT            CONSTRAINT [DF_tm_Country_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tm_Country] PRIMARY KEY CLUSTERED ([CountryId] ASC)
);



