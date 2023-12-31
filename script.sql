USE [master]
GO
/****** Object:  Database [practical]    Script Date: 11/18/2023 12:09:40 AM ******/
CREATE DATABASE [practical]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'practical', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\practical.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'practical_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\practical_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [practical] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [practical].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [practical] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [practical] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [practical] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [practical] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [practical] SET ARITHABORT OFF 
GO
ALTER DATABASE [practical] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [practical] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [practical] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [practical] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [practical] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [practical] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [practical] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [practical] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [practical] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [practical] SET  DISABLE_BROKER 
GO
ALTER DATABASE [practical] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [practical] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [practical] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [practical] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [practical] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [practical] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [practical] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [practical] SET RECOVERY FULL 
GO
ALTER DATABASE [practical] SET  MULTI_USER 
GO
ALTER DATABASE [practical] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [practical] SET DB_CHAINING OFF 
GO
ALTER DATABASE [practical] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [practical] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [practical] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [practical] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'practical', N'ON'
GO
ALTER DATABASE [practical] SET QUERY_STORE = OFF
GO
USE [practical]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 11/18/2023 12:09:40 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[categoryId] [int] IDENTITY(1,1) NOT NULL,
	[categoryName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 11/18/2023 12:09:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[productId] [int] IDENTITY(1,1) NOT NULL,
	[productName] [varchar](40) NOT NULL,
	[description] [varchar](500) NULL,
	[price] [numeric](6, 2) NULL,
	[categoryId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 11/18/2023 12:09:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fullname] [varchar](200) NULL,
	[username] [varchar](100) NULL,
	[password] [nvarchar](100) NULL,
	[roletype] [varchar](20) NULL,
	[createdAt] [datetime] NULL,
	[updatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[users] ADD  DEFAULT (getdate()) FOR [createdAt]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([categoryId])
REFERENCES [dbo].[Category] ([categoryId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
/****** Object:  StoredProcedure [dbo].[getCategory]    Script Date: 11/18/2023 12:09:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[getCategory](@id int =0)
as begin
select * from Category where categoryId=@id or @id=0
end
GO
/****** Object:  StoredProcedure [dbo].[getProductData]    Script Date: 11/18/2023 12:09:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create proc [dbo].[getProductData](@id int=0)
as 
begin
select p.productId,p.productName,p.price,p.description,p.categoryId,c.categoryName from Product p
inner join Category c on c.categoryId=p.categoryId
where productId=@id or @id=0

end
GO
USE [master]
GO
ALTER DATABASE [practical] SET  READ_WRITE 
GO


insert into users (fullname,username,password,roletype,createdAt)values ('Test Admin','Admin','123456','Admin',getdate())
