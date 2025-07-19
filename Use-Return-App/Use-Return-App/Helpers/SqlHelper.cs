using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

public static class SqlHelper
{
    private static string GetConnectionString()
    {
     //   string dbFile = HttpContext.Current.Server.MapPath("~/App_Data/Database.mdf");
        return $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True;Connect Timeout=30;";
    }

    public static void EnsureDatabaseExists()
    {
        string folderPath = HttpContext.Current.Server.MapPath("~/App_Data");
        string dbPath = Path.Combine(folderPath, "Database.mdf");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        if (!File.Exists(dbPath))
        {
            DbSeeder.Run();
            DbSeeder.SeedData(dbPath);
          //  ExecuteScriptFromFile(dbPath, Path.Combine(folderPath, "schema.sql"));
        }
    }

    //private static void CreateDatabase(string dbPath)
    //{
    //    string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Integrated Security=True";
    //    string dbName = Path.GetFileNameWithoutExtension(dbPath);

    //    using (var connection = new SqlConnection(connectionString))
    //    {
    //        connection.Open();
    //        string cmdText = $@"CREATE DATABASE [{dbName}] ON (NAME = N'{dbName}', FILENAME = '{dbPath}') LOG ON (NAME = N'{dbName}_log', FILENAME = '{dbPath.Replace(".mdf", "_log.ldf")}')";
    //        using (var command = new SqlCommand(cmdText, connection))
    //        {
    //            command.ExecuteNonQuery();
    //        }
    //    }
    //}

    private static void ExecuteScriptFromFile(string dbPath, string scriptFilePath)
    {
        if (!File.Exists(scriptFilePath))
            throw new FileNotFoundException("File schema.sql not exists", scriptFilePath);

        string script = File.ReadAllText(scriptFilePath);
        string connStr = GetConnectionString();

        using (var connection = new SqlConnection(connStr))
        {
            connection.Open();
            var commands = script.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var cmd in commands)
            {
                var trimmedCmd = cmd.Trim();
                if (!string.IsNullOrWhiteSpace(trimmedCmd))
                {
                    using (var command = new SqlCommand(trimmedCmd, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }

    /// <summary>
    /// Thực thi câu lệnh SQL không trả về dữ liệu (INSERT, UPDATE, DELETE, CREATE,...).
    /// </summary>
    /// <param name="sql">Chuỗi SQL</param>
    /// <param name="parameters">Danh sách tham số (tuỳ chọn)</param>
    /// <returns>Số dòng bị ảnh hưởng</returns>
    public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
    {
        using (var connection = new SqlConnection(GetConnectionString()))
        {
            connection.Open();
            using (var command = new SqlCommand(sql, connection))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                return command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Thực thi câu lệnh SQL và trả về một giá trị đơn (ví dụ: SELECT COUNT(*)).
    /// </summary>
    /// <typeparam name="T">Kiểu dữ liệu trả về</typeparam>
    /// <param name="sql">Chuỗi SQL</param>
    /// <param name="parameters">Danh sách tham số (tuỳ chọn)</param>
    /// <returns>Giá trị đơn (hoặc mặc định nếu không có)</returns>
    public static T ExecuteScalar<T>(string sql, params SqlParameter[] parameters)
    {
        using (var connection = new SqlConnection(GetConnectionString()))
        {
            connection.Open();
            using (var command = new SqlCommand(sql, connection))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                object result = command.ExecuteScalar();
                return (result != null && result != DBNull.Value)
                    ? (T)Convert.ChangeType(result, typeof(T))
                    : default(T);
            }
        }
    }

    /// <summary>
    /// Thực thi truy vấn SELECT và trả về kết quả dưới dạng DataTable.
    /// Thích hợp để binding cho GridView, export,...
    /// </summary>
    /// <param name="sql">Chuỗi SQL SELECT</param>
    /// <param name="parameters">Danh sách tham số (tuỳ chọn)</param>
    /// <returns>DataTable chứa dữ liệu</returns>
    public static DataTable ExecuteDataTable(string sql, params SqlParameter[] parameters)
    {
        using (var connection = new SqlConnection(GetConnectionString()))
        {
            connection.Open();
            using (var command = new SqlCommand(sql, connection))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                using (var adapter = new SqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }
    }

    public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] parameters)
    {
        var connection = new SqlConnection(GetConnectionString());
        var command = new SqlCommand(sql, connection);

        if (parameters != null)
            command.Parameters.AddRange(parameters);

        connection.Open();
        // Khi reader bị đóng thì connection cũng sẽ tự đóng
        return command.ExecuteReader(CommandBehavior.CloseConnection);
    }

    public static void UpdateUserOnlineStatus(Guid userId, bool isOnline)
    {
        string sql = @"UPDATE NguoiDung 
                   SET TrangThai = @TrangThai, LanCuoiOnline = @LanCuoiOnline 
                   WHERE MaNguoiDung = @UserId";

        SqlParameter[] parameters = new[]
        {
        new SqlParameter("@TrangThai", isOnline),
        new SqlParameter("@LanCuoiOnline", DateTime.Now),
        new SqlParameter("@UserId", userId)
    };

        ExecuteNonQuery(sql, parameters);
    }


}
