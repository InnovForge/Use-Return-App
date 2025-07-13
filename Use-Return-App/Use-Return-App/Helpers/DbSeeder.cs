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

    private static void SeedData(string dbPath)
    {
        string connStr = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security=True";
        using (var connection = new SqlConnection(connStr))
        {
            connection.Open();

            var cmd = new SqlCommand(@"
                INSERT INTO Users (UserID, UserName, FullName, Email, PasswordHash, Phone)
                VALUES (@id, @username, @name, @email, @password, @phone)", connection);

            cmd.Parameters.AddWithValue("@id", Guid.NewGuid());
            cmd.Parameters.AddWithValue("@username", "admin");
            cmd.Parameters.AddWithValue("@name", "Admin User");
            cmd.Parameters.AddWithValue("@email", "admin@example.com");
            cmd.Parameters.AddWithValue("@password", "hashed-password");
            cmd.Parameters.AddWithValue("@phone", "0123456789");

            cmd.ExecuteNonQuery();
        }
    }
}
