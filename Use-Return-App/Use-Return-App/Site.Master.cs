using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Use_Return_App.Helpers;

namespace Use_Return_App
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string currentPath = Request.Url.AbsolutePath.ToLower();

                if (currentPath == "/" || currentPath.EndsWith("default.aspx"))
                {
                    liHome.Attributes["class"] += " active";
                }
                else if (currentPath.Contains("/about"))
                {
                    liAbout.Attributes["class"] += " active";
                }

                CartBadge.InnerText = CookieCartHelper.LoadCartFromCookie(Request).Count.ToString();
            }
        }
    }
}