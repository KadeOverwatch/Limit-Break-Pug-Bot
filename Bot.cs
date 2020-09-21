using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using Limit_Break_Pug_Bot.commands;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Limit_Break_Pug_Bot
{
    public class Bot
    {
        public DB db = new DB();
        public string pugAnnouncementsChannel_ID = "757595493269504061";

        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task Start()
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;

            Client.MessageReactionAdded += OnReactionAdded;
            Client.MessageReactionRemoved += OnReactionRemoved;


            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableMentionPrefix = true,
                EnableDms = false,
                DmHelp = true,
                EnableDefaultHelp = true,
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<TestCommands>();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }

        private async Task OnReactionAdded(MessageReactionAddEventArgs e)
        {
            if (e.Channel.Id.ToString() == pugAnnouncementsChannel_ID)
            {
                var player = db.GetPlayer(e.User).Result;
                var eve = db.GetEvent(e.Message).Result;
                await db.CreateRegistration(player, eve);

                await e.Guild.GetMemberAsync(e.User.Id).Result
                    .SendMessageAsync($"You have been signed up for the pug occurring on {eve.Scheduled_Date.ToShortDateString()} at {eve.Scheduled_Date.ToShortTimeString()}!");
            }
            await Task.CompletedTask;
        }

        private async Task OnReactionRemoved(MessageReactionRemoveEventArgs e)
        {
            if (e.Channel.Id.ToString() == pugAnnouncementsChannel_ID)
            {
                var player = db.GetPlayer(e.User).Result;
                var eve = db.GetEvent(e.Message).Result;
                await db.UpdateRegistration(player, eve);

                await e.Guild.GetMemberAsync(e.User.Id).Result
                    .SendMessageAsync($"You have removed yourself from the pug occuring on {eve.Scheduled_Date.ToShortDateString()} at {eve.Scheduled_Date.ToShortTimeString()}.");
            }
            await Task.CompletedTask;
        }
    }
}
