using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Use_Return_App.Admin
{
    public partial class ChiTietHoaDon : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["MaVaiTro"].ToString() != "2")
                {
                    Response.Redirect("Login.aspx");
                }
                if (Session["MaPhieuThue"] == null)
                {
                    Response.Redirect("QuanLyPhieuThue.aspx");
                }
                LoadPhieuThue();
            }
        }

        public void LoadPhieuThue()
        {
            Guid MaPhieuThue = Guid.Parse(Session["MaPhieuThue"].ToString());

            string sql = "SELECT * FROM PhieuThue WHERE MaPhieuThue = @MaPhieuThue";

            SqlParameter[] parameters = new SqlParameter[]
            {
              new SqlParameter("@MaPhieuThue", MaPhieuThue)
            };

            this.GridView1.DataSource = SqlHelper.ExecuteDataTable(sql, parameters);
            this.GridView1.DataBind();
        }
    }
}