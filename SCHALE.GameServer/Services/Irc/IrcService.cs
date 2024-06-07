using System.Net;
using SCHALE.Common.Database;

namespace SCHALE.GameServer.Services.Irc
{
    public class IrcService(
        ILogger<IrcService> _logger,
        SCHALEContext _context,
        ExcelTableService excelTableService,
        IConfiguration configuration
    ) : BackgroundService
    {
        private readonly IrcServer server =
            new(
                IPAddress.Any,
                int.Parse(configuration["IRC:Port"]!),
                _logger,
                _context,
                excelTableService
            );

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await server.StartAsync(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            server.Stop();
            await base.StopAsync(stoppingToken);
        }
    }

    internal static class IrcServiceExtensions
    {
        public static void AddIrcService(this IServiceCollection services)
        {
            services.AddHostedService<IrcService>();
        }
    }
}
