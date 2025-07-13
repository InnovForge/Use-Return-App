using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Use_Return_App.Admin
{
    public partial class QuanLyNguoiDung : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUsers();
            }
        }

        public void LoadUsers()
        {
            string sql = "SELECT * FROM Users WHERE RoleID = 2";
            GridView1.DataSource = SqlHelper.ExecuteDataTable(sql);
            GridView1.DataBind();
        }
        public void DeleteUser(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "DeleteUser")
            {
                int userId = Convert.ToInt32(e.CommandArgument);
                string sql = $"DELETE FROM Users WHERE UserID = {userId}";
                SqlHelper.ExecuteNonQuery(sql);
                LoadUsers();
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Xoa")
            {
                string userId = e.CommandArgument.ToString();
                string sql = "DELETE FROM Users WHERE UserID = @UserID";

                SqlParameter[] parameters = {
                  new SqlParameter("@UserID", userId)
                };
                SqlHelper.ExecuteNonQuery(sql, parameters);
                LoadUsers();
            }

        }
    }
}