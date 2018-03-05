CREATE TABLE [dbo].[tx_Reminder] (
    [ReminderId]   INT            IDENTITY (1, 1) NOT NULL,
    [Title]        NVARCHAR (200) NOT NULL,
    [Description]  NVARCHAR (MAX) NULL,
    [ReminderTime] DATETIME       NOT NULL,
    [CreatedBy]    NVARCHAR (128) NOT NULL,
    [CreatedOn]    DATETIME       CONSTRAINT [DF_tx_Reminder_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [IsRead]       BIT            CONSTRAINT [DF_tx_Reminder_IsRead] DEFAULT ((0)) NOT NULL,
    [IsActive]     BIT            CONSTRAINT [DF_tx_Reminder_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tx_Reminder] PRIMARY KEY CLUSTERED ([ReminderId] ASC),
    CONSTRAINT [FK_tx_Reminder_AspNetUsers] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id])
);









