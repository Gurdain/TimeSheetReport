USE [master]
GO
/****** Object:  Database [TimeSheetReport_DB]    Script Date: 21-08-2019 11:45:24 ******/
CREATE DATABASE [TimeSheetReport_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TimeSheetReport', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\TimeSheetReport.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TimeSheetReport_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\TimeSheetReport_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [TimeSheetReport_DB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TimeSheetReport_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TimeSheetReport_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TimeSheetReport_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TimeSheetReport_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TimeSheetReport_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TimeSheetReport_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TimeSheetReport_DB] SET  MULTI_USER 
GO
ALTER DATABASE [TimeSheetReport_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TimeSheetReport_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TimeSheetReport_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TimeSheetReport_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TimeSheetReport_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TimeSheetReport_DB] SET QUERY_STORE = OFF
GO
USE [TimeSheetReport_DB]
GO
/****** Object:  Table [dbo].[Attachment]    Script Date: 21-08-2019 11:45:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attachment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Path] [varchar](max) NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[TaskId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[createdOn] [datetime] NOT NULL,
	[modifiedOn] [datetime] NOT NULL,
	[deletedOn] [datetime] NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Attachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailQueue]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailQueue](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ToAddress] [varchar](50) NOT NULL,
	[FromAddress] [varchar](50) NOT NULL,
	[Subject] [varchar](50) NOT NULL,
	[Body] [varchar](max) NOT NULL,
	[Tries] [int] NOT NULL,
	[createdOn] [datetime] NOT NULL,
	[modifiedOn] [datetime] NOT NULL,
	[deletedOn] [datetime] NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_EmailQueue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Exception]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exception](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [varchar](50) NOT NULL,
	[Message] [varchar](max) NOT NULL,
	[Url] [varchar](50) NOT NULL,
	[Method] [varchar](50) NOT NULL,
	[createdOn] [datetime] NOT NULL,
	[modifiedOn] [datetime] NOT NULL,
	[deletedOn] [datetime] NULL,
	[isDeleted] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MailLog]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ToAddress] [varchar](50) NOT NULL,
	[FromAddress] [varchar](50) NOT NULL,
	[Subject] [varchar](50) NOT NULL,
	[Body] [varchar](max) NOT NULL,
	[EmailStatus] [bit] NOT NULL,
	[createdOn] [datetime] NOT NULL,
	[modifiedOn] [datetime] NOT NULL,
	[deletedOn] [datetime] NULL,
	[isDeleted] [bit] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[createdOn] [datetime] NOT NULL,
	[modifiedOn] [datetime] NOT NULL,
	[deletedOn] [datetime] NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[createdOn] [datetime] NOT NULL,
	[modifiedOn] [datetime] NOT NULL,
	[deletedOn] [datetime] NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[SubmitBy] [datetime] NOT NULL,
	[TraineeId] [int] NOT NULL,
	[TrainerId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[Extension] [bit] NOT NULL,
	[createdOn] [datetime] NOT NULL,
	[modifiedOn] [datetime] NOT NULL,
	[deletedOn] [datetime] NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[RoleId] [int] NOT NULL,
	[DOB] [date] NOT NULL,
	[createdOn] [datetime] NOT NULL,
	[modifiedOn] [datetime] NOT NULL,
	[deletedOn] [datetime] NULL,
	[isDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Attachment] ADD  CONSTRAINT [DF_Attachment_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[EmailQueue] ADD  CONSTRAINT [DF_EmailQueue_Tries]  DEFAULT ((0)) FOR [Tries]
GO
ALTER TABLE [dbo].[EmailQueue] ADD  CONSTRAINT [DF_EmailQueue_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Exception] ADD  CONSTRAINT [DF_Exception_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[MailLog] ADD  CONSTRAINT [DF_MailLog_EmailStatus]  DEFAULT ((0)) FOR [EmailStatus]
GO
ALTER TABLE [dbo].[MailLog] ADD  CONSTRAINT [DF_MailLogs_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Status] ADD  CONSTRAINT [DF_Status_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Task] ADD  CONSTRAINT [DF_Task_StatusId]  DEFAULT ((5)) FOR [StatusId]
GO
ALTER TABLE [dbo].[Task] ADD  CONSTRAINT [DF_Task_Extention]  DEFAULT ((0)) FOR [Extension]
GO
ALTER TABLE [dbo].[Task] ADD  CONSTRAINT [DF_Task_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_User_isDeleted]  DEFAULT ((0)) FOR [isDeleted]
GO
ALTER TABLE [dbo].[Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Attachment_Task] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Task] ([Id])
GO
ALTER TABLE [dbo].[Attachment] CHECK CONSTRAINT [FK_Attachment_Task]
GO
ALTER TABLE [dbo].[Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Attachment_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Attachment] CHECK CONSTRAINT [FK_Attachment_Users]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Status]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Users] FOREIGN KEY([TraineeId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Users]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Users1] FOREIGN KEY([TrainerId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Users1]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Role]
GO
/****** Object:  StoredProcedure [dbo].[Attachment_Delete]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Attachment_Delete](
@Id int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Attachment
					SET isDeleted = 1, deletedOn = GETUTCDATE()
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Attachment_Insert]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Attachment_Insert](
@Id int out,
@Title varchar(50),
@Path varchar(max),
@Type varchar(50),
@TaskId int,
@UserId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					INSERT INTO Attachment
									(Title, Path, Type, TaskId, UserId, createdOn, modifiedOn)
					VALUES
									(@Title, @Path, @Type, @TaskId, @UserId, GETUTCDATE(), GETUTCDATE())
					SET @Id = SCOPE_IDENTITY()
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Attachment_Read]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Attachment_Read]
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Attachment
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Attachment_ReadById]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Attachment_ReadById](
@Id int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Attachment
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Attachment_Update]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Attachment_Update](
@Id int,
@Title varchar(50),
@Path varchar(max),
@Type varchar(50),
@TaskId int,
@UserId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Attachment
					SET Title = @Title, Path = @Path, Type = @Type, TaskId = @TaskId, UserId = @UserId, ModifiedOn = GETUTCDATE()
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[EmailQueue_Delete]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[EmailQueue_Delete](
@Id int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE EmailQueue
					SET isDeleted = 1, deletedOn = GETUTCDATE()
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[EmailQueue_Insert]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[EmailQueue_Insert](
@Id int out,
@ToAddress varchar(50),
@FromAddress varchar(50),
@Subject varchar(50),
@Body varchar(max)
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					INSERT INTO EmailQueue
									(ToAddress, FromAddress, Subject, Body, createdOn, modifiedOn)
					VALUES
									(@ToAddress, @FromAddress, @Subject, @Body, GETUTCDATE(), GETUTCDATE())
					SET @Id = SCOPE_IDENTITY()
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[EmailQueue_Read]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[EmailQueue_Read]
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM EmailQueue
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[EmailQueue_Undelete]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[EmailQueue_Undelete](
@Id int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE EmailQueue
					SET IsDeleted = 0, DeletedOn = NULL
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[EmailQueue_Update]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[EmailQueue_Update](
@Id int,
@ToAddress varchar(50),
@FromAddress varchar(50),
@Subject varchar(50),
@Body varchar(max),
@Tries int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE EmailQueue
					SET ToAddress = @ToAddress, FromAddress = @FromAddress, Subject = @Subject, Body = @Body, Tries = @Tries, ModifiedOn = GETUTCDATE()
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Exception_Delete]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Exception_Delete](
@Id int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Exception
					SET isDeleted = 1, deletedOn = GETUTCDATE()
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Exception_Insert]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Exception_Insert](
@Number varchar(50),
@Message varchar(max),
@Url varchar(50),
@Method varchar(50)
)  
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					INSERT INTO Exception
									(Number, Message, Url, Method, createdOn, modifiedOn) 
					VALUES
									(@Number, @Message, @Url, @Method, GETUTCDATE(), GETUTCDATE())
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Exception_Read]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Exception_Read]
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Exception
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Exception_ReadById]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Exception_ReadById](
@Id int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Exception
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[MailLog_Delete]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MailLog_Delete](
@Id int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE MailLog
					SET isDeleted = 1, deletedOn = GETUTCDATE()
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[MailLog_Insert]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MailLog_Insert](
@Id int out,
@ToAddress varchar(50),
@FromAddress varchar(50),
@Subject varchar(50),
@Body varchar(max),
@EmailStatus int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					INSERT INTO MailLog
									(ToAddress, FromAddress, Subject, Body, EmailStatus, createdOn, modifiedOn)
					VALUES
									(@ToAddress, @FromAddress, @Subject, @Body, @EmailStatus, GETUTCDATE(), GETUTCDATE())
					SET @Id = SCOPE_IDENTITY()
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[MailLog_Read]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MailLog_Read]
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM MailLog
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[MailLog_Update]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MailLog_Update](
@Id int,
@ToAddress varchar(50),
@FromAddress varchar(50),
@Subject varchar(50),
@Body varchar(max),
@EmailStatus bit
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE MailLog
					SET ToAddress = @ToAddress, FromAddress = @FromAddress, Subject = @Subject, Body = @Body, EmailStatus = @EmailStatus, ModifiedOn = GETUTCDATE()
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Role_Delete]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Role_Delete](
@Id int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Attachment
					SET isDeleted = 1, deletedOn = GETUTCDATE()
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Role_Insert]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Role_Insert](
@Id int out,
@Name varchar(50)
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					INSERT INTO Role
									(Name, createdOn, modifiedOn)
					VALUES
									(@Name, GETUTCDATE(), GETUTCDATE())
					SET @Id = SCOPE_IDENTITY()
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Role_Read]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Role_Read]
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Role
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Role_ReadById]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Role_ReadById](
@Id int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Role
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Role_Update]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Role_Update](
@Id int,
@Name varchar(50)
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Role
					SET Name = @Name, ModifiedOn = GETUTCDATE()
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Status_Delete]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Status_Delete](
@Id int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Status
					SET isDeleted = 1, deletedOn = GETUTCDATE()
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Status_Insert]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Status_Insert](
@Id int out,
@Name varchar(50)
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					INSERT INTO Status
									(Name, createdOn, modifiedOn)
					VALUES
									(@Name, GETUTCDATE(), GETUTCDATE())
					SET @Id = SCOPE_IDENTITY()
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Status_Read]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Status_Read]
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Status
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Status_ReadById]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Status_ReadById](
@Id int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Status
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Status_Update]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Status_Update](
@Id int,
@Name varchar(50)
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Status
					SET Name = @Name, ModifiedOn = GETUTCDATE()
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Task_Delete]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Task_Delete](
@Id int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Task
					SET isDeleted = 1, deletedOn = GETUTCDATE()
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Task_Insert]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Task_Insert](
@Id int out,
@Title varchar(50),
@Description varchar(max),
@SubmitBy datetime,
@TraineeId int,
@TrainerId int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					INSERT INTO Task
									(Title, Description, SubmitBy, TraineeId, TrainerId, createdOn, modifiedOn)
					VALUES
									(@Title, @Description, @SubmitBy, @TraineeId, @TrainerId, GETUTCDATE(), GETUTCDATE())
					SET @Id = SCOPE_IDENTITY()
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Task_Read]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Task_Read]
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Task
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Task_ReadById]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Task_ReadById](
@Id int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Task
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Task_Undelete]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Task_Undelete](
@Id int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Task
					SET isDeleted = 0, DeletedOn = NULL
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Task_Update]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Task_Update](
@Id int,
@Title varchar(50),
@Description varchar(max),
@SubmitBy datetime,
@TraineeId int,
@TrainerId int,
@StatusId int,
@Extension bit
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Task
					SET Title = @Title, Description = @Description, SubmitBy = @SubmitBy, TraineeId = @TraineeId, TrainerId = @TrainerId, StatusId = @StatusId, Extension = @Extension, ModifiedOn = GETUTCDATE()
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Users_Delete]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Users_Delete](
@Id int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Users
					SET isDeleted = 1, deletedOn = GETUTCDATE()
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Users_Insert]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Users_Insert](
@Id int out,
@Name varchar(50),
@Phone varchar(50),
@Email varchar(50),
@Password varchar(50),
@RoleId int,
@DOB date
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					INSERT INTO Users
									(Name, Phone, Email, Password, RoleId, DOB, createdOn, modifiedOn)
					VALUES
									(@Name, @Phone, @Email, @Password, @RoleId, @DOB, GETUTCDATE(), GETUTCDATE())
					SET @Id = SCOPE_IDENTITY()
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Users_Login]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Users_Login](
@Password varchar(20),
@Email varchar(50)
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Users
					WHERE Password = @Password
					AND Email = @Email
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Users_Read]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Users_Read]
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Users
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Users_ReadById]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Users_ReadById](
@Id int
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					SELECT * FROM Users
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
/****** Object:  StoredProcedure [dbo].[Users_Update]    Script Date: 21-08-2019 11:45:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Users_Update](
@Id int,
@Name varchar(50),
@Phone varchar(50),
@Email varchar(50),
@Password varchar(50),
@RoleId int,
@DOB date
)
AS
BEGIN
	SET NOCOUNT OFF;
	BEGIN TRY
	BEGIN TRANSACTION
					UPDATE Users
					SET Name = @Name, Phone = @Phone, Email = @Email, Password = @Password, RoleId = @RoleId, DOB = @DOB, ModifiedOn = GETUTCDATE()
					WHERE Id = @Id
	COMMIT;
	END TRY
	BEGIN CATCH
			SELECT
					Error_Message() AS ErrorMessage;
			ROLLBACK TRANSACTION;
	END CATCH;
END
GO
USE [master]
GO
ALTER DATABASE [TimeSheetReport_DB] SET  READ_WRITE 
GO
