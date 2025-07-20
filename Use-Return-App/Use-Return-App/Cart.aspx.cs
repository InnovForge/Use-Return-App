using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Use_Return_App
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCartData();
            }
        }

        private void BindCartData()
        {
            List<Dictionary<string, object>> cart = Session["Cart"] as List<Dictionary<string, object>> ?? new List<Dictionary<string, object>>();
            rptCart.DataSource = cart;
            rptCart.DataBind();
        }
    }
}