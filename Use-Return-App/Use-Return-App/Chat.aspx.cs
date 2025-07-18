using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Use_Return_App
{
    public partial class Chat : System.Web.UI.Page
    {
    
        protected string ToUserAvatar { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userId"] == null)
                {
                    Response.Redirect("~/DANGNHAP.aspx");
                }

               string userId = Page.RouteData.Values["userId"] as string;

                if (!string.IsNullOrEmpty(userId))
                {
                   
                    LoadUserInfo(userId);
                    Guid user = Guid.Parse(Session["userId"].ToString());
                    var dt = GetConversationList(user);

             
                    rptConversations.DataSource = dt;
                    rptConversations.DataBind();
                }
            }
        }
        private void LoadUserInfo(string userId)
        {
            string sql = @"SELECT HoTen, AnhDaiDien, DangHoatDong, NgayTao
                   FROM NguoiDung
                   WHERE MaNguoiDung = @id";

            using (var conn = new SqlConnection(/* your connection string */))
            using (var reader = SqlHelper.ExecuteReader(sql, new SqlParameter("@id", userId)))
            {
                if (reader.Read())
                {
                    string hoTen = reader["HoTen"]?.ToString();
                    string anh = string.IsNullOrEmpty(reader["AnhDaiDien"]?.ToString())
                        ? "https://placehold.co/600x400/green/white?text=avatar"
                        : reader["AnhDaiDien"].ToString();
                    bool dangOnline = Convert.ToBoolean(reader["DangHoatDong"]);

                    // Gán ra HTML
                    TenNguoiDung.InnerText = hoTen;
                    AvatarNguoiDung.Src = anh;
                    TrangThai.InnerText = dangOnline ? "Đang hoạt động" : "Hoạt động trước đó";
                    ToUserAvatar = anh;
                }
            }
        }

     

        public static DataTable GetConversationList(Guid currentUserId)
        {
            string sql = @"
        WITH AllConversations AS (
            SELECT 
                MessageId,
                SentTime,
                Content,
                SenderId,
                ReceiverId,
                CASE 
                    WHEN SenderId = @UserId THEN ReceiverId
                    ELSE SenderId
                END AS OtherUserId
            FROM Message
            WHERE SenderId = @UserId OR ReceiverId = @UserId
        ),
        LatestMessages AS (
            SELECT *,
                   ROW_NUMBER() OVER (PARTITION BY OtherUserId ORDER BY SentTime DESC) AS rn
            FROM AllConversations
        )
        SELECT 
            u.MaNguoiDung AS UserId,
            u.HoTen,
            u.AnhDaiDien,
            m.Content AS LastMessage,
            m.SentTime AS LastTime
        FROM LatestMessages m
        JOIN NguoiDung u ON u.MaNguoiDung = m.OtherUserId
        WHERE m.rn = 1
        ORDER BY m.SentTime DESC;
    ";

            return SqlHelper.ExecuteDataTable(sql, new SqlParameter("@UserId", currentUserId));
        }

        [WebMethod]
        public static List<MessageDto> LoadMessages(Guid currentUser, Guid otherUser, int skip, int take)
        {
            return GetMessagesBetweenUsers(currentUser, otherUser, skip, take);
        }

        public static List<MessageDto> GetMessagesBetweenUsers(Guid user1, Guid user2, int skip, int take)
        {
            var result = new Dictionary<Guid, MessageDto>();

            string sql = $@"
        SELECT * FROM (
            SELECT 
                m.MessageId, m.SenderId, m.ReceiverId, m.Content, m.SentTime, m.IsRead,
                a.AttachmentId, a.FileType, a.FileUrl, a.FileName, a.UploadedAt,
                ROW_NUMBER() OVER (ORDER BY m.SentTime DESC) AS RowNum
            FROM Message m
            LEFT JOIN Attachment a ON m.MessageId = a.MessageId
            WHERE 
                (m.SenderId = @user1 AND m.ReceiverId = @user2)
                OR
                (m.SenderId = @user2 AND m.ReceiverId = @user1)
        ) AS MessagesWithRowNum
        WHERE RowNum > @skip AND RowNum <= @skip + @take
        ORDER BY RowNum";

            using (var reader = SqlHelper.ExecuteReader(sql,
                new SqlParameter("@user1", user1),
                new SqlParameter("@user2", user2),
                new SqlParameter("@skip", skip),
                new SqlParameter("@take", take)))
            {
                while (reader.Read())
                {
                    Guid messageId = reader.GetGuid(0);
                    if (!result.ContainsKey(messageId))
                    {
                        result[messageId] = new MessageDto
                        {
                            MessageId = messageId,
                            SenderId = reader.GetGuid(1),
                            ReceiverId = reader.GetGuid(2),
                            Content = reader.GetString(3),
                            SentTime = reader.GetDateTime(4),
                            IsRead = reader.GetBoolean(5),
                            Attachments = new List<AttachmentDto>()
                        };
                    }

                    if (!reader.IsDBNull(6))
                    {
                        result[messageId].Attachments.Add(new AttachmentDto
                        {
                            AttachmentId = reader.GetGuid(6),
                            FileType = reader.IsDBNull(7) ? null : reader.GetString(7),
                            FileUrl = reader.IsDBNull(8) ? null : reader.GetString(8),
                            FileName = reader.IsDBNull(9) ? null : reader.GetString(9),
                            UploadedAt = reader.IsDBNull(10) ? DateTime.MinValue : reader.GetDateTime(10)
                        });
                    }
                }
            }

            return result.Values.OrderBy(m => m.SentTime).ToList(); // sắp lại theo thời gian tăng
        }


        public class MessageDto
        {
            public Guid MessageId { get; set; }
            public Guid SenderId { get; set; }
            public Guid ReceiverId { get; set; }
            public string Content { get; set; }
            public DateTime SentTime { get; set; }
            public bool IsRead { get; set; }
            public List<AttachmentDto> Attachments { get; set; } = new List<AttachmentDto>();
        }

        public class AttachmentDto
        {
            public Guid AttachmentId { get; set; }
            public string FileType { get; set; }
            public string FileUrl { get; set; }
            public string FileName { get; set; }
            public DateTime UploadedAt { get; set; }
        }

    }
}