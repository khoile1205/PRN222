-- Tạo cơ sở dữ liệu Caffeine
CREATE DATABASE Caffeine;

-- Sử dụng cơ sở dữ liệu Caffeine
USE Caffeine;

-- Tạo bảng Staff (Nhân viên)
CREATE TABLE Staff (
    StaffID INT PRIMARY KEY,
    Name VARCHAR(50),
    Position VARCHAR(50),
    Salary DECIMAL,
    Shift VARCHAR(50),
    Profile TEXT,
    Login VARCHAR(50)
);

-- Tạo bảng Beverage (Đồ uống)
CREATE TABLE Beverage (
    BeverageID INT PRIMARY KEY,
    Name VARCHAR(50),
    Price DECIMAL,
    Description TEXT,
    Inventory INT
);

-- Tạo bảng CafeTable (Bàn)
CREATE TABLE CafeTable (
    TableID INT PRIMARY KEY,
    Status VARCHAR(50)
);

-- Tạo bảng Bill (Hóa đơn)
CREATE TABLE Bill (
    BillID INT PRIMARY KEY,
    StaffID INT,
    TableID INT,
    DateTime DATETIME,
    TotalAmount DECIMAL,
    FOREIGN KEY (StaffID) REFERENCES Staff(StaffID),
    FOREIGN KEY (TableID) REFERENCES CafeTable(TableID)
);

-- Tạo bảng Promotion (Khuyến mãi)
CREATE TABLE Promotion (
    PromotionID INT PRIMARY KEY,
    Description TEXT,
    Discount DECIMAL
);

-- Tạo bảng Inventory (Tồn kho)
CREATE TABLE Inventory (
    InventoryID INT PRIMARY KEY,
    BeverageID INT,
    Quantity INT,
    FOREIGN KEY (BeverageID) REFERENCES Beverage(BeverageID)
);

-- Tạo bảng Revenue (Doanh thu)
CREATE TABLE Revenue (
    RevenueID INT PRIMARY KEY,
    Date DATE,
    TotalRevenue DECIMAL
);

-- Chèn một số dữ liệu mẫu vào bảng Staff
INSERT INTO Staff (StaffID, Name, Position, Salary, Shift, Profile, Login)
VALUES
(1, 'Nguyen Van A', 'Manager', 20000000, 'Morning', 'Profile_A', 'Login_A'),
(2, 'Tran Thi B', 'Waiter', 8000000, 'Afternoon', 'Profile_B', 'Login_B'),
(3, 'Le Thi C', 'Barista', 7000000, 'Morning', 'Profile_C', 'Login_C'),
(4, 'Pham Van D', 'Waiter', 8000000, 'Evening', 'Profile_D', 'Login_D'),
(5, 'Do Thi E', 'Cashier', 9000000, 'Afternoon', 'Profile_E', 'Login_E'),
(6, 'Nguyen Thi F', 'Manager', 20000000, 'Morning', 'Profile_F', 'Login_F'),
(7, 'Tran Van G', 'Waiter', 8000000, 'Afternoon', 'Profile_G', 'Login_G'),
(8, 'Hoang Thi H', 'Barista', 7000000, 'Morning', 'Profile_H', 'Login_H'),
(9, 'Nguyen Van I', 'Cashier', 9000000, 'Evening', 'Profile_I', 'Login_I'),
(10, 'Pham Thi J', 'Manager', 20000000, 'Morning', 'Profile_J', 'Login_J'),
(11, 'Le Van K', 'Waiter', 8000000, 'Afternoon', 'Profile_K', 'Login_K'),
(12, 'Tran Thi L', 'Barista', 7000000, 'Morning', 'Profile_L', 'Login_L'),
(13, 'Nguyen Van M', 'Cashier', 9000000, 'Afternoon', 'Profile_M', 'Login_M'),
(14, 'Do Van N', 'Manager', 20000000, 'Morning', 'Profile_N', 'Login_N'),
(15, 'Hoang Thi O', 'Waiter', 8000000, 'Evening', 'Profile_O', 'Login_O'),
(16, 'Le Thi P', 'Barista', 7000000, 'Morning', 'Profile_P', 'Login_P'),
(17, 'Nguyen Van Q', 'Cashier', 9000000, 'Afternoon', 'Profile_Q', 'Login_Q'),
(18, 'Pham Thi R', 'Manager', 20000000, 'Morning', 'Profile_R', 'Login_R'),
(19, 'Tran Van S', 'Waiter', 8000000, 'Afternoon', 'Profile_S', 'Login_S'),
(20, 'Do Thi T', 'Barista', 7000000, 'Morning', 'Profile_T', 'Login_T');


-- Chèn một số dữ liệu mẫu vào bảng Beverage
INSERT INTO Beverage (BeverageID, Name, Price, Description, Inventory)
VALUES
(1, 'Coffee', 20000, 'Delicious coffee', 100),
(2, 'Tea', 15000, 'Refreshing tea', 50),
(3, 'Latte', 25000, 'Smooth and creamy', 80),
(4, 'Espresso', 22000, 'Strong and rich', 60),
(5, 'Cappuccino', 27000, 'Frothy and warm', 75),
(6, 'Mocha', 30000, 'Chocolate and coffee', 90),
(7, 'Americano', 18000, 'Classic black coffee', 100),
(8, 'Green Tea', 20000, 'Refreshing and healthy', 50),
(9, 'Black Tea', 15000, 'Bold and robust', 40),
(10, 'Herbal Tea', 17000, 'Calming and aromatic', 55),
(11, 'Fruit Juice', 25000, 'Fresh and fruity', 70),
(12, 'Smoothie', 30000, 'Thick and nutritious', 30),
(13, 'Milkshake', 28000, 'Creamy and sweet', 45),
(14, 'Iced Coffee', 20000, 'Chilled coffee drink', 95),
(15, 'Lemonade', 18000, 'Tart and refreshing', 85),
(16, 'Matcha Latte', 32000, 'Green tea latte', 65),
(17, 'Hot Chocolate', 25000, 'Rich and warming', 50),
(18, 'Flat White', 27000, 'Silky and smooth', 60),
(19, 'Chai Latte', 29000, 'Spiced tea latte', 75),
(20, 'Iced Tea', 15000, 'Cool and refreshing', 80);


-- Chèn một số dữ liệu mẫu vào bảng CafeTable
INSERT INTO CafeTable (TableID, Status)
VALUES
(1, 'Available'),
(2, 'Occupied'),
(3, 'Available'),
(4, 'Occupied'),
(5, 'Reserved'),
(6, 'Available'),
(7, 'Occupied'),
(8, 'Reserved'),
(9, 'Available'),
(10, 'Occupied'),
(11, 'Reserved'),
(12, 'Available'),
(13, 'Occupied'),
(14, 'Reserved'),
(15, 'Available'),
(16, 'Occupied'),
(17, 'Reserved'),
(18, 'Available'),
(19, 'Occupied'),
(20, 'Reserved');

-- Chèn một số dữ liệu mẫu vào bảng Bill
INSERT INTO Bill (BillID, StaffID, TableID, DateTime, TotalAmount)
VALUES
(1, 1, 1, '2023-02-23 09:00:00', 35000),
(2, 2, 2, '2023-02-23 10:00:00', 45000),
(3, 3, 3, '2023-02-23 11:00:00', 60000),
(4, 4, 4, '2023-02-23 12:00:00', 75000),
(5, 5, 5, '2023-02-23 13:00:00', 50000),
(6, 6, 6, '2023-02-23 14:00:00', 35000),
(7, 7, 7, '2023-02-23 15:00:00', 80000),
(8, 8, 8, '2023-02-23 16:00:00', 90000),
(9, 9, 9, '2023-02-23 17:00:00', 65000),
(10, 10, 10, '2023-02-23 18:00:00', 70000),
(11, 11, 11, '2023-02-23 19:00:00', 45000),
(12, 12, 12, '2023-02-23 20:00:00', 60000),
(13, 13, 13, '2023-02-23 21:00:00', 50000),
(14, 14, 14, '2023-02-23 22:00:00', 75000),
(15, 15, 15, '2023-02-23 23:00:00', 80000),
(16, 16, 16, '2023-02-24 09:00:00', 35000),
(17, 17, 17, '2023-02-24 10:00:00', 45000),
(18, 18, 18, '2023-02-24 11:00:00', 90000),
(19, 19, 19, '2023-02-24 12:00:00', 65000),
(20, 20, 20, '2023-02-24 13:00:00', 70000);

-- Chèn một số dữ liệu mẫu vào bảng Promotion
INSERT INTO Promotion (PromotionID, Description, Discount)
VALUES
(1, 'Summer Sale', 10),
(2, 'Winter Sale', 15),
(3, 'Holiday Discount', 20),
(4, 'Loyalty Program', 25),
(5, 'Flash Sale', 30),
(6, 'Special Offer', 10),
(7, 'Weekend Deal', 20),
(8, 'New Year Promo', 25),
(9, 'Spring Sale', 15),
(10, 'Summer Promo', 10),
(11, 'Back to School', 20),
(12, 'Halloween Special', 15),
(13, 'Black Friday', 30),
(14, 'Cyber Monday', 25),
(15, 'Valentine’s Day', 20),
(16, 'St. Patrick’s Day', 10),
(17, 'Easter Promo', 15),
(18, 'Mother’s Day', 20),
(19, 'Father’s Day', 15),
(20, 'Anniversary Special', 25);

-- Chèn một số dữ liệu mẫu vào bảng Inventory
INSERT INTO Inventory (InventoryID, BeverageID, Quantity)
VALUES
(1, 1, 100),
(2, 2, 50),
(3, 3, 80),
(4, 4, 60),
(5, 5, 75),
(6, 6, 90),
(7, 7, 100),
(8, 8, 50),
(9, 9, 40),
(10, 10, 55),
(11, 11, 70),
(12, 12, 30),
(13, 13, 45),
(14, 14, 95),
(15, 15, 85),
(16, 16, 65),
(17, 17, 50),
(18, 18, 60),
(19, 19, 75),
(20, 20, 80);

-- Chèn một số dữ liệu mẫu vào bảng Revenue
INSERT INTO Revenue (RevenueID, Date, TotalRevenue)
VALUES
(1, '2023-02-23', 35000),
(2, '2023-02-24', 45000),
(3, '2023-02-25', 60000),
(4, '2023-02-26', 75000),
(5, '2023-02-27', 50000),
(6, '2023-02-28', 35000),
(7, '2023-03-01', 80000),
(8, '2023-03-02', 90000),
(9, '2023-03-03', 65000),
(10, '2023-03-04', 70000),
(11, '2023-03-05', 45000),
(12, '2023-03-06', 60000),
(13, '2023-03-07', 50000),
(14, '2023-03-08', 75000),
(15, '2023-03-09', 80000),
(16, '2023-03-10', 35000),
(17, '2023-03-11', 45000),
(18, '2023-03-12', 90000),
(19, '2023-03-13', 65000),
(20, '2023-03-14', 70000);
