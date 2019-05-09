using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bot.Resources.Database;
using System.Linq;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Bot.Data;

namespace Bot.Core.Commands
{
    public class Shop : ModuleBase<SocketCommandContext>
    {
        [Group("shop"), Alias("KSK"), Summary("ShopGroup")]
        public class ShopGroup: ModuleBase<SocketCommandContext>
        {
            [Command("view")]
            public async Task Shopview()
            {
                EmbedBuilder embed = new EmbedBuilder();

                embed.WithColor(Color.LightGrey);
                embed.WithAuthor("Shopview");
                embed.WithDescription("**FOOD**\n" +
                    "**carrot**: 70 coins\n" +
                    "**salad**: 50 coins\n" +
                    "**apple**: 50 coins\n" +
                    "**mango**: 70 coins\n" +
                    "**pineapple**: 80 coins\n" +
                    "**milk**: 50 coins\n" +
                    "**steak**: 150 coins\n" +
                    "**chop**: 120 coins\n" +
                    "**fish**: 90 coins\n" +
                    "\n" +
                    "**PETS**\n" +
                    "**cat**: 1500 coins\n" +
                    "**dog**: 1500 coins\n" +
                    "**bunny**: 1000 coins\n" +
                    "**unicorn**: 10000 coins\n" +
                    "**wolf**: 5000 coins\n" +
                    "\n" +
                    "**ITEMS**\n" +
                    "**ball**: 50 coins\n" +
                    "**name tag**: 500 coins\n" +
                    "**leash**: 80 coins\n" +
                    "**rainbow**: 120 coins\n" +
                    "**little house**: 160 coins\n");

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            [Command("buy")]
            public async Task Buy(string itemName)
            {
                SocketGuildUser socketGuildUser = Context.User as SocketGuildUser;
                ulong userID = socketGuildUser.Id;
                int id;
                int prize;
                using (var DBContext = new SqliteDbContext())
                {
                   id = DBContext.items.Where(x => x.IName == itemName).Select(x => x.ItemID).FirstOrDefault();
                   prize = DBContext.items.Where(x => x.IName == itemName).Select(x => x.Price).FirstOrDefault();
                };
                int i = Data.Data.removeCoins(userID, prize, id);
                if (i == 0) await ReplyAsync(Context.User.Mention + " successfully bought " + itemName);
                else await ReplyAsync("You don't have enough coins");
            }

            [Command("login")]
            public async Task Add()
            {
                ulong userID = (Context.Message.Author as IUser).Id;
                if (Data.Data.isAvailable(userID))
                {
                    await Data.Data.addCoins(userID, 25);
                }
                else
                {
                    await Context.Message.Channel.SendMessageAsync("You have already logged in today.");
                }
            }
        }
    }
}
