/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

INSERT [dbo].[AspNetUsers] ([Id], [Salutation], [FirstName], [LastName], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [IsActive], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName],[Street],[City],[ProvinceId],[CountryId],[PostalCode],[Department],[UserType],[UserProfile],[CompanyName],[Status],[IsReceiveAlerts],[CellNumber]) VALUES (N'315b6f29-e412-420b-8548-a2ed4dcb5094', N'Mr', N'Mayank', N'Shrivastava', 1, 0, CAST(N'2016-05-06 04:45:08.413' AS DateTime), CAST(N'2016-05-06 04:45:08.413' AS DateTime), CAST(N'2016-05-06 04:45:08.413' AS DateTime), CAST(N'2016-01-01 00:00:00.000' AS DateTime), 1, N'mayank3285@yahoo.com', 1, N'AE/HvcgaL/Jw8G7rTiKyGELRibN2Qx2oyaZSxu3lTJZNDfqLW7Ccv1GVbi3U+MH+zA==', N'45e60402-94c1-4292-a2b5-a7017959a000', NULL, 0, 0, NULL, 1, 0, N'mayank3285@yahoo.com','Test','Test',56,2,'14785','Test','1','1','2',0,1,'(147) 852-0369')
GO
INSERT [dbo].[tm_Users] ([Id], [Hometown], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ac98be78-1ce9-4a8c-9fe9-560334caa2e4', N'Gurgaon', N'ab@cms.com', 0, N'AFYymasjWKoAX6ocUk1Fe1/s2oNAldwENvH1N68nnJZu+BcLXXdlBbRTPRTo9bjcxw==', N'92a24b66-8a60-47e3-8a94-53c9a079ae67', NULL, 0, 0, NULL, 1, 0, N'ab@cms.com')
GO
INSERT [dbo].[tm_Users] ([Id], [Hometown], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'cccc0316-d25c-46b1-8255-2b6c95f84c5f', N'Chhatarpur', N'mayank3285@yahoo.com', 0, N'ACAtM/Zp5cLFPFzt42CbUbhq3Lmz63X0rXTD4uA9CDAEv6qzwP0/YFcGvbbIksAfYg==', N'82945edd-0191-45dd-ab01-ff41f260f37c', NULL, 0, 0, NULL, 0, 0, N'mayank3285@yahoo.com')
GO
SET IDENTITY_INSERT [dbo].[tm_ClaimStatus] ON 

GO
INSERT [dbo].[tm_ClaimStatus] ([ClaimStatusId], [Status]) VALUES (1, N'Active')
GO
INSERT [dbo].[tm_ClaimStatus] ([ClaimStatusId], [Status]) VALUES (2, N'Processed')
GO
SET IDENTITY_INSERT [dbo].[tm_ClaimStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[tm_Country] ON 

GO
INSERT [dbo].[tm_Country] ([CountryId], [CountryName]) VALUES (1, N'USA')
GO
INSERT [dbo].[tm_Country] ([CountryId], [CountryName]) VALUES (2, N'Canada')
GO
INSERT [dbo].[tm_Country] ([CountryId], [CountryName]) VALUES (3, N'Mexico')
GO
INSERT [dbo].[tm_Country] ([CountryId], [CountryName]) VALUES (4, N'Chile')
GO
SET IDENTITY_INSERT [dbo].[tm_Country] OFF
GO
SET IDENTITY_INSERT [dbo].[tm_Province] ON 

GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (1, N'Alabama', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (2, N'Alaska', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (3, N'Arizona', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (4, N'Arkansas', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (5, N'California', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (6, N'Colorado', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (7, N'Connecticut', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (8, N'Delaware', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (9, N'Florida', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (10, N'Georgia', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (11, N'Hawaii', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (12, N'Idaho', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (13, N'Illinois', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (14, N'Indiana', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (15, N'Iowa', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (16, N'Kansas', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (17, N'Kentucky', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (18, N'Louisiana', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (19, N'Maine', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (20, N'Maryland', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (21, N'Massachusetts', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (22, N'Michigan', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (23, N'Minnesota', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (24, N'Mississippi', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (25, N'Missouri', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (26, N'Montana', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (27, N'Nebraska', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (28, N'Nevada', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (29, N'New Hampshire', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (30, N'New Jersey', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (31, N'New Mexico', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (32, N'New York', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (33, N'North Carolina', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (34, N'North Dakota', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (35, N'Ohio', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (36, N'Oklahoma', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (37, N'Oregon', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (38, N'Pennsylvania', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (39, N'Rhode Island', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (40, N'South Carolina', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (41, N'South Dakota', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (42, N'Tennessee', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (43, N'Texas', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (44, N'Utah', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (45, N'Vermont', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (46, N'Virginia', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (47, N'Washington', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (48, N'West Virginia', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (49, N'Wisconsin', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (50, N'Wyoming', 1)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (51, N' Nunavut', 2)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (52, N' Quebec', 2)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (53, N' Northwest Territories', 2)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (54, N' Ontario', 2)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (55, N' British Columbia', 2)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (56, N' Alberta', 2)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (57, N' Saskatchewan', 2)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (58, N' Manitoba', 2)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (59, N' Yukon', 2)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (60, N' Newfoundland and Labrador', 2)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (61, N' New Brunswick', 2)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (62, N' Nova Scotia', 2)
GO
INSERT [dbo].[tm_Province] ([ProvinceId], [ProvinceName], [CountryId]) VALUES (63, N' Prince Edward Island', 2)
GO
SET IDENTITY_INSERT [dbo].[tm_Province] OFF
GO
SET IDENTITY_INSERT [dbo].[tx_Contact] ON 

GO
INSERT [dbo].[tx_Contact] ([ContactId], [ContactName], [CompanyName], [Type], [EmailId], [Phone], [Cell], [Street], [City], [ProvinceId], [CountryId], [Postal]) VALUES (1, N'Vishal Khanna', N'DBS', NULL, N'vishal@khanna.com', N'43594099', N'38942300', N'4th St', N' Ontario', 54, 2, N'2345423')
GO
INSERT [dbo].[tx_Contact] ([ContactId], [ContactName], [CompanyName], [Type], [EmailId], [Phone], [Cell], [Street], [City], [ProvinceId], [CountryId], [Postal]) VALUES (2, N'Mayank Shrivastava', N'EVS', NULL, N'mayank@shrivastava.com', N'87456348', N'89897999', N'32', N'New York', 32, 1, N'768776')
GO
SET IDENTITY_INSERT [dbo].[tx_Contact] OFF
GO
SET IDENTITY_INSERT [dbo].[tx_Claim] ON 

GO
INSERT [dbo].[tx_Claim] ([ClaimId], [ClientCompanyName], [ClaimName], [ContactId], [CompanyFileNo], [ClaimNo], [ClaimStatusId], [SubscribeToAlert], [VisibleToClient]) VALUES (1, N'ABC', N'Mayank', 2, N'File#121', N'1214232', 1, 1, 0)
GO
INSERT [dbo].[tx_Claim] ([ClaimId], [ClientCompanyName], [ClaimName], [ContactId], [CompanyFileNo], [ClaimNo], [ClaimStatusId], [SubscribeToAlert], [VisibleToClient]) VALUES (2, N'XYZ', N'Vishal', 1, N'File#3234', N'345400', 2, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[tx_Claim] OFF
GO
SET IDENTITY_INSERT [dbo].[tx_Account] ON 

GO
INSERT [dbo].[tx_Account] ([AccountId], [AccountName], [CompanyName], [ParentCompanyName], [Type], [Owner], [EmailId], [Phone], [Street], [City], [Province], [Country], [Postal]) VALUES (1, N'ABC Account', N'DBS', NULL, N'Insurer', N'DBS', N'dbs@dbs.dbs', N'2498348032', N'jkmnmm', N'New York', 32, 1, N'34234')
GO
INSERT [dbo].[tx_Account] ([AccountId], [AccountName], [CompanyName], [ParentCompanyName], [Type], [Owner], [EmailId], [Phone], [Street], [City], [Province], [Country], [Postal]) VALUES (2, N'Shire Account', N'ABS Shire', NULL, N'Insurer', N'DBS', N'dbs@dbs.dbs', N'342345232', N'43rd St', N'New York', 32, 1, N'34543')
GO
SET IDENTITY_INSERT [dbo].[tx_Account] OFF
GO
SET IDENTITY_INSERT [dbo].[tx_ClaimDetail] ON 

GO
INSERT [dbo].[tx_ClaimDetail] ([ClaimDetailId], [ClaimId], [PolicyNo], [ClaimantEmailId], [LossType], [ClaimAmount], [LossAddressStreet], [LossAddressCity], [LossAddressProvinceId], [LossAddressCountryId], [LossAddressPostal], [LossDateFrom], [LossDateTo], [InformedDateFrom], [InformedDateTo], [FirstContactDate], [FirstInspectionDate], [RCV], [ACV], [Adjuster], [ClaimRep], [ClaimNote], [AspNetUser_Id]) VALUES (3, 1, N'POL21313', N'mayank@shrivastava.com', N'House', CAST(123345 AS Decimal(18, 0)), N'31st St', N'New York', 32, 1, N'243543', CAST(N'2015-02-13 00:00:00.000' AS DateTime), CAST(N'2016-03-12 00:00:00.000' AS DateTime), CAST(N'2015-03-01 00:00:00.000' AS DateTime), CAST(N'2015-03-02 00:00:00.000' AS DateTime), CAST(N'2015-03-01 00:00:00.000' AS DateTime), CAST(N'2015-04-01 00:00:00.000' AS DateTime), N'KSADK', N'KSKSK', N'cccc0316-d25c-46b1-8255-2b6c95f84c5f', N'jkjska', N'jsdfkjhk', NULL)
GO
INSERT [dbo].[tx_ClaimDetail] ([ClaimDetailId], [ClaimId], [PolicyNo], [ClaimantEmailId], [LossType], [ClaimAmount], [LossAddressStreet], [LossAddressCity], [LossAddressProvinceId], [LossAddressCountryId], [LossAddressPostal], [LossDateFrom], [LossDateTo], [InformedDateFrom], [InformedDateTo], [FirstContactDate], [FirstInspectionDate], [RCV], [ACV], [Adjuster], [ClaimRep], [ClaimNote], [AspNetUser_Id]) VALUES (4, 2, N'POL23424', N'vishal@khanna.com', N'Office', CAST(343878 AS Decimal(18, 0)), N'44th St', N'New York', 32, 1, N'98000', CAST(N'2016-02-12 00:00:00.000' AS DateTime), CAST(N'2016-02-12 00:00:00.000' AS DateTime), CAST(N'2016-03-02 00:00:00.000' AS DateTime), CAST(N'2016-03-02 00:00:00.000' AS DateTime), CAST(N'2016-03-02 00:00:00.000' AS DateTime), CAST(N'2016-03-02 00:00:00.000' AS DateTime), N'JFHDSKF', N'jhgK', N'ac98be78-1ce9-4a8c-9fe9-560334caa2e4', N'jhkk', N'hkjhk', NULL)
GO
SET IDENTITY_INSERT [dbo].[tx_ClaimDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[tm_TaxCategory] ON 

GO
INSERT [dbo].[tm_TaxCategory] ([TaxCategoryId], [TaxName]) VALUES (1, N'Category 1')
GO
INSERT [dbo].[tm_TaxCategory] ([TaxCategoryId], [TaxName]) VALUES (2, N'Category 2')
GO
INSERT [dbo].[tm_TaxCategory] ([TaxCategoryId], [TaxName]) VALUES (3, N'Category 3')
GO
SET IDENTITY_INSERT [dbo].[tm_TaxCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[tm_ServiceItem] ON 

GO
INSERT [dbo].[tm_ServiceItem] ([ServiceItemId], [ServiceItemDescription]) VALUES (1, N'Service 1')
GO
INSERT [dbo].[tm_ServiceItem] ([ServiceItemId], [ServiceItemDescription]) VALUES (2, N'Service 2')
GO
INSERT [dbo].[tm_ServiceItem] ([ServiceItemId], [ServiceItemDescription]) VALUES (3, N'Service 3')
GO
SET IDENTITY_INSERT [dbo].[tm_ServiceItem] OFF
GO
