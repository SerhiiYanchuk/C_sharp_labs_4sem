USE TradingBusiness;

GO

INSERT INTO cProducts (ProductName, ProductPrice)
VALUES
('�������', 30),
('������', 100),
('�����', 50),
('������', 35),
('�����', 20),
('������', 32),
('���������', 34),
('��������', 15),
('�����', 20),
('������', 70);

GO

INSERT INTO cStores (StoreNumber, StoreAddress, Product1, Product2, Product3, Product4, Product5, Product6, Product7, Product8, Product9, Product10 )
VALUES
(100, '��������� ������ ��.5', 1, 2, 3, 4, 5, 6, 7, 8, 9, 10),
(200, '������������������ ��.3', 1, 2, 4, 5, 7, 9, 10, NULL, NULL, NULL),
(300, '����������� ���.2', 3, 4, 7, 8, 10, NULL, NULL, NULL, NULL, NULL),
(400, '���� �������� ��.4', 1, 2, 3, 4, 8, 9, NULL, NULL, NULL, NULL),
(500, '��������� ��.6', 4, 5, 6, 7, 8, 9, 10, NULL, NULL, NULL);

GO

INSERT INTO vSelling (ProductId, StoreId, Quantity)
VALUES
(1, 1, 5), 
(2, 1, 13), 
(3, 1, 0), 
(4, 1, 3), 
(5, 1, 12), 
(6, 1, 18), 
(7, 1, 20), 
(8, 1, 3), 
(9, 1, 1), 
(10, 1, 9),
(1, 2, 27), 
(2, 2, 6),
(4, 2, 10),
(5, 2, 9),
(6, 2, 13),
(9, 2, 10),
(10, 2, 6),
(3, 3, 5),
(4, 3, 8),
(7, 3, 16),
(8, 3, 19),
(10, 3, 12),
(1, 4, 24),
(2, 4, 7),
(3, 4, 17),
(4, 4, 14),
(8, 4, 11),
(9, 4, 10),
(4, 5, 21),
(5, 5, 1),
(6, 5, 35),
(7, 5, 14),
(8, 5, 5),
(9, 5, 8),
(10, 5, 12);
