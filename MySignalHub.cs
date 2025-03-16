using Microsoft.AspNetCore.SignalR;

namespace Consumer
{
    public class MySignalRHub : Hub
    {
        //public static readonly ConcurrentDictionary<string, MySignalRHub> _ = new();
        public async Task SendDataUpdate(object data)
        {
            await Clients.All.SendAsync("ReceiveDataUpdate", data);
        }
        //public override async Task OnConnectedAsync()
        //{
        //    await Clients.Others.SendAsync(method: "Method1", $"{Context.ConnectionId} has joined");
        //    await Clients.All.SendAsync(method: "Method1", $"{Context.ConnectionId} has joined");
        //    await Clients.Caller.SendAsync(method: "Method1", $"{Context.ConnectionId} has joined");
        //    await Clients.Group("XYZ").SendAsync(method: "Method1", $"{Context.ConnectionId} has joined");
        //}

        //public override Task OnDisconnectedAsync(Exception? exception)
        //{
        //    return base.OnDisconnectedAsync(exception);
        //}

    }
}
