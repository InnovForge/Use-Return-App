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

                userCmd.Parameters.AddWithValue("@id", Guid.NewGuid());
                userCmd.Parameters.AddWithValue("@maVaiTro", maVaiTro);
                userCmd.Parameters.AddWithValue("@hoTen", "admin"); 
                userCmd.Parameters.AddWithValue("@email", "admin@example.com");
                userCmd.Parameters.AddWithValue("@soDienThoai", "0123456789");
                userCmd.Parameters.AddWithValue("@matKhauHash", matKhauHash);
                userCmd.Parameters.AddWithValue("@anhDaiDien", DBNull.Value);

                userCmd.ExecuteNonQuery();
            }

            var danhMucCmd = new SqlCommand(@"
    IF NOT EXISTS (SELECT 1 FROM DanhMuc WHERE TenDanhMuc = N'Điện tử')
        INSERT INTO DanhMuc (TenDanhMuc) VALUES (N'Điện tử');

    IF NOT EXISTS (SELECT 1 FROM DanhMuc WHERE TenDanhMuc = N'Sách')
        INSERT INTO DanhMuc (TenDanhMuc) VALUES (N'Sách');

    IF NOT EXISTS (SELECT 1 FROM DanhMuc WHERE TenDanhMuc = N'Trang phục')
        INSERT INTO DanhMuc (TenDanhMuc) VALUES (N'Trang phục');", connection);
            danhMucCmd.ExecuteNonQuery();

        }
    }
}
