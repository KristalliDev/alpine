using DisCatSharp.CommandsNext;
using DisCatSharp.CommandsNext.Attributes;
using DisCatSharp.Lavalink.Entities;
using DisCatSharp.Lavalink.Enums;
using DisCatSharp.Lavalink;

namespace alpine.Commands
{
    public class Play : BaseCommandModule
    {
        [GroupCommand, Command("play"), Aliases("p"), Description("Plays Spotify songs from uri")]
        public async Task PlayModule(CommandContext ctx, [RemainingText] string query)
        {
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

            LavalinkTrackLoadingResult loadResult = await gp.LoadTracksAsync(LavalinkSearchType.Plain, query);

            if (loadResult.LoadType == LavalinkLoadResultType.Empty ||
                loadResult.LoadType == LavalinkLoadResultType.Error)
            {
                await ctx.RespondAsync($"Track search failed for {query}");
            }

            LavalinkTrack track = loadResult.LoadType switch
            {
                LavalinkLoadResultType.Track => loadResult.GetResultAs<LavalinkTrack>(),
                LavalinkLoadResultType.Playlist => loadResult.GetResultAs<LavalinkPlaylist>().Tracks.First(),
                LavalinkLoadResultType.Search => loadResult.GetResultAs<LavalinkTrack>(),
                _ => throw new InvalidOperationException("Unexpected load result type.")
            };
            await gp.PlayAsync(track);
            await ctx.RespondAsync($"Now playing {track.Info.Title}!");
        }
    }
}