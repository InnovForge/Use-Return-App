using Humanizer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Use_Return_App.Models;
using static Humanizer.On;

namespace Use_Return_App.Admin
{
    public partial class CRUDDanhMuc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["MaVaiTro"] == null ||  Session["MaVaiTro"].ToString() != "2")
                {
                    Response.Redirect("Login.aspx");
                }
                LoadDanhMuc();
            }

        }
        public void LoadDanhMuc()
        {
            String sql = "SELECT * FROM DanhMuc";
            GridView1.DataSource = SqlHelper.ExecuteDataTable(sql);
            GridView1.DataBind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            GridView1.EditIndex = -1; // Đặt lại chế độ chỉnh sửa
            LoadDanhMuc();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // int maDanhMuc = int.Parse(GridView1.DataKeys[e.RowIndex].Value);
            int maDanhMuc = (int)GridView1.DataKeys[e.RowIndex].Value;
            string sql = "DELETE FROM DanhMuc WHERE MaDanhMuc = @MaDanhMuc";
            SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@MaDanhMuc", maDanhMuc));
            LoadDanhMuc();

        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            LoadDanhMuc();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int maDanhMuc = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            string tenDanhMuc = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("TextBoxEditTenDanhMuc")).Text;
            string sql = "UPDATE DanhMuc SET TenDanhMuc = @TenDanhMuc WHERE MaDanhMuc = @MaDanhMuc";
            SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@TenDanhMuc", tenDanhMuc), new SqlParameter("@MaDanhMuc", maDanhMuc));
            GridView1.EditIndex = -1; // Đặt lại chế độ chỉnh sửa
            LoadDanhMuc();

        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            GridViewRow footerRow = GridView1.FooterRow;
            TextBox txtTenDanhMuc = (TextBox)footerRow.FindControl("TextBoxAddTenDanhMuc");

            string tenDanhMuc = txtTenDanhMuc.Text.Trim();
            if (string.IsNullOrEmpty(tenDanhMuc))
            {
                Label lbl = (Label)footerRow.FindControl("LabelLoi");
                lbl.Text = "Tên danh mục không được để trống.";
                return;
            }

            string sqlInsert = "INSERT INTO DanhMuc (TenDanhMuc) OUTPUT INSERTED.MaDanhMuc VALUES (@TenDanhMuc)";
            object maDanhMucMoi = SqlHelper.ExecuteScalar<string>(sqlInsert, new SqlParameter("@TenDanhMuc", tenDanhMuc));

            if (maDanhMucMoi != null)
            {
             //   Guid maNguoiSoHuu = Guid.Parse(Session["UserID"].ToString());

                string sqlInsertDoDung = @"
        INSERT INTO DoDung (TieuDe, MoTa, MaDanhMuc, MaNguoiSoHuu, GiaMoiNgay, TienCoc, SoLuong, TinhTrang)
        VALUES (@TieuDe, @MoTa, @MaDanhMuc, @MaNguoiSoHuu, @GiaMoiNgay, @TienCoc, @SoLuong, @TinhTrang)";

                SqlHelper.ExecuteNonQuery(sqlInsertDoDung,
                    new SqlParameter("@TieuDe", "Đồ dùng mẫu"),
                    new SqlParameter("@MoTa", "Đây là mô tả của đồ dùng mẫu"),
                    new SqlParameter("@MaDanhMuc", maDanhMucMoi),
                    new SqlParameter("@MaNguoiSoHuu", "8ead7e11-07e3-401b-b36d-1daebc4ef028"),
                    new SqlParameter("@GiaMoiNgay", 10000),
                    new SqlParameter("@TienCoc", 20000),
                    new SqlParameter("@SoLuong", 1),
                    new SqlParameter("@TinhTrang", "Mới"));
            }

            txtTenDanhMuc.Text = string.Empty;
            LoadDanhMuc();
        }

    }
}