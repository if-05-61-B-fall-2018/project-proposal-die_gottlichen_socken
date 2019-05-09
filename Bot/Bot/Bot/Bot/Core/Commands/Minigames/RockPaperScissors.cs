using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bot.Resources.Database;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Bot.Core.Commands.Minigames
{
    public class RockPaperScissors : ModuleBase<SocketCommandContext>
    {
        [Command("rps")]
        public async Task RockPaperScissor(string input)
        {
            int x = Data.Data.realRemoveCoins(Context.Message.Author.Id, 10);
            Random rnd = new Random();
            if (!(input.ToLower() == "rock" || input.ToLower() == "paper" || input.ToLower() == "scissor"))
            {
                await ReplyAsync("Move must be either rock, paper or scissor");
            }

            else
            {
                int i = rnd.Next(0, 2);
                if (i == 0) // 0 is rock
                {
                    if (input.ToLower() == "rock")
                    {
                        await Data.Data.addCoins(Context.Message.Author.Id, 10);
                        await ReplyAsync("Draw! You get 10 Coins back");
                    }

                    else if(input.ToLower() == "paper")
                    {
                        await Data.Data.addCoins(Context.Message.Author.Id, 30);
                        await ReplyAsync("Nice! You won 20 coins");
                    }

                    else
                    {
                        await ReplyAsync("Ups! You have lost 10 Coins");
                    }
                }

                else if (i == 1) // 1 is paper
                {
                    if (input.ToLower() == "paper")
                    {
                        await Data.Data.addCoins(Context.Message.Author.Id, 10);
                        await ReplyAsync("Draw! You get 10 Coins back");
                    }

                    else if (input.ToLower() == "scissor")
                    {
                        await Data.Data.addCoins(Context.Message.Author.Id, 30);
                        await ReplyAsync("Nice! You won 20 coins");
                    }

                    else
                    {
                        await ReplyAsync("Ups! You have lost 10 Coins");
                    }
                }

                else if (i == 2) // 2 is scissor
                {
                    if (input.ToLower() == "scissor")
                    {
                        await Data.Data.addCoins(Context.Message.Author.Id, 10);
                        await ReplyAsync("Draw! You get 10 Coins back");
                    }

                    else if (input.ToLower() == "paper")
                    {
                        await Data.Data.addCoins(Context.Message.Author.Id, 30);
                        await ReplyAsync("Nice! You won 20 coins");
                    }

                    else
                    {
                        await ReplyAsync("Ups! You have lost 10 Coins");
                    }
                }
            }
        }
    }
}
