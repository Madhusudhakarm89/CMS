CREATE TABLE [dbo].[tx_Mapping_ContactDocument] (
    [DocumentId]   INT            IDENTITY (1, 1) NOT NULL,
    [ContactId]    INT            NULL,
    [FileName]     NVARCHAR (300) NULL,
    [FileLocation] NVARCHAR (300) NULL,
    CONSTRAINT [PK_tx_Mapping_ContactDocument] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
    CONSTRAINT [FK_tx_Mapping_ContactDocument_tx_Contact] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[tx_Contact] ([ContactId])
);





