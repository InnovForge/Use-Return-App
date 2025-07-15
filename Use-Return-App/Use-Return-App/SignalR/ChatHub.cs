using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;

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
            }
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var userId = GetUserByConnectionId(Context.ConnectionId);
            if (userId != null)
            {
                ConnectedUsers.TryRemove(userId, out _);
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
            if (ConnectedUsers.TryGetValue(toUserId, out var connectionId))
            {
                Clients.Client(connectionId).receiveMessage(fromUser, message);
            }


            Clients.Caller.receiveMessage(fromUser, message);
        }
    }
}