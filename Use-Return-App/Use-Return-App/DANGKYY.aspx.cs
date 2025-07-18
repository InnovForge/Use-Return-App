using System;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace Use_Return_App
{
    public partial class DANGKYY : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtHoTen.Attributes["autocomplete"] = "off";
                txtEmail.Attributes["autocomplete"] = "off";
                txtPassword.Attributes["autocomplete"] = "new-password";
                txtConfirmPassword.Attributes["autocomplete"] = "new-password";
                txtPhone.Attributes["autocomplete"] = "off";
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            string matKhau = txtPassword.Text.Trim();
            string xacNhan = txtConfirmPassword.Text.Trim();
            string soDienThoai = txtPhone.Text.Trim();
            int maVaiTro = 1; // Người dùng mặc định

            if (string.IsNullOrEmpty(hoTen))
            {
                lblMessage.Text = "Vui lòng nhập họ tên.";
                return;
            }

            if (string.IsNullOrEmpty(email))
            {
                lblMessage.Text = "Vui lòng nhập email.";
                return;
            }
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                lblMessage.Text = "Email không đúng định dạng.";
                return;
            }
            if (string.IsNullOrEmpty(matKhau) || string.IsNullOrEmpty(xacNhan))
            {
                lblMessage.Text = "Vui lòng nhập mật khẩu và xác nhận.";
                return;
            }

            if (matKhau != xacNhan)
            {
                lblMessage.Text = "Mật khẩu xác nhận không khớp.";
                return;
            }
            if (string.IsNullOrEmpty(soDienThoai))
            {
                lblMessage.Text = "Vui lòng nhập số điện thoại.";
                return;
            }

            string sqlCheckEmail = "SELECT COUNT(*) FROM NguoiDung WHERE Email = @Email";
            int emailCount = SqlHelper.ExecuteScalar<int>(sqlCheckEmail, new SqlParameter("@Email", email));

            if (emailCount > 0)
            {
                lblMessage.Text = "Email đã được đăng ký.";
                return;
            }

            string sqlCheckPhone = "SELECT COUNT(*) FROM NguoiDung WHERE SoDienThoai = @Phone";
            int phoneCount = SqlHelper.ExecuteScalar<int>(sqlCheckPhone, new SqlParameter("@Phone", soDienThoai));

            if (phoneCount > 0)
            {
                lblMessage.Text = "Số điện thoại đã được đăng ký.";
                return;
            }


            string matKhauHash = BCrypt.Net.BCrypt.HashPassword(matKhau);
            Guid maNguoiDung = Guid.NewGuid();

            string sql = @"
                INSERT INTO NguoiDung 
                (MaNguoiDung, MaVaiTro, HoTen, Email, SoDienThoai, MatKhauHash, DangHoatDong, NgayTao)
                VALUES 
                (@id, @role, @name, @email, @phone, @hash, 1, GETDATE())";

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
                    Response.Redirect("DANGNHAP.aspx");
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
