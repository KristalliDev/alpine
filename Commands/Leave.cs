using DisCatSharp.CommandsNext;
using DisCatSharp.CommandsNext.Attributes;
using DisCatSharp.Lavalink;

namespace alpine.Commands
{
    public class Leave : BaseCommandModule
    {
        [GroupCommand, Command("leave"), Description("Leaves the voice channel")]
        public async Task LeaveModule(CommandContext ctx)
        {
            var lava = ctx.Client.GetLavalink();
            var gp = lava.GetGuildPlayer(ctx.Guild!);

            if (gp == null)
            {
                await ctx.RespondAsync("Lavalink not connected");
                return;
            }

            await gp.DisconnectAsync();
            await ctx.RespondAsync($"Left {gp.Channel.Mention}");
        }
    }
}