using DisCatSharp.CommandsNext;
using DisCatSharp.CommandsNext.Attributes;
using DisCatSharp.Lavalink;

namespace alpine.Commands
{
    public class Join : BaseCommandModule
    {
        [GroupCommand, Command("join"), Description("Joins the voice channel")]
        public async Task JoinModule(CommandContext ctx)
        {
            var voiceState = ctx.Member.VoiceState;
            var channel = ctx.Member.VoiceState!.Channel!;
            var lava = ctx.Client.GetLavalink();
            var session = lava.ConnectedSessions.Values.First();

            if (voiceState != null && voiceState.Channel! != null!)
            {
                await session.ConnectAsync(channel);
                await ctx.RespondAsync($"Joined {channel.Mention}");
            }
            else
            {
                await ctx.RespondAsync("You are not in a voice channel!");
            }
        }
    }
}