CREATE TABLE [dbo].[tx_UserGroup] (
    [UserGroupId]   INT            IDENTITY (1, 1) NOT NULL,
    [UserGroupName] NVARCHAR (200) NOT NULL,
    [IsActive]      BIT            CONSTRAINT [DF_tx_UserGroup_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tx_UserGroup] PRIMARY KEY CLUSTERED ([UserGroupId] ASC)
);







