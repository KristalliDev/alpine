using DisCatSharp.CommandsNext;
using DisCatSharp.CommandsNext.Attributes;
using DisCatSharp.Entities;

namespace alpine.Commands
{
    public class GitHub : BaseCommandModule
    {
        [Command("GitHub"), Aliases("git"), Description("Information about the GitHub repository")]
        public async Task GitHubModule(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder();
            
            embed.WithTitle("GitHub");
            embed.WithUrl("https://github.com/KristalliDev/alpine");
            embed.WithColor(DiscordColor.DarkGray);
            embed.WithDescription("A simple little bot");
            embed.WithFooter("Brought to you by a dumbass!");
            embed.WithThumbnail("https://cdn.discordapp.com/avatars/1214555385348759563/cbc00307c9a05a8c2352afe0aaafaf08.png?size=128");
            embed.WithAuthor("KristalliDev", "https://github.com/KristalliDev/", "https://avatars.githubusercontent.com/u/119222895?v=4");
            embed.WithTimestamp(DateTime.UtcNow);
            
            await ctx.RespondAsync(embed: embed);
        }
    }
}