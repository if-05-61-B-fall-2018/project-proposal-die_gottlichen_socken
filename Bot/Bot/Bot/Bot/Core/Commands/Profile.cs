using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using Bot.Resources.Database;

using Discord;
using Discord.Commands;

namespace Bot.Core.Commands
{
    public class Profile : ModuleBase<SocketCommandContext>
    {
        [Command("Profile")]
        public async Task UserProfile(ulong userID = 1)
        {
            if (userID == 1) userID = (Context.Message.Author as IGuildUser).Id;
            EmbedBuilder embed = new EmbedBuilder();

            using (var DBContext = new SqliteDbContext())
            {
                MyUser current = DBContext.myUser.Where(x => x.UserID == userID).FirstOrDefault();
                UserItems userItems = DBContext.userItems.Where(x => x.UserID == userID).FirstOrDefault();
                var allitems = DBContext.userItems.AsEnumerable().Where(x => x.UserID == userID).Select(x => x.ItemID);
                List<string> list = new List<string>();

                foreach (var item in allitems)
                {
                    string name = DBContext.items.Where(x => x.ItemID == item).Select(x => x.IName).ToString();
                    list.Add(name);
                }

                embed.WithColor(Color.LightGrey);
                embed.WithAuthor("Profile");
                embed.WithDescription("Name: " + current.UserID + "\n Coins: " + current.Coins + "\n Items: ");

                for (int i = 0; i < list.Count; i++)
                {
                    embed.WithDescription(list[i] + "\n");
                }

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
        }
    }
}
