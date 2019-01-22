using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace Bot.Core.Commands
{
    class Shop : ModuleBase<SocketCommandContext>
    {
        [Command("shopview")]
        public async Task Shopview()
        {
            EmbedBuilder embed = new EmbedBuilder();

            embed.WithColor(Color.LightGrey);
            embed.WithAuthor("Shopview");
            embed.WithDescription("FOOD\n" +
                "**carrot**                  20 coins\n" +
                "**salad**                  15 coins\n" +
                "**steak**                  50 coins\n" +
                "**pineapple                  30 coins\n" +
                "**milk**                  15 coins\n" +
                "**fish                  30 coins\n" +
                "**apple**                  20 coins\n" +
                "**mango**                  25 coins\n" +
                "**chop**                  40 coins\n" +
                
                "PETS\n" +
                "**cat**                  150 coins\n" +
                "**dog**                  150 coins\n" +
                "**bunny**                  100 coins\n" +
                "**unicorn**                  300 coins\n" +
                "**wolf**                  200 coins\n" +


                "ITEMS\n" +
                "**ball**                  10 coins\n" +
                "**name tag**                  50 coins\n" +
                "**leash**                  25 coins\n" +
                "**rainbow**                  45 coins\n" +
                "**little house**                  30 coins\n" +
                
                "ROLES" +
                "**DJ**                  150 coins\n" +
                "**Color**                  50 coins\n" +
                "**Custom role**                  100 coins\n");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}
