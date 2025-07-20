using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Use_Return_App.Helpers;

namespace Use_Return_App
{
    public partial class Item : System.Web.UI.Page
    {
        protected string SoDienThoai { get; set; }
        protected string SoDienThoaiAn
        {
            get
            {
                if (string.IsNullOrEmpty(SoDienThoai) || SoDienThoai.Length < 3)
                    return SoDienThoai;

                return new string('*', SoDienThoai.Length - 3) + SoDienThoai.Substring(SoDienThoai.Length - 3);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["itid"];

                if (Guid.TryParse(id, out Guid itemId))
                {
                   
                    var images = getImages(itemId); 

                    rptMainImages.DataSource = images;
                    rptMainImages.DataBind();

                    rptThumbImages.DataSource = images;
                    rptThumbImages.DataBind();

                    var row = GetChiTietDoDung(itemId);
    
                    if (row != null)
                    {
                        lblTieuDe.Text = row["TieuDe"].ToString();
                        litMoTa.Text = row["MoTa"].ToString();
                        lblGiaMoiNgay.Text = Convert.ToDecimal(row["GiaMoiNgay"]).ToString("N0", new System.Globalization.CultureInfo("vi-VN") { NumberFormat = { NumberGroupSeparator = "." } }) + " đ";
                        lblTienCoc.Text = Convert.ToDecimal(row["TienCoc"]).ToString("N0", new System.Globalization.CultureInfo("vi-VN") { NumberFormat = { NumberGroupSeparator = "." } }) + " đ";
                        lnkMessage.HRef = "/messages/u/" + row["MaNguoiSoHuu"];
                        SoDienThoai = row["SoDienThoai"].ToString();
                        //lblTinhTrang.Text = row["TinhTrang"].ToString();
                        //lblTrangThai.Text = row["TrangThai"].ToString();
                        //lblNgayTao.Text = ((DateTime)row["NgayTao"]).ToString("dd/MM/yyyy");

                        //lblNguoiSoHuu.Text = row["TenNguoiSoHuu"].ToString();
                        //lblEmail.Text = row["Email"].ToString();
                        //imgDaiDienNguoiDung.ImageUrl = row["AnhDaiDien"].ToString();
                        //lblTenDanhMuc.Text = row["TenDanhMuc"].ToString();
                    }
                    else
                    {
                        Response.Redirect("~/404.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/404.aspx");
                }
         
            }
        }

        protected void rptMainImages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var row = (DataRowView)e.Item.DataItem;
                string duongDan = row["DuongDanAnh"].ToString();

                var img = (HtmlImage)e.Item.FindControl("imgMain");
                if (img != null)
                {
                    if (duongDan.StartsWith("http://") || duongDan.StartsWith("https://"))
                    {
                        img.Src = duongDan; 
                    }
                    else
                    {
                        img.Src = "/ImageDoDung/" + duongDan;
                    }
                }
            }
        }


        private DataTable getImages(Guid maDoDung)
        {
            string sql = "SELECT DuongDanAnh FROM HinhAnhDoDung WHERE MaDoDung = @id ORDER BY ThuTuHienThi";
            return SqlHelper.ExecuteDataTable(sql, new SqlParameter("@id", maDoDung));
        }

        private DataRow GetChiTietDoDung(Guid id)
        {
            string sql = @"
        SELECT 
            dd.MaDoDung,
            dd.TieuDe,
            dd.MoTa,
            dd.GiaMoiNgay,
            dd.TienCoc,
            dd.SoLuong,
            dd.TinhTrang,
            dd.NgayTao,
            dd.TrangThai,
            nd.HoTen AS TenNguoiSoHuu,
            nd.Email,
            nd.AnhDaiDien,
            dm.TenDanhMuc,
            dd.MaNguoiSoHuu,
            nd.SoDienThoai
        FROM DoDung dd
        JOIN NguoiDung nd ON dd.MaNguoiSoHuu = nd.MaNguoiDung
        JOIN DanhMuc dm ON dd.MaDanhMuc = dm.MaDanhMuc
        WHERE dd.MaDoDung = @id";

            var table = SqlHelper.ExecuteDataTable(sql, new SqlParameter("@id", id));
            return table.Rows.Count > 0 ? table.Rows[0] : null;
        }


        protected void btnThueNgay_Click(object sender, EventArgs e)
        {
            string id = Request.QueryString["itid"];
            if (!string.IsNullOrEmpty(id))
            {
                Response.Redirect($"checkout?itid={id}");
            }
            else
            {
                Response.Redirect("~/404.aspx");
            }
        }

        protected void btnAddToCart_onClick(object sender, EventArgs e)
        {
            var cart = CookieCartHelper.LoadCartFromCookie(Request);
            var random = new Random();
            var newItem = new CartItem
            {
                ProductId = "P00_" + random.Next(1000, 9999),
                ProductName = "Sản phẩm A",
                Quantity = 1,
                Price = 100000
            };

            var existing = cart.FirstOrDefault(x => x.ProductId == newItem.ProductId);
            if (existing != null)
            {
                existing.Quantity += newItem.Quantity;
            }
            else
            {
                cart.Add(newItem);
            }

            CookieCartHelper.SaveCartToCookie(cart, Response);
        }

        [WebMethod]
        public static int AddToCart(string productId)
        {
            productId = new Random().Next(1000, 9999).ToString();
            var context = HttpContext.Current;

            var cart = CookieCartHelper.LoadCartFromCookie(context.Request);
         
            var existing = cart.FirstOrDefault(x => x.ProductId == productId);
            if (existing != null)
            {
                existing.Quantity += 1;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = "Tên sản phẩm demo",
                    Quantity = 1,
                    Price = 100000
                });
            }

            CookieCartHelper.SaveCartToCookie(cart, context.Response);

            return cart.Count;
        }

    }
    public class HinhAnhDoDung
    {
        public string DuongDanAnh { get; set; }
        public string TieuDe { get; set; }
    }

}