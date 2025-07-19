using Humanizer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using Use_Return_App.Helpers;

namespace Use_Return_App
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        public static List<Card> LoadCards(int page)
        {
            int pageSize = 12;
            int offset = page * pageSize;

            string sql = @"
                SELECT 
                dd.MaDoDung, 
                dd.TieuDe, 
                dd.MoTa, 
                dd.GiaMoiNgay, 
                dd.NgayTao, 
                ha.DuongDanAnh
            FROM DoDung dd
            OUTER APPLY (
                SELECT TOP 1 DuongDanAnh
                FROM HinhAnhDoDung
                WHERE HinhAnhDoDung.MaDoDung = dd.MaDoDung
                ORDER BY ThuTuHienThi ASC
            ) ha
            ORDER BY dd.NgayTao DESC
            OFFSET @offset ROWS
            FETCH NEXT @pageSize ROWS ONLY;";

            DataTable table = SqlHelper.ExecuteDataTable(sql,
                new SqlParameter("@offset", offset),
                new SqlParameter("@pageSize", pageSize));

            var list = new List<Card>();

            foreach (DataRow row in table.Rows)
            {
                string tieuDe = row.Field<string>("TieuDe");
                Guid id = row.Field<Guid>("MaDoDung");
                string slug = Utils.GenerateSlug(tieuDe);

                list.Add(new Card
                {
                    MaDoDung = id,
                    TieuDe = tieuDe,
                    LinkItemDetail = $"/{slug}?itid={id}",
                    MoTa = row.Field<string>("MoTa"),
                    GiaMoiNgay = row.Field<decimal>("GiaMoiNgay"),
                    NgayTao = row.Field<DateTime>("NgayTao"),
                    DuongDanAnh = row.IsNull("DuongDanAnh") ? null : row.Field<string>("DuongDanAnh")
                });
            }


            return list;
        }


    }


    public class Card
    {
        public Guid MaDoDung { get; set; }
        public string LinkItemDetail { get; set; }
        public string TieuDe { get; set; }
        public string MoTa { get; set; }
        public decimal GiaMoiNgay { get; set; }
        public DateTime NgayTao { get; set; }
        public string DuongDanAnh { get; set; }
        public string NgayTaoText => NgayTao.Humanize(false, DateTime.Now, new CultureInfo("vi"));
    }
}
