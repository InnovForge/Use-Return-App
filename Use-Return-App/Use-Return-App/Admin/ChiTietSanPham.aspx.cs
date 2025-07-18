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

        public Guid Temp
        {
            get { return ViewState["Temp"] != null ? (Guid)ViewState["Temp"] : Guid.Empty; }
            set { ViewState["Temp"] = value; }
        }

        public void LoadDoDung()
        {
            Guid maSP = Guid.Parse(Context.Items["MaDoDung"].ToString());
            Temp = maSP;
            String sql = "SELECT * FROM DoDung WHERE MaDoDung = '" + maSP + "'";
            this.GridView1.DataSource = SqlHelper.ExecuteDataTable(sql);
            this.GridView1.DataBind();
        }
        public void LoadDoDungEdit()
        {
            String sql = "SELECT * FROM DoDung WHERE MaDoDung = '" + Temp + "'";
            this.GridView1.DataSource = SqlHelper.ExecuteDataTable(sql);
            this.GridView1.DataBind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1; // Đặt lại chế độ chỉnh sửa
           LoadDoDungEdit();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string madm = e.Values["MaDoDung"].ToString();

            int kq = SqlHelper.ExecuteNonQuery("DELETE FROM DoDung WHERE MaDoDung = '" + madm + "'");
            if (kq > 0)
            {
                Response.Write("<script>alert('Xóa thành công');</script>");
                //this.GridView1.DataSource = SqlHelper.ExecuteDataTable(sql);
                //this.GridView1.DataBind();
                LoadDoDungEdit();
            }
            else
            {
                Response.Write("<script>alert('Xóa thất bại');</script>");
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
             LoadDoDungEdit();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }
    }
}