CREATE TABLE [dbo].[tx_Mapping_AccountUserGroup] (
    [DocumentId]  INT IDENTITY (1, 1) NOT NULL,
    [AccountId]   INT NULL,
    [UserGroupId] INT NULL,
    CONSTRAINT [PK_tx_Mapping_AccountUserGroup] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
    CONSTRAINT [FK_tx_Account_tx_Mapping_AccountUserGroup] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[tx_Account] ([AccountId]),
    CONSTRAINT [FK_tx_Mapping_AccountUserGroup_tx_UserGroup] FOREIGN KEY ([UserGroupId]) REFERENCES [dbo].[tx_UserGroup] ([UserGroupId])
);







