using Bogus;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;

public static class DbSeeder
{
    public static void Run()
    {
        string folderPath = HttpContext.Current.Server.MapPath("~/App_Data");
        string dbPath = Path.Combine(folderPath, "Database.mdf");
        string logPath = dbPath.Replace(".mdf", "_log.ldf");
        string dbName = Path.GetFileNameWithoutExtension(dbPath);
        string schemaPath = Path.Combine(folderPath, "schema.sql");

        string masterConn = @"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True";
        using (var connection = new SqlConnection(masterConn))
        {
            connection.Open();

            string dropIfExists = $@"
                IF EXISTS (SELECT name FROM sys.databases WHERE name = N'{dbName}')
                BEGIN
                    ALTER DATABASE [{dbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    DROP DATABASE [{dbName}];
                END";

            using (var cmd = new SqlCommand(dropIfExists, connection))
                cmd.ExecuteNonQuery();
        }


        if (File.Exists(dbPath)) File.Delete(dbPath);
        if (File.Exists(logPath)) File.Delete(logPath);

        using (var connection = new SqlConnection(masterConn))
        {
            connection.Open();

            string createDb = $@"
                CREATE DATABASE [{dbName}]
                ON (NAME = N'{dbName}', FILENAME = N'{dbPath}')
                LOG ON (NAME = N'{dbName}_log', FILENAME = N'{logPath}')";

            using (var cmd = new SqlCommand(createDb, connection))
                cmd.ExecuteNonQuery();
        }

        ExecuteScriptFromFile(dbPath, schemaPath);

       SeedData(dbPath);
    }

    private static void ExecuteScriptFromFile(string dbPath, string scriptFilePath)
    {
        string script = File.ReadAllText(scriptFilePath);
        var commands = System.Text.RegularExpressions.Regex.Split(script, @"^\s*GO\s*$", System.Text.RegularExpressions.RegexOptions.Multiline);

        string connStr = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security=True";
        using (var connection = new SqlConnection(connStr))
        {
            connection.Open();
            foreach (var cmdText in commands)
            {
                var trimmed = cmdText.Trim();
                if (!string.IsNullOrEmpty(trimmed))
                {
                    using (var cmd = new SqlCommand(trimmed, connection))
                        cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public static void SeedData(string dbPath)
    {
        string connStr = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security=True";
        using (var connection = new SqlConnection(connStr))
        {
            connection.Open();
            var roleCmd = new SqlCommand(@"
    IF NOT EXISTS (SELECT 1 FROM VaiTro WHERE TenVaiTro = N'Người dùng')
    BEGIN
        INSERT INTO VaiTro (TenVaiTro, MoTa)
        VALUES (N'Người dùng', N'Người sử dụng thông thường');
    END
    IF NOT EXISTS (SELECT 1 FROM VaiTro WHERE TenVaiTro = N'Quản trị')
    BEGIN
        INSERT INTO VaiTro (TenVaiTro, MoTa)
        VALUES (N'Quản trị', N'Tài khoản quản trị hệ thống');
    END", connection);
            roleCmd.ExecuteNonQuery();

            var getRoleCmd = new SqlCommand("SELECT MaVaiTro FROM VaiTro WHERE TenVaiTro = N'Quản trị'", connection);
            var maVaiTro = (int)getRoleCmd.ExecuteScalar();

            string matKhau = "admin123";
            string matKhauHash = BCrypt.Net.BCrypt.HashPassword(matKhau);

            var checkUserCmd = new SqlCommand("SELECT COUNT(*) FROM NguoiDung WHERE Email = @Email", connection);
            checkUserCmd.Parameters.AddWithValue("@Email", "admin@example.com");
            int userExists = (int)checkUserCmd.ExecuteScalar();

            if (userExists == 0)
            {
                var userCmd = new SqlCommand(@"
        INSERT INTO NguoiDung 
            (MaNguoiDung, MaVaiTro, HoTen, Email, SoDienThoai, MatKhauHash, AnhDaiDien)
        VALUES 
            (@id, @maVaiTro, @hoTen, @email, @soDienThoai, @matKhauHash, @anhDaiDien)", connection);

                userCmd.Parameters.AddWithValue("@id", "8ead7e11-07e3-401b-b36d-1daebc4ef028");
                userCmd.Parameters.AddWithValue("@maVaiTro", maVaiTro);
                userCmd.Parameters.AddWithValue("@hoTen", "admin"); 
                userCmd.Parameters.AddWithValue("@email", "admin@example.com");
                userCmd.Parameters.AddWithValue("@soDienThoai", "0123456789");
                userCmd.Parameters.AddWithValue("@matKhauHash", matKhauHash);
                userCmd.Parameters.AddWithValue("@anhDaiDien", DBNull.Value);

                userCmd.ExecuteNonQuery();

                var userCmd2 = new SqlCommand(@"
    INSERT INTO NguoiDung 
        (MaVaiTro, HoTen, Email, SoDienThoai, MatKhauHash, AnhDaiDien)
    VALUES 
        (@maVaiTro, @hoTen, @email, @soDienThoai, @matKhauHash, @anhDaiDien)", connection);

                userCmd2.Parameters.AddWithValue("@maVaiTro", 2);
                userCmd2.Parameters.AddWithValue("@hoTen", "user1");
                userCmd2.Parameters.AddWithValue("@email", "user1@example.com");
                userCmd2.Parameters.AddWithValue("@soDienThoai", "0987654321");
                userCmd2.Parameters.AddWithValue("@matKhauHash", BCrypt.Net.BCrypt.HashPassword("user123")); // Hoặc biến hash nếu có
                userCmd2.Parameters.AddWithValue("@anhDaiDien", DBNull.Value);

                userCmd2.ExecuteNonQuery();

            }

            var danhMucCmd = new SqlCommand(@"
    IF NOT EXISTS (SELECT 1 FROM DanhMuc WHERE TenDanhMuc = N'Điện tử')
        INSERT INTO DanhMuc (TenDanhMuc) VALUES (N'Điện tử');

    IF NOT EXISTS (SELECT 1 FROM DanhMuc WHERE TenDanhMuc = N'Sách')
        INSERT INTO DanhMuc (TenDanhMuc) VALUES (N'Sách');

    IF NOT EXISTS (SELECT 1 FROM DanhMuc WHERE TenDanhMuc = N'Tổng hợp')
        INSERT INTO DanhMuc (TenDanhMuc) VALUES (N'Tổng hợp');", connection);
            danhMucCmd.ExecuteNonQuery();

            var getDanhMucCmd = new SqlCommand("SELECT MaDanhMuc FROM DanhMuc WHERE TenDanhMuc = N'Tổng hợp'", connection);
            var maDanhMuc = (int)getDanhMucCmd.ExecuteScalar();

            var faker = new Faker("vi");

            var insertDoDungCmd = new SqlCommand(@"
    INSERT INTO DoDung 
        (MaDoDung, MaNguoiSoHuu, MaDanhMuc, TieuDe, MoTa, GiaMoiNgay, SoLuong, TinhTrang, TrangThai, NgayTao, TienCoc)
    OUTPUT INSERTED.MaDoDung
    VALUES 
        (@id, @nguoiSoHuu, @danhMuc, @tieuDe, @moTa, @giaMoiNgay, @soLuong, @tinhTrang, @trangThai, GETDATE(), @tienCoc)", connection);


            var insertAnhCmd = new SqlCommand(@"
    INSERT INTO HinhAnhDoDung 
        (MaHinh, MaDoDung, DuongDanAnh, ThuTuHienThi)
    VALUES 
        (@maHinh, @maDoDung, @duongDan, @thuTu)", connection);

            for (int i = 0; i < 100; i++)
            {
                decimal gia = faker.Random.Decimal(10000, 500000);
                decimal heSoTienCoc = faker.Random.Decimal(7m, 10m);
                decimal tienCoc = gia * heSoTienCoc;
                Guid maDoDung = Guid.NewGuid();

                insertDoDungCmd.Parameters.Clear();
                insertDoDungCmd.Parameters.AddWithValue("@id", maDoDung);
                insertDoDungCmd.Parameters.AddWithValue("@nguoiSoHuu", Guid.Parse("8ead7e11-07e3-401b-b36d-1daebc4ef028"));
                insertDoDungCmd.Parameters.AddWithValue("@danhMuc", maDanhMuc);
                insertDoDungCmd.Parameters.AddWithValue("@tieuDe", faker.Commerce.ProductName());
                insertDoDungCmd.Parameters.AddWithValue("@moTa", faker.Commerce.ProductDescription());
                insertDoDungCmd.Parameters.AddWithValue("@giaMoiNgay", gia);
                insertDoDungCmd.Parameters.AddWithValue("@soLuong", faker.Random.Int(1, 10));
                insertDoDungCmd.Parameters.AddWithValue("@tinhTrang", faker.PickRandom("Mới", "Như mới", "Đã qua sử dụng", "Cũ"));
                insertDoDungCmd.Parameters.AddWithValue("@trangThai", "Available");
                insertDoDungCmd.Parameters.AddWithValue("@tienCoc", tienCoc);

                var insertedId = (Guid)insertDoDungCmd.ExecuteScalar();

                int soAnh = faker.Random.Int(1, 10);
                for (int j = 0; j < soAnh; j++)
                {
                    insertAnhCmd.Parameters.Clear();
                    insertAnhCmd.Parameters.AddWithValue("@maHinh", Guid.NewGuid());
                    insertAnhCmd.Parameters.AddWithValue("@maDoDung", insertedId);

                    var fakeUrl = faker.Image.PicsumUrl(600, 400); // ví dụ: https://picsum.photos/id/237/600/400

                    insertAnhCmd.Parameters.AddWithValue("@duongDan", fakeUrl);
                    insertAnhCmd.Parameters.AddWithValue("@thuTu", j);

                    insertAnhCmd.ExecuteNonQuery();
                }
            }



        }
    }
}
