using DisCatSharp.CommandsNext;
using DisCatSharp.CommandsNext.Attributes;

namespace alpine.Commands
{
    public class Die : BaseCommandModule
    {
        [Command("Die"), Description("Dies"), RequireOwner]
        public async Task DieModule(CommandContext ctx)
        {
            await ctx.RespondAsync("Dies.");
            await ctx.Client.DisconnectAsync();
        }
    }
}