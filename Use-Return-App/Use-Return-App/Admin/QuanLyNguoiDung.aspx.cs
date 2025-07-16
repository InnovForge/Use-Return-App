using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Use_Return_App.Admin
{
    public partial class QuanLyNguoiDung : System.Web.UI.Page
    {
        const string sql = @"
             SELECT  u.MaNguoiDung,
                     u.HoTen,
                     u.AnhDaiDien,
                     u.Email,
                     u.MatKhauHash,
                     u.SoDienThoai,
                     u.NgayTao,
                     u.DangHoatDong,
                     v.TenVaiTro,
                     u.MaVaiTro 
        FROM    NguoiDung  u
        JOIN    VaiTro     v ON u.MaVaiTro = v.MaVaiTro
        WHERE   u.MaVaiTro = 1";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUsers();
            }
        }

        public void LoadUsers()
        {
           
            GridView1.DataSource = SqlHelper.ExecuteDataTable(sql);
            GridView1.DataBind();
        }

        protected string ShortHash(object hashObj)
        {
            var h = hashObj as string ?? "";
            return h.Length <= 10 ? h : $"{h.Substring(0, 7)}…";   // $2y$10…
        }

        protected string ShortGuid(object guidObj)
        {
            var g = guidObj?.ToString() ?? "";
            return g.Length <= 8 ? g : g.Substring(0, 7) + "…";
        }



        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string madm = e.Values["MaNguoiDung"].ToString();

            int kq = SqlHelper.ExecuteNonQuery("DELETE FROM NguoiDung WHERE MaNguoiDung = '" + madm + "'");
            if (kq > 0)
            {
                Response.Write("<script>alert('Xóa thành công');</script>");
                this.GridView1.DataSource = SqlHelper.ExecuteDataTable(sql);
                this.GridView1.DataBind();
            }
            else
            {
                Response.Write("<script>alert('Xóa thất bại');</script>");
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            this.GridView1.DataSource = SqlHelper.ExecuteDataTable(sql);
            this.GridView1.DataBind();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string maND = e.NewValues["MaNguoiDung"].ToString();

            TextBox txtHoTen = (TextBox)row.FindControl("EditHoTen"); ;
            TextBox txtEmail = (TextBox)row.FindControl("EditEmail");
            TextBox txtSoDienThoai = (TextBox)row.FindControl("EditPhone");
      //      TextBox txtMaKhau = (TextBox)GridView1.FooterRow.FindControl("EditPass");
            DropDownList ddlRole = (DropDownList)GridView1.FooterRow.FindControl("ddlRole");
            FileUpload fuAvatar = (FileUpload)row.FindControl("fuAvatarEdit");
            HiddenField hfOld = (HiddenField)row.FindControl("hfOldAvatar");
            string oldFileName = hfOld.Value;     // ảnh hiện tại trong DB
            string newFileName = oldFileName;     // mặc định: giữ nguyên

            if (fuAvatar.HasFile)
            {
                string fileName = Path.GetFileName(fuAvatar.FileName);
                string folder = Server.MapPath("~/ImageUsers/");
                string path = Path.Combine(folder, fileName);
                int counter = 1;
                string name = Path.GetFileNameWithoutExtension(fileName);
                string ext = Path.GetExtension(fileName);

                while (File.Exists(path))
                {
                    fileName = $"{name}-{counter}{ext}";
                    path = Path.Combine(folder, fileName);
                    counter++;
                }

                fuAvatar.SaveAs(path);
                newFileName = fileName;

                if (!string.IsNullOrEmpty(oldFileName))
                {
                    string oldPath = Path.Combine(folder, oldFileName);
                    if (File.Exists(oldPath)) File.Delete(oldPath);
                }
            }


            string tenND = txtHoTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtSoDienThoai.Text.Trim();
         //   string pass = txtMaKhau.Text.Trim();
            int roleId = int.Parse(ddlRole.SelectedValue);

            string sql = "UPDATE NguoiDung SET HoTen = N'" + tenND + "', Email = N'"+ email + "',AnhDaiDien = '"+(object)newFileName+"', SoDienThoai = '"+ phone + "',MaVaiTro = '"+roleId+"'  WHERE MaNguoiDung = '" + maND + "'";

            int kq = SqlHelper.ExecuteNonQuery(sql);
            if (kq > 0)
            {
                Response.Write("<script>alert('Cập nhật thành công');</script>");
                GridView1.EditIndex = -1; // Đặt lại chế độ chỉnh sửa
                this.GridView1.DataSource = SqlHelper.ExecuteDataTable(sql);
                this.GridView1.DataBind();
            }
            else
            {
                Response.Write("<script>alert('Cập nhật thất bại');</script>");
            }




        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1; // Đặt lại chế độ chỉnh sửa
            this.GridView1.DataSource = SqlHelper.ExecuteDataTable(sql);
            this.GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TextBox txtHoTen = (TextBox)GridView1.FooterRow.FindControl("txtHoTen");
            TextBox txtEmail = (TextBox)GridView1.FooterRow.FindControl("txtEmail");
            TextBox txtSoDienThoai = (TextBox)GridView1.FooterRow.FindControl("txtPhone");
            TextBox txtMaKhau = (TextBox)GridView1.FooterRow.FindControl("txtPassword");
            DropDownList ddlRole = (DropDownList)GridView1.FooterRow.FindControl("DropDownList1");
            FileUpload fuAvt = (FileUpload)GridView1.FooterRow.FindControl("fuAvatar");

            string fileName = null;
            if (fuAvt.HasFile)
            {
                string ext = Path.GetExtension(fuAvt.FileName).ToLower();
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                {
                    fileName = Guid.NewGuid() + ext;
                    string path = Server.MapPath("~/ImageUsers/") + fileName;
                    fuAvt.SaveAs(path);
                }
                else
                {
                    Response.Write("<script>alert('Chỉ chấp nhận JPG/PNG');</script>");
                    return;
                }
            }


            string tenND = txtHoTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtSoDienThoai.Text.Trim();
            string pass = txtMaKhau.Text.Trim();
            int roleId = int.Parse(ddlRole.SelectedValue);


            int kq = SqlHelper.ExecuteNonQuery("INSERT INTO NguoiDung( HoTen, Email,AnhDaiDien ,SoDienThoai,MatKhauHash,MaVaiTro) VALUES (N'" + tenND + "', N'"+email+"','"+fileName+"', N'"+phone+"', N'"+pass+"','"+roleId+"')");

            if (kq > 0)
            {
                Response.Write("<script>alert('Thêm thành công');</script>");
                this.GridView1.DataSource = SqlHelper.ExecuteDataTable(sql);
                this.GridView1.DataBind();
            }
            else
            {
                Response.Write("<script>alert('Thêm thất bại');</script>");
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}