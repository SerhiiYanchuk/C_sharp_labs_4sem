CREATE DATABASE TradingBusiness;
GO

USE TradingBusiness;

GO

CREATE TABLE cProducts
(
	ProductId INT IDENTITY,
	ProductName NVARCHAR(50) UNIQUE NOT NULL,
	ProductPrice MONEY NOT NULL
);

ALTER TABLE cProducts
ADD CONSTRAINT PK_cProducts_ProductId PRIMARY KEY(ProductId);

GO

CREATE TABLE cStores 
(
	StoreId INT IDENTITY,
	StoreNumber INT NOT NULL,
	StoreAddress NVARCHAR(50) UNIQUE NOT NULL,
	Product1 INT NULL DEFAULT NULL,
	Product2 INT NULL DEFAULT NULL,
	Product3 INT NULL DEFAULT NULL,
	Product4 INT NULL DEFAULT NULL,
	Product5 INT NULL DEFAULT NULL,
	Product6 INT NULL DEFAULT NULL,
	Product7 INT NULL DEFAULT NULL,
	Product8 INT NULL DEFAULT NULL,
	Product9 INT NULL DEFAULT NULL,
	Product10 INT NULL DEFAULT NULL
);


ALTER TABLE cStores
ADD CONSTRAINT PK_cStores_StoreId PRIMARY KEY(StoreId),
-- рубашка
	CONSTRAINT FK_Product1_To_cProducts FOREIGN KEY(Product1) REFERENCES cProducts(ProductId),
-- костюм	
	CONSTRAINT FK_Product2_To_cProducts FOREIGN KEY(Product2) REFERENCES cProducts(ProductId),
-- брюки	
	CONSTRAINT FK_Product3_To_cProducts FOREIGN KEY(Product3) REFERENCES cProducts(ProductId),
-- джинсы	
	CONSTRAINT FK_Product4_To_cProducts FOREIGN KEY(Product4) REFERENCES cProducts(ProductId),
-- кофта
	CONSTRAINT FK_Product5_To_cProducts FOREIGN KEY(Product5) REFERENCES cProducts(ProductId),
-- ститер
	CONSTRAINT FK_Product6_To_cProducts FOREIGN KEY(Product6) REFERENCES cProducts(ProductId),
-- толстовка
	CONSTRAINT FK_Product7_To_cProducts FOREIGN KEY(Product7) REFERENCES cProducts(ProductId),
-- футболка
	CONSTRAINT FK_Product8_To_cProducts FOREIGN KEY(Product8) REFERENCES cProducts(ProductId),
-- шорты
	CONSTRAINT FK_Product9_To_cProducts FOREIGN KEY(Product9) REFERENCES cProducts(ProductId),
-- куртка
	CONSTRAINT FK_Product10_To_cProducts FOREIGN KEY(Product10) REFERENCES cProducts(ProductId);

GO

CREATE TABLE vSelling
(
	SellingId INT IDENTITY,
	ProductId INT NOT NULL,
	StoreId INT NOT NULL,
	Quantity INT NOT NULL DEFAULT 0
);


ALTER TABLE vSelling
ADD CONSTRAINT PK_vSelling_SellingId PRIMARY KEY(SellingId),
	CONSTRAINT FK_vSelling_To_cProducts FOREIGN KEY(ProductId) REFERENCES cProducts(ProductId),
	CONSTRAINT FK_vSelling_To_cStores FOREIGN KEY(StoreId) REFERENCES cStores(StoreId);

