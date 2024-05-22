using DisCatSharp.CommandsNext;
using DisCatSharp.CommandsNext.Attributes;
using DisCatSharp.Lavalink;
using DisCatSharp.Lavalink.Entities;
using DisCatSharp.Lavalink.Enums;

namespace alpine.Commands
{
    public class LavaCmd : BaseCommandModule
    {
        [Command("join")]
        public async Task JoinModule(CommandContext ctx)
        {
            var voiceState = ctx.Member.VoiceState;
            var channel = ctx.Member.VoiceState!.Channel!;
            var lava = ctx.Client.GetLavalink();

            if (voiceState != null && voiceState.Channel! != null!)
            {
                var session = lava.ConnectedSessions.Values.First();

                await session.ConnectAsync(channel: channel);
                await ctx.RespondAsync($"Joined {channel.Mention}");
            }
            else
            {
                await ctx.RespondAsync("You are not in a voice channel!");
            }
        }

        [Command("leave")]
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

        [Command("play")]
        public async Task PlayModule(CommandContext ctx, [RemainingText] string url, LavalinkTrack track)
        {
            // await ctx.RespondAsync("Not implemented yet");

            if (ctx.Member.VoiceState == null || ctx.Member.VoiceState.Channel! == null!)
            {
                await ctx.RespondAsync("You be not in voice channel");
                return;
            }

            var lava = ctx.Client.GetLavalink();
            var gp = lava.GetGuildPlayer(ctx.Guild!);

            if (gp == null)
            {
                await ctx.RespondAsync("Lavalink is not connected");
                return;
            }

            var loadResult = await gp.LoadTracksAsync(LavalinkSearchType.Youtube, url);

            if (loadResult.LoadType == LavalinkLoadResultType.Empty ||
                loadResult.LoadType == LavalinkLoadResultType.Error)
            {
                await ctx.RespondAsync($"Track search failed for {url}");
            }

            LavalinkTrack lavalinkTrack = loadResult.LoadType switch
            {
                LavalinkLoadResultType.Track => loadResult.GetResultAs<LavalinkTrack>(),
                LavalinkLoadResultType.Playlist => loadResult.GetResultAs<LavalinkPlaylist>().Tracks.First(),
                LavalinkLoadResultType.Search => loadResult.GetResultAs<LavalinkTrack>(), // Change this line
                _ => throw new InvalidOperationException("Unexpected load result type.")
            };
            await gp.PlayAsync(url);
            await ctx.RespondAsync($"Now playing {url}!");
        }
    }
}