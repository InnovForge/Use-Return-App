using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Use_Return_App.Admin
{
    public partial class Dashboard : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["MaVaiTro"].ToString() != "2")
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        protected void lnkUsers_Click(object sender, EventArgs e)
        {
            Server.Transfer("QuanLyNguoiDung.aspx");
        }

        protected void lnkProducts_Click(object sender, EventArgs e)
        {
            Server.Transfer("AdminDanhMuc.aspx");

        }

        protected void lnkOrders_Click(object sender, EventArgs e)
        {
            Server.Transfer("QuanLyPhieuThue.aspx");
        }

        protected void lnkReports_Click(object sender, EventArgs e)
        {
            Server.Transfer("ThongKe.aspx");

        }

        protected void LinkInfor_Click(object sender, EventArgs e)
        {
            Server.Transfer("ThongTinAdmin.aspx");

        }
    }
}