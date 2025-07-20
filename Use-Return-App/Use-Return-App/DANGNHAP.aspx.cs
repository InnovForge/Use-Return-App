using System;
using System.Data;
using System.Data.SqlClient;

namespace Use_Return_App
{
    public partial class DANGNHAP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string soDienThoai = txtSoDienThoai.Text.Trim();
                string password = txtMatKhau.Text.Trim();

                string sql = "SELECT MaNguoiDung, HoTen,AnhDaiDien, MatKhauHash FROM NguoiDung WHERE SoDienThoai = @sdt AND DangHoatDong = 1";
                var table = SqlHelper.ExecuteDataTable(sql, new SqlParameter("@sdt", soDienThoai));

                if (table.Rows.Count > 0)
                {
                    var row = table.Rows[0];
                    string hash = row["MatKhauHash"].ToString();

                    if (BCrypt.Net.BCrypt.Verify(password, hash))
                    {
                        Session["UserID"] = row["MaNguoiDung"].ToString();
                        Session["HoTen"] = row["HoTen"].ToString();
                        string anh = string.IsNullOrEmpty(row["AnhDaiDien"]?.ToString())
                ? "https://placehold.co/600x400/green/white?text=avatar" : row["AnhDaiDien"].ToString();

                        Session["AnhDaiDien"] = anh;
                        Response.Redirect("Default.aspx");
                        return;
                    }
                }

                lblMessage.Text = "Số điện thoại hoặc mật khẩu không đúng.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi hệ thống: " + ex.Message;
            }
        }
        protected void lnkDangKy_Click(object sender, EventArgs e)
        {
            Response.Redirect("DANGKYY.aspx");
        }
    }
}
