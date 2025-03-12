using Microsoft.AspNetCore.SignalR;

namespace MediaWave.Hubs;

public class StreamHub : Hub
{
    private readonly ILogger<StreamHub> _logger;

    public StreamHub(ILogger<StreamHub> logger)
    {
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation($"Client connected: {Context.ConnectionId}");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation($"Client disconnected: {Context.ConnectionId}");
        await base.OnDisconnectedAsync(exception);
    }

    public async Task StartStreaming(string streamId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, streamId);
        await Clients.Group(streamId).SendAsync("StreamStarted", streamId);
    }

    public async Task StopStreaming(string streamId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, streamId);
        await Clients.Group(streamId).SendAsync("StreamStopped", streamId);
    }

    public async Task SendStreamData(string streamId, byte[] data)
    {
        await Clients.Group(streamId).SendAsync("ReceiveStreamData", streamId, data);
    }
} 