USE TradingBusiness;

GO

-- 1. Вывести все товары

SELECT ProductName FROM cProducts;

-- 2. Вывести торговые точки, где в продаются >= 7 товаров

SELECT StoreNumber, StoreAddress FROM cStores
	WHERE Product7 IS NOT NULL;

-- 3. Вывести количество торговых точек

SELECT COUNT(*) AS QuantityStores FROM cStores;

-- 4. Вывести торговые точки и название товаров, которые продаются

SELECT cStores.StoreAddress, P1.ProductName, P2.ProductName, P3.ProductName, P4.ProductName, P5.ProductName, 
	   P6.ProductName, P7.ProductName, P8.ProductName, P9.ProductName, P10.ProductName 
	   FROM cStores
LEFT JOIN cProducts AS P1 ON cStores.Product1 = P1.ProductId
LEFT JOIN cProducts AS P2 ON cStores.Product2 = P2.ProductId
LEFT JOIN cProducts AS P3 ON cStores.Product3 = P3.ProductId
LEFT JOIN cProducts AS P4 ON cStores.Product4 = P4.ProductId
LEFT JOIN cProducts AS P5 ON cStores.Product5 = P5.ProductId
LEFT JOIN cProducts AS P6 ON cStores.Product6 = P6.ProductId
LEFT JOIN cProducts AS P7 ON cStores.Product7 = P7.ProductId
LEFT JOIN cProducts AS P8 ON cStores.Product8 = P8.ProductId
LEFT JOIN cProducts AS P9 ON cStores.Product9 = P9.ProductId
LEFT JOIN cProducts AS P10 ON cStores.Product10 = P10.ProductId;

-- 5. Вывести самые дорогие товары 

SELECT ProductName, ProductPrice FROM cProducts
	WHERE ProductPrice = (SELECT MAX(ProductPrice) FROM cProducts);

-- 6. Вывести количество проданых товаров по каждому виду в общем

SELECT cProducts.ProductName, SUM(vSelling.Quantity) AS Quantity
FROM vSelling
INNER JOIN cProducts ON vSelling.ProductId = cProducts.ProductId
GROUP BY cProducts.ProductName;

-- 7. Вывести самые популярные товары

SELECT cProducts.ProductName, SUM(vSelling.Quantity) AS Quantity, 
FROM vSelling
INNER JOIN cProducts ON vSelling.ProductId = cProducts.ProductId
GROUP BY cProducts.ProductName
HAVING SUM(vSelling.Quantity) = 
	(SELECT MAX(S) FROM (SELECT SUM(vSelling.Quantity) AS S FROM vSelling GROUP BY vSelling.ProductId) AS M);

-- 8. Посчитать заработок каждой торговой точки

SELECT cStores.StoreAddress, SUM(vSelling.Quantity*cProducts.ProductPrice) AS '$' FROM vSelling
INNER JOIN cProducts ON vSelling.ProductId = cProducts.ProductId
INNER JOIN cStores ON vSelling.StoreId = cStores.StoreId
GROUP BY cStores.StoreAddress
ORDER BY '$' DESC;

-- 9. Выбрать торг точки, где можно купить рубашку, костюм, брюки

SELECT StoreAddress FROM cStores
	WHERE 1 IN (Product1, Product2, Product3, Product4, Product5, Product6, Product7, Product8, Product9, Product10) AND
		  2 IN (Product1, Product2, Product3, Product4, Product5, Product6, Product7, Product8, Product9, Product10) AND
		  3 IN (Product1, Product2, Product3, Product4, Product5, Product6, Product7, Product8, Product9, Product10);

-- 10. Вывести торг точки и количество проданого товара

SELECT cStores.StoreAddress, ISNULL(V1.Quantity, 0), ISNULL(V2.Quantity, 0), ISNULL(V3.Quantity, 0), ISNULL(V4.Quantity, 0), ISNULL(V5.Quantity, 0), 
	   ISNULL(V6.Quantity, 0), ISNULL(V7.Quantity, 0), ISNULL(V8.Quantity, 0), ISNULL(V9.Quantity, 0), ISNULL(V10.Quantity, 0) 
	   FROM cStores
LEFT JOIN vSelling AS V1 ON cStores.Product1 = V1.ProductId AND cStores.StoreId = V1.StoreId
LEFT JOIN vSelling AS V2 ON cStores.Product2 = V2.ProductId AND cStores.StoreId = V2.StoreId
LEFT JOIN vSelling AS V3 ON cStores.Product3 = V3.ProductId AND cStores.StoreId = V3.StoreId
LEFT JOIN vSelling AS V4 ON cStores.Product4 = V4.ProductId AND cStores.StoreId = V4.StoreId
LEFT JOIN vSelling AS V5 ON cStores.Product5 = V5.ProductId AND cStores.StoreId = V5.StoreId
LEFT JOIN vSelling AS V6 ON cStores.Product6 = V6.ProductId AND cStores.StoreId = V6.StoreId
LEFT JOIN vSelling AS V7 ON cStores.Product7 = V7.ProductId AND cStores.StoreId = V7.StoreId
LEFT JOIN vSelling AS V8 ON cStores.Product8 = V8.ProductId AND cStores.StoreId = V8.StoreId
LEFT JOIN vSelling AS V9 ON cStores.Product9 = V9.ProductId AND cStores.StoreId = V9.StoreId
LEFT JOIN vSelling AS V10 ON cStores.Product10 = V10.ProductId AND cStores.StoreId = V10.StoreId;


GO

-- INSERT \ UPDATE  

CREATE PROCEDURE sp_UpdateSelling
    @produnctName NVARCHAR(50),
    @storeNumber INT,
    @quantity INT,
	@result NVARCHAR(50) OUTPUT
AS
BEGIN
	DECLARE @p INT;
	SET @p = (SELECT ProductId FROM cProducts WHERE ProductName = @produnctName);

	IF @p IS NULL
	BEGIN
		SET @result = 'Такого товара нет';
		RETURN;
	END;

	DECLARE @s INT;
	SET @s = (SELECT StoreId FROM cStores WHERE StoreNumber = @storeNumber);

	IF @s IS NULL
	BEGIN
		SET @result = 'Такой торговой точки нет';
		RETURN;
	END;

	IF EXISTS (SELECT * FROM vSelling WHERE @p = ProductId AND @s = StoreId) 
	BEGIN
		UPDATE vSelling
		SET Quantity = Quantity + @quantity
		WHERE ProductId = @p AND StoreId = @s;
		SET @result = 'Обновлено';
	END;
	ELSE
	BEGIN
		INSERT INTO vSelling (ProductId, StoreId, Quantity)
		VALUES
		(@p, @s, @quantity);
		SET @result = 'Добавлено';
	END;
END;

DECLARE @ppp NVARCHAR(50);
EXEC sp_UpdateSelling 'Рубашка', 100, 5, @ppp OUTPUT;
PRINT @ppp;

-- DELETE 

CREATE PROCEDURE sp_DeleteSelling
    @produnctName NVARCHAR(50),
    @storeNumber INT,
	@result NVARCHAR(50) OUTPUT
AS
BEGIN
	DECLARE @p INT;
	SET @p = (SELECT ProductId FROM cProducts WHERE ProductName = @produnctName);

	IF @p IS NULL
	BEGIN
		SET @result = 'Такого товара нет';
		RETURN;
	END;

	DECLARE @s INT;
	SET @s = (SELECT StoreId FROM cStores WHERE StoreNumber = @storeNumber);

	IF @s IS NULL
	BEGIN
		SET @result = 'Такой торговой точки нет';
		RETURN;
	END;

	IF EXISTS (SELECT * FROM vSelling WHERE @p = ProductId AND @s = StoreId) 
	BEGIN
		DELETE vSelling
		WHERE ProductId = @p AND StoreId = @s;
		SET @result = 'Удалено';
	END;
	ELSE
	BEGIN
		SET @result = 'Данных про продажи нет';
	END;
END;