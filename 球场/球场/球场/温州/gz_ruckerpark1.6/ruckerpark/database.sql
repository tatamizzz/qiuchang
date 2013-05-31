CREATE PROCEDURE goods_stock_now AS
select distinct goods_info.goods_name as '商品名称',goods_info.goods_price as '销售价',goods_info.goods_spcification as '商品单位',goods_spcification_big as '商品规格',goods_price as '商品价格',goods_stock as '商品库存',b.goods_purchase_price as '进价' from goods_info left join goods_order as b on b.goods_huohao=goods_info.goods_huohao,goods_stock where goods_info.goods_huohao=goods_stock.goods_huohao
GO

insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('1','',' 大峡谷','','',3,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('2','',' 小峡谷','','',2,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('3','',' 脉动','','',4,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('4','',' 百事可乐','','',3,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('5','',' 可口可乐','','',3,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('6','',' 雪碧','','',3,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('7','',' 佳得乐','','',5,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('8','',' 美年达','','',3,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('9','',' 面包','','',3,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('10','',' 饼干','','',4,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('11','',' 纸巾','','',1,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('12','',' 康师傅碗面','','','5','8888','','2009-09-24 03:46:26'); 

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105540','','1',' 大峡谷','','0',0,'354','','8888','','2009-09-24 03:46:26');

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105540','','2',' 小峡谷','','0',0,'63','','8888','','2009-09-24 03:46:26');

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105540','','3',' 脉动','','0',0,'82','','8888','','2009-09-24 03:46:26'); 

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105540','','4',' 百事可乐','','0',0,'81','','8888','','2009-09-24 03:46:26');

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105540','','5',' 可口可乐','','0',0,'27','','8888','','2009-09-24 03:46:26');

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105540','','6',' 雪碧','','0',0,'83','','8888','','2009-09-24 03:46:26'); 

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105540','','7',' 佳得乐','','0',0,'4','','8888','','2009-09-24 03:46:26');

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105540','','8',' 美年达','','0',0,'17','','8888','','2009-09-24 03:46:26'); 

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105930','','9',' 面包','','0',0,'9','','8888','','2009-09-24 03:46:26'); 

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105930','','10',' 饼干','','0',0,'8','','8888','','2009-09-24 03:46:26');

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105930','','11',' 纸巾','','0',0,'23','','8888','','2009-09-24 03:46:26'); 

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105930','','12',' 康师傅碗面','','0',0,'27','','8888','','2009-09-24 03:46:26');

insert into goods_stock(goods_huohao,goods_stock)values('1','354'); 
insert into goods_stock(goods_huohao,goods_stock)values('2','63');
insert into goods_stock(goods_huohao,goods_stock)values('3','82');
insert into goods_stock(goods_huohao,goods_stock)values('4','81'); 
insert into goods_stock(goods_huohao,goods_stock)values('5','27');
insert into goods_stock(goods_huohao,goods_stock)values('6','83'); 
insert into goods_stock(goods_huohao,goods_stock)values('7','4'); 
insert into goods_stock(goods_huohao,goods_stock)values('8','17'); 
insert into goods_stock(goods_huohao,goods_stock)values('9','9'); 
insert into goods_stock(goods_huohao,goods_stock)values('10','8'); 
insert into goods_stock(goods_huohao,goods_stock)values('11','23'); 
insert into goods_stock(goods_huohao,goods_stock)values('12','27'); 
