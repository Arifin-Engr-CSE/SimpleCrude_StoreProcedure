CREATE TABLE [dbo].[tbl_student] (
    [Id]      INT          IDENTITY (101, 1) NOT NULL,
    [Name]    VARCHAR (20) NULL,
    [Gender]  VARCHAR (20) NULL,
    [Address] VARCHAR (20) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

