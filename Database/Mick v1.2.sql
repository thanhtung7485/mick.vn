USE [MickDatabase]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 9/12/2015 2:42:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Domain]    Script Date: 9/12/2015 2:42:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Domain](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Domain] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name], [Description]) VALUES (1, N'hoang', N'abc')
INSERT [dbo].[Category] ([Id], [Name], [Description]) VALUES (2, N'Tung', N'def')
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Domain] ON 

INSERT [dbo].[Domain] ([Id], [Name], [Description]) VALUES (1, N'Banking', N'Banking')
INSERT [dbo].[Domain] ([Id], [Name], [Description]) VALUES (2, N'Education', N'Education')
INSERT [dbo].[Domain] ([Id], [Name], [Description]) VALUES (3, N'Management', N'Management')
INSERT [dbo].[Domain] ([Id], [Name], [Description]) VALUES (4, N'Business', N'Business')
INSERT [dbo].[Domain] ([Id], [Name], [Description]) VALUES (5, N'Advertising', N'Advertising')
INSERT [dbo].[Domain] ([Id], [Name], [Description]) VALUES (6, N'Transfer', N'Transfer')
INSERT [dbo].[Domain] ([Id], [Name], [Description]) VALUES (7, N'Computing', N'Computing')
SET IDENTITY_INSERT [dbo].[Domain] OFF
