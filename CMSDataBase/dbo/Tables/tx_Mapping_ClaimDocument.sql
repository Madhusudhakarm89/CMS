CREATE TABLE [dbo].[tx_Mapping_ClaimDocument] (
    [DocumentId]      INT            IDENTITY (1, 1) NOT NULL,
    [ClaimId]         INT            NOT NULL,
    [FileType]        NVARCHAR (30)  NOT NULL,
    [FileName]        NVARCHAR (300) NOT NULL,
    [FileLocation]    NVARCHAR (300) NOT NULL,
    [FileDisplayName] NVARCHAR (250) NOT NULL,
    [CreatedBy]       NVARCHAR (128) NOT NULL,
    [CreatedOn]       DATETIME       CONSTRAINT [DF_tx_Mapping_ClaimDocument_CreatedOn] DEFAULT (getdate()) NOT NULL,
    [LastModifiedBy]  NVARCHAR (128) NOT NULL,
    [LastModifiedOn]  DATETIME       CONSTRAINT [DF_tx_Mapping_ClaimDocument_LastModifiedOn] DEFAULT (getdate()) NOT NULL,
    [IsActive]        BIT            CONSTRAINT [DF_tx_Mapping_ClaimDocument_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tx_Mapping_ClaimDocument] PRIMARY KEY CLUSTERED ([DocumentId] ASC),
    CONSTRAINT [FK_tx_Mapping_ClaimDocument_AspNetUsers_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_tx_Mapping_ClaimDocument_AspNetUsers_LastModifiedBy] FOREIGN KEY ([LastModifiedBy]) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_tx_Mapping_ClaimDocument_tx_Claim] FOREIGN KEY ([ClaimId]) REFERENCES [dbo].[tx_Claim] ([ClaimId])
);



