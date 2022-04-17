using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Signalr.Server
{
    public class ChatHub:Hub
    {
        private static List<string> connectionList = new List<string>();
        public override Task OnConnectedAsync()
        {
            string connectionId=Context.ConnectionId;
            if(!connectionList.Any(x=>x==connectionId))
                connectionList.Add(connectionId);
            Clients.All.SendAsync("HasNewConnection", connectionList.Count);
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            string connectionId = Context.ConnectionId;
            if (connectionList.Any(x => x == connectionId))
                connectionList.Remove(connectionId);
            Clients.All.SendAsync("HasNewConnection", connectionList.Count);
            return base.OnDisconnectedAsync(exception);
        }
        public async Task PublicMessage(string message)
        {
            string connectionId = Context.ConnectionId;
            await Clients.All.SendAsync("HasNewMessage", connectionId, message);
        }
    }
}
