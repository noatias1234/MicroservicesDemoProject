using MapPresenterApp.Core.Configuration;
using MapPresenterApp.Core.Interface;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

namespace MapPresenterApp.Infrastructure.SignalRService
{
    public class SignalRService : ISignalRService
    {
        private readonly ILogger<SignalRService> _logger;
        private readonly Settings _settings;
        private HubConnection _connection;

        public SignalRService(ILogger<SignalRService> logger, Settings settings)
        {
            _logger = logger;
            _settings = settings;
            _connection = BuildConnection();
        }

        private HubConnection BuildConnection()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:50003")
                .WithAutomaticReconnect()
                .Build();

            return _connection;
        }

        public async Task StartAsync()
        {
            try
            {
                if (_connection.State is not HubConnectionState.Connected)
                    await _connection.StartAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SignalRService => Start connection fail");
            }
        }

        public async Task DisconnectAsync()
        {
            try
            {
                _logger.LogInformation("SignalR disconnected");
                await _connection.StopAsync();
                await _connection.DisposeAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SignalR service => Stop connection fail");
            }
        }

        public void GetNewMapPoint(Action<string> callBack)
        {
            _connection.On<string>(_settings.NewMapPointMethod, (message) =>
            {
                callBack?.Invoke(message);
            });
        }
    }
}
