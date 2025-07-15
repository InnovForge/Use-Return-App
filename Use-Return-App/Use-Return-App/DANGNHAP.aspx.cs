using System;
using System.Data;
using System.Data.SqlClient;

namespace Use_Return_App
{
    public partial class DANGNHAP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtTenDangNhap.Attributes["autocomplete"] = "off";
                txtMatKhau.Attributes["autocomplete"] = "new-password";
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string tenDangNhap = txtTenDangNhap.Text.Trim();
                string password = txtMatKhau.Text.Trim();

                string sql = "SELECT MaNguoiDung, HoTen, MatKhauHash FROM NguoiDung WHERE HoTen = @username AND DangHoatDong = 1";
                var table = SqlHelper.ExecuteDataTable(sql, new SqlParameter("@username", tenDangNhap));

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

                lblMessage.Text = "Tên đăng nhập hoặc mật khẩu không đúng.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi hệ thống: " + ex.Message;
            }
        }
    }
}
