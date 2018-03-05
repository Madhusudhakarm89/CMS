CREATE TABLE [dbo].[tx_Alert] (
    [AlertId]     INT            IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (200) NOT NULL,
    [Description] NVARCHAR (MAX) NULL,
    [AlertTo]     NVARCHAR (128) NOT NULL,
    [AlertBy]     NVARCHAR (128) NOT NULL,
    [CreatedOn]   DATETIME       CONSTRAINT [DF_tx_Alert_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [IsRead]      BIT            CONSTRAINT [DF_tx_Alert_IsRead] DEFAULT ((0)) NOT NULL,
    [IsActive]    BIT            CONSTRAINT [DF_tx_Alert_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tx_Alert] PRIMARY KEY CLUSTERED ([AlertId] ASC),
    CONSTRAINT [FK_tx_Alert_AspNetUsers] FOREIGN KEY ([AlertTo]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_tx_Alert_AspNetUsers1] FOREIGN KEY ([AlertBy]) REFERENCES [dbo].[AspNetUsers] ([Id])
);









