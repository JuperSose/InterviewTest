USE [Materials]
GO
/****** Object:  Table [dbo].[Buildings]    Script Date: 7/4/2022 12:20:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Buildings](
	[PKBuilding] [int] IDENTITY(1,1) NOT NULL,
	[Building] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Buildings] PRIMARY KEY CLUSTERED 
(
	[PKBuilding] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 7/4/2022 12:20:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[PKCustomers] [int] IDENTITY(1,1) NOT NULL,
	[Customer] [varchar](50) NOT NULL,
	[Prefix] [varchar](5) NOT NULL,
	[FKBuilding] [int] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[PKCustomers] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PartNumbers]    Script Date: 7/4/2022 12:20:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PartNumbers](
	[PKPartNumber] [int] IDENTITY(1,1) NOT NULL,
	[PartNumber] [varchar](50) NOT NULL,
	[FKCustomer] [int] NOT NULL,
	[Available] [bit] NOT NULL,
 CONSTRAINT [PK_PartNumbers] PRIMARY KEY CLUSTERED 
(
	[PKPartNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customers]  WITH CHECK ADD  CONSTRAINT [FK_Customers_Buildings] FOREIGN KEY([FKBuilding])
REFERENCES [dbo].[Buildings] ([PKBuilding])
GO
ALTER TABLE [dbo].[Customers] CHECK CONSTRAINT [FK_Customers_Buildings]
GO
ALTER TABLE [dbo].[PartNumbers]  WITH CHECK ADD  CONSTRAINT [FK_PartNumbers_Customers] FOREIGN KEY([FKCustomer])
REFERENCES [dbo].[Customers] ([PKCustomers])
GO
ALTER TABLE [dbo].[PartNumbers] CHECK CONSTRAINT [FK_PartNumbers_Customers]
GO
/****** Object:  StoredProcedure [dbo].[GetGridViewData]    Script Date: 7/4/2022 12:20:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetGridViewData]

AS
BEGIN

	SELECT 
	PartNumbers.PartNumber, Customers.Customer, Buildings.Building, PartNumbers.Available
FROM PartNumbers
INNER JOIN
	Customers
ON
	PartNumbers.FKCustomer = Customers.PKCustomers
INNER JOIN
	Buildings
ON
	Customers.FKBuilding = Buildings.PKBuilding

END
GO
