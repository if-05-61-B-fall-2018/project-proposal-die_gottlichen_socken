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
            await commmands.AddModulesAsync(Assembly.GetEntryAssembly());
            client.Ready += Client_Ready;
            client.Log += Client_Log;

            await client.LoginAsync(TokenType.Bot, "");
            await client.StartAsync();

            await Task.Delay(-1);
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

            var res = await commmands.ExecuteAsync(context, argPos);
            if (!res.IsSuccess) Console.WriteLine($"[{DateTime.Now} | Commands ] {context.Message.Content} | Error: {res.ErrorReason}");
        }
    }
}
