using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Use_Return_App.Admin
{
    public partial class ChiTietSanPham : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Context.Items["MaDoDung"] == null)
                {
                    Response.Redirect("QuanLySanPham.aspx");
                }
                LoadDoDung();
            }
        }
        public void LoadDoDung()
        {
            string maSP = Context.Items["MaDoDung"].ToString();
            String sql = "SELECT * FROM DoDung WHERE MaDoDung = '" + maSP + "'";
            this.GridView1.DataSource = SqlHelper.ExecuteDataTable(sql);
            this.GridView1.DataBind();
        }
    }
}