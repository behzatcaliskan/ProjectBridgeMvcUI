INSERT INTO Categories (CategoryName, Description)
SELECT 'Kolye', 'Gold, silver ve taşlı kolye modelleri'
WHERE NOT EXISTS (SELECT 1 FROM Categories WHERE CategoryName = 'Kolye');

INSERT INTO Categories (CategoryName, Description)
SELECT 'Yüzük', 'Minimal, taşlı ve günlük yüzük modelleri'
WHERE NOT EXISTS (SELECT 1 FROM Categories WHERE CategoryName = 'Yüzük');

INSERT INTO Categories (CategoryName, Description)
SELECT 'Bileklik', 'Çelik, rose ve zincir bileklik modelleri'
WHERE NOT EXISTS (SELECT 1 FROM Categories WHERE CategoryName = 'Bileklik');

INSERT INTO Categories (CategoryName, Description)
SELECT 'Küpe', 'İnci, halka ve minimal küpe modelleri'
WHERE NOT EXISTS (SELECT 1 FROM Categories WHERE CategoryName = 'Küpe');


INSERT INTO Suppliers (SupplierName, Phone, City)
SELECT 'Parıltı Takı Tedarik', '0532 111 22 33', 'İstanbul'
WHERE NOT EXISTS (SELECT 1 FROM Suppliers WHERE SupplierName = 'Parıltı Takı Tedarik');

INSERT INTO Suppliers (SupplierName, Phone, City)
SELECT 'GoldLine Aksesuar', '0533 222 44 55', 'İzmir'
WHERE NOT EXISTS (SELECT 1 FROM Suppliers WHERE SupplierName = 'GoldLine Aksesuar');

INSERT INTO Suppliers (SupplierName, Phone, City)
SELECT 'Rose Bijuteri', '0534 333 55 66', 'Ankara'
WHERE NOT EXISTS (SELECT 1 FROM Suppliers WHERE SupplierName = 'Rose Bijuteri');


INSERT INTO Customers (FullName, Phone, Email)
SELECT 'Ayşe Yılmaz', '0555 111 22 33', 'ayse@mail.com'
WHERE NOT EXISTS (SELECT 1 FROM Customers WHERE Email = 'ayse@mail.com');

INSERT INTO Customers (FullName, Phone, Email)
SELECT 'Elif Demir', '0555 222 33 44', 'elif@mail.com'
WHERE NOT EXISTS (SELECT 1 FROM Customers WHERE Email = 'elif@mail.com');

INSERT INTO Customers (FullName, Phone, Email)
SELECT 'Merve Kaya', '0555 333 44 55', 'merve@mail.com'
WHERE NOT EXISTS (SELECT 1 FROM Customers WHERE Email = 'merve@mail.com');
