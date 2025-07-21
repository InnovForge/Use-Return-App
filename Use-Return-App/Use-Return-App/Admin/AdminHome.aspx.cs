using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Humanizer.In;

namespace Use_Return_App.Admin
{
    public partial class AdminHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MaVaiTro"] == null || Session["MaVaiTro"].ToString() != "2")
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}