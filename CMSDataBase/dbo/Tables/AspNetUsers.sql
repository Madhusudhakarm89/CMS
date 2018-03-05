USE [ClaimManagementSystem]
GO

/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 11/16/2016 9:41:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Salutation] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[IsApproved] [bit] NOT NULL CONSTRAINT [DF_AspNetUsers_IsApproved]  DEFAULT ((0)),
	[IsLockedOut] [bit] NOT NULL CONSTRAINT [DF_AspNetUsers_IsLockedOut]  DEFAULT ((0)),
	[CreateDate] [datetime] NOT NULL CONSTRAINT [DF_AspNetUsers_CreateDate]  DEFAULT (getdate()),
	[LastLoginDate] [datetime] NULL,
	[LastPasswordChangedDate] [datetime] NULL,
	[LastLockoutDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [DF_AspNetUsers_IsActive]  DEFAULT ((1)),
	[Email] [nvarchar](256) NOT NULL,
	[EmailConfirmed] [bit] NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NULL,
	[TwoFactorEnabled] [bit] NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NULL,
	[AccessFailedCount] [int] NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[Street] [nvarchar](50) NULL,
	[City] [nvarchar](200) NULL,
	[ProvinceId] [int] NULL,
	[CountryId] [int] NULL,
	[PostalCode] [nvarchar](30) NULL,
	[Department] [nvarchar](200) NULL,
	[UserType] [nvarchar](200) NULL,
	[UserProfile] [nvarchar](200) NULL,
	[CompanyName] [nvarchar](200) NULL,
	[Status] [nvarchar](50) NULL,
	[IsReceiveAlerts] [bit] NOT NULL DEFAULT ((0)),
	[CellNumber] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD FOREIGN KEY([CountryId])
REFERENCES [dbo].[tm_Country] ([CountryId])
GO

ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD FOREIGN KEY([ProvinceId])
REFERENCES [dbo].[tm_Province] ([ProvinceId])
GO



Alter table AspNetUsers
Add Street nvarchar(200),
 City nvarchar(200),
 ProvinceId int, 
 CONSTRAINT ProvinceId FOREIGN KEY(ProvinceId) REFERENCES tm_Province(ProvinceId),
 CountryId int,
 CONSTRAINT CountryId FOREIGN KEY(CountryId) REFERENCES tm_Country(CountryId),
 PostalCode nvarchar(30),
 Department nvarchar(200),
 UserType nvarchar(200),
 UserProfile nvarchar(200),
 CompanyName nvarchar(200),
 Status nvarchar(50),
 IsReceiveAlerts bit Not Null default 0,
 Cellnumber nvarchar(max)
