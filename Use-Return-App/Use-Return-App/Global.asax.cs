using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Use_Return_App.Models;

[assembly: OwinStartup(typeof(Use_Return_App.App_Start.Startup))]
namespace Use_Return_App
{

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
         //  RouteConfig.RegisterRoutes(RouteTable.Routes);
        //DbSeeder.Run();
            SqlHelper.EnsureDatabaseExists();

            //string hashed = BCrypt.Net.BCrypt.HashPassword("123456");
            //bool matched = BCrypt.Net.BCrypt.Verify("123456", hashed);

      //      SqlHelper.ExecuteNonQuery(
      //    @"INSERT INTO Users (UserID, UserName, FullName, Email, PasswordHash, Phone)
      //VALUES (@id, @username, @name, @email, @password, @phone)",
      //    new SqlParameter("@id", Guid.NewGuid()), // UNIQUEIDENTIFIER
      //    new SqlParameter("@username", "ngtuonghy"),
      //    new SqlParameter("@name", "Nguyen Hy"),
      //    new SqlParameter("@email", "hy1@example.com"),
      //    new SqlParameter("@password", hashed),
      //    new SqlParameter("@phone", "0909090909")
      //);



        }
    }
}