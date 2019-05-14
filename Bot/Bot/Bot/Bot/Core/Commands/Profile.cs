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
        public async Task UserProfile(IUser user = null)
        {
            ulong userID = 0;
            if (user == null) { userID = (Context.Message.Author as IUser).Id; user = (Context.Message.Author as IUser); }
            else userID = user.Id;
            EmbedBuilder embed = new EmbedBuilder();

            using (var DBContext = new SqliteDbContext())
            {
                MyUser current = DBContext.myUser.Where(x => x.UserID == userID).FirstOrDefault();
                UserItems userItems = DBContext.userItems.Where(x => x.UserID == userID).FirstOrDefault();
                var allitems = DBContext.userItems.AsEnumerable().Where(x => x.UserID == userID).Select(x => x.ItemID);
                List<string> list = new List<string>();

                var itemInfo = from x in DBContext.userItems.AsEnumerable()
                               where x.UserID == userID
                               select new
                               {
                                   id = x.ItemID,
                                   amount = x.Amount
                               };

                foreach (var item in itemInfo)
                {
                    string name = DBContext.items.Where(x => x.ItemID == item.id).Select(x => x.IName).FirstOrDefault();
                    list.Add("---" + name + " " + item.amount + "x");
                }

                embed.WithColor(Color.LightGrey);
                embed.WithAuthor("Profile");
                string ret;
                ret = "**Name:** " + user.Username + "\n **Coins:** " + current.Coins + "\n **Items:** \n";

                for (int i = 0; i < list.Count; i++)
                {
                    ret += list[i] + "\n";
                }

                embed.WithDescription(ret);
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
        }
    }
}