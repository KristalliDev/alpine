using DisCatSharp.CommandsNext;
using DisCatSharp.CommandsNext.Attributes;
using DisCatSharp.Entities;

namespace alpine.Commands
{
    public class Status : BaseCommandModule
    {
        [Command("status"), Aliases("stat"), Description("Change the status of the bot temporarily")]
        [Hidden, RequireOwner]
        public async Task StatusCommand(CommandContext ctx, [RemainingText] string status)
        {
            await ctx.Client.UpdateStatusAsync(new DiscordActivity($"{status}", ActivityType.Custom));
            await ctx.RespondAsync($"Status updated to {status}");
        }
    }
}