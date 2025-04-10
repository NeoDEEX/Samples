﻿if exists (select 1
            from  sysobjects
           where  id = object_id('Product')
            and   type = 'U')
   drop table Product
go

create table Product (
   Id                   int                  identity,
   ProductName          nvarchar(50)         not null,
   SupplierId           int                  not null,
   UnitPrice            decimal(12,2)        null default 0,
   Package              nvarchar(30)         null,
   IsDiscontinued       bit                  not null default 0,
   ModifiedBy			nvarchar(32)		 null,
   ModifiedAt			datetime			 null default sysdatetime()
   constraint PK_PRODUCT primary key (Id)
)
go

SET IDENTITY_INSERT Product ON
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(1,'Chai',1,18.00,'10 boxes x 20 bags',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(2,'Chang',1,19.00,'24 - 12 oz bottles',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(3,'Aniseed Syrup',1,10.00,'12 - 550 ml bottles',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(4,'Chef Anton''s Cajun Seasoning',2,22.00,'48 - 6 oz jars',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(5,'Chef Anton''s Gumbo Mix',2,21.35,'36 boxes',1)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(6,'Grandma''s Boysenberry Spread',3,25.00,'12 - 8 oz jars',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(7,'Uncle Bob''s Organic Dried Pears',3,30.00,'12 - 1 lb pkgs.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(8,'Northwoods Cranberry Sauce',3,40.00,'12 - 12 oz jars',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(9,'Mishi Kobe Niku',4,97.00,'18 - 500 g pkgs.',1)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(10,'Ikura',4,31.00,'12 - 200 ml jars',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(11,'Queso Cabrales',5,21.00,'1 kg pkg.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(12,'Queso Manchego La Pastora',5,38.00,'10 - 500 g pkgs.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(13,'Konbu',6,6.00,'2 kg box',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(14,'Tofu',6,23.25,'40 - 100 g pkgs.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(15,'Genen Shouyu',6,15.50,'24 - 250 ml bottles',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(16,'Pavlova',7,17.45,'32 - 500 g boxes',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(17,'Alice Mutton',7,39.00,'20 - 1 kg tins',1)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(18,'Carnarvon Tigers',7,62.50,'16 kg pkg.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(19,'Teatime Chocolate Biscuits',8,9.20,'10 boxes x 12 pieces',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(20,'Sir Rodney''s Marmalade',8,81.00,'30 gift boxes',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(21,'Sir Rodney''s Scones',8,10.00,'24 pkgs. x 4 pieces',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(22,'Gustaf''s Knäckebröd',9,21.00,'24 - 500 g pkgs.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(23,'Tunnbröd',9,9.00,'12 - 250 g pkgs.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(24,'Guaraná Fantástica',10,4.50,'12 - 355 ml cans',1)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(25,'NuNuCa Nuß-Nougat-Creme',11,14.00,'20 - 450 g glasses',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(26,'Gumbär Gummibärchen',11,31.23,'100 - 250 g bags',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(27,'Schoggi Schokolade',11,43.90,'100 - 100 g pieces',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(28,'Rössle Sauerkraut',12,45.60,'25 - 825 g cans',1)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(29,'Thüringer Rostbratwurst',12,123.79,'50 bags x 30 sausgs.',1)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(30,'Nord-Ost Matjeshering',13,25.89,'10 - 200 g glasses',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(31,'Gorgonzola Telino',14,12.50,'12 - 100 g pkgs',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(32,'Mascarpone Fabioli',14,32.00,'24 - 200 g pkgs.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(33,'Geitost',15,2.50,'500 g',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(34,'Sasquatch Ale',16,14.00,'24 - 12 oz bottles',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(35,'Steeleye Stout',16,18.00,'24 - 12 oz bottles',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(36,'Inlagd Sill',17,19.00,'24 - 250 g  jars',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(37,'Gravad lax',17,26.00,'12 - 500 g pkgs.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(38,'Côte de Blaye',18,263.50,'12 - 75 cl bottles',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(39,'Chartreuse verte',18,18.00,'750 cc per bottle',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(40,'Boston Crab Meat',19,18.40,'24 - 4 oz tins',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(41,'Jack''s New England Clam Chowder',19,9.65,'12 - 12 oz cans',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(42,'Singaporean Hokkien Fried Mee',20,14.00,'32 - 1 kg pkgs.',1)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(43,'Ipoh Coffee',20,46.00,'16 - 500 g tins',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(44,'Gula Malacca',20,19.45,'20 - 2 kg bags',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(45,'Rogede sild',21,9.50,'1k pkg.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(46,'Spegesild',21,12.00,'4 - 450 g glasses',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(47,'Zaanse koeken',22,9.50,'10 - 4 oz boxes',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(48,'Chocolade',22,12.75,'10 pkgs.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(49,'Maxilaku',23,20.00,'24 - 50 g pkgs.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(50,'Valkoinen suklaa',23,16.25,'12 - 100 g bars',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(51,'Manjimup Dried Apples',24,53.00,'50 - 300 g pkgs.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(52,'Filo Mix',24,7.00,'16 - 2 kg boxes',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(53,'Perth Pasties',24,32.80,'48 pieces',1)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(54,'Tourtière',25,7.45,'16 pies',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(55,'Pâté chinois',25,24.00,'24 boxes x 2 pies',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(56,'Gnocchi di nonna Alice',26,38.00,'24 - 250 g pkgs.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(57,'Ravioli Angelo',26,19.50,'24 - 250 g pkgs.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(58,'Escargots de Bourgogne',27,13.25,'24 pieces',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(59,'Raclette Courdavault',28,55.00,'5 kg pkg.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(60,'Camembert Pierrot',28,34.00,'15 - 300 g rounds',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(61,'Sirop d''érable',29,28.50,'24 - 500 ml bottles',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(62,'Tarte au sucre',29,49.30,'48 pies',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(63,'Vegie-spread',7,43.90,'15 - 625 g jars',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(64,'Wimmers gute Semmelknödel',12,33.25,'20 bags x 4 pieces',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(65,'Louisiana Fiery Hot Pepper Sauce',2,21.05,'32 - 8 oz bottles',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(66,'Louisiana Hot Spiced Okra',2,17.00,'24 - 8 oz jars',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(67,'Laughing Lumberjack Lager',16,14.00,'24 - 12 oz bottles',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(68,'Scottish Longbreads',8,12.50,'10 boxes x 8 pieces',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(69,'Gudbrandsdalsost',15,36.00,'10 kg pkg.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(70,'Outback Lager',7,15.00,'24 - 355 ml bottles',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(71,'Flotemysost',15,21.50,'10 - 500 g pkgs.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(72,'Mozzarella di Giovanni',14,34.80,'24 - 200 g pkgs.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(73,'Röd Kaviar',17,15.00,'24 - 150 g jars',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(74,'Longlife Tofu',4,10.00,'5 kg pkg.',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(75,'Rhönbräu Klosterbier',12,7.75,'24 - 0.5 l bottles',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(76,'Lakkalikööri',23,18.00,'500 ml',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(77,'Original Frankfurter grüne Soße',12,13.00,'12 boxes',0)
INSERT INTO [Product] ([Id],[ProductName],[SupplierId],[UnitPrice],[Package],[IsDiscontinued])VALUES(78,'Stroopwafels',22,9.75,'24 pieces',0)
SET IDENTITY_INSERT Product OFF
GO

UPDATE Product SET ModifiedBy = 'InitialSetup'
GO

create table ServerLog (
   Id		int                 identity,
   Host		nvarchar(50)        not null,
   IP		nvarchar(50)		not null,
   Process	nvarchar(50)		not null,
   Message	nvarchar(2000),
   constraint PK_SERVERLOG primary key (Id)
)
go