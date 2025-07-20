using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;

public static class SqlHelper
{
    /// <summary>
    /// Trả về SqlConnection dựa vào connection string trong Web.config.
    /// </summary>
    public static SqlConnection GetConnection()
    {
        string env = ConfigurationManager.AppSettings["Environment"];
        string connName = env == "Production" ? "ProductionConnection" : "DefaultConnection";
        string connStr = ConfigurationManager.ConnectionStrings[connName].ConnectionString;
        return new SqlConnection(connStr);
    }

    /// <summary>
    /// Đảm bảo file cơ sở dữ liệu tồn tại trong App_Data. Nếu chưa có thì chạy DbSeeder.
    /// </summary>
    public static void EnsureDatabaseExists()
    {
        // Chỉ chạy trong môi trường Development
        string environment = ConfigurationManager.AppSettings["Environment"];
        if (!string.Equals(environment, "Development", StringComparison.OrdinalIgnoreCase))
            return;

        string folderPath = HttpContext.Current.Server.MapPath("~/App_Data");
        string dbPath = Path.Combine(folderPath, "Database.mdf");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        if (!File.Exists(dbPath))
        {
            DbSeeder.Run();
            DbSeeder.SeedData(dbPath);
        }
    }

    /// <summary>
    /// Thực thi script SQL từ file .sql.
    /// </summary>
    private static void ExecuteScriptFromFile(string scriptFilePath)
    {
        if (!File.Exists(scriptFilePath))
            throw new FileNotFoundException("File schema.sql không tồn tại", scriptFilePath);

        string script = File.ReadAllText(scriptFilePath);

        using (var connection = GetConnection())
        {
            connection.Open();
            var commands = script.Split(new[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var cmd in commands)
            {
                string trimmedCmd = cmd.Trim();
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
    /// Thực thi câu lệnh INSERT, UPDATE, DELETE,...
    /// </summary>
    public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            using (var command = new SqlCommand(sql, connection))
            {
                if (parameters?.Length > 0)
                    command.Parameters.AddRange(parameters);

                return command.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Thực thi SELECT COUNT, MAX,... trả về 1 giá trị đơn.
    /// </summary>
    public static T ExecuteScalar<T>(string sql, params SqlParameter[] parameters)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            using (var command = new SqlCommand(sql, connection))
            {
                if (parameters?.Length > 0)
                    command.Parameters.AddRange(parameters);

                object result = command.ExecuteScalar();
                return (result != null && result != DBNull.Value)
                    ? (T)Convert.ChangeType(result, typeof(T))
                    : default(T);
            }
        }
    }

    /// <summary>
    /// Thực thi SELECT và trả về DataTable.
    /// </summary>
    public static DataTable ExecuteDataTable(string sql, params SqlParameter[] parameters)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            using (var command = new SqlCommand(sql, connection))
            {
                if (parameters?.Length > 0)
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

    /// <summary>
    /// Thực thi SELECT và trả về SqlDataReader (chú ý: cần dùng using hoặc đọc xong phải Close).
    /// </summary>
    public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] parameters)
    {
        var connection = GetConnection();
        var command = new SqlCommand(sql, connection);

        if (parameters?.Length > 0)
            command.Parameters.AddRange(parameters);

        connection.Open();
        return command.ExecuteReader(CommandBehavior.CloseConnection); // tự close connection khi reader bị dispose
    }

    /// <summary>
    /// Cập nhật trạng thái online của người dùng.
    /// </summary>
    public static void UpdateUserOnlineStatus(Guid userId, bool isOnline)
    {
        string sql = @"
            UPDATE NguoiDung 
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
