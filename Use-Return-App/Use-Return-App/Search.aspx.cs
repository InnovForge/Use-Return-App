using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace Use_Return_App
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string keyword = Request.QueryString["query"];
                if (!string.IsNullOrEmpty(keyword))
                {
                    LoadKetQua(keyword);
                }
            }
        }
        private void LoadKetQua(string keyword)
        {
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT TieuDe, MoTa, GiaMoiNgay, SoLuong, TinhTrang, TrangThai, NgayTao FROM DoDung WHERE TieuDe LIKE @keyword AND TrangThai = 'Available'";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    rptKetQua.DataSource = dt;
                    rptKetQua.DataBind();
                    ltKetQua.Text = $"<p>Có <strong>{dt.Rows.Count}</strong> kết quả cho từ khóa: <em>{keyword}</em></p>";
                }
                else
                {
                    rptKetQua.DataSource = null;
                    rptKetQua.DataBind();
                    ltKetQua.Text = $"<p class='text-danger'>❌ Không tìm thấy kết quả nào cho từ khóa: <em>{keyword}</em></p>";
                }
            }
        }
    }
}