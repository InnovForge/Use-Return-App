-- ========================
-- SCHEMA (UUID VERSION) – SQLite
-- ========================

-- 1. USERS
CREATE TABLE Users (
    UserID TEXT PRIMARY KEY, -- UUID
    FullName TEXT NOT NULL,
	UserName TEXT UNIQUE NOT NULL,
    Email TEXT UNIQUE NOT NULL,
    PasswordHash TEXT NOT NULL,
    Phone TEXT,
    RegisteredDate DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- 2. WALLETS
CREATE TABLE Wallets (
    UserID TEXT PRIMARY KEY, -- UUID
    Balance REAL DEFAULT 0,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- 3. TRANSACTIONS
CREATE TABLE Transactions (
    TransactionID TEXT PRIMARY KEY, -- UUID
    UserID TEXT NOT NULL,
    Type TEXT NOT NULL, -- 'Deposit', 'Withdraw', 'Hold', 'Refund', 'Deduct'
    Amount REAL NOT NULL,
    Description TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- 4. CATEGORIES
CREATE TABLE Categories (
    CategoryID TEXT PRIMARY KEY, -- UUID
    CategoryName TEXT NOT NULL
);

-- 5. ITEMS
CREATE TABLE Items (
    ItemID TEXT PRIMARY KEY, -- UUID
    OwnerID TEXT NOT NULL,
    ItemName TEXT NOT NULL,
    Description TEXT,
    Quantity INTEGER NOT NULL CHECK (Quantity >= 0),
    DepositAmount REAL DEFAULT 0,
    CategoryID TEXT,
    IsAvailable INTEGER DEFAULT 1, -- 1 = available
    FOREIGN KEY (OwnerID) REFERENCES Users(UserID),
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

-- 6. RENTALS
CREATE TABLE Rentals (
    RentalID TEXT PRIMARY KEY, -- UUID
    ItemID TEXT NOT NULL,
    BorrowerID TEXT NOT NULL,
    RentDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    ReturnDate DATETIME,
    Status TEXT CHECK (Status IN ('Pending', 'Approved', 'Borrowed', 'Returned', 'Rejected')) NOT NULL,
    DepositAmount REAL,
    DepositStatus TEXT CHECK (DepositStatus IN ('Held', 'Refunded', 'Deducted')),
    Notes TEXT,
    FOREIGN KEY (ItemID) REFERENCES Items(ItemID),
    FOREIGN KEY (BorrowerID) REFERENCES Users(UserID)
);
