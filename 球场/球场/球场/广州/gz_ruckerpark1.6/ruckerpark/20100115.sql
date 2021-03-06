if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[court]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[court]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[total]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[total]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vipcard_goods_sale]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[vipcard_goods_sale]
GO

CREATE TABLE [dbo].[court] (
	[court_id] [int] IDENTITY (1, 1) NOT NULL ,
	[court_name] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[court_number] [int] NOT NULL ,
	[court_per_num] [int] NOT NULL ,
	[court_tel] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[court_price] [money] NULL ,
	[court_start] [datetime] NOT NULL ,
	[court_end] [datetime] NULL ,
	[xf_money] [float] NULL ,
	[court_hour] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[total] (
	[tagname] [char] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[num] [int] NULL ,
	[total] [money] NULL ,
	[sort] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[vipcard_goods_sale] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[cardid] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[goods_name] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[goods_price] [money] NULL ,
	[goods_num] [int] NULL ,
	[sale_date] [datetime] NULL 
) ON [PRIMARY]
GO

