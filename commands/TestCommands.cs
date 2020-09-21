using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using Limit_Break_Pug_Bot.Entities;
using System;
using System.Threading.Tasks;

namespace Limit_Break_Pug_Bot.commands
{
    public class TestCommands : BaseCommandModule
    {
        public string pugAnnouncementsChannel_ID = "757595493269504061";
        public DB db = new DB();

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
            var players = db.GetPlayers();

            foreach(Player player in players.Result)
            {
                await ctx.Channel.SendMessageAsync($"ID: {player.ID} - Battle Tag: {player.Battle_Tag} - Player SR: {player.Player_Rank} - Plays for: {player.Player_Team}");
                await Task.Delay(1000);
            }
        }

        [Command("CreatePug")]
        public async Task CreatePug(CommandContext ctx, DateTime scheduledDate)
        {
            DiscordMessage msg = await ctx.Channel.SendMessageAsync($"Attempting scheduling...").ConfigureAwait(false);
            await AnnounceEvent(ctx, scheduledDate, msg);
        }

        public async Task AnnounceEvent(CommandContext ctx, DateTime scheduledDate, DiscordMessage msg)
        {
            DiscordChannel channel = await ctx.Client.GetChannelAsync(UInt64.Parse(pugAnnouncementsChannel_ID));
            DiscordMessage message = await channel.SendMessageAsync($"New Pug Event scheduled on {scheduledDate.ToShortDateString()} at {scheduledDate.ToShortTimeString()}!");
            
            PugEvent e = new PugEvent();
            e.Scheduled_Date = scheduledDate;
            e.Discord_Message_ID = message.Id.ToString();


            await db.CreateEvent(e);
            await msg.ModifyAsync("The event has been scheduled!").ConfigureAwait(false);
        }


    }
}
