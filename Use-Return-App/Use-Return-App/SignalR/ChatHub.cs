using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;
using System.Data.SqlClient;

namespace Use_Return_App.SignalR
{
    public class ChatHub: Hub
    {

        private static ConcurrentDictionary<string, string> ConnectedUsers = new ConcurrentDictionary<string, string>();

        public override System.Threading.Tasks.Task OnConnected()
        {

            var userId = Context.QueryString["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                ConnectedUsers[userId] = Context.ConnectionId;
                SqlHelper.UpdateUserOnlineStatus(Guid.Parse(userId), true);
            }
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var userId = GetUserByConnectionId(Context.ConnectionId);
            if (userId != null)
            {
                ConnectedUsers.TryRemove(userId, out _);
                SqlHelper.UpdateUserOnlineStatus(Guid.Parse(userId), false); 
            }
            return base.OnDisconnected(stopCalled);
        }

        private string GetUserByConnectionId(string connectionId)
        {
            foreach (var pair in ConnectedUsers)
            {
                if (pair.Value == connectionId)
                    return pair.Key;
            }
            return null;
        }


        public void SendToUser(string toUserId, string fromUser, string message)
        {
            
            string sql = @"INSERT INTO Message (SenderId, ReceiverId, Content)
                   VALUES (@sender, @receiver, @content)";

            SqlHelper.ExecuteNonQuery(sql,
                new SqlParameter("@receiver", Guid.Parse(toUserId)),
                new SqlParameter("@sender", Guid.Parse(fromUser)),
                new SqlParameter("@content", message)
            );

       
            string userInfoSql = "SELECT HoTen, AnhDaiDien FROM NguoiDung WHERE MaNguoiDung = @id";
            var reader = SqlHelper.ExecuteReader(userInfoSql, new SqlParameter("@id", Guid.Parse(fromUser)));

            string hoTen = "", avatar = "";

            if (reader.Read())
            {
                hoTen = reader["HoTen"] as string;
                avatar = reader["AnhDaiDien"] as string;
            }
            reader.Close();

         
            var messagePayload = new
            {
                userId = fromUser,
                userName = hoTen,
                avatar = string.IsNullOrEmpty(avatar)
                    ? "https://placehold.co/600x400/green/white?text=avatar"
                    : avatar,
                message = message
            };

       
            if (ConnectedUsers.TryGetValue(toUserId, out var connectionId))
            {
                Clients.Client(connectionId).receiveMessage(messagePayload);
            }

   
            Clients.Caller.receiveMessage(messagePayload);
        }


    }
}