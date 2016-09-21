USE [master]
GO
/****** Object:  Database [toysforboys]    Script Date: 20/09/2016 11:51:26 ******/
CREATE DATABASE [toysforboys]
 CONTAINMENT = NONE
GO
ALTER DATABASE [toysforboys] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [toysforboys].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [toysforboys] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [toysforboys] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [toysforboys] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [toysforboys] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [toysforboys] SET ARITHABORT OFF 
GO
ALTER DATABASE [toysforboys] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [toysforboys] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [toysforboys] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [toysforboys] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [toysforboys] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [toysforboys] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [toysforboys] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [toysforboys] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [toysforboys] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [toysforboys] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [toysforboys] SET  ENABLE_BROKER 
GO
ALTER DATABASE [toysforboys] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [toysforboys] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [toysforboys] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [toysforboys] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [toysforboys] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [toysforboys] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [toysforboys] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [toysforboys] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [toysforboys] SET  MULTI_USER 
GO
ALTER DATABASE [toysforboys] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [toysforboys] SET DB_CHAINING OFF 
GO
ALTER DATABASE [toysforboys] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [toysforboys] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [toysforboys]
GO
/****** Object:  Table [dbo].[countries]    Script Date: 20/09/2016 11:51:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[countries](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
 CONSTRAINT [PK_countries] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[customers]    Script Date: 20/09/2016 11:51:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[streetAndNumber] [nvarchar](50) NULL,
	[city] [nvarchar](50) NULL,
	[state] [nvarchar](15) NULL,
	[postalCode] [nvarchar](15) NULL,
	[countryId] [int] NULL,
 CONSTRAINT [PK_customers] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[orderdetails]    Script Date: 20/09/2016 11:51:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orderdetails](
	[orderId] [int] NOT NULL,
	[productId] [int] NOT NULL,
	[quantityOrdered] [int] NULL,
	[priceEach] [numeric](10, 2) NULL,
 CONSTRAINT [PK_orderdetails] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC,
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[orders]    Script Date: 20/09/2016 11:51:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[orderDate] [datetime] NULL,
	[requiredDate] [datetime] NULL,
	[shippedDate] [datetime] NULL,
	[comments] [ntext] NULL,
	[customerId] [int] NULL,
	[status] [nvarchar](10) NULL,
 CONSTRAINT [PK_orders] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[productlines]    Script Date: 20/09/2016 11:51:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productlines](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[description] [ntext] NULL,
 CONSTRAINT [PK_productlines] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[products]    Script Date: 20/09/2016 11:51:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](70) NULL,
	[scale] [nvarchar](10) NULL,
	[description] [ntext] NULL,
	[quantityInStock] [int] NULL,
	[quantityInOrder] [int] NULL,
	[buyPrice] [numeric](10, 2) NULL,
	[productlineId] [int] NULL,
 CONSTRAINT [PK_products] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[customers]  WITH CHECK ADD  CONSTRAINT [FK_customers_countries] FOREIGN KEY([countryId])
REFERENCES [dbo].[countries] ([id])
GO
ALTER TABLE [dbo].[customers] CHECK CONSTRAINT [FK_customers_countries]
GO
ALTER TABLE [dbo].[orderdetails]  WITH CHECK ADD  CONSTRAINT [FK_orderdetails_orders] FOREIGN KEY([orderId])
REFERENCES [dbo].[orders] ([id])
GO
ALTER TABLE [dbo].[orderdetails] CHECK CONSTRAINT [FK_orderdetails_orders]
GO
ALTER TABLE [dbo].[orderdetails]  WITH CHECK ADD  CONSTRAINT [FK_orderdetails_products] FOREIGN KEY([productId])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[orderdetails] CHECK CONSTRAINT [FK_orderdetails_products]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_customers] FOREIGN KEY([customerId])
REFERENCES [dbo].[customers] ([id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_orders_customers]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [FK_products_productlines] FOREIGN KEY([productlineId])
REFERENCES [dbo].[productlines] ([id])
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [FK_products_productlines]
GO
USE [master]
GO
ALTER DATABASE [toysforboys] SET  READ_WRITE 
GO
