using System.Collections;
using Microsoft.Extensions.Logging;
using DisCatSharp;
using DisCatSharp.CommandsNext;
using DisCatSharp.CommandsNext.Converters;
using DisCatSharp.CommandsNext.Entities;
using DisCatSharp.Entities;
using DisCatSharp.Enums;
using Newtonsoft.Json;
using alpine.Commands;

namespace alpine
{
    internal abstract class Program
    {
        // Main
        private static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }
        // Help command formatter
        public class HelpFormatter : DefaultHelpFormatter
        {
            public HelpFormatter(CommandContext ctx) : base(ctx) { }

            public override CommandHelpMessage Build()
            {
                EmbedBuilder.Color = DiscordColor.VeryDarkGray;
                return base.Build();
            }
        }
        // Main function
        private static async Task MainAsync(IEnumerable? args)
        {
            // Json handler
            ArgumentNullException.ThrowIfNull(args);
            var json = await File.ReadAllTextAsync("config.json");
            dynamic config = JsonConvert.DeserializeObject(json)!;
            string token = config.discord.token.ToString();
            string prefix = config.discord.prefix.ToString();
            // Discord client and bot configuration
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All,
                MinimumLogLevel = LogLevel.Debug,
                MobileStatus = true,
            });
            // Command configuration
            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = [prefix],
            });
            // Registering commands and setting help formatter
            commands.SetHelpFormatter<HelpFormatter>();
            commands.RegisterCommands<Ping>();
            commands.RegisterCommands<Shut>();
            commands.RegisterCommands<Info>();
            // Connecting to discord
            await discord.ConnectAsync(new DiscordActivity("@alpine help | al!help", ActivityType.ListeningTo));
            await Task.Delay(-1);
        }
    }
}