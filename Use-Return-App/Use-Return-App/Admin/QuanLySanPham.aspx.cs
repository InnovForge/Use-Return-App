using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Use_Return_App.Admin
{
    public partial class QuanLySanPham : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session["MaDanhMuc"] == null)
                {
                    Response.Redirect("QuanLyDanhMuc.aspx");
                }
                LoadSanPham();
            }   
        }
        public void LoadSanPham()
        {
            string ma = Session["MaDanhMuc"].ToString();
            String sql = "SELECT * FROM DoDung WHERE MaDanhMuc = '" + ma + "'";
            this.GridView1.DataSource = SqlHelper.ExecuteDataTable(sql);
            this.GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "XemChiTiet")
            {
                Guid maDoDung = Guid.Parse(e.CommandArgument.ToString());

                Session["MaDoDung"] = maDoDung;
                Server.Transfer("~/Admin/ChiTietSanPham.aspx");
                //   Response.Redirect("~/Pages/XemChiTiet.aspx");
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}