USE [master]
GO
/****** Object:  Database [MarioDB]    Script Date: 15-10-2020 11:05:32 ******/
CREATE DATABASE [MarioDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MarioDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MarioDB.mdf' , SIZE = 139264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MarioDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MarioDB_log.ldf' , SIZE = 335872KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MarioDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MarioDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MarioDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MarioDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MarioDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MarioDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MarioDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [MarioDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MarioDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MarioDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MarioDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MarioDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MarioDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MarioDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MarioDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MarioDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MarioDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MarioDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MarioDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MarioDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MarioDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MarioDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MarioDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MarioDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MarioDB] SET RECOVERY FULL 
GO
ALTER DATABASE [MarioDB] SET  MULTI_USER 
GO
ALTER DATABASE [MarioDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MarioDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MarioDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MarioDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MarioDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MarioDB', N'ON'
GO
ALTER DATABASE [MarioDB] SET QUERY_STORE = OFF
GO
USE [MarioDB]
GO
/****** Object:  UserDefinedFunction [dbo].[udf_GetNumeric]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[udf_GetNumeric]
(
  @strAlphaNumeric VARCHAR(256)
)
RETURNS VARCHAR(256)
AS
BEGIN
  DECLARE @intAlpha INT
  SET @intAlpha = PATINDEX('%[^0-9]%', @strAlphaNumeric)
  BEGIN
    WHILE @intAlpha > 0
    BEGIN
      SET @strAlphaNumeric = STUFF(@strAlphaNumeric, @intAlpha, 1, '' )
      SET @intAlpha = PATINDEX('%[^0-9]%', @strAlphaNumeric )
    END
  END
  RETURN ISNULL(@strAlphaNumeric,0)
END
GO
/****** Object:  Table [dbo].[Address]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Streetname] [varchar](100) NULL,
	[HouseNumber] [varchar](10) NULL,
	[Zipcode] [int] NULL,
	[City] [varchar](100) NULL,
	[CountryCode] [varchar](5) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AddressCustomer]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddressCustomer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AddressID] [int] NULL,
	[CustomerID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Name] [varchar](50) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[PaymentMethodID] [int] NULL,
	[StoreID] [int] NULL,
	[Password] [char](64) NULL,
	[Newsletter] [bit] NULL,
	[Sms] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredient]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredient](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IngredientPropertiesID] [int] NULL,
	[Name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IngredientCategory]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IngredientCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IngredientProperties]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IngredientProperties](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IngredientCategoryID] [int] NULL,
	[Description] [varchar](500) NULL,
	[Price] [decimal](18, 0) NULL,
	[Spicy] [bit] NULL,
	[Vegetarian] [bit] NULL,
	[ValidFrom] [date] NULL,
	[ValidTill] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Item]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Item](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderItemID] [int] NULL,
	[ProductID] [int] NULL,
	[ProductPropertiesID] [int] NULL,
	[IngredientID] [int] NULL,
	[IngredientPropertiesID] [int] NULL,
	[PizzaCrustID] [int] NULL,
	[PizzaCrustPropertiesID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Municipality]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Municipality](
	[MunicipalityCode] [int] NOT NULL,
	[Name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MunicipalityCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[StoreID] [int] NULL,
	[OrderTime] [date] NULL,
	[Delivery] [bit] NULL,
	[AddressID] [int] NULL,
	[DeliveryDate] [date] NULL,
	[DeliveryTime] [time](0) NULL,
	[DeliveryCost] [decimal](18, 0) NULL,
	[VoucherID] [int] NULL,
	[Tip] [decimal](18, 0) NULL,
	[TotalPrice] [decimal](18, 0) NULL,
	[PaymentMethodID] [int] NULL,
	[TaxID] [int] NULL,
	[Discount] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NULL,
	[Quantity] [int] NULL,
	[Price] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentMethod]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentMethod](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PizzaCrust]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PizzaCrust](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PizzaCrustPropertiesID] [int] NULL,
	[Name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PizzaCrustProperties]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PizzaCrustProperties](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Diameter] [varchar](10) NULL,
	[Description] [varchar](500) NULL,
	[Fee] [decimal](18, 0) NULL,
	[Availability] [bit] NULL,
	[VersionValidFrom] [date] NULL,
	[VersionValidTill] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PizzaRecipe]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PizzaRecipe](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[IngredientID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductPropertiesID] [int] NULL,
	[Name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductProperties]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductProperties](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategoryID] [int] NULL,
	[Description] [varchar](500) NULL,
	[Price] [decimal](18, 0) NULL,
	[Spicy] [bit] NULL,
	[Vegetarian] [bit] NULL,
	[ValidFrom] [date] NULL,
	[ValidTill] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Store]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StoreAddressID] [int] NULL,
	[Name] [varchar](50) NULL,
	[PhoneNumber] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoreAddress]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreAddress](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StreetName] [varchar](100) NULL,
	[Number] [varchar](10) NULL,
	[Zipcode] [int] NULL,
	[City] [varchar](100) NULL,
	[CountryCode] [varchar](3) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tax]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tax](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Rate] [decimal](18, 0) NULL,
	[ValidFrom] [date] NULL,
	[ValidTill] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Voucher]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Voucher](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VoucherCode] [varchar](50) NULL,
	[Type] [int] NULL,
	[Amount] [decimal](18, 0) NULL,
	[Category] [int] NULL,
	[MinimumOrderAmount] [int] NULL,
	[IssueDate] [date] NULL,
	[ExpirationDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VoucherType]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VoucherType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Description] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Zipcode]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zipcode](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Zipcode] [varchar](10) NULL,
	[SeriesIndex] [int] NULL,
	[BreakpointFrom] [int] NULL,
	[BreakpointTo] [int] NULL,
	[City] [varchar](100) NULL,
	[Streetname] [varchar](100) NULL,
	[MunicipalityCode] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD FOREIGN KEY([Zipcode])
REFERENCES [dbo].[Zipcode] ([ID])
GO
ALTER TABLE [dbo].[AddressCustomer]  WITH CHECK ADD FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[AddressCustomer]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD FOREIGN KEY([PaymentMethodID])
REFERENCES [dbo].[PaymentMethod] ([ID])
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD FOREIGN KEY([StoreID])
REFERENCES [dbo].[Store] ([ID])
GO
ALTER TABLE [dbo].[Ingredient]  WITH CHECK ADD FOREIGN KEY([IngredientPropertiesID])
REFERENCES [dbo].[IngredientProperties] ([ID])
GO
ALTER TABLE [dbo].[IngredientProperties]  WITH CHECK ADD FOREIGN KEY([IngredientCategoryID])
REFERENCES [dbo].[IngredientCategory] ([ID])
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD FOREIGN KEY([IngredientID])
REFERENCES [dbo].[Ingredient] ([ID])
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD FOREIGN KEY([IngredientPropertiesID])
REFERENCES [dbo].[IngredientProperties] ([ID])
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD FOREIGN KEY([OrderItemID])
REFERENCES [dbo].[OrderItem] ([ID])
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD FOREIGN KEY([PizzaCrustID])
REFERENCES [dbo].[PizzaCrust] ([ID])
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD FOREIGN KEY([PizzaCrustPropertiesID])
REFERENCES [dbo].[PizzaCrustProperties] ([ID])
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Item]  WITH CHECK ADD FOREIGN KEY([ProductPropertiesID])
REFERENCES [dbo].[ProductProperties] ([ID])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([ID])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([PaymentMethodID])
REFERENCES [dbo].[PaymentMethod] ([ID])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([StoreID])
REFERENCES [dbo].[Store] ([ID])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([TaxID])
REFERENCES [dbo].[Tax] ([ID])
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD FOREIGN KEY([VoucherID])
REFERENCES [dbo].[Voucher] ([ID])
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD FOREIGN KEY([OrderID])
REFERENCES [dbo].[Order] ([ID])
GO
ALTER TABLE [dbo].[PizzaCrust]  WITH CHECK ADD FOREIGN KEY([PizzaCrustPropertiesID])
REFERENCES [dbo].[PizzaCrustProperties] ([ID])
GO
ALTER TABLE [dbo].[PizzaRecipe]  WITH CHECK ADD FOREIGN KEY([IngredientID])
REFERENCES [dbo].[Ingredient] ([ID])
GO
ALTER TABLE [dbo].[PizzaRecipe]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ID])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([ProductPropertiesID])
REFERENCES [dbo].[ProductProperties] ([ID])
GO
ALTER TABLE [dbo].[ProductProperties]  WITH CHECK ADD FOREIGN KEY([ProductCategoryID])
REFERENCES [dbo].[ProductCategory] ([ID])
GO
ALTER TABLE [dbo].[Store]  WITH CHECK ADD FOREIGN KEY([StoreAddressID])
REFERENCES [dbo].[StoreAddress] ([ID])
GO
ALTER TABLE [dbo].[StoreAddress]  WITH CHECK ADD FOREIGN KEY([Zipcode])
REFERENCES [dbo].[Zipcode] ([ID])
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD FOREIGN KEY([Category])
REFERENCES [dbo].[ProductCategory] ([ID])
GO
ALTER TABLE [dbo].[Voucher]  WITH CHECK ADD FOREIGN KEY([Type])
REFERENCES [dbo].[VoucherType] ([ID])
GO
ALTER TABLE [dbo].[Zipcode]  WITH CHECK ADD FOREIGN KEY([MunicipalityCode])
REFERENCES [dbo].[Municipality] ([MunicipalityCode])
GO
/****** Object:  StoredProcedure [dbo].[sp01ImportMarioMunicipality]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp01ImportMarioMunicipality]
AS
INSERT INTO dbo.Municipality
	(
	MunicipalityCode,
	"Name"
	)
	SELECT Kode = cast(Kode as int), Naam
	FROM ShadowDB.dbo.Gemeenten gm
	WHERE
	NOT EXISTS (SELECT * FROM Municipality m
              WHERE gm.Naam = m.Name)

GO
/****** Object:  StoredProcedure [dbo].[sp02ImportMarioZipcode]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp02ImportMarioZipcode]
AS
INSERT INTO dbo.Zipcode
	(
	Zipcode,
	SeriesIndex,
	BreakpointFrom ,
	BreakpointTo,
	City,
	Streetname,
	MunicipalityCode
	)
	SELECT Postcode, Reeksind = cast(Reeksind as int), 
	Breekpunt_van = cast(Breekpunt_van as int), 
	Breekpunt_tem = cast(Breekpunt_tem as int), 
	Woonplaats,
	Straatnaam, 
	Gemeeentecode = cast(Gemeeentecode as int)
	FROM ShadowDB.dbo.Postcodes p
	WHERE
	NOT EXISTS (SELECT * FROM Zipcode z
              WHERE p.Postcode = z.Zipcode AND p.Reeksind = z.SeriesIndex)

GO
/****** Object:  StoredProcedure [dbo].[sp03ImportMarioVoucherType]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp03ImportMarioVoucherType]
AS
INSERT INTO dbo.VoucherType
	(
	"Name",
	Description
	)
	SELECT DISTINCT GebruikteCoupon, GebruikteCoupon
	FROM ShadowDB.dbo.MarioOrderData m
		WHERE GebruikteCoupon != '' AND
	NOT EXISTS (SELECT * FROM VoucherType vt
              WHERE m.GebruikteCoupon = vt.Name)

GO
/****** Object:  StoredProcedure [dbo].[sp04ImportMarioProductCategory]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp04ImportMarioProductCategory]
AS
INSERT INTO dbo.ProductCategory 
	(
	"Name"
	)
	SELECT DISTINCT Subcategorie
	FROM ShadowDB.dbo.PizzaIngredienten p
	WHERE
	NOT EXISTS (SELECT * FROM ProductCategory pc
              WHERE p.Subcategorie = pc.Name)

GO
/****** Object:  StoredProcedure [dbo].[sp05ImportMarioVoucher]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp05ImportMarioVoucher]
AS
	INSERT INTO dbo.Voucher 
	(
	Type
	)
	SELECT DISTINCT vt.ID
	FROM ShadowDB.dbo.MarioOrderData od
	INNER JOIN MarioDB.dbo.VoucherType vt ON vt.Name = od.GebruikteCoupon
	WHERE NOT EXISTS (SELECT * FROM Voucher v
              WHERE v.Type = vt.ID)

GO
/****** Object:  StoredProcedure [dbo].[sp06ImportMarioPizzaCrustProperties]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp06ImportMarioPizzaCrustProperties]
AS
INSERT INTO dbo.PizzaCrustProperties
	(
	Diameter,
	"Description",
	Fee,
	"Availability",
	VersionValidFrom
	)
	SELECT Diameter, Omschrijving, Toeslag = cast(Toeslag as decimal(10,2)), CASE WHEN Beschikbaar = 'True' THEN CAST('True' as bit) ELSE CAST('False' as bit) END, '1900-01-01' AS VersionValidFrom
	FROM ShadowDB.dbo.PizzaBodems pb
	WHERE
	NOT EXISTS (SELECT * FROM PizzaCrustProperties pcp
              WHERE pb.Diameter = pcp.Diameter AND pb.Omschrijving = pcp.Description AND cast(pb.Toeslag as decimal(10,2)) = pcp.Fee AND pb.Beschikbaar = CAST(pcp.Availability as bit))

GO
/****** Object:  StoredProcedure [dbo].[sp07ImportMarioPizzaCrust]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp07ImportMarioPizzaCrust]
AS
INSERT INTO PizzaCrust
	(
	"Name",
	PizzaCrustPropertiesID
	)
	SELECT pb.Naam, pcp.ID
	FROM PizzaCrustProperties pcp
	JOIN ShadowDB.dbo.PizzaBodems pb ON pb.Diameter = pcp.Diameter AND pb.Omschrijving = pcp.Description AND cast(pb.Toeslag as decimal(10,2)) = pcp.Fee AND pb.Beschikbaar = CAST(pcp.Availability as bit)
	WHERE 
	NOT EXISTS (SELECT * FROM PizzaCrust pc
	WHERE pc.PizzaCrustPropertiesID = pcp.ID)




GO
/****** Object:  StoredProcedure [dbo].[sp08ImportMarioIngredientCategory]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp08ImportMarioIngredientCategory]
AS
INSERT INTO IngredientCategory
	(
	"Name"
	)
	Select ('Sauce')
		Where NOT EXISTS (SELECT * FROM IngredientCategory ic
	WHERE ic.Name = 'Sauce')
	

INSERT INTO IngredientCategory
	(
	"Name"
	)
	Select ('Extra ingredient')
		Where NOT EXISTS (SELECT * FROM IngredientCategory ic
	WHERE ic.Name = 'Extra ingredient')
GO
/****** Object:  StoredProcedure [dbo].[sp08ImportMarioIngredientProperties]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp08ImportMarioIngredientProperties]
AS
INSERT INTO IngredientProperties
	(
	IngredientCategoryID,
	Description,
	Price,
	ValidFrom
	)
	SELECT Distinct IngredientCategory.ID, Ingredient, ExtraPrice = cast(ExtraPrice as decimal(10,2)), '1900-01-01' AS ValidFrom
	FROM ShadowDB.dbo.ExtraIngredienten ein, MarioDB.dbo.IngredientCategory
	WHERE IngredientCategory.Name = 'Extra ingredient' AND
	NOT EXISTS (SELECT * FROM IngredientProperties ipr
	WHERE ipr.Description = ein.Ingredient)

INSERT INTO IngredientProperties
	(
	IngredientCategoryID,
	Description,
	ValidFrom
	)
	SELECT Distinct IngredientCategory.ID, PizzasausStandaard, '1900-01-01' AS ValidFrom
	FROM ShadowDB.dbo.PizzaIngredienten ein, MarioDB.dbo.IngredientCategory
	WHERE IngredientCategory.Name = 'Sauce' AND
	NOT EXISTS (SELECT * FROM IngredientProperties ipr
	WHERE ipr.Description = ein.PizzasausStandaard)



GO
/****** Object:  StoredProcedure [dbo].[sp09ImportMarioIngredient]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp09ImportMarioIngredient]
AS
INSERT INTO Ingredient
	(
	"Name",
	IngredientPropertiesID
	)
	SELECT Description, ID
	FROM MarioDB.dbo.IngredientProperties ip
	WHERE NOT EXISTS (SELECT * FROM Ingredient i
	WHERE i.Name = ip.Description)

GO
/****** Object:  StoredProcedure [dbo].[sp10ImportMarioCustomer]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp10ImportMarioCustomer]
AS
	INSERT INTO Customer
	(
	Email,
	"Name",
	PhoneNumber
	)
	SELECT DISTINCT Email, Klantnaam, Telefoon
	FROM ShadowDB.dbo.MarioOrderData o
	WHERE NOT EXISTS (SELECT * FROM Customer c
	WHERE c.Email = o.Email) AND o.Email != ''


GO
/****** Object:  StoredProcedure [dbo].[sp11ImportMarioProductCategory]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp11ImportMarioProductCategory]
AS
INSERT INTO ProductCategory
	(
	"Name"
	)
	SELECT op.Subcategorie
	FROM ShadowDB.dbo.OverigeProducten op
	WHERE NOT EXISTS (SELECT * FROM ProductCategory pc
	WHERE pc.Name = op.Subcategorie)

	UNION

	SELECT pin.Subcategorie
	FROM ShadowDB.dbo.PizzaIngredienten pin

	WHERE NOT EXISTS (SELECT * FROM ProductCategory pc
	WHERE pc.Name = pin.Subcategorie)


GO
/****** Object:  StoredProcedure [dbo].[sp12ImportMarioProductProperties]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp12ImportMarioProductProperties]
AS
INSERT INTO ProductProperties
	(
	"Description",
	Price,
	Spicy,
	Vegetarian,
	ProductCategoryID,
	ValidFrom
	)
	SELECT COALESCE(NULLIF(Productomschrijving,''), Productnaam), Prijs, Spicy, Vegetarisch, pc.ID, '1900-01-01' AS ValidFrom
	FROM ShadowDB.dbo.OverigeProducten op
	INNER JOIN MarioDB.dbo.ProductCategory pc ON op.Subcategorie = pc.Name
	WHERE NOT EXISTS (SELECT * FROM ProductProperties pp
	WHERE (pp.Description = op.Productomschrijving OR pp.Description = op.Productnaam) AND pp.Price = op.Prijs AND pp.Spicy = op.Spicy AND pp.Vegetarian = op.Vegetarisch)

	UNION

	SELECT COALESCE(NULLIF(Productomschrijving,''), ProductNaam), Prijs, Spicy, Vegetarisch, pc.ID, '1900-01-01' AS ValidFrom
	FROM ShadowDB.dbo.PizzaIngredienten pin
	INNER JOIN MarioDB.dbo.ProductCategory pc ON pin.Subcategorie = pc.Name
	WHERE NOT EXISTS (SELECT * FROM ProductProperties pp
	WHERE pp.Description = pin.Productomschrijving AND pp.Price = pin.Prijs AND pp.Spicy = pin.Spicy AND pp.Vegetarian = pin.Vegetarisch)

GO
/****** Object:  StoredProcedure [dbo].[sp13ImportMarioProduct]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp13ImportMarioProduct]
AS
INSERT INTO Product
	(
	ProductPropertiesID,
	"Name"
	)

	SELECT DISTINCT pp.ID, pin.Productnaam
	FROM MarioDB.dbo.ProductProperties pp
	INNER JOIN ShadowDB.dbo.PizzaIngredienten pin ON pp.Description = pin.Productomschrijving
	WHERE NOT EXISTS (SELECT * FROM Product pr
	WHERE pr.Name = pin.Productnaam)

	UNION

	SELECT DISTINCT pp.ID, op.Productnaam
	FROM MarioDB.dbo.ProductProperties pp
	INNER JOIN ShadowDB.dbo.OverigeProducten op ON pp.Description = op.Productomschrijving
	WHERE NOT EXISTS (SELECT * FROM Product pr
	WHERE pr.Name = op.Productnaam)

GO
/****** Object:  StoredProcedure [dbo].[sp14ImportMarioPizzaRecipe]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp14ImportMarioPizzaRecipe]
AS
INSERT INTO PizzaRecipe
	(
	ProductID,
	IngredientID
	)

	SELECT p.ID, i.ID
	FROM ShadowDB.dbo.PizzaIngredienten pin
	LEFT JOIN MarioDB.dbo.Product p ON p.Name = pin.Productnaam
	LEFT JOIN MarioDB.dbo.ProductProperties pp ON pp.ID = p.ProductPropertiesID
	LEFT JOIN MarioDB.dbo.Ingredient i ON i.Name = pin.Ingredientnaam
	LEFT JOIN MarioDB.dbo.IngredientProperties ip ON ip.ID =i.IngredientPropertiesID
	WHERE NOT EXISTS (SELECT * FROM PizzaRecipe pr
              WHERE pr.IngredientID = i.ID AND pr.ProductID = p.ID)

GO
/****** Object:  StoredProcedure [dbo].[sp15ImportMarioTax]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp15ImportMarioTax]
AS
	INSERT INTO dbo.Tax 
	(
	Rate,
	ValidFrom
	)
	SELECT '9' as Rate, '1900-01-01' as ValidFrom
	WHERE NOT EXISTS (SELECT * FROM Tax t
              WHERE t.Rate = '9' AND t.ValidFrom = '1900-01-01')

GO
/****** Object:  StoredProcedure [dbo].[sp16ImportMarioPaymentMethod]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp16ImportMarioPaymentMethod]
AS
	INSERT INTO dbo.PaymentMethod
	(
	Name
	)
	SELECT 'Cash' as Name
	WHERE NOT EXISTS (SELECT * FROM PaymentMethod p
              WHERE p.Name = 'Cash')

	INSERT INTO dbo.PaymentMethod
	(
	Name
	)
	SELECT 'Creditcard' as Name
	WHERE NOT EXISTS (SELECT * FROM PaymentMethod p
              WHERE p.Name = 'Creditcard')

	INSERT INTO dbo.PaymentMethod
	(
	Name
	)
	SELECT 'Debitcard' as Name
	WHERE NOT EXISTS (SELECT * FROM PaymentMethod p
              WHERE p.Name = 'Debitcard')

GO
/****** Object:  StoredProcedure [dbo].[sp23ImportOLDMarioAddress]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp23ImportOLDMarioAddress]
AS
INSERT INTO "Address"
	(
	Streetname,
	HouseNumber,
	Zipcode,
	City,
	CountryCode
	)
	SELECT DISTINCT Straat, Nummer, z.ID, UPPER(Plaats), Landcode
	FROM ShadowDB.dbo.Winkels w, MarioDB.dbo.Zipcode z
	WHERE w.Postcode = replace(z.Zipcode,' ','') AND 
	((dbo.udf_GetNumeric(Nummer) Between z.BreakpointFrom And z.BreakpointTo
       Or dbo.udf_GetNumeric(Nummer) Between z.BreakpointTo And z.BreakpointFrom)
       And (dbo.udf_GetNumeric(Nummer) % 2 = z.BreakpointFrom % 2)) AND
	   NOT EXISTS (SELECT * FROM Address a
	WHERE a.Streetname = Straat and a.City = UPPER(Plaats) and a.HouseNumber = Nummer)
GO
/****** Object:  StoredProcedure [dbo].[sp24ImportMarioStoreAddress]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp24ImportMarioStoreAddress]
AS
INSERT INTO StoreAddress
	(
	Streetname,
	Number,
	Zipcode,
	City,
	CountryCode
	)
	SELECT DISTINCT Straat, Nummer, z.ID, UPPER(Plaats), Landcode
	FROM ShadowDB.dbo.Winkels w, MarioDB.dbo.Zipcode z
	WHERE w.Postcode = replace(z.Zipcode,' ','') AND 
	((dbo.udf_GetNumeric(Nummer) Between z.BreakpointFrom And z.BreakpointTo
       Or dbo.udf_GetNumeric(Nummer) Between z.BreakpointTo And z.BreakpointFrom)
       And (dbo.udf_GetNumeric(Nummer) % 2 = z.BreakpointFrom % 2)) AND
	   NOT EXISTS (SELECT * FROM StoreAddress sa
	WHERE sa.Streetname = Straat and sa.City = UPPER(Plaats) and sa.Number = Nummer)
GO
/****** Object:  StoredProcedure [dbo].[sp25ImportMarioStore]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp25ImportMarioStore]
AS
INSERT INTO Store
	(
	"Name",
	PhoneNumber,
	StoreAddressID
	)
	SELECT w.Naam, w.Telefoon, sa.ID
	FROM ShadowDB.dbo.Winkels w 
	INNER JOIN MarioDB.dbo.StoreAddress sa ON UPPER(w.Plaats) = sa.City AND w.Straat = sa.StreetName AND w.Nummer = sa.Number
	WHERE NOT EXISTS (SELECT * FROM Store st
	WHERE st.Name = w.Naam AND st.PhoneNumber = w.Telefoon)

GO
/****** Object:  StoredProcedure [dbo].[sp30Import!MarioOrder]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp30Import!MarioOrder]
AS
INSERT INTO "Order"
	(
	OrderTime,
	Delivery,
	DeliveryDate,
	--DeliveryTime,
	DeliveryCost
	)
	SELECT Besteldatum, /*= convert(date, Besteldatum, 3),*/ CASE WHEN AfleverType = '0' THEN CAST('0' as bit) ELSE CAST('1' as bit) END, 
	AfleverDatum /*= convert(date, AfleverDatum, 3)*/, 
	--AfleverMoment = try_convert(time(0), AfleverMoment, 0),
	Bezorgkosten = try_parse(Bezorgkosten as decimal(10,2) USING 'El-GR')
	FROM ShadowDB.dbo.MarioOrderData

GO
/****** Object:  StoredProcedure [dbo].[spExecuteProcedures]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spExecuteProcedures]
AS

DECLARE @SPECIFIC_SCHEMA varchar(200), @SPECIFIC_NAME varchar(200)

SELECT SPECIFIC_SCHEMA, SPECIFIC_NAME
INTO #tempStoredProcedures
	FROM MarioDB.INFORMATION_SCHEMA.ROUTINES
	WHERE ROUTINE_TYPE = 'PROCEDURE' AND ROUTINE_NAME LIKE '%ImportMario%'
	ORDER BY ROUTINE_NAME ASC

WHILE EXISTS(SELECT * From #tempStoredProcedures)
BEGIN
	SET NOCOUNT ON;
	
	Select Top 1 @SPECIFIC_SCHEMA = SPECIFIC_SCHEMA, @SPECIFIC_NAME = SPECIFIC_NAME From #tempStoredProcedures

    EXECUTE (@SPECIFIC_SCHEMA + '.' + @SPECIFIC_NAME);

    Delete #tempStoredProcedures Where SPECIFIC_NAME = @SPECIFIC_NAME
END

DROP TABLE #tempStoredProcedures

GO
/****** Object:  StoredProcedure [dbo].[spImportIntoMarioDB]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spImportIntoMarioDB]
AS
BEGIN
--SET IDENTITY_INSERT Municipality ON

------------------------------------------------------
	INSERT INTO dbo.Municipality
	(
	MunicipalityCode,
	"Name"
	)
	SELECT Kode = cast(Kode as int), Naam
	FROM ShadowDB.dbo.Gemeenten
------------------------------------------------------
	INSERT INTO dbo.Zipcode
	(
	Zipcode,
	SeriesIndex,
	BreakpointFrom ,
	BreakpointTo,
	City,
	Streetname,
	MunicipalityCode
	)
	SELECT Postcode, Reeksind = cast(Reeksind as int), 
	Breekpunt_van = cast(Breekpunt_van as int), 
	Breekpunt_tem = cast(Breekpunt_tem as int), 
	Woonplaats,
	Straatnaam, 
	Gemeeentecode = cast(Gemeeentecode as int)
	FROM ShadowDB.dbo.Postcodes
------------------------------------------------------
	INSERT INTO dbo.VoucherType
	(
	"Name"
	)
	SELECT GebruikteCoupon
	FROM ShadowDB.dbo.MarioOrderData
------------------------------------------------------
	INSERT INTO dbo.ProductCategory
	(
	"Name"
	)
	SELECT Categorie
	FROM ShadowDB.dbo.PizzaIngredienten
------------------------------------------------------
	INSERT INTO dbo.Voucher
	(
	Amount
	)
	SELECT CouponKorting = TRY_PARSE(CouponKorting AS decimal(10,2) USING 'El-GR')
	FROM ShadowDB.dbo.MarioOrderData
------------------------------------------------------
	INSERT INTO dbo.PizzaCrustProperties
	(
	Diameter,
	"Description",
	Fee,
	"Availability"
	)
	SELECT Diameter, Omschrijving, Toeslag = cast(Toeslag as decimal(10,2)), CASE WHEN Beschikbaar = 'True' THEN CAST('True' as bit) ELSE CAST('False' as bit) END
	FROM ShadowDB.dbo.PizzaBodems
------------------------------------------------------
	INSERT INTO PizzaCrust
	(
	"Name"
	)
	SELECT Naam
	FROM ShadowDB.dbo.PizzaBodems
------------------------------------------------------
	INSERT INTO IngredientProperties
	(
	Price
	)
	SELECT ExtraPrice = cast(ExtraPrice as decimal(10,2))
	FROM ShadowDB.dbo.ExtraIngredienten
	INNER JOIN ShadowDB.dbo.PizzaIngredienten ON ShadowDB.dbo.ExtraIngredienten.Ingredient = ShadowDB.dbo.PizzaIngredienten.Productnaam
------------------------------------------------------
	INSERT INTO IngredientProperties
	(
	Spicy,
	Vegetarian
	)
	SELECT Spicy, Vegetarisch
	FROM ShadowDB.dbo.ExtraIngredienten
	INNER JOIN ShadowDB.dbo.PizzaIngredienten ON ShadowDB.dbo.ExtraIngredienten.Ingredient = ShadowDB.dbo.PizzaIngredienten.Productnaam
------------------------------------------------------
	INSERT INTO Ingredient
	(
	"Name"
	)
	SELECT Ingredient
	FROM ShadowDB.dbo.ExtraIngredienten
------------------------------------------------------
	INSERT INTO Customer
	(
	Email,
	"Name",
	PhoneNumber
	)
	SELECT Email, Klantnaam, Telefoon
	FROM ShadowDB.dbo.MarioOrderData WHERE Email != ShadowDB.dbo.MarioOrderData.Email
------------------------------------------------------
	INSERT INTO "Order"
	(
	OrderTime,
	Delivery,
	DeliveryDate,
	--DeliveryTime,
	DeliveryCost
	)
	SELECT Besteldatum, /*= convert(date, Besteldatum, 3),*/ CASE WHEN AfleverType = '0' THEN CAST('0' as bit) ELSE CAST('1' as bit) END, 
	AfleverDatum /*= convert(date, AfleverDatum, 3)*/, 
	--AfleverMoment = try_convert(time(0), AfleverMoment, 0),
	Bezorgkosten = try_parse(Bezorgkosten as decimal(10,2) USING 'El-GR')
	FROM ShadowDB.dbo.MarioOrderData
-----------------------------------------------------
	INSERT INTO ProductProperties
	(
	"Description",
	Price,
	Spicy,
	Vegetarian
	)
	SELECT Productomschrijving, Prijs, Spicy, Vegetarisch
	FROM ShadowDB.dbo.OverigeProducten
-----------------------------------------------------
	INSERT INTO ProductProperties
	(
	"Description",
	Price,
	Spicy,
	Vegetarian
	)
	SELECT Productomschrijving, Prijs, Spicy, Vegetarisch
	FROM ShadowDB.dbo.PizzaIngredienten
-----------------------------------------------------
	INSERT INTO "Address"
	(
	Streetname,
	HouseNumber,
	City,
	CountryCode
	)
	SELECT TRIM('0123456789' FROM Adres), dbo.udf_GetNumeric(Adres), Woonplaats, 'NL' 
	FROM ShadowDB.dbo.MarioOrderData
	WHERE ShadowDB.dbo.MarioOrderData.Woonplaats != ' '
-----------------------------------------------------
	
	INSERT INTO "Address"
	(
	Zipcode
	)
	SELECT ID =
	
	CASE
	WHEN (cast((select HouseNumber from "Address") as int) % 2 <> 0)
		THEN
		(
		SELECT ID FROM dbo.Zipcode 
		WHERE
		(
		(City = (select City from dbo.Zipcode) AND Streetname = (select Streetname from dbo.Zipcode))
		AND (dbo.Zipcode.BreakpointFrom % 2 <> 0
		AND (cast((select HouseNumber from "Address") as int) >= (select BreakpointFrom from Zipcode) AND cast((select HouseNumber from "Address") as int) <= (select BreakpointTo from Zipcode)))
		)
		)
	ELSE 
		(
		SELECT ID FROM dbo.Zipcode 
		WHERE 		
		(
		(City = (select City from dbo.Zipcode) AND Streetname = (select Streetname from dbo.Zipcode))
		AND (dbo.Zipcode.BreakpointFrom % 2 = 0
		AND (cast((select HouseNumber from "Address") as int) >= (select BreakpointFrom from Zipcode) AND cast((select HouseNumber from "Address") as int) <= (select BreakpointTo from Zipcode)))
		)
		)
	END	
	FROM dbo.Zipcode

-----------------------------------------------------
	INSERT INTO StoreAddress
	(
	StreetName,
	Number,
	City,
	CountryCode
	)
	SELECT Straat, Nummer, 
	Plaats, LandCode
	FROM ShadowDB.dbo.Winkels
-----------------------------------------------------
	INSERT INTO Store
	(
	"Name",
	PhoneNumber
	)
	SELECT Naam, Telefoon
	FROM ShadowDB.dbo.Winkels
-----------------------------------------------------

--SET IDENTITY_INSERT Municipality ON
--SET IDENTITY_INSERT Zipcode ON
END;
GO
/****** Object:  StoredProcedure [dbo].[spReport1StoreAddressNotImported]    Script Date: 15-10-2020 11:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spReport1StoreAddressNotImported]
AS
Select * FROM ShadowDB.dbo.Winkels
Where NOT EXISTS (SELECT * FROM StoreAddress a
	WHERE a.Streetname = Straat and a.City = UPPER(Plaats) and a.Number = Nummer)
GO
USE [master]
GO
ALTER DATABASE [MarioDB] SET  READ_WRITE 
GO
