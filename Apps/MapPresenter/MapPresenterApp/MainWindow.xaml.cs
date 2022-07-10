using System.ComponentModel;
using System.Windows;
using MapPresenterApp.Core.Configuration;
using MapPresenterApp.Core.Interface;
using Microsoft.Extensions.Logging;

namespace MapPresenterApp
{
    public partial class MainWindow : Window
    {
        private readonly ISignalRService _signalRService;
        private readonly ILogger<MainWindow> _logger;
        private readonly Settings _settings;

        public MainWindow(ISignalRService signalRService, ILogger<MainWindow> logger,
            Settings settings)
        {
            InitializeComponent();

            _signalRService = signalRService;
            _logger = logger;
            _settings = settings;
            
            ConfigureSignalR();
        }

        private async void ConfigureSignalR()
        {
            _signalRService.GetNewMapPoint(NewMapEntityCommand);
            await _signalRService.StartAsync();
        }

        private void NewMapEntityCommand(string newMapEntity)
        {
            _logger.LogInformation("New map point: {newMapEntity} ", newMapEntity );
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _signalRService.DisconnectAsync();
            base.OnClosing(e);
        }
    }
}