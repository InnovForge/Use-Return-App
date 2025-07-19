using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Use_Return_App.Admin
{
    public partial class ChiTietSanPham : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["MaDoDung"] == null)
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
            Guid maSP = Guid.Parse(Session["MaDoDung"].ToString());
            Temp = maSP;

            string sql = "SELECT * FROM DoDung WHERE MaDoDung = @MaDoDung";

            SqlParameter[] parameters = new SqlParameter[]
            {
              new SqlParameter("@MaDoDung", maSP)
            };

            this.GridView1.DataSource = SqlHelper.ExecuteDataTable(sql, parameters);
            this.GridView1.DataBind();
        }

        //public void LoadDoDungEdit()
        //{
        //    String sql = "SELECT * FROM DoDung WHERE MaDoDung = '" + Temp + "'";
        //    this.GridView1.DataSource = SqlHelper.ExecuteDataTable(sql);
        //    this.GridView1.DataBind();
        //}

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1; // Đặt lại chế độ chỉnh sửa
            LoadDoDung();
        }

        public DataTable GetImages(string maDoDung)
        {
            string sql = "SELECT * FROM HinhAnhDoDung WHERE MaDoDung = @MaDoDung";
            SqlParameter[] parameters = { new SqlParameter("@MaDoDung", maDoDung) };
            return SqlHelper.ExecuteDataTable(sql, parameters);
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
                LoadDoDung();
            }
            else
            {
                Response.Write("<script>alert('Xóa thất bại');</script>");
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            LoadDoDung();
        }

        protected void rptImages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var row = (DataRowView)e.Item.DataItem;
                string duongDan = row["DuongDanAnh"].ToString();

                var img = (Image)e.Item.FindControl("Image1");
                if (img != null)
                {
                    if (duongDan.StartsWith("http://") || duongDan.StartsWith("https://"))
                        img.ImageUrl = duongDan;
                    else
                        img.ImageUrl = "~/ImageDoDung/" + duongDan;
                }
            }
        }


        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            TextBox txtMaDoDung = (TextBox)row.FindControl("TextBoxEditMaDoDung");
            TextBox txtTieuDe = (TextBox)row.FindControl("TextBoxEditTieuDe");
            TextBox txtMoTa = (TextBox)row.FindControl("TextBoxEditMoTa");
            TextBox txtGiaMoiNgay = (TextBox)row.FindControl("TextBoxEditGiaMoiNgay");
            TextBox txtSoLuong = (TextBox)row.FindControl("TextBoxEditSoLuong");
            TextBox txtTienCoc = (TextBox)row.FindControl("TextBoxEditTienCoc");
            DropDownList ddlTT = (DropDownList)row.FindControl("DropDownEditList");

            FileUpload fuNewImages = (FileUpload)row.FindControl("fuNewImages");
            Repeater rpt = (Repeater)row.FindControl("rptImagesEdit");

            foreach (RepeaterItem item in rpt.Items)
            {
                CheckBox chkDelete = (CheckBox)item.FindControl("chkDelete");
                HiddenField hfImageId = (HiddenField)item.FindControl("hfImageId");

                if (chkDelete != null && chkDelete.Checked)
                {
                    Guid id = Guid.Parse(hfImageId.Value);

                    // Lấy đường dẫn ảnh để xoá file vật lý
                    string getPathSql = "SELECT DuongDanAnh FROM HinhAnhDoDung WHERE MaHinh = @MaHinh";
                    string path = SqlHelper.ExecuteScalar<string>(getPathSql, new SqlParameter("@MaHinh", id));

                    if (!string.IsNullOrEmpty(path))
                    {
                        string fullPath = Server.MapPath("~/ImageDoDung" + path);
                        if (File.Exists(fullPath))
                        {
                            File.Delete(fullPath);
                        }
                    }

                    string delSql = "DELETE FROM HinhAnhDoDung WHERE MaHinh = @MaHinh";
                    SqlHelper.ExecuteNonQuery(delSql, new SqlParameter("@MaHinh", id));
                }
            }

            // int maDoDungValue = int.Parse(txtMaDoDung.Text.Trim());
            Guid maDoDungValue = Guid.Parse(txtMaDoDung.Text.Trim());
            string tieuDe = txtTieuDe.Text.Trim();
            string moTa = txtMoTa.Text.Trim();
            decimal giaMoiNgay = decimal.Parse(txtGiaMoiNgay.Text.Trim());
            int soLuong = int.Parse(txtSoLuong.Text.Trim());
            decimal tienCoc = decimal.Parse(txtTienCoc.Text.Trim());
            string trangThai = ddlTT.SelectedValue;

            if (fuNewImages.HasFiles)
            {
                foreach (HttpPostedFile file in fuNewImages.PostedFiles)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string folderPath = Server.MapPath("~/ImageDoDung/");

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string savePath = Path.Combine(folderPath, fileName);
                    file.SaveAs(savePath);

                    string insertSql = @"
                INSERT INTO HinhAnhDoDung (MaHinh, MaDoDung, DuongDanAnh)
                VALUES (@MaHinh, @MaDoDung, @Path)";
                    SqlHelper.ExecuteNonQuery(insertSql,
                        new SqlParameter("@MaHinh", Guid.NewGuid()),
                        new SqlParameter("@MaDoDung", maDoDungValue),
                        new SqlParameter("@Path", fileName)
                    );
                }
            }


            string sql = "UPDATE DoDung SET TieuDe = @TieuDe, MoTa = @MoTa, GiaMoiNgay = @GiaMoiNgay, SoLuong = @SoLuong, TienCoc = @TienCoc ,TrangThai = @TrangThai WHERE MaDoDung = @MaDoDung";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDoDung", maDoDungValue),
                new SqlParameter("@TieuDe", tieuDe),
                new SqlParameter("@MoTa", moTa),
                new SqlParameter("@GiaMoiNgay", giaMoiNgay),
                new SqlParameter("@SoLuong", soLuong),
                new SqlParameter("@TienCoc", tienCoc),
                new SqlParameter("@TrangThai", trangThai)
            };
            int kq = SqlHelper.ExecuteNonQuery(sql, parameters);
            if (kq > 0)
            {
                Response.Write("<script>alert('Cập nhật thành công');</script>");
                GridView1.EditIndex = -1; // Đặt lại chế độ chỉnh sửa
                LoadDoDung();
            }
            else
            {
                Response.Write("<script>alert('Cập nhật thất bại');</script>");
            }
        }

        protected void ButtonThem_Click(object sender, EventArgs e)
        {
            GridViewRow footerRow = GridView1.FooterRow;
            TextBox txtTieuDe = (TextBox)footerRow.FindControl("TextBoxAddTieuDe");
            TextBox txtMoTa = (TextBox)footerRow.FindControl("TextBoxAddMoTa");
            TextBox txtGiaMoiNgay = (TextBox)footerRow.FindControl("TextBoxAddGiaMoiNgay");
            TextBox txtSoLuong = (TextBox)footerRow.FindControl("TextBoxAddSoLuong");
            TextBox txtTienCoc = (TextBox)footerRow.FindControl("TextBoxAddTienCoc");
            DropDownList ddlTT = (DropDownList)footerRow.FindControl("DropDownAddList");

            string tieuDe = txtTieuDe.Text.Trim();
            string moTa = txtMoTa.Text.Trim();
            decimal giaMoiNgay = decimal.Parse(txtGiaMoiNgay.Text.Trim());
            int soLuong = int.Parse(txtSoLuong.Text.Trim());
            string trangThai = ddlTT.SelectedValue;
            decimal tienCoc = decimal.Parse(txtTienCoc.Text.Trim());
            string maDanhMuc = Session["MaDanhMuc"].ToString();

            string sql = "INSERT INTO DoDung (MaNguoiSoHuu ,TieuDe, MoTa, GiaMoiNgay, SoLuong,TienCoc, TrangThai,MaDanhMuc) VALUES (@MaNguoiSoHuu ,@TieuDe, @MoTa, @GiaMoiNgay, @SoLuong,@TienCoc, @TrangThai,@MaDanhMuc)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaNguoiSoHuu", "8ead7e11-07e3-401b-b36d-1daebc4ef028"),
                new SqlParameter("@TieuDe", tieuDe),
                new SqlParameter("@MoTa", moTa),
                new SqlParameter("@GiaMoiNgay", giaMoiNgay),
                new SqlParameter("@SoLuong", soLuong),
                new SqlParameter("@TienCoc", tienCoc),
                new SqlParameter("@TrangThai", trangThai),
                new SqlParameter("@MaDanhMuc", maDanhMuc),

            };
            int kq = SqlHelper.ExecuteNonQuery(sql, parameters);
            if (kq > 0)
            {
                // Lấy ID đồ dùng mới thêm
                string sqlGetNewId = "SELECT TOP 1 MaDoDung FROM DoDung WHERE TieuDe = @TieuDe ORDER BY NgayTao DESC";
                Guid maDoDungMoi = SqlHelper.ExecuteScalar<Guid>(sqlGetNewId, new SqlParameter("@TieuDe", tieuDe));

                // Lấy danh sách tất cả file được upload
                HttpFileCollection uploadedFiles = Request.Files;

                for (int i = 0; i < uploadedFiles.Count; i++)
                {
                    HttpPostedFile file = uploadedFiles[i];

                    if (file != null && file.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string savePath = Server.MapPath("~/ImageDoDung/" + fileName);
                        file.SaveAs(savePath);

                        string insertImgSql = @"INSERT INTO HinhAnhDoDung (MaHinh, MaDoDung, DuongDanAnh)
                                    VALUES (@MaHinh, @MaDoDung, @Path)";
                        SqlHelper.ExecuteNonQuery(insertImgSql,
                            new SqlParameter("@MaHinh", Guid.NewGuid()),
                            new SqlParameter("@MaDoDung", maDoDungMoi),
                            new SqlParameter("@Path", fileName));
                    }
                }
                Response.Write("<script>alert('Thêm thành công');</script>");
                LoadDoDung();
            }
            else
            {
                Response.Write("<script>alert('Thêm thất bại');</script>");

            }

        }

        protected void rptImages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}