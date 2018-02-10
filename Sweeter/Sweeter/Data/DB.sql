USE [master]
GO

/****** Object:  Database [JayData]    Script Date: 30.01.2018 22:06:19 ******/
/*IF DB_ID (N'JayData') IS NOT NULL
BREAK;
GO*/
CREATE DATABASE [JayData]
 CONTAINMENT = NONE
GO

ALTER DATABASE [JayData] SET COMPATIBILITY_LEVEL = 130
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [JayData].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [JayData] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [JayData] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [JayData] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [JayData] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [JayData] SET ARITHABORT OFF 
GO

ALTER DATABASE [JayData] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [JayData] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [JayData] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [JayData] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [JayData] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [JayData] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [JayData] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [JayData] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [JayData] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [JayData] SET  DISABLE_BROKER 
GO

ALTER DATABASE [JayData] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [JayData] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [JayData] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [JayData] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [JayData] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [JayData] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [JayData] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [JayData] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [JayData] SET  MULTI_USER 
GO

ALTER DATABASE [JayData] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [JayData] SET DB_CHAINING OFF 
GO

ALTER DATABASE [JayData] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [JayData] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [JayData] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [JayData] SET QUERY_STORE = OFF
GO

USE [JayData]
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO

ALTER DATABASE [JayData] SET  READ_WRITE 
GO

CREATE TABLE [dbo].[AccountTable](
	[IDuser] [bigint] IDENTITY(1, 1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Avatar] [nvarchar](50) NULL,
 CONSTRAINT [PK_AccountTable] PRIMARY KEY CLUSTERED 
(
	[IDuser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CommentTable](
	[IDcomment] [bigint] IDENTITY(1, 1) NOT NULL,
	[IDpost] [bigint] NOT NULL,
	[IDuser] [bigint] NOT NULL,
	[Text] [text] NOT NULL,
	[LikeNumder] [int] NOT NULL,
 CONSTRAINT [PK_CommentTable] PRIMARY KEY CLUSTERED 
(
	[IDcomment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[LikesToCommentTable](
	[IDus_com] [bigint] IDENTITY(1, 1) NOT NULL,
	[IDuser] [bigint] NOT NULL,
	[IDcomment] [bigint] NOT NULL,
 CONSTRAINT [PK_LikesToCommentTable] PRIMARY KEY CLUSTERED 
(
	[IDus_com] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[LikesToPostTable](
	[IDus_post] [bigint] IDENTITY(1, 1) NOT NULL,
	[IDuser] [bigint] NOT NULL,
	[IDpost] [bigint] NOT NULL,
 CONSTRAINT [PK_LikesToPostTable] PRIMARY KEY CLUSTERED 
(
	[IDus_post] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PostTable](
	[IDpost] [bigint] IDENTITY(1, 1) NOT NULL,
	[IDuser] [bigint] NOT NULL,
	[Text] [text] NOT NULL,
	[PublicDate] [datetime] NOT NULL,
	[LikeNumder] [int] NOT NULL,
	[CommentNumber] [int] NOT NULL,
 CONSTRAINT [PK_PostTable] PRIMARY KEY CLUSTERED 
(
	[IDpost] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[PostTable]  WITH CHECK ADD  CONSTRAINT [FK_PostTable_AccountTable] FOREIGN KEY([IDuser])
REFERENCES [dbo].[AccountTable] ([IDuser])
GO

ALTER TABLE [dbo].[PostTable] CHECK CONSTRAINT [FK_PostTable_AccountTable]
GO

ALTER TABLE [dbo].[CommentTable]  WITH CHECK ADD  CONSTRAINT [FK_CommentTable_PostTable] FOREIGN KEY([IDpost])
REFERENCES [dbo].[PostTable] ([IDpost])
GO

ALTER TABLE [dbo].[CommentTable] CHECK CONSTRAINT [FK_CommentTable_PostTable]
GO

ALTER TABLE [dbo].[LikesToCommentTable]  WITH CHECK ADD  CONSTRAINT [FK_LikesToCommentTable_AccountTable] FOREIGN KEY([IDuser])
REFERENCES [dbo].[AccountTable] ([IDuser])
GO

ALTER TABLE [dbo].[LikesToCommentTable] CHECK CONSTRAINT [FK_LikesToCommentTable_AccountTable]
GO

ALTER TABLE [dbo].[LikesToCommentTable]  WITH CHECK ADD  CONSTRAINT [FK_LikesToCommentTable_CommentTable] FOREIGN KEY([IDcomment])
REFERENCES [dbo].[CommentTable] ([IDcomment])
GO

ALTER TABLE [dbo].[LikesToCommentTable] CHECK CONSTRAINT [FK_LikesToCommentTable_CommentTable]
GO

ALTER TABLE [dbo].[LikesToPostTable]  WITH CHECK ADD  CONSTRAINT [FK_LikesToPostTable_AccountTable] FOREIGN KEY([IDuser])
REFERENCES [dbo].[AccountTable] ([IDuser])
GO

ALTER TABLE [dbo].[LikesToPostTable] CHECK CONSTRAINT [FK_LikesToPostTable_AccountTable]
GO

ALTER TABLE [dbo].[LikesToPostTable]  WITH CHECK ADD  CONSTRAINT [FK_LikesToPostTable_PostTable] FOREIGN KEY([IDpost])
REFERENCES [dbo].[PostTable] ([IDpost])
GO

ALTER TABLE [dbo].[LikesToPostTable] CHECK CONSTRAINT [FK_LikesToPostTable_PostTable]
GO

INSERT INTO [dbo].[AccountTable]
           ([Name]
           ,[Email]
           ,[Password]
           ,[Username]
           ,[Avatar])
     VALUES
           ('Vasa'
           ,'Nub@gmail.com'
           ,'123'
           ,'Nub'
           ,NULL)
INSERT INTO [dbo].[AccountTable]
           ([Name]
           ,[Email]
           ,[Password]
           ,[Username]
           ,[Avatar])
     VALUES
           ('Lilo'
           ,'Lilo@gmail.com'
           ,'321'
           ,'Master'
           ,NULL)
INSERT INTO [dbo].[AccountTable]
           ([Name]
           ,[Email]
           ,[Password]
           ,[Username]
           ,[Avatar])
     VALUES
           ('Bob'
           ,'Bob@gmail.com'
           ,'huk'
           ,'Hucker'
           ,NULL)
INSERT INTO [dbo].[PostTable]
           ([IDuser]
           ,[Text]
           ,[PublicDate]
           ,[LikeNumder]
           ,[CommentNumber])
     VALUES
           (1
           ,'Hi'
           ,1998
           ,1
           ,1)
INSERT INTO [dbo].[PostTable]
           ([IDuser]
           ,[Text]
           ,[PublicDate]
           ,[LikeNumder]
           ,[CommentNumber])
     VALUES
           (2
           ,'Hi'
           ,1998
           ,2
           ,2)
INSERT INTO [dbo].[PostTable]
           ([IDuser]
           ,[Text]
           ,[PublicDate]
           ,[LikeNumder]
           ,[CommentNumber])
     VALUES
           (3
           ,'Hi'
           ,1998
           ,3
           ,3)
INSERT INTO [dbo].[CommentTable]
           ([IDpost]
           ,[IDuser]
           ,[Text]
           ,[LikeNumder])
     VALUES
           (1
           ,1
           ,'I am first'
           ,1)
INSERT INTO [dbo].[CommentTable]
           ([IDpost]
           ,[IDuser]
           ,[Text]
           ,[LikeNumder])
     VALUES
           (2
           ,2
           ,'Ohhhh, Its my first comment'
           ,2)
INSERT INTO [dbo].[CommentTable]
           ([IDpost]
           ,[IDuser]
           ,[Text]
           ,[LikeNumder])
     VALUES
           (3
           ,3
           ,'Good. Its good idea'
           ,3)
GO
