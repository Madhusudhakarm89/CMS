CREATE TABLE [dbo].[tm_ClaimStatus] (
    [ClaimStatusId] INT           IDENTITY (1, 1) NOT NULL,
    [Status]        NVARCHAR (50) NULL,
    [SortOrder]     INT           NULL,
    [IsActive]      BIT           CONSTRAINT [DF_tm_ClaimStatus_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tm_ClaimStatus] PRIMARY KEY CLUSTERED ([ClaimStatusId] ASC)
);





