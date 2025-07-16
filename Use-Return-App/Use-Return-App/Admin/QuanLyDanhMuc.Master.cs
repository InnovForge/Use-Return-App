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
                Context.Items["maDanhMuc"] = maDanhMuc;
                Server.Transfer("QuanLySanPham.aspx");
            }
        }
    }
}