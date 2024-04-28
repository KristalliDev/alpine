using DisCatSharp.CommandsNext;
using DisCatSharp.CommandsNext.Attributes;

namespace alpine.Commands
{
    public class Ping : BaseCommandModule
    {
        [Command("ping"), Aliases("pong"), Description("Sends the bot latency")]
        public async Task PingModule(CommandContext ctx)
        {
            await ctx.RespondAsync($"Pong! Latency: {ctx.Client.Ping}ms");
        }
    }
}