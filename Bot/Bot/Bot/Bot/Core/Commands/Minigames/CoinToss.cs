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
    public class CoinToss : ModuleBase<SocketCommandContext>
    {
        static Random rnd = new Random();
        [Command("coinflipvs")]
        public async Task CoinFlipVS(IUser usr = null)
        {
            IUser user = Context.Message.Author;
            Global.vsusr = usr;
            Global.thisusr = user;

            RestUserMessage msg = await Context.Message.Channel.SendMessageAsync(user + " has challenged you to a CoinToss game. Will you accept?");

            var emote = new Emoji(":ok:");
            await msg.AddReactionAsync(emote);

            Global.messageIDtoTrack = msg.Id;
            Global.emoteMethod = "cointoss";
        }

        internal static async void CoinflipVsAccepted(ISocketMessageChannel Context)
        {
            IUser usr = Global.vsusr;
            IUser user = Global.thisusr;

            RestUserMessage msg = await Context.SendMessageAsync("What will you choose? Head or Tail?");

            var emote = new Emoji(":expressionless:");
            await msg.AddReactionAsync(emote);
            emote = new Emoji(":chipmunk:");
            await msg.AddReactionAsync(emote);

            Global.messageIDtoTrack = msg.Id;
            Global.emoteMethod = "cointossChoose";

        }
        internal static async void CoinflipVsChoose(ISocketMessageChannel Context, String b)
        {
            IUser usr = Global.vsusr;
            IUser user = Global.thisusr;
            int x = Data.Data.realRemoveCoins(user.Id, 10);
            int y = Data.Data.realRemoveCoins(usr.Id, 10);
            if (!(b.ToLower() == "head" || b.ToLower() == "tail"))
            {
                await Context.SendMessageAsync("Coinside must be either head or tail");
            }
            else
            {
                int i = rnd.Next(0, 1);
                if (i == 0)
                {
                    if (b.ToString().ToLower() == "head")
                    {
                        await Data.Data.addCoins(usr.Id, 50);
                        await Context.SendMessageAsync("Congratulations " + Global.vsusr.Mention + " has won 40 coins");
                    }
                    else
                    {
                        await Data.Data.addCoins(user.Id, 50);
                        await Context.SendMessageAsync("Congratulations " + Global.thisusr.Mention + " has won 40 coins");
                    }
                }
                else
                {
                    if (b.ToString().ToLower() == "tail")
                    {
                        await Data.Data.addCoins(usr.Id, 50);
                        await Context.SendMessageAsync("Congratulations " + Global.vsusr.Mention + " has won 40 coins");

                    }
                    else
                    {
                        await Data.Data.addCoins(user.Id, 50);
                        await Context.SendMessageAsync("Congratulations " + Global.thisusr.Mention + " has won 40 coins");
                    }

                }
                Global.emoteMethod = "";
            }
        }
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
