CREATE TABLE [dbo].[tx_Mapping_AccountDocument] (
    [DocumentId]   INT            IDENTITY (1, 1) NOT NULL,
    [AccountId]    INT            NOT NULL,
    [FileName]     NVARCHAR (300) NULL,
    [FileLocation] NVARCHAR (300) NULL,
    CONSTRAINT [PK_tx_Mapping_AccountDocument] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
    CONSTRAINT [FK_tx_Account_tx_Mapping_AccountDocument] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[tx_Account] ([AccountId])
);







