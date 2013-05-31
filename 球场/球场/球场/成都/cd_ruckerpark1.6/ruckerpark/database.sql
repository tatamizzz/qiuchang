CREATE PROCEDURE goods_stock_now AS
select distinct goods_info.goods_name as '��Ʒ����',goods_info.goods_price as '���ۼ�',goods_info.goods_spcification as '��Ʒ��λ',goods_spcification_big as '��Ʒ���',goods_price as '��Ʒ�۸�',goods_stock as '��Ʒ���',b.goods_purchase_price as '����' from goods_info left join goods_order as b on b.goods_huohao=goods_info.goods_huohao,goods_stock where goods_info.goods_huohao=goods_stock.goods_huohao
GO

insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('1','',' ��Ͽ��','','',3,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('2','',' СϿ��','','',2,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('3','',' ����','','',4,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('4','',' ���¿���','','',3,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('5','',' �ɿڿ���','','',3,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('6','',' ѩ��','','',3,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('7','',' �ѵ���','','',5,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('8','',' �����','','',3,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('9','',' ���','','',3,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('10','',' ����','','',4,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('11','',' ֽ��','','',1,'8888','','2009-09-24 03:46:26'); insert into goods_info(goods_huohao,goods_tiaoma,goods_name,goods_spcification,goods_spcification_big,goods_price,reporter,modify_person,adddate)values('12','',' ��ʦ������','','','5','8888','','2009-09-24 03:46:26'); 

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105540','','1',' ��Ͽ��','','0',0,'354','','8888','','2009-09-24 03:46:26');

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105540','','2',' СϿ��','','0',0,'63','','8888','','2009-09-24 03:46:26');

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105540','','3',' ����','','0',0,'82','','8888','','2009-09-24 03:46:26'); 

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105540','','4',' ���¿���','','0',0,'81','','8888','','2009-09-24 03:46:26');

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105540','','5',' �ɿڿ���','','0',0,'27','','8888','','2009-09-24 03:46:26');

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105540','','6',' ѩ��','','0',0,'83','','8888','','2009-09-24 03:46:26'); 

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105540','','7',' �ѵ���','','0',0,'4','','8888','','2009-09-24 03:46:26');

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105540','','8',' �����','','0',0,'17','','8888','','2009-09-24 03:46:26'); 

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105930','','9',' ���','','0',0,'9','','8888','','2009-09-24 03:46:26'); 

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105930','','10',' ����','','0',0,'8','','8888','','2009-09-24 03:46:26');

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105930','','11',' ֽ��','','0',0,'23','','8888','','2009-09-24 03:46:26'); 

insert into goods_order(orderid,goods_tiaoma,goods_huohao,goods_name,goods_spcification,goods_stock_now,goods_purchase_price,goods_number_order,goods_number_storage,reporter,check_staff,adddatetime)values('20090924105930','','12',' ��ʦ������','','0',0,'27','','8888','','2009-09-24 03:46:26');

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
