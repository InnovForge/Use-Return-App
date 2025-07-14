using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Use_Return_App
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text.Trim();
                string password = txtMatKhau.Text.Trim();

                string sql = "SELECT MaNguoiDung, HoTen, MatKhauHash FROM NguoiDung WHERE Email = @email AND DangHoatDong = 1";
                var table = SqlHelper.ExecuteDataTable(sql, new SqlParameter("@email", email));

                if (table.Rows.Count > 0)
                {
                    var row = table.Rows[0];
                    string hash = row["MatKhauHash"].ToString();

                    if (BCrypt.Net.BCrypt.Verify(password, hash))
                    {
                        Session["UserID"] = row["MaNguoiDung"].ToString();
                        Session["HoTen"] = row["HoTen"].ToString();
                        Response.Redirect("Default.aspx");
                        return;
                    }
                }

                lblMessage.Text = "Email hoặc mật khẩu không đúng.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi hệ thống: " + ex.Message;
            }
        }
    }
}