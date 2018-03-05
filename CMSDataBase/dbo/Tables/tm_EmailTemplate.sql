CREATE TABLE [dbo].[tm_EmailTemplate] (
    [EmailTemplateId] INT            IDENTITY (1, 1) NOT NULL,
    [TemplateName]    NVARCHAR (100) NULL,
    [TemplateHtml]    NVARCHAR (MAX) NULL,
    [BodyContent]     NVARCHAR (MAX) NULL,
    [Subject]         NVARCHAR (200) NULL,
    [From]            NVARCHAR (200) NULL,
    CONSTRAINT [PK_tm_EmailTemplate] PRIMARY KEY CLUSTERED ([EmailTemplateId] ASC)
);



