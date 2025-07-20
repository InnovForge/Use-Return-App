using System;
using System.Data;
using System.Data.SqlClient;

namespace Use_Return_App
{
    public partial class lichsu : System.Web.UI.Page
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

                LoadLichSuThue(Session["UserID"].ToString());
            }
        }

        private void LoadLichSuThue(string maNguoiDung)
        {
            string sql = @"
                SELECT dd.TieuDe, pt.NgayBatDau, pt.NgayKetThuc, DATEDIFF(DAY, pt.NgayBatDau, pt.NgayKetThuc) AS SoNgay,  pt.TongTien, pt.TrangThai
                FROM PhieuThue pt
                INNER JOIN DoDung dd ON pt.MaDoDung = dd.MaDoDung
                WHERE pt.MaNguoiThue = @id
                ORDER BY pt.NgayBatDau DESC";

            DataTable dt = SqlHelper.ExecuteDataTable(sql, new SqlParameter("@id", maNguoiDung));

            gvLichSu.DataSource = dt;
            gvLichSu.DataBind();
        }
    }
}
