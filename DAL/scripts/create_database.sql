CREATE DATABASE [tpcontact]
GO

USE tpcontact
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contact](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[firstname] [varchar](255) NOT NULL,
	[lastname] [varchar](255) NOT NULL,
	[email] [varchar](255) NULL,
	[phone] [varchar](20) NULL
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[User](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[login] [varchar](255) NOT NULL,
	[password] [int] NOT NULL,
	[contact_id] [bigint] NOT NULL,
	CONSTRAINT [FK_Contact] FOREIGN KEY (contact_id) REFERENCES [dbo].[Contact](id),
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Contact_Book](
	[user_id] [bigint] NOT NULL,
	[contact_id] [bigint] NOT NULL
	CONSTRAINT [FK_User] FOREIGN KEY (user_id) REFERENCES [dbo].[User](id),
	CONSTRAINT [FK_Contact_Book] FOREIGN KEY (contact_id) REFERENCES [dbo].[Contact](id)
	)
GO
