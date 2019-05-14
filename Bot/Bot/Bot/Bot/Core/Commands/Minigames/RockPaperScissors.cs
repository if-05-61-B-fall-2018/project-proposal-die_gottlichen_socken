using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bot.Resources.Database;

using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;

namespace Bot.Core.Commands.Minigames
{
    public class RockPaperScissors : ModuleBase<SocketCommandContext>
    {

        internal static async void RPSChoose(ISocketMessageChannel Context, string input)
        {
            int x = Data.Data.realRemoveCoins(Global.thisusr.Id, 10);
            Random rnd = new Random();
            if (!(input.ToLower() == "rock" || input.ToLower() == "paper" || input.ToLower() == "scissor"))
            {
                await Context.SendMessageAsync("Move must be either rock, paper or scissor");
            }
            else
            {
                int i = rnd.Next(0, 2);
                if (i == 0) // 0 is rock
                {
                    if (input.ToLower() == "rock")
                    {
                        await Data.Data.addCoins(Global.thisusr.Id, 10);
                        await Context.SendMessageAsync("Draw! You get 10 Coins back");
                    }

                    else if (input.ToLower() == "paper")
                    {
                        await Data.Data.addCoins(Global.thisusr.Id, 30);
                        await Context.SendMessageAsync("Nice! You won 20 coins");
                    }

                    else
                    {
                        await Context.SendMessageAsync("Ups! You have lost 10 Coins");
                    }
                }

                else if (i == 1) // 1 is paper
                {
                    if (input.ToLower() == "paper")
                    {
                        await Data.Data.addCoins(Global.thisusr.Id, 10);
                        await Context.SendMessageAsync("Draw! You get 10 Coins back");
                    }

                    else if (input.ToLower() == "scissor")
                    {
                        await Data.Data.addCoins(Global.thisusr.Id, 30);
                        await Context.SendMessageAsync("Nice! You won 20 coins");
                    }

                    else
                    {
                        await Context.SendMessageAsync("Ups! You have lost 10 Coins");
                    }
                }

                else if (i == 2) // 2 is scissor
                {
                    if (input.ToLower() == "scissor")
                    {
                        await Data.Data.addCoins(Global.thisusr.Id, 10);
                        await Context.SendMessageAsync("Draw! You get 10 Coins back");
                    }

                    else if (input.ToLower() == "paper")
                    {
                        await Data.Data.addCoins(Global.thisusr.Id, 30);
                        await Context.SendMessageAsync("Nice! You won 20 coins");
                    }

                    else
                    {
                        await Context.SendMessageAsync("Ups! You have lost 10 Coins");
                    }
                }
            }
        }

    
        [Command("rockpaperscissor"), Alias("rps")]
        public async Task RockPaperScissor()
        {
            if (Data.Data.getCoins(Context.Message.Author.Id) < 10)
            {
                await ReplyAsync("You dont have enough Coins!");
                return;
            }

            IUser user = Context.Message.Author;
            Global.thisusr = user;

            RestUserMessage msg = await Context.Message.Channel.SendMessageAsync("What will you choose? Rock, Paper or Scissor?");

            var emote = new Emoji("📰");
            await msg.AddReactionAsync(emote);
            emote = new Emoji("🌑");
            await msg.AddReactionAsync(emote);
            emote = new Emoji("✂");
            await msg.AddReactionAsync(emote);

            Global.messageIDtoTrack = msg.Id;
            Global.emoteMethod = "rps";

        }
    }
}
