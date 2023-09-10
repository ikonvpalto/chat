using Microsoft.AspNetCore.SignalR;
using ChatServer.Models.Request;
using ChatServer.Models.Response;
using ChatServer.Services;
using Microsoft.AspNetCore.Components;

namespace ChatServer.Hubs;

[Route("chat")]
public sealed class ChatHub : Hub
{
    private const string ReceiveEndpoint = "Receive";
    private const string ReceiveAllEndpoint = "ReceiveAll";

    private readonly MessageService _messageService;

    public ChatHub(MessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task Send(MessageRequest request)
    {
        var response = new MessageResponse
        {
            Text = request.Text,
        };

        await Task.WhenAll(
            _messageService.SaveAsync(request),
            Clients.Others.SendAsync(ReceiveEndpoint, response));
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();

        var messages = await _messageService.GetAllAsync();
        await Clients.Caller.SendAsync(ReceiveAllEndpoint, messages);
    }


}
