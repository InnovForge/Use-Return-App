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

        }

        protected void lnkUsers_Click(object sender, EventArgs e)
        {
            Server.Transfer("QuanLyNguoiDung.aspx");
        }

        protected void lnkProducts_Click(object sender, EventArgs e)
        {
            Server.Transfer("QuanLySanPham.aspx");

        }

        protected void lnkOrders_Click(object sender, EventArgs e)
        {
            Server.Transfer("QuanLyDonHang.aspx");
        }

        protected void lnkReports_Click(object sender, EventArgs e)
        {
            Server.Transfer("ThongKe.aspx");

        }
    }
}