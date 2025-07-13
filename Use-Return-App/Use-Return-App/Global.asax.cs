using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Use_Return_App.Models;

namespace Use_Return_App
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            SqliteHelper.EnsureDatabaseExists();

      //      SqliteHelper.ExecuteNonQuery(
      //          @"INSERT INTO Users (UserID, FullName, Email, PasswordHash, Phone)
      //VALUES (@id, @name, @email, @password, @phone)",
      //          new SQLiteParameter("@id", Guid.NewGuid().ToString()), // UUID
      //          new SQLiteParameter("@name", "Nguyen Hy"),
      //          new SQLiteParameter("@email", "hy@example.com"),
      //          new SQLiteParameter("@password", "123456hash"),
      //          new SQLiteParameter("@phone", "0909090909")
      //      );


        }
    }
}