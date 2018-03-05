CREATE TABLE [dbo].[tx_ClaimNote] (
    [NoteId]         INT            IDENTITY (1, 1) NOT NULL,
    [ClaimId]        INT            NOT NULL,
    [Title]          NVARCHAR (500) NOT NULL,
    [Description]    NVARCHAR (MAX) NOT NULL,
    [IsTask]         BIT            CONSTRAINT [DF_tx_ClaimNote_IsTask] DEFAULT ((0)) NOT NULL,
    [TaskDueDate]    DATETIME       NULL,
    [AssignedTo]     NVARCHAR (128) NULL,
    [CreatedDate]    NVARCHAR (30)  NULL,
    [CreatedBy]      NVARCHAR (128) NOT NULL,
    [CreatedOn]      DATETIME       CONSTRAINT [DF_tx_ClaimNote_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [LastModifiedBy] NVARCHAR (128) NOT NULL,
    [LastModifiedOn] DATETIME       CONSTRAINT [DF_tx_ClaimNote_LastModifiedOn] DEFAULT (getdate()) NOT NULL,
    [IsActive]       BIT            CONSTRAINT [DF_tx_ClaimNote_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tx_ClaimNote] PRIMARY KEY CLUSTERED ([NoteId] ASC),
    CONSTRAINT [FK_tx_ClaimNote_AspNetUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_tx_ClaimNote_AspNetUsers_AssignedToUser] FOREIGN KEY ([AssignedTo]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_tx_ClaimNote_AspNetUsers_LastModifiedBy] FOREIGN KEY ([LastModifiedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_tx_ClaimNote_tx_Claim] FOREIGN KEY ([ClaimId]) REFERENCES [dbo].[tx_Claim] ([ClaimId])
);



