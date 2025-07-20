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
    public partial class YeuThich : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && Session["MaNguoiDung"] != null)
            {
                Guid maNguoiDung = Guid.Parse(Session["MaNguoiDung"].ToString());
                string connStr = ConfigurationManager.ConnectionStrings["YourConnectionStringName"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string sql = @"
                        SELECT dd.TieuDe, dd.MoTa, dd.GiaMoiNgay
                        FROM YeuThich yt
                        JOIN DoDung dd ON yt.MaDoDung = dd.MaDoDung
                        WHERE yt.MaNguoiDung = @MaND";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@MaND", maNguoiDung);
                    SqlDataReader reader = cmd.ExecuteReader();
                    rptYeuThich.DataSource = reader;
                    rptYeuThich.DataBind();
                }
            }
            else if (Session["MaNguoiDung"] == null)
            {
                Response.Redirect("DANGNHAP.aspx");
            }
        }
    }
}
