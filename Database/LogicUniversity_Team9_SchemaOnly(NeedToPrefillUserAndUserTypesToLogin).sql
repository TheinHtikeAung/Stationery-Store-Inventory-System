USE [LogicUniversityDB_Team9]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_user_type] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Supplier](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[supplier_name] [nvarchar](50) NOT NULL,
	[contact_name] [nvarchar](50) NULL,
	[phone_number] [varchar](50) NULL,
	[fax_number] [varchar](50) NULL,
	[address] [nvarchar](200) NULL,
	[email] [nvarchar](50) NULL,
 CONSTRAINT [PK_supplier] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StationeryCategory]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StationeryCategory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[category_name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_stationary_category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CollectionPoint]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CollectionPoint](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_CollectionPoint] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[department_name] [nvarchar](100) NOT NULL,
	[contact_name] [nvarchar](100) NULL,
	[phone] [nvarchar](50) NULL,
	[department_head] [int] NULL,
	[collection_point] [int] NOT NULL,
	[representative_name] [nvarchar](100) NULL,
 CONSTRAINT [PK_department] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stationery]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Stationery](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](50) NOT NULL,
	[stationery_name] [nvarchar](200) NOT NULL,
	[category] [int] NOT NULL,
	[price] [float] NOT NULL,
	[reorder_level] [int] NOT NULL,
	[reorder_quantity] [int] NOT NULL,
	[quantity_in_stock] [int] NOT NULL,
	[pending_quantity_to_distribute] [int] NOT NULL,
	[first_supplier] [int] NULL,
	[second_supplier] [int] NULL,
	[third_supplier] [int] NULL,
	[unit_of_measure] [varchar](50) NOT NULL,
	[bin] [varchar](50) NULL,
 CONSTRAINT [PK_stationary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[StockTransaction]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockTransaction](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[stationery] [int] NOT NULL,
	[balance] [nvarchar](50) NOT NULL,
	[description] [nvarchar](100) NOT NULL,
	[quantity] [nvarchar](50) NOT NULL,
	[datetime_inserted] [datetime] NOT NULL,
 CONSTRAINT [PK_StockTransaction] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[StationeryView]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[StationeryView]
AS
SELECT     dbo.StationeryCategory.category_name, dbo.Supplier.supplier_name, dbo.Stationery.id, dbo.Stationery.code, dbo.Stationery.stationery_name, 
                      dbo.Stationery.category, dbo.Stationery.price, dbo.Stationery.reorder_level, dbo.Stationery.reorder_quantity, dbo.Stationery.quantity_in_stock, 
                      dbo.Stationery.pending_quantity_to_distribute, dbo.Stationery.first_supplier, dbo.Stationery.second_supplier, dbo.Stationery.third_supplier, 
                      dbo.Stationery.unit_of_measure, dbo.Stationery.bin, dbo.Supplier.code AS supplier_code, 
                      dbo.Stationery.quantity_in_stock - dbo.Stationery.pending_quantity_to_distribute AS available_quantity
FROM         dbo.Stationery LEFT OUTER JOIN
                      dbo.StationeryCategory ON dbo.Stationery.category = dbo.StationeryCategory.id LEFT OUTER JOIN
                      dbo.Supplier ON dbo.Stationery.third_supplier = dbo.Supplier.id AND dbo.Stationery.first_supplier = dbo.Supplier.id AND 
                      dbo.Stationery.second_supplier = dbo.Supplier.id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Stationery"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 217
               Right = 278
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "StationeryCategory"
            Begin Extent = 
               Top = 5
               Left = 414
               Bottom = 93
               Right = 578
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Supplier"
            Begin Extent = 
               Top = 117
               Left = 413
               Bottom = 235
               Right = 574
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'StationeryView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'StationeryView'
GO
/****** Object:  Table [dbo].[User]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[firstname] [nvarchar](50) NULL,
	[lastname] [nvarchar](50) NULL,
	[email] [nvarchar](50) NULL,
	[phone_number] [nvarchar](50) NULL,
	[department] [int] NULL,
	[user_type] [int] NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequisitionFullfillment]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequisitionFullfillment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[department] [int] NOT NULL,
	[stationery] [int] NOT NULL,
	[requested_quantity] [int] NOT NULL,
	[fulfill_quantity] [int] NOT NULL,
	[unfulfill_quantity] [int] NOT NULL,
 CONSTRAINT [PK_Fulfillment_request] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Disbursement]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Disbursement](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[department] [int] NOT NULL,
	[department_rept] [nvarchar](50) NOT NULL,
	[distributed_date] [date] NOT NULL,
 CONSTRAINT [PK_Disbursement] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[GetDepartmentForFulfillment]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GetDepartmentForFulfillment]
AS
SELECT     dbo.Department.id, dbo.Department.department_name, dbo.Department.representative_name, dbo.CollectionPoint.name
FROM         dbo.CollectionPoint INNER JOIN
                      dbo.Department ON dbo.CollectionPoint.id = dbo.Department.collection_point
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "CollectionPoint"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 94
               Right = 198
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Department"
            Begin Extent = 
               Top = 6
               Left = 236
               Bottom = 124
               Right = 428
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GetDepartmentForFulfillment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GetDepartmentForFulfillment'
GO
/****** Object:  Table [dbo].[PurchaseOrder]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrder](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[po_stationery] [int] NOT NULL,
	[po_quantity_in_hand] [int] NOT NULL,
	[po_reorder_level] [int] NOT NULL,
	[po_reordered_quantity] [int] NOT NULL,
	[po_supplier] [int] NOT NULL,
	[po_reorder_date] [datetime] NOT NULL,
	[po_status] [int] NOT NULL,
 CONSTRAINT [PK_PurchaseOrder] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discrepancy]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discrepancy](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[stationery] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[remark] [nvarchar](200) NULL,
	[datetime_reported] [date] NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_Discrepancy] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[UserView]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UserView]
AS
SELECT     dbo.[User].id, dbo.[User].username, dbo.[User].password, dbo.[User].firstname, dbo.[User].lastname, dbo.[User].email, dbo.[User].phone_number, 
                      dbo.[User].department, dbo.[User].user_type, dbo.UserType.type, dbo.Department.department_name, dbo.Department.id AS Expr1, dbo.UserType.id AS Expr2
FROM         dbo.Department RIGHT OUTER JOIN
                      dbo.[User] ON dbo.Department.id = dbo.[User].department LEFT OUTER JOIN
                      dbo.UserType ON dbo.[User].user_type = dbo.UserType.id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Department"
            Begin Extent = 
               Top = 103
               Left = 607
               Bottom = 221
               Right = 799
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "User"
            Begin Extent = 
               Top = 54
               Left = 330
               Bottom = 172
               Right = 491
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UserType"
            Begin Extent = 
               Top = 0
               Left = 608
               Bottom = 88
               Right = 768
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 14
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserView'
GO
/****** Object:  Table [dbo].[DisbursementDetails]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DisbursementDetails](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[disbursement] [int] NOT NULL,
	[stationery] [int] NOT NULL,
	[requested_quantity] [int] NOT NULL,
	[fulfilled_quantity] [int] NOT NULL,
	[previous_unfulfilled_quantity] [int] NOT NULL,
 CONSTRAINT [PK_Disbursement_details] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OAuth]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OAuth](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_obj] [int] NOT NULL,
	[access_token] [varchar](500) NOT NULL,
 CONSTRAINT [PK_auth] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[Inventorhy_Status_Report_View]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Inventorhy_Status_Report_View]
AS
SELECT  dbo.Stationery.code, dbo.Stationery.stationery_name, dbo.Stationery.bin, dbo.Stationery.unit_of_measure, dbo.StockTransaction.balance, dbo.Stationery.reorder_level, dbo.StockTransaction.datetime_inserted
FROM     dbo.Stationery INNER JOIN
               dbo.StockTransaction ON dbo.Stationery.id = dbo.StockTransaction.stationery
GROUP BY dbo.Stationery.code, dbo.Stationery.stationery_name, dbo.Stationery.bin, dbo.Stationery.unit_of_measure, dbo.Stationery.reorder_level, dbo.StockTransaction.datetime_inserted, dbo.StockTransaction.balance
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Stationery"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 160
               Right = 316
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "StockTransaction"
            Begin Extent = 
               Top = 6
               Left = 354
               Bottom = 160
               Right = 559
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Inventorhy_Status_Report_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Inventorhy_Status_Report_View'
GO
/****** Object:  View [dbo].[GetDepartmentInfo]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GetDepartmentInfo]
AS
SELECT     dbo.Department.*, dbo.[User].username, dbo.CollectionPoint.name
FROM         dbo.Department LEFT OUTER JOIN
                      dbo.CollectionPoint ON dbo.Department.collection_point = dbo.CollectionPoint.id LEFT OUTER JOIN
                      dbo.[User] ON dbo.Department.department_head = dbo.[User].id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[47] 4[10] 2[16] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Department"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 221
               Right = 230
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "User"
            Begin Extent = 
               Top = 1
               Left = 463
               Bottom = 119
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CollectionPoint"
            Begin Extent = 
               Top = 163
               Left = 345
               Bottom = 251
               Right = 505
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2415
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GetDepartmentInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GetDepartmentInfo'
GO
/****** Object:  View [dbo].[GetDepartmentFulfillment]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GetDepartmentFulfillment]
AS
SELECT     dbo.Department.department_name, dbo.Department.id
FROM         dbo.RequisitionFullfillment INNER JOIN
                      dbo.Department ON dbo.RequisitionFullfillment.department = dbo.Department.id
GROUP BY dbo.Department.department_name, dbo.Department.id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "RequisitionFullfillment"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 124
               Right = 238
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Department"
            Begin Extent = 
               Top = 6
               Left = 276
               Bottom = 124
               Right = 484
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GetDepartmentFulfillment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GetDepartmentFulfillment'
GO
/****** Object:  View [dbo].[Fulfillment_View]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Fulfillment_View]
AS
SELECT     dbo.RequisitionFullfillment.id, dbo.RequisitionFullfillment.department, dbo.RequisitionFullfillment.stationery, dbo.Stationery.stationery_name, 
                      dbo.RequisitionFullfillment.requested_quantity, dbo.RequisitionFullfillment.fulfill_quantity, dbo.RequisitionFullfillment.unfulfill_quantity, 
                      dbo.Stationery.quantity_in_stock - dbo.Stationery.pending_quantity_to_distribute + dbo.RequisitionFullfillment.fulfill_quantity AS stock_control
FROM         dbo.Department INNER JOIN
                      dbo.RequisitionFullfillment ON dbo.Department.id = dbo.RequisitionFullfillment.department INNER JOIN
                      dbo.Stationery ON dbo.RequisitionFullfillment.stationery = dbo.Stationery.id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Department"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 124
               Right = 230
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RequisitionFullfillment"
            Begin Extent = 
               Top = 6
               Left = 268
               Bottom = 124
               Right = 452
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Stationery"
            Begin Extent = 
               Top = 6
               Left = 490
               Bottom = 124
               Right = 730
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Fulfillment_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Fulfillment_View'
GO
/****** Object:  View [dbo].[Discrepancy_View]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Discrepancy_View]
AS
SELECT     dbo.Discrepancy.id, dbo.Discrepancy.quantity, dbo.Discrepancy.remark, dbo.Discrepancy.datetime_reported, dbo.Discrepancy.status, 
                      dbo.Stationery.stationery_name, dbo.Stationery.code, dbo.Stationery.price
FROM         dbo.Discrepancy INNER JOIN
                      dbo.Stationery ON dbo.Discrepancy.stationery = dbo.Stationery.id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Discrepancy"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 217
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Stationery"
            Begin Extent = 
               Top = 6
               Left = 255
               Bottom = 125
               Right = 495
            End
            DisplayFlags = 280
            TopColumn = 10
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Discrepancy_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Discrepancy_View'
GO
/****** Object:  Table [dbo].[Requisition]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Requisition](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[stationery] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[user_obj] [int] NOT NULL,
	[status] [int] NOT NULL,
	[reject_remark] [nvarchar](100) NULL,
	[datetime_requested] [datetime] NOT NULL,
	[department] [int] NOT NULL,
	[user_approved_by] [int] NULL,
 CONSTRAINT [PK_Requisitions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDelegate]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDelegate](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_to] [int] NOT NULL,
	[user_by] [int] NOT NULL,
	[date_from] [date] NOT NULL,
	[date_to] [date] NOT NULL,
	[department] [int] NOT NULL,
 CONSTRAINT [PK_delegate] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[Supervisor_Discrepancy_View]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Supervisor_Discrepancy_View]
AS
SELECT     dbo.Stationery.stationery_name, dbo.Discrepancy.id, dbo.Discrepancy.quantity, dbo.Discrepancy.remark, dbo.Discrepancy.datetime_reported, 
                      dbo.Discrepancy.status, dbo.Stationery.price, dbo.Stationery.price * dbo.Discrepancy.quantity AS totalprice, dbo.Discrepancy.stationery
FROM         dbo.Stationery INNER JOIN
                      dbo.Discrepancy ON dbo.Stationery.id = dbo.Discrepancy.stationery
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Stationery"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 278
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Discrepancy"
            Begin Extent = 
               Top = 6
               Left = 316
               Bottom = 125
               Right = 495
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Supervisor_Discrepancy_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Supervisor_Discrepancy_View'
GO
/****** Object:  View [dbo].[ReOrderReport_View]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ReOrderReport_View]
AS
SELECT     dbo.PurchaseOrder.id, dbo.PurchaseOrder.po_reorder_level, dbo.StationeryCategory.category_name, dbo.Stationery.stationery_name, 
                      dbo.PurchaseOrder.po_reorder_date, dbo.PurchaseOrder.po_reordered_quantity
FROM         dbo.Stationery INNER JOIN
                      dbo.StationeryCategory ON dbo.Stationery.category = dbo.StationeryCategory.id INNER JOIN
                      dbo.PurchaseOrder ON dbo.Stationery.id = dbo.PurchaseOrder.po_stationery
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "StationeryCategory"
            Begin Extent = 
               Top = 6
               Left = 316
               Bottom = 95
               Right = 480
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PurchaseOrder"
            Begin Extent = 
               Top = 6
               Left = 518
               Bottom = 125
               Right = 719
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "Stationery"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 278
            End
            DisplayFlags = 280
            TopColumn = 7
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReOrderReport_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReOrderReport_View'
GO
/****** Object:  View [dbo].[PurchaseOrderView]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[PurchaseOrderView]
AS
SELECT     dbo.PurchaseOrder.*, dbo.Supplier.supplier_name, dbo.Stationery.stationery_name, dbo.Stationery.code, dbo.Stationery.category, 
                      dbo.Stationery.unit_of_measure
FROM         dbo.PurchaseOrder INNER JOIN
                      dbo.Supplier ON dbo.PurchaseOrder.po_supplier = dbo.Supplier.id INNER JOIN
                      dbo.Stationery ON dbo.PurchaseOrder.po_stationery = dbo.Stationery.id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PurchaseOrder"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 124
               Right = 239
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Supplier"
            Begin Extent = 
               Top = 98
               Left = 312
               Bottom = 216
               Right = 473
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Stationery"
            Begin Extent = 
               Top = 0
               Left = 510
               Bottom = 118
               Right = 750
            End
            DisplayFlags = 280
            TopColumn = 10
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PurchaseOrderView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PurchaseOrderView'
GO
/****** Object:  View [dbo].[RetrievelFormMaster_View]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[RetrievelFormMaster_View]
AS
SELECT     dbo.Stationery.id, dbo.Stationery.stationery_name, SUM(dbo.RequisitionFullfillment.requested_quantity) AS Needed, SUM(dbo.RequisitionFullfillment.fulfill_quantity) 
                      AS Retrieved
FROM         dbo.Stationery INNER JOIN
                      dbo.RequisitionFullfillment ON dbo.Stationery.id = dbo.RequisitionFullfillment.stationery
GROUP BY dbo.Stationery.id, dbo.Stationery.stationery_name
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Stationery"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 124
               Right = 294
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RequisitionFullfillment"
            Begin Extent = 
               Top = 6
               Left = 332
               Bottom = 124
               Right = 532
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RetrievelFormMaster_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RetrievelFormMaster_View'
GO
/****** Object:  View [dbo].[RetrievalFormDetails_View]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[RetrievalFormDetails_View]
AS
SELECT     dbo.Department.id, dbo.Department.department_name, SUM(dbo.RequisitionFullfillment.requested_quantity) AS Needed, 
                      SUM(dbo.RequisitionFullfillment.fulfill_quantity) AS Actual, dbo.RequisitionFullfillment.stationery
FROM         dbo.Department INNER JOIN
                      dbo.RequisitionFullfillment ON dbo.Department.id = dbo.RequisitionFullfillment.department
GROUP BY dbo.Department.id, dbo.Department.department_name, dbo.RequisitionFullfillment.stationery
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Department"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 124
               Right = 246
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "RequisitionFullfillment"
            Begin Extent = 
               Top = 6
               Left = 284
               Bottom = 124
               Right = 484
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RetrievalFormDetails_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RetrievalFormDetails_View'
GO
/****** Object:  View [dbo].[RequisitionView]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[RequisitionView]
AS
SELECT     dbo.Requisition.quantity, dbo.Requisition.stationery, dbo.Requisition.id AS requisition_id, dbo.Requisition.user_obj, dbo.Stationery.stationery_name, 
                      dbo.Requisition.status, dbo.Stationery.id AS stationery_id, dbo.[User].id AS user_id, dbo.[User].username, dbo.Requisition.department, dbo.[User].firstname, 
                      dbo.[User].lastname, dbo.Requisition.datetime_requested, dbo.Requisition.reject_remark, dbo.Requisition.user_approved_by
FROM         dbo.Requisition INNER JOIN
                      dbo.Stationery ON dbo.Requisition.stationery = dbo.Stationery.id INNER JOIN
                      dbo.[User] ON dbo.Requisition.user_obj = dbo.[User].id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[38] 4[4] 2[31] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Requisition"
            Begin Extent = 
               Top = 4
               Left = 330
               Bottom = 220
               Right = 516
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Stationery"
            Begin Extent = 
               Top = 20
               Left = 766
               Bottom = 138
               Right = 945
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "User"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 124
               Right = 199
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 10
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1635
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RequisitionView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RequisitionView'
GO
/****** Object:  View [dbo].[Report_View]    Script Date: 09/17/2013 16:41:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Report_View]
AS
SELECT     dbo.Disbursement.department, dbo.Department.department_name, dbo.DisbursementDetails.requested_quantity, dbo.DisbursementDetails.fulfilled_quantity, 
                      dbo.Stationery.stationery_name, dbo.Stationery.price, dbo.StationeryCategory.category_name, dbo.Disbursement.distributed_date
FROM         dbo.Disbursement INNER JOIN
                      dbo.DisbursementDetails ON dbo.Disbursement.id = dbo.DisbursementDetails.disbursement INNER JOIN
                      dbo.Stationery ON dbo.DisbursementDetails.stationery = dbo.Stationery.id INNER JOIN
                      dbo.StationeryCategory ON dbo.Stationery.category = dbo.StationeryCategory.id INNER JOIN
                      dbo.Department ON dbo.Disbursement.department = dbo.Department.id
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Disbursement"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 125
               Right = 209
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "DisbursementDetails"
            Begin Extent = 
               Top = 6
               Left = 247
               Bottom = 125
               Right = 475
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Stationery"
            Begin Extent = 
               Top = 126
               Left = 38
               Bottom = 245
               Right = 278
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "StationeryCategory"
            Begin Extent = 
               Top = 126
               Left = 316
               Bottom = 215
               Right = 480
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Department"
            Begin Extent = 
               Top = 6
               Left = 513
               Bottom = 125
               Right = 705
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
     ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Report_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'    Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Report_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Report_View'
GO
/****** Object:  ForeignKey [FK_Department_CollectionPoint]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_CollectionPoint] FOREIGN KEY([collection_point])
REFERENCES [dbo].[CollectionPoint] ([id])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_CollectionPoint]
GO
/****** Object:  ForeignKey [FK_Disbursement_Department]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[Disbursement]  WITH CHECK ADD  CONSTRAINT [FK_Disbursement_Department] FOREIGN KEY([department])
REFERENCES [dbo].[Department] ([id])
GO
ALTER TABLE [dbo].[Disbursement] CHECK CONSTRAINT [FK_Disbursement_Department]
GO
/****** Object:  ForeignKey [FK_DisbursementDetails_Disbursement]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[DisbursementDetails]  WITH CHECK ADD  CONSTRAINT [FK_DisbursementDetails_Disbursement] FOREIGN KEY([disbursement])
REFERENCES [dbo].[Disbursement] ([id])
GO
ALTER TABLE [dbo].[DisbursementDetails] CHECK CONSTRAINT [FK_DisbursementDetails_Disbursement]
GO
/****** Object:  ForeignKey [FK_DisbursementDetails_Stationery]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[DisbursementDetails]  WITH CHECK ADD  CONSTRAINT [FK_DisbursementDetails_Stationery] FOREIGN KEY([stationery])
REFERENCES [dbo].[Stationery] ([id])
GO
ALTER TABLE [dbo].[DisbursementDetails] CHECK CONSTRAINT [FK_DisbursementDetails_Stationery]
GO
/****** Object:  ForeignKey [FK_Discrepancy_Stationery]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[Discrepancy]  WITH CHECK ADD  CONSTRAINT [FK_Discrepancy_Stationery] FOREIGN KEY([stationery])
REFERENCES [dbo].[Stationery] ([id])
GO
ALTER TABLE [dbo].[Discrepancy] CHECK CONSTRAINT [FK_Discrepancy_Stationery]
GO
/****** Object:  ForeignKey [FK_OAuth_User]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[OAuth]  WITH CHECK ADD  CONSTRAINT [FK_OAuth_User] FOREIGN KEY([user_obj])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[OAuth] CHECK CONSTRAINT [FK_OAuth_User]
GO
/****** Object:  ForeignKey [FK_PurchaseOrder_Stationery]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrder_Stationery] FOREIGN KEY([po_stationery])
REFERENCES [dbo].[Stationery] ([id])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK_PurchaseOrder_Stationery]
GO
/****** Object:  ForeignKey [FK_PurchaseOrder_Supplier]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrder_Supplier] FOREIGN KEY([po_supplier])
REFERENCES [dbo].[Supplier] ([id])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK_PurchaseOrder_Supplier]
GO
/****** Object:  ForeignKey [FK_Requisition_Department]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[Requisition]  WITH CHECK ADD  CONSTRAINT [FK_Requisition_Department] FOREIGN KEY([department])
REFERENCES [dbo].[Department] ([id])
GO
ALTER TABLE [dbo].[Requisition] CHECK CONSTRAINT [FK_Requisition_Department]
GO
/****** Object:  ForeignKey [FK_Requisition_Stationery]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[Requisition]  WITH CHECK ADD  CONSTRAINT [FK_Requisition_Stationery] FOREIGN KEY([stationery])
REFERENCES [dbo].[Stationery] ([id])
GO
ALTER TABLE [dbo].[Requisition] CHECK CONSTRAINT [FK_Requisition_Stationery]
GO
/****** Object:  ForeignKey [FK_Requisition_User]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[Requisition]  WITH CHECK ADD  CONSTRAINT [FK_Requisition_User] FOREIGN KEY([user_obj])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Requisition] CHECK CONSTRAINT [FK_Requisition_User]
GO
/****** Object:  ForeignKey [FK_Requisition_User_Approved_By]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[Requisition]  WITH CHECK ADD  CONSTRAINT [FK_Requisition_User_Approved_By] FOREIGN KEY([user_approved_by])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Requisition] CHECK CONSTRAINT [FK_Requisition_User_Approved_By]
GO
/****** Object:  ForeignKey [FK_RequisitionFullfillment_Department]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[RequisitionFullfillment]  WITH CHECK ADD  CONSTRAINT [FK_RequisitionFullfillment_Department] FOREIGN KEY([department])
REFERENCES [dbo].[Department] ([id])
GO
ALTER TABLE [dbo].[RequisitionFullfillment] CHECK CONSTRAINT [FK_RequisitionFullfillment_Department]
GO
/****** Object:  ForeignKey [FK_RequisitionFullfillment_Stationery]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[RequisitionFullfillment]  WITH CHECK ADD  CONSTRAINT [FK_RequisitionFullfillment_Stationery] FOREIGN KEY([stationery])
REFERENCES [dbo].[Stationery] ([id])
GO
ALTER TABLE [dbo].[RequisitionFullfillment] CHECK CONSTRAINT [FK_RequisitionFullfillment_Stationery]
GO
/****** Object:  ForeignKey [FK_Stationery_StationeryCategory]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[Stationery]  WITH CHECK ADD  CONSTRAINT [FK_Stationery_StationeryCategory] FOREIGN KEY([category])
REFERENCES [dbo].[StationeryCategory] ([id])
GO
ALTER TABLE [dbo].[Stationery] CHECK CONSTRAINT [FK_Stationery_StationeryCategory]
GO
/****** Object:  ForeignKey [FK_Stationery_Supplier1]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[Stationery]  WITH CHECK ADD  CONSTRAINT [FK_Stationery_Supplier1] FOREIGN KEY([first_supplier])
REFERENCES [dbo].[Supplier] ([id])
GO
ALTER TABLE [dbo].[Stationery] CHECK CONSTRAINT [FK_Stationery_Supplier1]
GO
/****** Object:  ForeignKey [FK_Stationery_Supplier2]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[Stationery]  WITH CHECK ADD  CONSTRAINT [FK_Stationery_Supplier2] FOREIGN KEY([second_supplier])
REFERENCES [dbo].[Supplier] ([id])
GO
ALTER TABLE [dbo].[Stationery] CHECK CONSTRAINT [FK_Stationery_Supplier2]
GO
/****** Object:  ForeignKey [FK_Stationery_Supplier3]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[Stationery]  WITH CHECK ADD  CONSTRAINT [FK_Stationery_Supplier3] FOREIGN KEY([third_supplier])
REFERENCES [dbo].[Supplier] ([id])
GO
ALTER TABLE [dbo].[Stationery] CHECK CONSTRAINT [FK_Stationery_Supplier3]
GO
/****** Object:  ForeignKey [FK_StockTransaction_Stationery]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[StockTransaction]  WITH CHECK ADD  CONSTRAINT [FK_StockTransaction_Stationery] FOREIGN KEY([stationery])
REFERENCES [dbo].[Stationery] ([id])
GO
ALTER TABLE [dbo].[StockTransaction] CHECK CONSTRAINT [FK_StockTransaction_Stationery]
GO
/****** Object:  ForeignKey [FK_User_Department]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Department] FOREIGN KEY([department])
REFERENCES [dbo].[Department] ([id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Department]
GO
/****** Object:  ForeignKey [FK_User_UserType]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserType] FOREIGN KEY([user_type])
REFERENCES [dbo].[UserType] ([id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserType]
GO
/****** Object:  ForeignKey [FK_UserDelegate_Department]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[UserDelegate]  WITH CHECK ADD  CONSTRAINT [FK_UserDelegate_Department] FOREIGN KEY([department])
REFERENCES [dbo].[Department] ([id])
GO
ALTER TABLE [dbo].[UserDelegate] CHECK CONSTRAINT [FK_UserDelegate_Department]
GO
/****** Object:  ForeignKey [FK_UserDelegate_User_By]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[UserDelegate]  WITH CHECK ADD  CONSTRAINT [FK_UserDelegate_User_By] FOREIGN KEY([user_by])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[UserDelegate] CHECK CONSTRAINT [FK_UserDelegate_User_By]
GO
/****** Object:  ForeignKey [FK_UserDelegate_User_To]    Script Date: 09/17/2013 16:41:14 ******/
ALTER TABLE [dbo].[UserDelegate]  WITH CHECK ADD  CONSTRAINT [FK_UserDelegate_User_To] FOREIGN KEY([user_to])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[UserDelegate] CHECK CONSTRAINT [FK_UserDelegate_User_To]
GO
