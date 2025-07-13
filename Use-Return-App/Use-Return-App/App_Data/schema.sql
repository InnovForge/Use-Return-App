-- 1. USERS
CREATE TABLE Users (
    UserID UNIQUEIDENTIFIER PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    UserName NVARCHAR(100) UNIQUE NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    RegisteredDate DATETIME DEFAULT GETDATE()
);
GO

-- 2. WALLETS
CREATE TABLE Wallets (
    UserID UNIQUEIDENTIFIER PRIMARY KEY,
    Balance FLOAT DEFAULT 0,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO

-- 3. TRANSACTIONS
CREATE TABLE Transactions (
    TransactionID UNIQUEIDENTIFIER PRIMARY KEY,
    UserID UNIQUEIDENTIFIER NOT NULL,
    Type NVARCHAR(20) NOT NULL CHECK (Type IN ('Deposit', 'Withdraw', 'Hold', 'Refund', 'Deduct')),
    Amount FLOAT NOT NULL,
    Description NVARCHAR(MAX),
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO

-- 4. CATEGORIES
CREATE TABLE Categories (
    CategoryID UNIQUEIDENTIFIER PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL
);
GO

-- 5. ITEMS
CREATE TABLE Items (
    ItemID UNIQUEIDENTIFIER PRIMARY KEY,
    OwnerID UNIQUEIDENTIFIER NOT NULL,
    ItemName NVARCHAR(100) NOT NULL,
    Description NVARCHAR(MAX),
    Quantity INT NOT NULL CHECK (Quantity >= 0),
    DepositAmount FLOAT DEFAULT 0,
    CategoryID UNIQUEIDENTIFIER,
    IsAvailable BIT DEFAULT 1,
    FOREIGN KEY (OwnerID) REFERENCES Users(UserID),
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);
GO

-- 6. RENTALS
CREATE TABLE Rentals (
    RentalID UNIQUEIDENTIFIER PRIMARY KEY,
    ItemID UNIQUEIDENTIFIER NOT NULL,
    BorrowerID UNIQUEIDENTIFIER NOT NULL,
    RentDate DATETIME DEFAULT GETDATE(),
    ReturnDate DATETIME,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN ('Pending', 'Approved', 'Borrowed', 'Returned', 'Rejected')),
    DepositAmount FLOAT,
    DepositStatus NVARCHAR(20) CHECK (DepositStatus IN ('Held', 'Refunded', 'Deducted')),
    Notes NVARCHAR(MAX),
    FOREIGN KEY (ItemID) REFERENCES Items(ItemID),
    FOREIGN KEY (BorrowerID) REFERENCES Users(UserID)
);
GO
