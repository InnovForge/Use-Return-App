using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Use_Return_App.Admin
{
    public partial class Login : System.Web.UI.Page
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

                string sql = "SELECT MaNguoiDung, HoTen,AnhDaiDien, MaVaiTro ,MatKhauHash FROM NguoiDung WHERE Email = @email AND MaVaiTro = 2";
                var table = SqlHelper.ExecuteDataTable(sql, new SqlParameter("@email", email));

                if (table.Rows.Count > 0)
                {
                    var row = table.Rows[0];
                    string hash = row["MatKhauHash"].ToString();

                    if (BCrypt.Net.BCrypt.Verify(password, hash))
                    {
                        Session["UserID"] = row["MaNguoiDung"].ToString();
                        Session["HoTen"] = row["HoTen"].ToString();
                        Session["MaVaiTro"] = row["MaVaiTro"].ToString();
                        string anh = string.IsNullOrEmpty(row["AnhDaiDien"]?.ToString())
                ? "https://placehold.co/600x400/green/white?text=avatar" : row["AnhDaiDien"].ToString();

                        Session["AnhDaiDien"] = anh;
                        Response.Redirect("AdminHome.aspx");
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