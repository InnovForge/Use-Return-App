using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Use_Return_App.Admin
{
    public partial class QuanLyDanhMuc : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session["UserID"] == null || Session["MaVaiTro"] == null || Session["MaVaiTro"].ToString() != "2")
                {
                    Response.Redirect("Login.aspx");
                }
                LoadDanhMuc();
            }

        }
        public void LoadDanhMuc()
        {
            String sql = "SELECT * FROM DanhMuc";
            DataList1.DataSource = SqlHelper.ExecuteDataTable(sql);
            DataList1.DataBind();
        }

        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "ChonDanhMuc")
            {
                string maDanhMuc = e.CommandArgument.ToString();
                Session["MaDanhMuc"] = maDanhMuc;
                Server.Transfer("~/Admin/QuanLySanPham.aspx");

            }
           
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Server.Transfer("AdminHome.aspx");

        }

        protected void LinkButton2_Command(object sender, CommandEventArgs e)
        {
              if (e.CommandName == "ThemDanhMuc")
            {
                Server.Transfer("~/Admin/CRUDDanhMuc.aspx");
            }
        }
    }
}