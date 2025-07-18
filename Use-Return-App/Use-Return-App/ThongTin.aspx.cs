using System;
using System.Data;
using System.Data.SqlClient;

namespace Use_Return_App
{
    public partial class ThongTin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("login.aspx");
                    return;
                }

                LoadThongTinNguoiDung(Session["UserID"].ToString());
            }
        }

        private void LoadThongTinNguoiDung(string maNguoiDung)
        {
            string sql = "SELECT HoTen, Email, SoDienThoai, NgayTao, DangHoatDong FROM NguoiDung WHERE MaNguoiDung = @id";
            DataTable dt = SqlHelper.ExecuteDataTable(sql, new SqlParameter("@id", maNguoiDung));

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                lblHoTen.Text = row["HoTen"].ToString();
                lblEmail.Text = row["Email"].ToString();
                lblSoDienThoai.Text = row["SoDienThoai"].ToString();

                if (DateTime.TryParse(row["NgayTao"].ToString(), out DateTime ngayTao))
                {
                    lblNgayTao.Text = ngayTao.ToString("dd/MM/yyyy");
                }

                bool dangHoatDong = Convert.ToBoolean(row["DangHoatDong"]);
                lblTrangThai.Text = dangHoatDong ? "Đang hoạt động" : "Đã khóa";
            }
        }
    }
}
