-- Create Database
CREATE DATABASE CleanArchCRUD
GO

-- Create Table
USE [CleanArchCRUD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Email] [varchar](200) NOT NULL UNIQUE,
	[Password] [varchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Insert Data

INSERT INTO [User] (Name, Email, Password)
VALUES ('admin', 'admin@teste.com', 'admin')
GO