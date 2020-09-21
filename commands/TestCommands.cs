using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;
using System.Threading.Tasks;

namespace Limit_Break_Pug_Bot.commands
{
    public class TestCommands : BaseCommandModule
    {
        [Command("TrackReaction")]
        public async Task RespondReaction(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForReactionAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.Channel.SendMessageAsync(message.Result.Emoji);
        }

        [Command("GetPlayers")]
        public async Task GetPlayers(CommandContext ctx)
        {
            DB db = new DB();
            var players = db.GetPlayers();

            foreach(Player player in players.Result)
            {
                await ctx.Channel.SendMessageAsync($"ID: {player.ID} - Battle Tag: {player.Battle_Tag} - Player SR: {player.Player_Rank} - Plays for: {player.Player_Team}");
                await Task.Delay(1000);
            }
        }
    }
}
