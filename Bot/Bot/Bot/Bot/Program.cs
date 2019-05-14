using System;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Discord.Audio;

namespace Bot
{
    class Program
    {

        private DiscordSocketClient client;
        private CommandService commmands;

        static void Main(string[] args)
        {
            new Program().MainASync().GetAwaiter().GetResult();
        }

        private async Task MainASync()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig
            {

            });

            commmands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = false,
                DefaultRunMode = RunMode.Async,
            });

            client.MessageReceived += Client_MessageReceived;
            client.ReactionAdded += OnReaktionAdded;
            await commmands.AddModulesAsync(Assembly.GetEntryAssembly());
            client.Ready += Client_Ready;
            client.Log += Client_Log;

            await client.LoginAsync(TokenType.Bot, "NDk1OTM2MjU5MDEzNzM4NTA2.XKw5Lg.u81KSNjPFtTdntchgRmMK5wNhZo");
            await client.StartAsync();

            await Task.Delay(-1);
        }

        private Task OnReaktionAdded(Cacheable<IUserMessage, ulong> cache, ISocketMessageChannel channel, SocketReaction reaction)
        {
            if (reaction.UserId == 495936259013738506) return Task.CompletedTask;
            if (reaction.MessageId == Global.messageIDtoTrack)
            {
                if (Global.vsusr != null&& reaction.UserId == Global.vsusr.Id)
                {
                    if (Global.emoteMethod == "cointoss")
                    {
                        if(reaction.UserId == Global.thisusr.Id) return Task.CompletedTask;
                        if (reaction.Emote.Name == "🆗")
                        {
                            Core.Commands.Minigames.CoinToss.CoinflipVsAccepted(channel);
                        }
                    }
                    else if (Global.emoteMethod == "cointossChoose")
                    {
                        if (reaction.UserId == Global.thisusr.Id) return Task.CompletedTask;
                        if (reaction.Emote.Name == "😑")
                        {
                            Core.Commands.Minigames.CoinToss.CoinflipVsChoose(channel, "head");
                        }
                        else if (reaction.Emote.Name == "🐿")
                        {
                            Core.Commands.Minigames.CoinToss.CoinflipVsChoose(channel, "tail");
                        }
                    }
                }
                else
                {
                    if (Global.emoteMethod == "rps")
                    {
                        if (reaction.Emote.Name == "📰")
                        {
                            Core.Commands.Minigames.RockPaperScissors.RPSChoose(channel, "paper");
                        }
                        else if (reaction.Emote.Name == "🌑")
                        {
                            Core.Commands.Minigames.RockPaperScissors.RPSChoose(channel, "rock");
                        }
                        else if (reaction.Emote.Name == "✂")
                        {
                            Core.Commands.Minigames.RockPaperScissors.RPSChoose(channel, "scissor");
                        }
                    }
                }
            }
            return Task.CompletedTask;
        }

        private async Task Client_Log(LogMessage arg)
        {
            Console.WriteLine($"[{DateTime.Now} | {arg.Source} ] {arg.Message}");
        }

        private async Task Client_Ready()
        {
            //await client.SetGameAsync("witch cute neko lolis :3");
        }

        private async Task Client_MessageReceived(SocketMessage arg)
        {
            var msg = arg as SocketUserMessage;
            var context = new SocketCommandContext(client, msg);

            if (context.Message == null || context.Message.Content == "") return;
            if (context.User.IsBot) return;

            int argPos = 0;
            if (!(msg.HasStringPrefix("!",ref argPos)||msg.HasMentionPrefix(client.CurrentUser, ref argPos))) return;
            if (Data.Data.getBlacklist(arg.Author.Id) == 1) return;

            var res = await commmands.ExecuteAsync(context, argPos);
            if (!res.IsSuccess) Console.WriteLine($"[{DateTime.Now} | Commands ] {context.Message.Content} | Error: {res.ErrorReason}");
        }
    }
}
