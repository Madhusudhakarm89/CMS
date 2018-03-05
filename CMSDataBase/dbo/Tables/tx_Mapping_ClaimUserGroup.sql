CREATE TABLE [dbo].[tx_Mapping_ClaimUserGroup] (
    [DocumentId]  INT IDENTITY (1, 1) NOT NULL,
    [ClaimId]     INT NULL,
    [UserGroupId] INT NULL,
    CONSTRAINT [PK_tx_Mapping_ClaimUserGroup] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
    CONSTRAINT [FK_tx_Mapping_ClaimUserGroup_tx_Claim] FOREIGN KEY ([ClaimId]) REFERENCES [dbo].[tx_Claim] ([ClaimId]),
    CONSTRAINT [FK_tx_Mapping_ClaimUserGroup_tx_UserGroup] FOREIGN KEY ([UserGroupId]) REFERENCES [dbo].[tx_UserGroup] ([UserGroupId])
);



