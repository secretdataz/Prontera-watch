using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PronteraWatch
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        // Retrieve client and CommandService instance via ctor
        public CommandHandler(DiscordSocketClient client, CommandService commands)
        {
            _commands = commands;
            _client = client;
        }

        public async Task InstallCommandsAsync()
        {
            // Hook the MessageReceived event into our command handler
            _client.MessageReceived += HandleCommandAsync;

            // Here we discover all of the command modules in the entry 
            // assembly and load them. Starting from Discord.NET 2.0, a
            // service provider is required to be passed into the
            // module registration method to inject the 
            // required dependencies.
            //
            // If you do not use Dependency Injection, pass null.
            // See Dependency Injection guide for more information.
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                            services: null);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Don't process DMs
            if (messageParam.Channel is SocketDMChannel)
            {
                return;
            }

            // Don't process messages outside of whitelisted channels
            if (!PronteraWatchApp._appConfig.ChannelWhitelist.Contains(messageParam.Channel.Id))
            {
                return;
            }

            // Don't process the command if it was a system message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;

            var content = message.Content;
            var pattern = "<(.+?)>";

            MatchCollection matches = Regex.Matches(content, pattern);
            if (matches.Count > 0)
            {
                string Reply = "";
                foreach (Match match in matches)
                {
                    string name = match.Groups[1].Value;
                    if (name.Contains(':') || name.Contains('@') || name.Contains('#'))
                    {
                        continue; // This is probably an emoji or a mention, skip it
                    }

                    uint id;
                    bool isNumber = uint.TryParse(name, out id);
                    if (!isNumber)
                    {
                        id = PronteraWatchApp.NameToIdDatabase.ResolveName(name);
                    }

                    if (id == 0)
                    {
                        Reply += $"{name} - Not found\n";
                    } else
                    {
                        Reply += $"{name} - https://www.divine-pride.net/database/item/{id}\n";
                    }
                }
                if (string.IsNullOrWhiteSpace(Reply))
                {
                    return;
                }
                await message.ReplyAsync(Reply.Trim());
                return;
            }

            // Determine if the message is a command based on the prefix and make sure no bots trigger commands
            if (!(message.HasStringPrefix("-", ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            // Create a WebSocket-based command context based on the message
            var context = new SocketCommandContext(_client, message);

            // Execute the command with the command context we just
            // created, along with the service provider for precondition checks.
            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);
        }
    }
}
