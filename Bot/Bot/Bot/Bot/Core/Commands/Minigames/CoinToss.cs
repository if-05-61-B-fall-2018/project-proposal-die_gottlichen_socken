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
    public class CoinToss : ModuleBase<SocketCommandContext>
    {
        static Random rnd = new Random();
        [Command("coinflip")]
        public async Task CoinFlip(string input)
        {
            int x = Data.Data.realRemoveCoins(Context.Message.Author.Id, 10);
            if(!(input.ToLower() == "head" || input.ToLower() == "tail"))
            {
                await ReplyAsync("Coinside must be either head or tail");
            }
            else
            {
                int i = rnd.Next(0, 1);
                if(i == 0) // 0 is head
                {
                    if(input.ToLower() == "head")
                    {
                        await Data.Data.addCoins(Context.Message.Author.Id, 20);
                        await ReplyAsync("Nice! You won 10 Coins");
                    }
                    else
                    {
                        await ReplyAsync("Ups! You have lost 10 Coins");
                    }  
                }
                else if(i == 1) // 1 = tails
                {
                    if (input.ToLower() == "tail")
                    {
                        await Data.Data.addCoins(Context.Message.Author.Id, 20);
                        await ReplyAsync("Nice! You won 10 Coins");
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
