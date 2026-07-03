INSERT INTO Sales (ProductId, Quantity, TotalPrice, SaleDate)
SELECT ProductId, 2, Price * 2, '2026-06-20 10:30:00'
FROM Products
WHERE ProductName = 'Gold Taþlý Kolye'
AND NOT EXISTS (
    SELECT 1 FROM Sales 
    WHERE ProductId = Products.ProductId 
    AND SaleDate = '2026-06-20 10:30:00'
);

INSERT INTO Sales (ProductId, Quantity, TotalPrice, SaleDate)
SELECT ProductId, 3, Price * 3, '2026-06-21 14:15:00'
FROM Products
WHERE ProductName = 'Silver Minimal Yüzük'
AND NOT EXISTS (
    SELECT 1 FROM Sales 
    WHERE ProductId = Products.ProductId 
    AND SaleDate = '2026-06-21 14:15:00'
);

INSERT INTO Sales (ProductId, Quantity, TotalPrice, SaleDate)
SELECT ProductId, 1, Price * 1, '2026-06-22 16:45:00'
FROM Products
WHERE ProductName = 'Rose Bileklik'
AND NOT EXISTS (
    SELECT 1 FROM Sales 
    WHERE ProductId = Products.ProductId 
    AND SaleDate = '2026-06-22 16:45:00'
);

INSERT INTO Sales (ProductId, Quantity, TotalPrice, SaleDate)
SELECT ProductId, 2, Price * 2, '2026-06-23 12:10:00'
FROM Products
WHERE ProductName = 'Ýnci Detaylý Küpe'
AND NOT EXISTS (
    SELECT 1 FROM Sales 
    WHERE ProductId = Products.ProductId 
    AND SaleDate = '2026-06-23 12:10:00'
);