IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'new_ruckerpark')
	DROP DATABASE [new_ruckerpark]
GO

CREATE DATABASE [new_ruckerpark]  ON (NAME = N'ruckerpark_Data', FILENAME = N'e:\Data\new_ruckerpark_Data.MDF' , SIZE = 8, FILEGROWTH = 10%) LOG ON (NAME = N'ruckerpark_Log', FILENAME = N'e:\Data\new_ruckerpark_Log.LDF' , SIZE = 1, FILEGROWTH = 10%)
 COLLATE Chinese_PRC_CI_AS
GO

exec sp_dboption N'new_ruckerpark', N'autoclose', N'false'
GO

exec sp_dboption N'new_ruckerpark', N'bulkcopy', N'false'
GO

exec sp_dboption N'new_ruckerpark', N'trunc. log', N'true'
GO

exec sp_dboption N'new_ruckerpark', N'torn page detection', N'true'
GO

exec sp_dboption N'new_ruckerpark', N'read only', N'false'
GO

exec sp_dboption N'new_ruckerpark', N'dbo use', N'false'
GO

exec sp_dboption N'new_ruckerpark', N'single', N'false'
GO

exec sp_dboption N'new_ruckerpark', N'autoshrink', N'true'
GO

exec sp_dboption N'new_ruckerpark', N'ANSI null default', N'false'
GO

exec sp_dboption N'new_ruckerpark', N'recursive triggers', N'false'
GO

exec sp_dboption N'new_ruckerpark', N'ANSI nulls', N'false'
GO

exec sp_dboption N'new_ruckerpark', N'concat null yields null', N'false'
GO

exec sp_dboption N'new_ruckerpark', N'cursor close on commit', N'false'
GO

exec sp_dboption N'new_ruckerpark', N'default to local cursor', N'false'
GO

exec sp_dboption N'new_ruckerpark', N'quoted identifier', N'false'
GO

exec sp_dboption N'new_ruckerpark', N'ANSI warnings', N'false'
GO

exec sp_dboption N'new_ruckerpark', N'auto create statistics', N'true'
GO

exec sp_dboption N'new_ruckerpark', N'auto update statistics', N'true'
GO

use [new_ruckerpark]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[goods_stock_init]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[goods_stock_init]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[goodsorderliststateadd]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[goodsorderliststateadd]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[goods_stock_add]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[goods_stock_add]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[goods_sale_insert]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[goods_sale_insert]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vipcard_sale_add]') and OBJECTPROPERTY(id, N'IsTrigger') = 1)
drop trigger [dbo].[vipcard_sale_add]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[balltj]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[balltj]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[goods_stock_now]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[goods_stock_now]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[mrxsj]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[mrxsj]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[total_count_new]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[total_count_new]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vipcardcz]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[vipcardcz]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vipcardsale]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[vipcardsale]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[zyyetj]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[zyyetj]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[card_consumption]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[card_consumption]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[court]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[court]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[goods_info]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[goods_info]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[goods_order]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[goods_order]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[goods_order_list_state]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[goods_order_list_state]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[goods_sale]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[goods_sale]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[goods_specification]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[goods_specification]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[goods_stock]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[goods_stock]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[integral]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[integral]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sale]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[sale]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[tmpa]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[tmpa]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[total]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[total]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[total_table]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[total_table]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[total_table_new]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[total_table_new]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[users]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[users]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vipcard]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[vipcard]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vipcard_cz]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[vipcard_cz]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vipcard_goods_sale]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[vipcard_goods_sale]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[vipcard_sale]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[vipcard_sale]
GO

CREATE TABLE [dbo].[card_consumption] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[cardid] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[begdatetime] [datetime] NULL ,
	[enddatetime] [datetime] NULL ,
	[ball_number] [int] NULL ,
	[xf_point] [float] NULL ,
	[xf_money] [float] NULL 
) ON [PRIMARY]
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

CREATE TABLE [dbo].[goods_info] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[goods_huohao] [int] NOT NULL ,
	[goods_tiaoma] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[goods_name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[goods_spcification] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[goods_spcification_big] [char] (10) COLLATE Chinese_PRC_CI_AS NULL ,
	[goods_price] [float] NOT NULL ,
	[reporter] [int] NULL ,
	[modify_person] [int] NULL ,
	[adddate] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[goods_order] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[orderid] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[goods_tiaoma] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[goods_huohao] [int] NULL ,
	[goods_name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[goods_spcification] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[goods_stock_now] [int] NULL ,
	[goods_purchase_price] [money] NULL ,
	[goods_number_order] [int] NULL ,
	[goods_number_storage] [int] NULL ,
	[reporter] [int] NULL ,
	[check_staff] [int] NULL ,
	[adddatetime] [datetime] NOT NULL ,
	[goods_num] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[goods_order_list_state] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[orderid] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[state] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[adddate] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[goods_sale] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[goods_huohao] [int] NULL ,
	[goods_tiaoma] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[goods_amounts] [int] NULL ,
	[cardid] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[adddate] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[goods_specification] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[specification_name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[adddate] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[goods_stock] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[goods_huohao] [int] NOT NULL ,
	[goods_stock] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[integral] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[integral] [int] NULL ,
	[product_name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[product_num] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[sale] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[num] [int] NULL ,
	[name] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[price] [float] NULL ,
	[num_day] [int] NULL ,
	[cardid] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[tmpa] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[cardid] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[point] [float] NOT NULL ,
	[m_name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[m_age] [nvarchar] (4) COLLATE Chinese_PRC_CI_AS NULL ,
	[m_qq] [nvarchar] (15) COLLATE Chinese_PRC_CI_AS NULL ,
	[m_mobile] [nvarchar] (15) COLLATE Chinese_PRC_CI_AS NULL ,
	[adddate] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[total] (
	[tagname] [char] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[num] [int] NULL ,
	[total] [money] NULL ,
	[sort] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[total_table] (
	[时间] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[会员消费] [int] NULL ,
	[非会员消费] [int] NULL ,
	[借球] [int] NULL ,
	[会员卡充值] [int] NULL ,
	[饮料销售] [float] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[total_table_new] (
	[标签] [int] NULL ,
	[非会员消费] [float] NULL ,
	[借球] [float] NULL ,
	[会员卡充值] [float] NULL ,
	[会员消费] [float] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[users] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[cardid] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[password] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[rights] [int] NOT NULL ,
	[adddate] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[vipcard] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[cardid] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[point] [float] NOT NULL ,
	[m_name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[m_age] [nvarchar] (4) COLLATE Chinese_PRC_CI_AS NULL ,
	[m_qq] [nvarchar] (15) COLLATE Chinese_PRC_CI_AS NULL ,
	[m_mobile] [nvarchar] (15) COLLATE Chinese_PRC_CI_AS NULL ,
	[adddate] [datetime] NOT NULL ,
	[hoop_name] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[integral] [int] NULL ,
	[xf_integral] [int] NULL ,
	[shop_integral] [int] NULL ,
	[istk] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[vipcard_cz] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[cardid] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[chongzhi] [int] NOT NULL ,
	[adddatetime] [datetime] NOT NULL 
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

CREATE TABLE [dbo].[vipcard_sale] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[cardid] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NOT NULL ,
	[salemoney] [int] NOT NULL ,
	[adddatetime] [datetime] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[card_consumption] WITH NOCHECK ADD 
	CONSTRAINT [PK_card_consumption] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[goods_info] WITH NOCHECK ADD 
	CONSTRAINT [PK_goods_info] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[goods_order] WITH NOCHECK ADD 
	CONSTRAINT [PK_goods_order] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[goods_sale] WITH NOCHECK ADD 
	CONSTRAINT [PK_goods_sale] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[goods_stock] WITH NOCHECK ADD 
	CONSTRAINT [PK_goods_stock] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[integral] WITH NOCHECK ADD 
	CONSTRAINT [PK_integral] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[sale] WITH NOCHECK ADD 
	CONSTRAINT [PK_sale] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[total_table] WITH NOCHECK ADD 
	CONSTRAINT [PK_total_table] PRIMARY KEY  CLUSTERED 
	(
		[时间]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[vipcard] WITH NOCHECK ADD 
	CONSTRAINT [PK_vipcard] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[goods_info] WITH NOCHECK ADD 
	CONSTRAINT [DF_goods_info_adddate] DEFAULT (getdate()) FOR [adddate]
GO

ALTER TABLE [dbo].[goods_order] WITH NOCHECK ADD 
	CONSTRAINT [DF_goods_order_goods_stock_now] DEFAULT (0) FOR [goods_stock_now],
	CONSTRAINT [DF_goods_order_goods_purchase_price] DEFAULT (0.0) FOR [goods_purchase_price],
	CONSTRAINT [DF_goods_order_adddatetime] DEFAULT (getdate()) FOR [adddatetime],
	CONSTRAINT [DF_goods_order_goods_num] DEFAULT (0) FOR [goods_num]
GO

ALTER TABLE [dbo].[goods_order_list_state] WITH NOCHECK ADD 
	CONSTRAINT [DF_goods_order_list_state_adddate] DEFAULT (getdate()) FOR [adddate]
GO

ALTER TABLE [dbo].[goods_sale] WITH NOCHECK ADD 
	CONSTRAINT [DF_goods_sale_adddate] DEFAULT (getdate()) FOR [adddate]
GO

ALTER TABLE [dbo].[goods_specification] WITH NOCHECK ADD 
	CONSTRAINT [DF_goods_specification_adddate] DEFAULT (getdate()) FOR [adddate]
GO

ALTER TABLE [dbo].[goods_stock] WITH NOCHECK ADD 
	CONSTRAINT [DF_goods_stock_goods_stock] DEFAULT (0) FOR [goods_stock]
GO

ALTER TABLE [dbo].[sale] WITH NOCHECK ADD 
	CONSTRAINT [DF_sale_num] DEFAULT (1) FOR [num],
	CONSTRAINT [DF_sale_num_day] DEFAULT (0) FOR [num_day]
GO

ALTER TABLE [dbo].[users] WITH NOCHECK ADD 
	CONSTRAINT [DF_user_rights] DEFAULT (0) FOR [rights],
	CONSTRAINT [DF_user_adddate] DEFAULT (getdate()) FOR [adddate]
GO

ALTER TABLE [dbo].[vipcard] WITH NOCHECK ADD 
	CONSTRAINT [DF_vipcard_point] DEFAULT (0) FOR [point],
	CONSTRAINT [DF_vipcard_adddate] DEFAULT (getdate()) FOR [adddate],
	CONSTRAINT [DF_vipcard_integral] DEFAULT (0) FOR [integral],
	CONSTRAINT [DF_vipcard_xf_integral] DEFAULT (0) FOR [xf_integral],
	CONSTRAINT [DF__vipcard__shop_in__39987BE6] DEFAULT (0) FOR [shop_integral],
	CONSTRAINT [DF_vipcard_istk] DEFAULT (1) FOR [istk]
GO

ALTER TABLE [dbo].[vipcard_cz] WITH NOCHECK ADD 
	CONSTRAINT [DF_vipcard_cz_chongzhi] DEFAULT (0) FOR [chongzhi],
	CONSTRAINT [DF_vipcard_cz_adddate] DEFAULT (getdate()) FOR [adddatetime]
GO

ALTER TABLE [dbo].[vipcard_sale] WITH NOCHECK ADD 
	CONSTRAINT [DF_vipcard_sale_adddatetime] DEFAULT (getdate()) FOR [adddatetime]
GO

 CREATE  INDEX [IX_integral] ON [dbo].[integral]([integral]) ON [PRIMARY]
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE balltj(@starttime datetime,@endtime datetime) 
AS
select convert(char(10),max(enddatetime),120) as '日期',count(*)*5 as '非会员借球金额' from card_consumption where ball_number is not null and xf_point is null and xf_money is not null and enddatetime between @starttime and @endtime group by datepart(day,enddatetime)
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE goods_stock_now AS
select distinct goods_info.goods_name as '商品名称',goods_info.goods_price as '销售价',goods_info.goods_spcification as '商品单位',goods_spcification_big as '商品规格',goods_price as '商品价格',goods_stock as '商品库存',b.goods_purchase_price as '进价',b.goods_num as '进货箱数' from goods_info left join goods_order as b on b.goods_huohao=goods_info.goods_huohao,goods_stock where goods_info.goods_huohao=goods_stock.goods_huohao
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE mrxsj  (@starttime datetime='2008-07-23',@endtime datetime='2008-07-28')
AS
--每日分时结算
select max(datepart(hour,enddatetime)) as '时间',sum(xf_point) as '会员消费',sum(xf_money) as '非会员消费' from card_consumption where enddatetime>@starttime and enddatetime<@endtime group by datepart(hour,enddatetime)
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE total_count_new  (@starttime datetime='2008-07-23',@endtime datetime='2008-07-28')
AS

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[total_table_new]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[total_table_new]

declare @sqlstr varchar(8000),@goods_name nvarchar(200)
set @sqlstr='CREATE TABLE [dbo].[total_table_new] 
([标签] [int] NULL,
[非会员消费] [float] NULL,
[借球] [float] NULL,
[会员卡充值] [float] NULL,
[会员消费] [float] NULL,'
	
declare mycursor cursor scroll for
select distinct goods_name from goods_sale,goods_info where goods_info.goods_huohao=goods_sale.goods_huohao and goods_sale.adddate between @starttime and @endtime
open mycursor

fetch next from mycursor into @goods_name
  while(@@fetch_status=0)
    begin
      set @sqlstr=@sqlstr+'['+@goods_name+']'+' [float] NULL,'
      fetch next from mycursor into @goods_name
    end



set @sqlstr=left(@sqlstr,len(@sqlstr)-1)
set @sqlstr=@sqlstr+') 
ON [PRIMARY]'

exec (@sqlstr)

declare @fhyxf_num float,--非会员消费
        @fhyxf_money float,
        @jq_num float,--借球
        @jq_money float,
        @hykcz_num float,--会员卡充值
        @hykcz_money float,
        @hykxs_num float,--会员卡销售
        @hykxs_money float,
        @hyxf_num float,--会员消费
        @hyxf_money float
select @fhyxf_num=count(1),@fhyxf_money=isnull(sum(xf_money),0) from card_consumption where enddatetime is not null and xf_money is not null and begdatetime between @starttime and @endtime
select @jq_num=count(1),@jq_money=count(1)*5 from card_consumption where ball_number is not null and xf_point is null and xf_money is not null and begdatetime between @starttime and @endtime
select @hykcz_num=count(1),@hykcz_money=isnull(sum(chongzhi),0) from vipcard_cz where adddatetime between @starttime and @endtime
select @hykxs_num=count(1),@hykxs_money=isnull(sum(salemoney),0) from vipcard_sale where adddatetime between @starttime and @endtime
select @hyxf_num=count(1),@hyxf_money=isnull(sum(xf_point),0) from card_consumption where enddatetime is not null and xf_point is not null and begdatetime between @starttime and @endtime
insert into total_table_new(标签,非会员消费,借球,会员卡充值,会员消费) values(1,@fhyxf_num,@jq_num,@hykcz_num+@hykxs_num,@hyxf_num)
insert into total_table_new(标签,非会员消费,借球,会员卡充值,会员消费) values(2,@fhyxf_money,@jq_money,@hykcz_money+@hykxs_money,@hyxf_money)

fetch first from mycursor into @goods_name
  while(@@fetch_status=0)
    begin
      declare @goods_xf_num varchar(50),@goods_xf_money varchar(50)

      declare @tmpsql varchar(2000)
      select @goods_xf_num=sum(goods_amounts),@goods_xf_money=isnull(sum(goods_amounts*goods_price),0) from goods_sale,goods_info where goods_info.goods_huohao=goods_sale.goods_huohao and goods_name=@goods_name and goods_sale.adddate between @starttime and @endtime
      
      set @tmpsql='update total_table_new set '+@goods_name+'='+@goods_xf_num+' where 标签=1'
      exec (@tmpsql)
      set @tmpsql='update total_table_new set '+@goods_name+'='+@goods_xf_money+' where 标签=2'
      exec (@tmpsql)

      fetch next from mycursor into @goods_name
    end


close mycursor
deallocate mycursor
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE vipcardcz(@starttime datetime,@endtime datetime) 
AS
select cardid as '卡号',chongzhi as '会员卡充值金额',adddatetime as '充值日期' from vipcard_cz where adddatetime between @starttime and @endtime
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE vipcardsale(@starttime datetime,@endtime datetime) 
AS
select cardid as '卡号',salemoney as '会员卡销售金额',adddatetime as '销售日期' from vipcard_sale where adddatetime between @starttime and @endtime
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE zyyetj (@starttime datetime='2008-07-23',@endtime datetime='2008-07-28')
AS
select convert(char(10),max(enddatetime),120) as '日期',sum(xf_point) as '消费点数',sum(xf_money) as '消费金额' from card_consumption where enddatetime >= @starttime and enddatetime <= @endtime group by datepart(day,enddatetime)
GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE TRIGGER goods_stock_init ON dbo.goods_info 
FOR INSERT
AS

declare @goods_huohao int
select @goods_huohao=goods_huohao from inserted

insert into goods_stock(goods_huohao,goods_stock) values(@goods_huohao,0)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE TRIGGER goodsorderliststateadd ON dbo.goods_order 
FOR INSERT
AS

declare @orderid nvarchar(50),@goods_order_num int
select top 1 @orderid=orderid from inserted
select @goods_order_num=count(1) from goods_order_list_state where orderid=@orderid
if @goods_order_num=0 begin
insert into goods_order_list_state(orderid,state) values(@orderid,'not check')
end

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE TRIGGER goods_stock_add ON dbo.goods_order 
FOR  insert
AS
declare @goods_huohao int,@goods_number_order int,@orderid nvarchar(50)
select @goods_huohao=goods_huohao,@goods_number_order=goods_number_order,@orderid=orderid from inserted
update goods_stock set goods_stock=goods_stock+@goods_number_order where goods_huohao=@goods_huohao
update goods_order_list_state set state='checked' where orderid=@orderid


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE TRIGGER goods_sale_insert ON dbo.goods_sale 
FOR INSERT
AS

declare @GoodsID int,@Qty int
select @GoodsID=goods_huohao,@Qty=goods_amounts from inserted

update goods_stock set
goods_stock=goods_stock - @Qty
where goods_huohao=@GoodsID

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE TRIGGER vipcard_sale_add ON dbo.vipcard 
FOR INSERT
AS
declare @cardid int,@money int
select @cardid=cardid,@money=point from inserted

insert into vipcard_sale(cardid,salemoney) values(@cardid,@money)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

