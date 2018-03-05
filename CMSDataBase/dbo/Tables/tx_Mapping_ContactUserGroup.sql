CREATE TABLE [dbo].[tx_Mapping_ContactUserGroup] (
    [DocumentId]  INT IDENTITY (1, 1) NOT NULL,
    [ContactId]   INT NULL,
    [UserGroupId] INT NULL,
    CONSTRAINT [PK_tx_Mapping_ContactUserGroup] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
    CONSTRAINT [FK_tx_Mapping_ContactUserGroup_tx_Contact] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[tx_Contact] ([ContactId]),
    CONSTRAINT [FK_tx_Mapping_ContactUserGroup_tx_UserGroup] FOREIGN KEY ([UserGroupId]) REFERENCES [dbo].[tx_UserGroup] ([UserGroupId])
);







