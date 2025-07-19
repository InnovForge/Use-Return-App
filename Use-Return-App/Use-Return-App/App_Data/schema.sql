-- Cơ sở dữ liệu cho "Web Thuê Đồ Dùng"

CREATE TABLE VaiTro (
    MaVaiTro          INT IDENTITY PRIMARY KEY,
    TenVaiTro         NVARCHAR(50) NOT NULL UNIQUE,
    MoTa              NVARCHAR(255)

);

CREATE TABLE DanhMuc (
    MaDanhMuc         INT IDENTITY PRIMARY KEY,
    TenDanhMuc        NVARCHAR(100) NOT NULL,
);


CREATE TABLE NguoiDung (
    MaNguoiDung       UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    MaVaiTro          INT NOT NULL REFERENCES VaiTro(MaVaiTro),
    HoTen             NVARCHAR(120) NOT NULL,
    Email             NVARCHAR(120) NOT NULL UNIQUE,
    SoDienThoai       NVARCHAR(20) UNIQUE,
    MatKhauHash       NVARCHAR(256) NOT NULL,
    AnhDaiDien        NVARCHAR(255),
    DangHoatDong      BIT DEFAULT 1,
    NgayTao           DATETIME DEFAULT GETDATE()
);



CREATE TABLE DoDung (
    MaDoDung          UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    MaNguoiSoHuu      UNIQUEIDENTIFIER NOT NULL REFERENCES NguoiDung(MaNguoiDung) ON DELETE CASCADE,
    MaNguoiSoHuu      UNIQUEIDENTIFIER REFERENCES NguoiDung(MaNguoiDung) ON DELETE CASCADE,
    MaDanhMuc         INT NOT NULL REFERENCES DanhMuc(MaDanhMuc),
    TieuDe            NVARCHAR(150) NOT NULL,
    MoTa              NVARCHAR(MAX),
    GiaMoiNgay        DECIMAL(12,2) NOT NULL,
    SoLuong           INT DEFAULT 1,
	TienCoc           DECIMAL(12,2) NOT NULL,
    TinhTrang         NVARCHAR(255),
    TrangThai         NVARCHAR(20) DEFAULT 'Available' CHECK (TrangThai IN ('Available','Unavailable','Deleted')),
    NgayTao           DATETIME DEFAULT GETDATE()
);

CREATE TABLE HinhAnhDoDung (
    MaHinh            UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    MaDoDung          UNIQUEIDENTIFIER NOT NULL REFERENCES DoDung(MaDoDung) ON DELETE CASCADE,
    DuongDanAnh       NVARCHAR(255) NOT NULL,
    ThuTuHienThi      INT DEFAULT 0
);

CREATE TABLE PhieuThue (
    MaPhieuThue       UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    MaDoDung          UNIQUEIDENTIFIER NOT NULL REFERENCES DoDung(MaDoDung),
    MaNguoiThue       UNIQUEIDENTIFIER NOT NULL REFERENCES NguoiDung(MaNguoiDung),
    NgayBatDau        DATE NOT NULL,
    NgayKetThuc       DATE NOT NULL,
    SoNgay            AS (DATEDIFF(day, NgayBatDau, NgayKetThuc) + 1) PERSISTED,
    
    TongTien          DECIMAL(12,2) NOT NULL,
    TienCoc           DECIMAL(12,2) NOT NULL,

    TrangThai         NVARCHAR(20) DEFAULT 'Pending' 
        CHECK (TrangThai IN ('Pending','Confirmed','PickedUp','Returned','Cancelled','Expired')),

    ThoiGianHetHan    DATETIME NULL, 
    NgayTao           DATETIME DEFAULT GETDATE()
);

CREATE TABLE ThanhToan (
    MaThanhToan       NVARCHAR(18) PRIMARY KEY,
    MaPhieuThue       UNIQUEIDENTIFIER NOT NULL REFERENCES PhieuThue(MaPhieuThue) ON DELETE CASCADE,

    SoTien            DECIMAL(12,2) NOT NULL,
    PhuongThuc        NVARCHAR(30) NOT NULL 
        CHECK (PhuongThuc IN ('COD','Bank','E-Wallet','CreditCard')),

    ThoiGianThanhToan DATETIME DEFAULT GETDATE(),

    TrangThai         NVARCHAR(20) DEFAULT 'Pending' 
        CHECK (TrangThai IN ('Success','Failed','Refunded','Pending')),

    TenNganHang       NVARCHAR(255) NULL,
    MaGiaoDichNgoai   NVARCHAR(50) NULL, 
    GhiChu            NVARCHAR(255) NULL
);

CREATE TABLE Message (
    MessageId       UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    SenderId        UNIQUEIDENTIFIER NOT NULL REFERENCES NguoiDung(MaNguoiDung),
    ReceiverId      UNIQUEIDENTIFIER NOT NULL REFERENCES NguoiDung(MaNguoiDung) ON DELETE CASCADE,
    Content         NVARCHAR(MAX) NOT NULL,
    SentTime        DATETIME DEFAULT GETDATE(),
    IsRead          BIT DEFAULT 0
);

CREATE TABLE Attachment (
    AttachmentId     UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
    MessageId        UNIQUEIDENTIFIER NOT NULL REFERENCES Message(MessageId) ON DELETE CASCADE,
    FileType         NVARCHAR(50),
    FileUrl          NVARCHAR(1000),
    FileName         NVARCHAR(255),
    UploadedAt       DATETIME DEFAULT GETDATE()
);



--CREATE TABLE GiaoDich (
--    MaGiaoDich        UNIQUEIDENTIFIER DEFAULT NEWID() PRIMARY KEY,
--    MaNguoiDung       UNIQUEIDENTIFIER NOT NULL REFERENCES NguoiDung(MaNguoiDung) ON DELETE CASCADE,
--    LoaiGiaoDich      NVARCHAR(20) NOT NULL CHECK (LoaiGiaoDich IN ('Deposit','Withdraw','Hold','Refund','Deduct')),
--    SoTien            DECIMAL(12,2) NOT NULL,
--    MoTa              NVARCHAR(MAX),
--    NgayTao           DATETIME DEFAULT GETDATE()
--);


