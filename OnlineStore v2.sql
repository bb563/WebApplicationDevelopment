USE [master]
GO
/****** Object:  Database [OnlineStore]    Script Date: 6/24/2024 4:24:22 PM ******/
CREATE DATABASE [OnlineStore]
 
GO
ALTER DATABASE [OnlineStore] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OnlineStore].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OnlineStore] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OnlineStore] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OnlineStore] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OnlineStore] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OnlineStore] SET ARITHABORT OFF 
GO
ALTER DATABASE [OnlineStore] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [OnlineStore] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OnlineStore] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OnlineStore] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OnlineStore] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OnlineStore] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OnlineStore] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OnlineStore] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OnlineStore] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OnlineStore] SET  ENABLE_BROKER 
GO
ALTER DATABASE [OnlineStore] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OnlineStore] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OnlineStore] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OnlineStore] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OnlineStore] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OnlineStore] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OnlineStore] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OnlineStore] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OnlineStore] SET  MULTI_USER 
GO
ALTER DATABASE [OnlineStore] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OnlineStore] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OnlineStore] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OnlineStore] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OnlineStore] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OnlineStore] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [OnlineStore] SET QUERY_STORE = ON
GO
ALTER DATABASE [OnlineStore] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [OnlineStore]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 6/24/2024 4:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[AdminID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](15) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[Hide] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 6/24/2024 4:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[CartID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[Total] [decimal](10, 2) NULL,
	[Hide] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CartID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartDetail]    Script Date: 6/24/2024 4:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartDetail](
	[CartDetailID] [int] IDENTITY(1,1) NOT NULL,
	[CartID] [int] NULL,
	[ProductID] [int] NULL,
	[Quantity] [int] NULL,
	[Hide] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CartDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 6/24/2024 4:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](100) NULL,
	[Description] [nvarchar](255) NULL,
	[Hide] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 6/24/2024 4:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[Phone] [nvarchar](15) NULL,
	[Address] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[Hide] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 6/24/2024 4:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[InvoiceDate] [datetime] NULL,
	[ShipAddress] [nvarchar](255) NULL,
	[Phone] [nvarchar](15) NULL,
	[Status] [nvarchar](50) NULL,
	[Total] [decimal](10, 2) NULL,
	[Date] [datetime] NULL,
	[Hide] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 6/24/2024 4:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetail](
	[InvoiceDetailID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceID] [int] NULL,
	[ProductID] [int] NULL,
	[Quantity] [int] NULL,
	[UnitPrice] [decimal](10, 2) NULL,
	[Hide] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[InvoiceDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 6/24/2024 4:24:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](100) NULL,
	[Description] [nvarchar](255) NULL,
	[Price] [decimal](10, 2) NULL,
	[ImageURL] [nvarchar](255) NULL,
	[CategoryID] [int] NULL,
	[SaleOff] [decimal](10, 2) NULL,
	[Quantity] [int] NULL,
	[IsBestSeller] [bit] NOT NULL,
	[IsHot] [bit] NULL,
	[Hide] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Admin] ON 

INSERT [dbo].[Admin] ([AdminID], [FirstName], [LastName], [Email], [Phone], [Address], [Password], [Hide]) VALUES (1, N'Nguyen', N'Quyet', N'naq.pondeeptry@gmail.com', N'0364907782', N'Dak Nong', N'f2c0122ab1e424e7aee5334cc581abb6318f2bb60803ccbc8fa565f6b6e7c939', 0)
SET IDENTITY_INSERT [dbo].[Admin] OFF
GO
SET IDENTITY_INSERT [dbo].[Cart] ON 

INSERT [dbo].[Cart] ([CartID], [CustomerID], [CreatedDate], [Total], [Hide]) VALUES (1, 1, CAST(N'2024-06-21T16:28:08.903' AS DateTime), CAST(800000.00 AS Decimal(10, 2)), 0)
SET IDENTITY_INSERT [dbo].[Cart] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryID], [CategoryName], [Description], [Hide]) VALUES (1, N'Quần áo', N'Các sản phẩm thời trang như áo quần, váy, đầm, ...', 0)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [Description], [Hide]) VALUES (2, N'Giày dép', N'Các sản phẩm giày dép thời trang', 0)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [Description], [Hide]) VALUES (3, N'Phụ kiện', N'Các loại phụ kiện thời trang như túi xách, mũ, kính mắt, ...', 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [Description], [Hide]) VALUES (5, N'Máy bán dép', N'Dép đây dép đây', 1)
INSERT [dbo].[Category] ([CategoryID], [CategoryName], [Description], [Hide]) VALUES (6, N'Máy bán dép', N'fdsfsdf', 0)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Email], [Phone], [Address], [Password], [Hide]) VALUES (1, N'Nguyễn', N'Văn B', N'vana@example.com', N'0123456789', N'123 Đường ABC, TP.HCM', N'password123', 0)
INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Email], [Phone], [Address], [Password], [Hide]) VALUES (2, N'Trần', N'Thị B', N'thib@example.com', N'0987654321', N'456 Đường XYZ, Hà Nội', N'password456', 0)
INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Email], [Phone], [Address], [Password], [Hide]) VALUES (3, N'Nguyen', N'Quyet', N'naq.pondeeptry1@gmail.com', N'0364907782', N'Dak Nong', N'f2c0122ab1e424e7aee5334cc581abb6318f2bb60803ccbc8fa565f6b6e7c939', 0)
INSERT [dbo].[Customer] ([CustomerID], [FirstName], [LastName], [Email], [Phone], [Address], [Password], [Hide]) VALUES (4, N'Nguyen', N'Quyet', N'naq.pondeeptry@gmail.com', N'0364907782', N'Dak Nong', N'f2c0122ab1e424e7aee5334cc581abb6318f2bb60803ccbc8fa565f6b6e7c939', 0)
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Invoice] ON 

INSERT [dbo].[Invoice] ([InvoiceID], [CustomerID], [InvoiceDate], [ShipAddress], [Phone], [Status], [Total], [Date], [Hide]) VALUES (1, 1, CAST(N'2024-06-21T16:28:08.917' AS DateTime), N'123 Đường ABC, TP.HCM 2', N'0123456789', N'Pending', CAST(800000.00 AS Decimal(10, 2)), CAST(N'2024-06-21T16:28:08.917' AS DateTime), 0)
INSERT [dbo].[Invoice] ([InvoiceID], [CustomerID], [InvoiceDate], [ShipAddress], [Phone], [Status], [Total], [Date], [Hide]) VALUES (2, 3, CAST(N'2024-06-22T18:36:08.803' AS DateTime), NULL, NULL, N'Completed', CAST(500000.00 AS Decimal(10, 2)), CAST(N'2024-06-22T18:36:08.803' AS DateTime), 0)
INSERT [dbo].[Invoice] ([InvoiceID], [CustomerID], [InvoiceDate], [ShipAddress], [Phone], [Status], [Total], [Date], [Hide]) VALUES (3, 3, CAST(N'2024-06-22T18:41:01.343' AS DateTime), NULL, NULL, N'Pending', CAST(500000.00 AS Decimal(10, 2)), CAST(N'2024-06-22T18:41:01.343' AS DateTime), 0)
INSERT [dbo].[Invoice] ([InvoiceID], [CustomerID], [InvoiceDate], [ShipAddress], [Phone], [Status], [Total], [Date], [Hide]) VALUES (4, 3, CAST(N'2024-06-22T18:44:40.337' AS DateTime), NULL, NULL, N'Completed', CAST(500000.00 AS Decimal(10, 2)), CAST(N'2024-06-22T18:44:40.337' AS DateTime), 0)
INSERT [dbo].[Invoice] ([InvoiceID], [CustomerID], [InvoiceDate], [ShipAddress], [Phone], [Status], [Total], [Date], [Hide]) VALUES (5, 3, CAST(N'2024-06-22T19:48:33.833' AS DateTime), NULL, NULL, N'Completed', CAST(500000.00 AS Decimal(10, 2)), CAST(N'2024-06-22T19:48:33.833' AS DateTime), 0)
INSERT [dbo].[Invoice] ([InvoiceID], [CustomerID], [InvoiceDate], [ShipAddress], [Phone], [Status], [Total], [Date], [Hide]) VALUES (6, 3, CAST(N'2024-06-22T19:49:10.503' AS DateTime), NULL, NULL, N'Completed', CAST(1600000.00 AS Decimal(10, 2)), CAST(N'2024-06-22T19:49:10.503' AS DateTime), 0)
INSERT [dbo].[Invoice] ([InvoiceID], [CustomerID], [InvoiceDate], [ShipAddress], [Phone], [Status], [Total], [Date], [Hide]) VALUES (7, 3, CAST(N'2024-06-23T10:47:27.723' AS DateTime), NULL, NULL, N'Completed', CAST(5200000.00 AS Decimal(10, 2)), CAST(N'2024-06-23T10:47:27.723' AS DateTime), 0)
INSERT [dbo].[Invoice] ([InvoiceID], [CustomerID], [InvoiceDate], [ShipAddress], [Phone], [Status], [Total], [Date], [Hide]) VALUES (8, 3, CAST(N'2024-06-23T20:14:17.537' AS DateTime), NULL, NULL, N'Completed', CAST(1600000.00 AS Decimal(10, 2)), CAST(N'2024-06-23T20:14:17.537' AS DateTime), 0)
INSERT [dbo].[Invoice] ([InvoiceID], [CustomerID], [InvoiceDate], [ShipAddress], [Phone], [Status], [Total], [Date], [Hide]) VALUES (9, 3, CAST(N'2024-06-24T15:07:00.863' AS DateTime), NULL, NULL, N'Completed', CAST(950000.00 AS Decimal(10, 2)), CAST(N'2024-06-24T15:07:00.863' AS DateTime), 0)
INSERT [dbo].[Invoice] ([InvoiceID], [CustomerID], [InvoiceDate], [ShipAddress], [Phone], [Status], [Total], [Date], [Hide]) VALUES (10, 3, CAST(N'2024-06-24T15:10:01.280' AS DateTime), NULL, NULL, N'Completed', CAST(950000.00 AS Decimal(10, 2)), CAST(N'2024-06-24T15:10:01.280' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[Invoice] OFF
GO
SET IDENTITY_INSERT [dbo].[InvoiceDetail] ON 

INSERT [dbo].[InvoiceDetail] ([InvoiceDetailID], [InvoiceID], [ProductID], [Quantity], [UnitPrice], [Hide]) VALUES (10, 9, 5, 1, CAST(600000.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[InvoiceDetail] ([InvoiceDetailID], [InvoiceID], [ProductID], [Quantity], [UnitPrice], [Hide]) VALUES (11, 9, 6, 1, CAST(200000.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[InvoiceDetail] ([InvoiceDetailID], [InvoiceID], [ProductID], [Quantity], [UnitPrice], [Hide]) VALUES (12, 9, 9, 1, CAST(150000.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[InvoiceDetail] ([InvoiceDetailID], [InvoiceID], [ProductID], [Quantity], [UnitPrice], [Hide]) VALUES (13, 10, 5, 1, CAST(600000.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[InvoiceDetail] ([InvoiceDetailID], [InvoiceID], [ProductID], [Quantity], [UnitPrice], [Hide]) VALUES (14, 10, 6, 1, CAST(200000.00 AS Decimal(10, 2)), 0)
INSERT [dbo].[InvoiceDetail] ([InvoiceDetailID], [InvoiceID], [ProductID], [Quantity], [UnitPrice], [Hide]) VALUES (15, 10, 9, 1, CAST(150000.00 AS Decimal(10, 2)), 0)
SET IDENTITY_INSERT [dbo].[InvoiceDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (5, N'Áo khoác', N'Áo khoác giữ ấm mùa đông', CAST(600000.00 AS Decimal(10, 2)), N'http://example.com/images/aokhoac.jpg', 1, CAST(5.00 AS Decimal(10, 2)), 15, 1, 1, 0)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (6, N'Sandal nữ', N'Sandal nữ phong cách', CAST(200000.00 AS Decimal(10, 2)), N'http://example.com/images/sandalnu.jpg', 2, CAST(0.00 AS Decimal(10, 2)), 25, 1, 1, 0)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (7, N'Bốt nam', N'Bốt nam cổ cao', CAST(800000.00 AS Decimal(10, 2)), N'http://example.com/images/botnam.jpg', 2, CAST(15.00 AS Decimal(10, 2)), 10, 1, 1, 0)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (8, N'Nón lưỡi trai', N'Nón lưỡi trai thời trang', CAST(100000.00 AS Decimal(10, 2)), N'http://example.com/images/nonluoitrai.jpg', 3, CAST(0.00 AS Decimal(10, 2)), 50, 1, 1, 1)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (9, N'Kính râm', N'Kính râm chống tia UV', CAST(150000.00 AS Decimal(10, 2)), N'http://example.com/images/kinhram.jpg', 3, CAST(0.00 AS Decimal(10, 2)), 30, 1, 1, 1)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (10, N'Túi xách nữ', N'Túi xách nữ hàng hiệu', CAST(700000.00 AS Decimal(10, 2)), N'http://example.com/images/tuixachnu.jpg', 3, CAST(0.00 AS Decimal(10, 2)), 20, 1, 1, 1)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (11, N'Đầm dạ hội', N'Đầm dạ hội sang trọng', CAST(900000.00 AS Decimal(10, 2)), N'http://example.com/images/damdahoi.jpg', 1, CAST(20.00 AS Decimal(10, 2)), 5, 1, 1, 0)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (12, N'Áo sơ mi', N'Áo sơ mi công sở', CAST(250000.00 AS Decimal(10, 2)), N'http://example.com/images/aosomi.jpg', 1, CAST(0.00 AS Decimal(10, 2)), 40, 1, 1, 0)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (13, N'Quần short', N'Quần short nam mát mẻ', CAST(200000.00 AS Decimal(10, 2)), N'http://example.com/images/quanshort.jpg', 1, CAST(0.00 AS Decimal(10, 2)), 60, 1, 1, 0)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (14, N'Giày cao gót', N'Giày cao gót thời trang', CAST(500000.00 AS Decimal(10, 2)), N'http://example.com/images/giaycaogot.jpg', 2, CAST(0.00 AS Decimal(10, 2)), 35, 1, 1, 0)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (15, N'Áo len', N'Áo len giữ ấm', CAST(300000.00 AS Decimal(10, 2)), N'http://example.com/images/aolen.jpg', 1, CAST(0.00 AS Decimal(10, 2)), 25, 1, 1, 0)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (16, N'Dép lê', N'Dép lê nam nữ', CAST(50000.00 AS Decimal(10, 2)), N'http://example.com/images/deple.jpg', 2, CAST(0.00 AS Decimal(10, 2)), 100, 1, 1, 0)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (17, N'Ví da', N'Ví da bò thật', CAST(450000.00 AS Decimal(10, 2)), N'http://example.com/images/vida.jpg', 3, CAST(0.00 AS Decimal(10, 2)), 15, 0, 1, 1)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (18, N'Mũ len', N'Mũ len giữ ấm', CAST(70000.00 AS Decimal(10, 2)), N'http://example.com/images/mulen.jpg', 3, CAST(0.00 AS Decimal(10, 2)), 30, 0, 1, 1)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (19, N'Tất vớ', N'Tất vớ nam nữ', CAST(20000.00 AS Decimal(10, 2)), N'http://example.com/images/tatvo.jpg', 3, CAST(0.00 AS Decimal(10, 2)), 200, 0, 1, 1)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (20, N'Giày lười', N'Giày lười thời trang', CAST(400000.00 AS Decimal(10, 2)), N'http://example.com/images/giayluoi.jpg', 2, CAST(0.00 AS Decimal(10, 2)), 20, 0, 1, 0)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (21, N'Balo', N'Balo laptop', CAST(600000.00 AS Decimal(10, 2)), N'http://example.com/images/balo.jpg', 3, CAST(0.00 AS Decimal(10, 2)), 10, 0, 1, 1)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (22, N'Đồng hồ', N'Đồng hồ đeo tay thời trang', CAST(1500000.00 AS Decimal(10, 2)), N'http://example.com/images/dongho.jpg', 3, CAST(0.00 AS Decimal(10, 2)), 5, 0, 1, 1)
INSERT [dbo].[Product] ([ProductID], [ProductName], [Description], [Price], [ImageURL], [CategoryID], [SaleOff], [Quantity], [IsBestSeller], [IsHot], [Hide]) VALUES (25, N'Áo khoác', N'fdsfsdf', CAST(1223.00 AS Decimal(10, 2)), N'https://zeanus.vn/upload/product/zn-0108/ao-khoac-gio-nam-2-lop-mu-roi-akn-130.jpg', 2, CAST(12.00 AS Decimal(10, 2)), 133, 0, 0, 0)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Customer__A9D10534092AAF2F]    Script Date: 6/24/2024 4:24:23 PM ******/
ALTER TABLE [dbo].[Customer] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admin] ADD  DEFAULT ((0)) FOR [Hide]
GO
ALTER TABLE [dbo].[Cart] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Cart] ADD  DEFAULT ((0)) FOR [Hide]
GO
ALTER TABLE [dbo].[CartDetail] ADD  DEFAULT ((0)) FOR [Hide]
GO
ALTER TABLE [dbo].[Category] ADD  DEFAULT ((0)) FOR [Hide]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT ((0)) FOR [Hide]
GO
ALTER TABLE [dbo].[Invoice] ADD  DEFAULT (getdate()) FOR [InvoiceDate]
GO
ALTER TABLE [dbo].[Invoice] ADD  DEFAULT ((0)) FOR [Hide]
GO
ALTER TABLE [dbo].[InvoiceDetail] ADD  DEFAULT ((0)) FOR [Hide]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [IsBestSeller]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [Hide]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD FOREIGN KEY([CartID])
REFERENCES [dbo].[Cart] ([CartID])
GO
ALTER TABLE [dbo].[CartDetail]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customer] ([CustomerID])
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Invoice] ([InvoiceID])
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductID])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Category] ([CategoryID])
GO
USE [master]
GO
ALTER DATABASE [OnlineStore] SET  READ_WRITE 
GO
