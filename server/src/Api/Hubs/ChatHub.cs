using ChatServer.Services.Models.Request;
using ChatServer.Services.Models.Response;
using Microsoft.AspNetCore.SignalR;
using ChatServer.Services.Services;
using ChatServer.Services.Services.Messages;
using Microsoft.AspNetCore.Components;

namespace ChatServer.Api.Hubs;

[Route("chat")]
public sealed class ChatHub : Hub
{
    private const string ReceiveEndpoint = "Receive";
    private const string ReceiveAllEndpoint = "ReceiveAll";

    private readonly MessageGetAllQuery _getAllMessagesQuery;
    private readonly MessageSaveCommand _saveMessagesCommand;

    public ChatHub(
        MessageGetAllQuery getAllMessagesQuery,
        MessageSaveCommand saveMessagesCommand)
    {
        _getAllMessagesQuery = getAllMessagesQuery;
        _saveMessagesCommand = saveMessagesCommand;
    }

    public async Task Send(MessageRequest request)
    {
        var response = new MessageResponse
        {
            Text = request.Text,
        };

        await Task.WhenAll(
            _saveMessagesCommand.DoAsync(request, CancellationToken.None),
            Clients.Others.SendAsync(ReceiveEndpoint, response));
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();

        var messages = await _getAllMessagesQuery.QueryAsync(CancellationToken.None);
        await Clients.Caller.SendAsync(ReceiveAllEndpoint, messages);
    }


}
