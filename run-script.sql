USE [ProjectAssignment]
GO

/****** Object:  Table [dbo].[HealthDeclaration]    Script Date: 07/11/2023 22:38:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HealthDeclaration](
	[Id] [nvarchar](450) NOT NULL,
	[DeviceId] [nvarchar](max) NULL,
	[DeviceType] [nvarchar](max) NULL,
	[DeviceName] [nvarchar](max) NULL,
	[GroupId] [nvarchar](max) NULL,
	[DataType] [nvarchar](max) NULL,
	[Timestamp] [bigint] NOT NULL,
 CONSTRAINT [PK_HealthDeclaration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


GO

/****** Object:  Table [dbo].[HealthDeclarationItem]    Script Date: 07/11/2023 22:44:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HealthDeclarationItem](
	[Id] [nvarchar](450) NOT NULL,
	[FullPowerMode] [bit] NOT NULL,
	[ActivePowerControl] [bit] NOT NULL,
	[FirmwareVersion] [int] NOT NULL,
	[Temperature] [int] NOT NULL,
	[Humidity] [int] NOT NULL,
	[Version] [int] NOT NULL,
	[MessageType] [nvarchar](max) NULL,
	[Occupancy] [bit] NOT NULL,
	[StateChanged] [int] NOT NULL,
	[HeaderId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_HealthDeclarationItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[HealthDeclarationItem]  WITH CHECK ADD  CONSTRAINT [FK_HealthDeclarationItem_HealthDeclaration_HeaderId] FOREIGN KEY([HeaderId])
REFERENCES [dbo].[HealthDeclaration] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[HealthDeclarationItem] CHECK CONSTRAINT [FK_HealthDeclarationItem_HealthDeclaration_HeaderId]
GO
