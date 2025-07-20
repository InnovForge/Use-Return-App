using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Data.SqlClient;

namespace Use_Return_App
{
    public partial class DanhSachDoDung : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDanhSachDoDung();
            }
        }

        private void LoadDanhSachDoDung()
        {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string query = "SELECT MaDoDung, TieuDe, MoTa, GiaMoiNgay FROM DoDung WHERE TrangThai = 'Available'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                rptDoDung.DataSource = reader;
                rptDoDung.DataBind();
            }
        }

        protected void btnLuu_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            if (Session["MaNguoiDung"] != null)
            {
                Guid maNguoiDung = Guid.Parse(Session["MaNguoiDung"].ToString());
                Guid maDoDung = Guid.Parse(e.CommandArgument.ToString());

                string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Kiểm tra xem đã lưu chưa
                    string checkSql = "SELECT COUNT(*) FROM YeuThich WHERE MaNguoiDung = @MaND AND MaDoDung = @MaDD";
                    SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                    checkCmd.Parameters.AddWithValue("@MaND", maNguoiDung);
                    checkCmd.Parameters.AddWithValue("@MaDD", maDoDung);
                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists == 0)
                    {
                        string insertSql = "INSERT INTO YeuThich (MaNguoiDung, MaDoDung) VALUES (@MaND, @MaDD)";
                        SqlCommand insertCmd = new SqlCommand(insertSql, conn);
                        insertCmd.Parameters.AddWithValue("@MaND", maNguoiDung);
                        insertCmd.Parameters.AddWithValue("@MaDD", maDoDung);
                        insertCmd.ExecuteNonQuery();
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Đã lưu đồ dùng!');", true);
            }
            else
            {
                Response.Redirect("DANGNHAP.aspx");
            }
        }
    }
}
