using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Use_Return_App
{
    public partial class dangky : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            string matKhau = txtPassword.Text.Trim();
            string soDienThoai = txtPhone.Text.Trim();
            int maVaiTro = int.Parse(ddlVaiTro.SelectedValue);

            if (maVaiTro == 0)
            {
                lblMessage.Text = "Vui lòng chọn vai trò.";
                return;
            }

            string matKhauHash = BCrypt.Net.BCrypt.HashPassword(matKhau);
            Guid maNguoiDung = Guid.NewGuid();

            string sql = @"
                INSERT INTO NguoiDung (MaNguoiDung, MaVaiTro, HoTen, Email, SoDienThoai, MatKhauHash, DangHoatDong, NgayTao)
                VALUES (@id, @role, @name, @email, @phone, @hash, 1, GETDATE())";

            try
            {
                int rows = SqlHelper.ExecuteNonQuery(sql,
                    new SqlParameter("@id", maNguoiDung),
                    new SqlParameter("@role", maVaiTro),
                    new SqlParameter("@name", hoTen),
                    new SqlParameter("@email", email),
                    new SqlParameter("@phone", soDienThoai),
                    new SqlParameter("@hash", matKhauHash)
                );

                if (rows > 0)
                {
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    lblMessage.Text = "Đăng ký thành công! Mời bạn đăng nhập.";
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    lblMessage.Text = "Đăng ký thất bại.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Lỗi: " + ex.Message;
            }
        }
    }
}
