using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Use_Return_App.Admin
{
    public partial class QuanLyDonHang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session["UserID"] == null || Session["MaVaiTro"] == null || Session["MaVaiTro"].ToString() != "2")
                {
                    Response.Redirect("Login.aspx");
                }
                LoadDonHang();
            }
        }

        public void LoadDonHang()
        {
            string sql = "SELECT * FROM PhieuThue";
            GridView1.DataSource = SqlHelper.ExecuteDataTable(sql);
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ChiTiet")
            {
                //string MaPhieuThue = e.CommandArgument.ToString();
                //Session["MaPhieuThue"] = MaPhieuThue;
                //Server.Transfer("~/Admin/ChiTietPhieuThue.aspx");
                Guid MaPhieuThue = Guid.Parse(e.CommandArgument.ToString());
                Session["MaPhieuThue"] = MaPhieuThue;
                Server.Transfer("~/Admin/ChiTietPhieuThue.aspx");

            }
        }
    }
}