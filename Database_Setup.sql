-- Create the database if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Store_X_DB')
BEGIN
    CREATE DATABASE Store_X_DB;
END
GO

-- Use the newly created database
USE Store_X_DB;
GO

-- =============================================
-- === TABLE CREATION ==========================
-- =============================================

-- Categories Table
CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL UNIQUE
);
GO

-- Suppliers Table
CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    SupplierName NVARCHAR(150) NOT NULL UNIQUE,
    Address NVARCHAR(255),
    Phone VARCHAR(20),
    CooperationDate DATE
);
GO

-- Products Table
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(150) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL CHECK (Price >= 0),
    StockQuantity INT NOT NULL DEFAULT 0 CHECK (StockQuantity >= 0),
    DateAdded DATETIME NOT NULL DEFAULT GETDATE(),
    CategoryID INT,
    SupplierID INT,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID)
);
GO

-- Employees Table
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255),
    Phone VARCHAR(20),
    StartDate DATE NOT NULL,
    Salary DECIMAL(18, 2) NOT NULL CHECK (Salary >= 0)
);
GO

-- Customers Table
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    CustomerName NVARCHAR(100) NOT NULL,
    Phone VARCHAR(20),
    Address NVARCHAR(255)
);
GO

-- Roles Table
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName VARCHAR(50) NOT NULL UNIQUE
);
GO

-- Accounts Table
CREATE TABLE Accounts (
    AccountID INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    CreateDate DATETIME NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1,
    EmployeeID INT UNIQUE,
    CustomerID INT UNIQUE,
    RoleID INT,
    RequestedRole VARCHAR(50),
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID) ON DELETE SET NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID) ON DELETE SET NULL,
    FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);
GO

-- Invoices Table
CREATE TABLE Invoices (
    InvoiceID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT,
    EmployeeID INT,
    InvoiceDate DATETIME NOT NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID),
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);
GO

-- InvoiceDetails Table
CREATE TABLE InvoiceDetails (
    InvoiceDetailID INT PRIMARY KEY IDENTITY(1,1),
    InvoiceID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    UnitPrice DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (InvoiceID) REFERENCES Invoices(InvoiceID) ON DELETE CASCADE,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);
GO

-- WarehouseHistory Table
CREATE TABLE WarehouseHistory (
    HistoryID INT PRIMARY KEY IDENTITY(1,1),
    ProductID INT NOT NULL,
    EmployeeID INT NOT NULL,
    TransactionType NVARCHAR(10) NOT NULL, -- 'Import' or 'Export'
    Quantity INT NOT NULL,
    TransactionDate DATETIME NOT NULL DEFAULT GETDATE(),
    Notes NVARCHAR(255),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID),
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);
GO


-- =============================================
-- === CREATE TABLE TYPE FOR STORED PROCEDURE ===
-- =============================================

CREATE TYPE dbo.InvoiceDetailType AS TABLE
(
    ProductID INT,
    Quantity INT,
    UnitPrice DECIMAL(18, 2)
);
GO


-- =============================================
-- === STORED PROCEDURES =======================
-- =============================================

-- Stored Procedure to Create an Invoice
CREATE PROCEDURE sp_CreateInvoice
    @EmployeeID INT,
    @CustomerID INT,
    @InvoiceDate DATETIME,
    @TotalAmount DECIMAL(18, 2),
    @Details dbo.InvoiceDetailType READONLY
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    -- Check stock for all items
    IF EXISTS (
        SELECT 1
        FROM @Details d
        JOIN Products p ON d.ProductID = p.ProductID
        WHERE p.StockQuantity < d.Quantity
    )
    BEGIN
        -- If any product is out of stock, rollback
        ROLLBACK TRANSACTION;
        -- Return 0 to indicate failure
        SELECT 0; 
        RETURN;
    END

    -- Insert into Invoices table
    INSERT INTO Invoices (EmployeeID, CustomerID, InvoiceDate, TotalAmount)
    VALUES (@EmployeeID, @CustomerID, @InvoiceDate, @TotalAmount);

    DECLARE @NewInvoiceID INT = SCOPE_IDENTITY();

    -- Insert into InvoiceDetails table
    INSERT INTO InvoiceDetails (InvoiceID, ProductID, Quantity, UnitPrice)
    SELECT @NewInvoiceID, ProductID, Quantity, UnitPrice FROM @Details;

    -- Update stock quantity in Products table
    UPDATE p
    SET p.StockQuantity = p.StockQuantity - d.Quantity
    FROM Products p
    JOIN @Details d ON p.ProductID = d.ProductID;

    COMMIT TRANSACTION;
    
    -- Return the new Invoice ID on success
    SELECT @NewInvoiceID; 
END
GO


-- Stored Procedure for Warehouse Transactions
CREATE PROCEDURE sp_CreateWarehouseTransaction
    @ProductID INT,
    @EmployeeID INT,
    @TransactionType NVARCHAR(10),
    @Quantity INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    IF @TransactionType = 'Export'
    BEGIN
        -- Check if there is enough stock
        IF (SELECT StockQuantity FROM Products WHERE ProductID = @ProductID) < @Quantity
        BEGIN
            ROLLBACK TRANSACTION;
            RAISERROR ('Not enough stock to export.', 16, 1);
            RETURN;
        END
        -- Update stock
        UPDATE Products SET StockQuantity = StockQuantity - @Quantity WHERE ProductID = @ProductID;
    END
    ELSE IF @TransactionType = 'Import'
    BEGIN
        -- Update stock
        UPDATE Products SET StockQuantity = StockQuantity + @Quantity WHERE ProductID = @ProductID;
    END
    ELSE
    BEGIN
        ROLLBACK TRANSACTION;
        RAISERROR ('Invalid transaction type.', 16, 1);
        RETURN;
    END

    -- Insert into history
    INSERT INTO WarehouseHistory (ProductID, EmployeeID, TransactionType, Quantity, TransactionDate)
    VALUES (@ProductID, @EmployeeID, @TransactionType, @Quantity, GETDATE());

    COMMIT TRANSACTION;
END
GO


-- =============================================
-- === INITIAL DATA INSERTION ==================
-- =============================================

-- Insert Roles
INSERT INTO Roles (RoleName) VALUES ('Admin'), ('Sale'), ('Warehouse'), ('Customer');
GO

-- Insert an Admin Employee
INSERT INTO Employees (FullName, StartDate, Salary) VALUES ('Administrator', GETDATE(), 99999);
DECLARE @AdminEmployeeID INT = SCOPE_IDENTITY();
GO

-- Insert an Admin Account
-- Password '123' is stored as '321' using the simple reverse helper
INSERT INTO Accounts (Username, PasswordHash, EmployeeID, RoleID) 
VALUES ('admin', '321', (SELECT EmployeeID FROM Employees WHERE FullName='Administrator'), (SELECT RoleID FROM Roles WHERE RoleName='Admin'));
GO