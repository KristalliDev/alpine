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
using DisCatSharp.Lavalink;
using DisCatSharp.Net;

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
                Locale = "en-UK",
                Timezone = "UTC"
            });
            // Lavalink
            var ep = new ConnectionEndpoint
            {
                Hostname = "127.0.0.1",
                Port = 2333,
            };

            var lavaConfig = new LavalinkConfiguration
            {
                Password = "youshallnotpass",
                RestEndpoint = ep,
                SocketEndpoint = ep
            };
            var lava = discord.UseLavalink();
            // Command configuration
            var commands = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = [prefix],
            });
            // Setting help formatter
            commands.SetHelpFormatter<HelpFormatter>();
            // Registering commands
            commands.RegisterCommands<Ping>();
            commands.RegisterCommands<Info>();
            commands.RegisterCommands<Die>();
            commands.RegisterCommands<GitHub>();
            commands.RegisterCommands<Status>();
            commands.RegisterCommands<LavaCmd>();
            // Connecting to discordd
            await discord.ConnectAsync(new DiscordActivity("@alpine help | al!help", ActivityType.ListeningTo));
            await lava.ConnectAsync(lavaConfig);
            await Task.Delay(-1);
        }
    }
}