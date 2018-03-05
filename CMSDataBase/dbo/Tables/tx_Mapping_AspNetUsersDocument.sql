USE [ClaimManagementSystem]
GO

/****** Object:  Table [dbo].[tx_Mapping_AspNetUsersDocument]    Script Date: 11/22/2016 10:44:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tx_Mapping_AspNetUsersDocument](
	[DocumentId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[FileType] [nvarchar](30) NOT NULL,
	[FileName] [nvarchar](300) NOT NULL,
	[FileLocation] [nvarchar](300) NOT NULL,
	[FileDisplayName] [nvarchar](250) NOT NULL,
	[CreatedBy] [nvarchar](128) NOT NULL,
	[CreatedOn] [datetime] NOT NULL CONSTRAINT [DF_tx_Mapping_AspNetUsersDocument_CreatedOn]  DEFAULT (getdate()),
	[LastModifiedBy] [nvarchar](128) NOT NULL,
	[LastModifiedOn] [datetime] NOT NULL CONSTRAINT [DF_tx_Mapping_AspNetUsersDocument_LastModifiedOn]  DEFAULT (getdate()),
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_tx_Mapping_AspNetUsersDocument_IsActive]  DEFAULT ((1)),
 CONSTRAINT [PK_tx_Mapping_AspNetUsersDocument] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[tx_Mapping_AspNetUsersDocument]  WITH CHECK ADD  CONSTRAINT [FK_tx_Mapping_AspNetUsersDocument_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[tx_Mapping_AspNetUsersDocument] CHECK CONSTRAINT [FK_tx_Mapping_AspNetUsersDocument_AspNetUsers]
GO

ALTER TABLE [dbo].[tx_Mapping_AspNetUsersDocument]  WITH CHECK ADD  CONSTRAINT [FK_tx_Mapping_AspNetUsersDocument_AspNetUsers_CreatedBy] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[tx_Mapping_AspNetUsersDocument] CHECK CONSTRAINT [FK_tx_Mapping_AspNetUsersDocument_AspNetUsers_CreatedBy]
GO

ALTER TABLE [dbo].[tx_Mapping_AspNetUsersDocument]  WITH CHECK ADD  CONSTRAINT [FK_tx_Mapping_AspNetUsersDocument_AspNetUsers_LastModifiedBy] FOREIGN KEY([LastModifiedBy])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[tx_Mapping_AspNetUsersDocument] CHECK CONSTRAINT [FK_tx_Mapping_AspNetUsersDocument_AspNetUsers_LastModifiedBy]
GO


