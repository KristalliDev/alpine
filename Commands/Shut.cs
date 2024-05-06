using DisCatSharp.CommandsNext;
using DisCatSharp.CommandsNext.Attributes;

namespace alpine.Commands
{
    public class Shut : BaseCommandModule
    {
        [Command("shut"), Aliases("shutdown"), Description("Shuts down the bot"), RequireOwner, Hidden]
        public async Task ShutModule(CommandContext ctx)
        {
            await ctx.RespondAsync("Shutting down...");
            await ctx.Client.DisconnectAsync();
        }
    }
}