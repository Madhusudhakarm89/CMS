CREATE TABLE [dbo].[tm_UserContactDetails] (
    [UserContactId] INT            IDENTITY (1, 1) NOT NULL,
    [UserId]        NVARCHAR (128) NULL,
    [Street]        NVARCHAR (200) NULL,
    [City]          NVARCHAR (200) NULL,
    [Province]      NVARCHAR (200) NULL,
    [Country]       NVARCHAR (200) NULL,
    [Postal]        NVARCHAR (10)  NULL,
    [Phone]         NVARCHAR (30)  NULL,
    [Cell]          NVARCHAR (30)  NULL,
    [Fax]           NVARCHAR (30)  NULL,
    [TollFree]      NVARCHAR (30)  NULL,
    CONSTRAINT [PK_tm_UserContactDetails] PRIMARY KEY CLUSTERED ([UserContactId] ASC),
    CONSTRAINT [FK_AspNetUsers_tm_UserContactDetails] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);







