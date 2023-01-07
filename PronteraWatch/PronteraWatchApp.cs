using Discord;
using Discord.Commands;
using Discord.WebSocket;
using LibProntera;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PronteraWatch
{
    internal class PronteraWatchApp
    {
        public static AppConfig _appConfig;
        public static ItemNameToIdDatabase NameToIdDatabase = new ItemNameToIdDatabase("name2id.txt");
        private DiscordSocketClient? _client;
        private CommandService _commandService;
        private CommandHandler _commandHandler;

        private Task LogMsg(LogMessage msg)
        {
            Log.Information(msg.Message);
            return Task.CompletedTask;
        }

        public async Task Run()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

            var configStr = await File.ReadAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "config.json"));
            _appConfig = JsonConvert.DeserializeObject<AppConfig>(configStr);
            if (_appConfig == null)
            {
                Log.Information("Cannot read config.");
                return;
            }

            _client = new DiscordSocketClient();
            _client.Log += LogMsg;
            _client.Ready += ClientReady;
            _commandService = new CommandService();
            _commandHandler = new CommandHandler(_client, _commandService);
            await _commandHandler.InstallCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, _appConfig.DiscordToken);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
        
        private async Task ClientReady()
        {
            return;
        }
    }
}
