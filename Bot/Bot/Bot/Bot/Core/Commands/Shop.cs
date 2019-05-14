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
        [Group("shop"),Summary("ShopGroup")]
        public class ShopGroup: ModuleBase<SocketCommandContext>
        {
            [Command("view")]
            public async Task Shopview()
            {
                EmbedBuilder embed = new EmbedBuilder();

                embed.WithColor(Color.LightGrey);
                embed.WithAuthor("Shopview");
                embed.WithDescription("**FOOD**\n" +
                    "**carrot**                  20 coins\n" +
                    "**salad**                  15 coins\n" +
                    "**steak**                  50 coins\n" +
                    "**pineapple**                  30 coins\n" +
                    "**milk**                  15 coins\n" +
                    "**fish**                  30 coins\n" +
                    "**apple**                  20 coins\n" +
                    "**mango**                  25 coins\n" +
                    "**chop**                  40 coins\n" +
                    "\n" +
                    "**PETS**\n" +
                    "**cat**                  150 coins\n" +
                    "**dog**                  150 coins\n" +
                    "**bunny**                  100 coins\n" +
                    "**unicorn**                  300 coins\n" +
                    "**wolf**                  200 coins\n" +
                    "\n" +
                    "**ITEMS**\n" +
                    "**ball**                  10 coins\n" +
                    "**name tag**                  50 coins\n" +
                    "**leash**                  25 coins\n" +
                    "**rainbow**                  45 coins\n" +
                    "**little house**                  30 coins\n" +
                    "\n" +
                    "**ROLES**\n" +
                    "**DJ**                  150 coins\n" +
                    "**Color**                  50 coins\n" +
                    "**Custom role**                  100 coins\n");

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
                await Data.Data.removeCoins(userID, prize, id);
            }

            [Command("login")]
            public async Task Add()
            {
                ulong userID = (Context.Message.Author as IUser).Id;
                await Data.Data.addCoins(userID, 25);
            }
        }
        
    }
}
