using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Web;

public static class SqliteHelper
{
    /// <summary>
    /// Trả về chuỗi kết nối SQLite từ đường dẫn ~/App_Data/database.db.
    /// </summary>
    private static string GetDbPath()
    {
        var dbFile = HttpContext.Current.Server.MapPath("~/App_Data/database.db");
        return $"Data Source={dbFile}";
    }

    public static void EnsureDatabaseExists()
    {
        string folderPath = HttpContext.Current.Server.MapPath("~/App_Data");
        string dbPath = Path.Combine(folderPath, "database.db");
        string schemaPath = Path.Combine(folderPath, "schema.sql");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        if (!File.Exists(dbPath))
        {
            SQLiteConnection.CreateFile(dbPath);
            ExecuteScriptFromFile(dbPath, schemaPath);
        }
    }

    private static void ExecuteScriptFromFile(string dbPath, string scriptFilePath)
    {
        if (!File.Exists(scriptFilePath))
            throw new FileNotFoundException("File schema.sql not exists", scriptFilePath);

        string script = File.ReadAllText(scriptFilePath);

        using (var connection = new SQLiteConnection($"Data Source={dbPath}"))
        {
            connection.Open();

            var commands = script.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var cmd in commands)
            {
                var trimmedCmd = cmd.Trim();
                if (!string.IsNullOrWhiteSpace(trimmedCmd))
                {
                    using (var command = new SQLiteCommand(trimmedCmd, connection))
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
    public static int ExecuteNonQuery(string sql, params SQLiteParameter[] parameters)
    {
        string dbPath = GetDbPath();

        using (var connection = new SQLiteConnection(dbPath))
        {
            connection.Open();
            using (var command = new SQLiteCommand(sql, connection))
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
    public static T ExecuteScalar<T>(string sql, params SQLiteParameter[] parameters)
    {
        string dbPath = GetDbPath();

        using (var connection = new SQLiteConnection(dbPath))
        {
            connection.Open();
            using (var command = new SQLiteCommand(sql, connection))
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
    public static DataTable ExecuteDataTable(string sql, params SQLiteParameter[] parameters)
    {
        string dbPath = GetDbPath();

        using (var connection = new SQLiteConnection(dbPath))
        {
            connection.Open();
            using (var command = new SQLiteCommand(sql, connection))
            {
                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                using (var adapter = new SQLiteDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
        }
    }

}
