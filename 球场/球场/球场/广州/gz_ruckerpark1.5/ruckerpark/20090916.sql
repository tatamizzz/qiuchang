if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[integral]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[integral]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[sale]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[sale]
GO

CREATE TABLE [dbo].[integral] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[integral] [int] NULL ,
	[product_name] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[product_num] [int] NULL 
) ON [PRIMARY]
GO
insert into integral(integral,product_name,product_num)values('300','虎扑扑克','100');
insert into integral(integral,product_name,product_num)values('400','佳得乐运动水壶','80');
insert into integral(integral,product_name,product_num)values('650','佳得乐大毛巾','50');
insert into integral(integral,product_name,product_num)values('3000','peak 篮球服','100');
insert into integral(integral,product_name,product_num)values('3500','peak 篮球','500');
insert into integral(integral,product_name,product_num)values('10000','peak artest球鞋','50');
CREATE TABLE [dbo].[sale] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[num] [int] NULL ,
	[name] [nvarchar] (20) COLLATE Chinese_PRC_CI_AS NULL ,
	[price] [float] NULL ,
	[num_day] [int] NULL ,
	[cardid] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO
alter table vipcard add shop_integral int null default 0;
update vipcard set shop_integral=0;

