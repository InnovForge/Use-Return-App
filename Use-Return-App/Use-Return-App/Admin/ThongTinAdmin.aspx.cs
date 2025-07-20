using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Use_Return_App.Admin
{
    public partial class ThongTinAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null || Session["MaVaiTro"] == null || Session["MaVaiTro"].ToString() != "2")
                {
                    Response.Redirect("Login.aspx");
                }
                infor();
            }
        }

        public void infor()
        {
            Guid id = Guid.Parse(Session["UserID"].ToString());
            string sql = "SELECT * FROM NguoiDung Where MaNguoiDung = @id";
            var table = SqlHelper.ExecuteDataTable(sql, new SqlParameter("@id", id));
            var row = table.Rows[0];
            lblHoTen.Text = row["HoTen"].ToString();
            lblEmail.Text = row["Email"].ToString();
            lblPhone.Text = row["SoDienThoai"].ToString();
            lblCreatedDate.Text = row["NgayTao"].ToString();
            string duongDan = row["AnhDaiDien"].ToString();
            if (duongDan.StartsWith("http://") || duongDan.StartsWith("https://"))
            {
                imgAvatar.ImageUrl = duongDan;  
            }
            else
            {
                imgAvatar.ImageUrl = "~/ImageUsers/" + row["AnhDaiDien"].ToString();
            }
        }


        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/Admin/EditThongTinAdmin.aspx");
        }
    }
}