using DisCatSharp;
using DisCatSharp.CommandsNext;
using DisCatSharp.CommandsNext.Attributes;
using DisCatSharp.Entities;

namespace alpine.Commands
{
    public class Info : BaseCommandModule
    {
        [Command("info"), Aliases("about"), Description("Shows information about the bot")]
        public async Task InfoModule(CommandContext ctx)
        {
            // TODO
            // Make an embed with the info of the bot
            var embed = new DiscordEmbedBuilder();
            embed.WithTitle("Alpine");
            embed.WithDescription("Info coming soon...");
            embed.WithUrl("https://github.com/KristalliDev/alpine");
            embed.AddFields(new DiscordEmbedField[]
            {
                new DiscordEmbedField("Version", "1.0.5", true),
                new DiscordEmbedField("Creator", "birb", true),
                new DiscordEmbedField("Framework", "DisCatSharp", true),
                new DiscordEmbedField("Language", "C#", true),
            });
            embed.WithColor(DiscordColor.VeryDarkGray);
            embed.WithThumbnail(ctx.Client.CurrentUser.GetAvatarUrl(ImageFormat.Auto));
            embed.WithFooter(ctx.Client.CurrentUser.Username, ctx.Client.CurrentUser.GetAvatarUrl(ImageFormat.Auto));
            embed.WithTimestamp(DateTimeOffset.Now);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}