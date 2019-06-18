using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using Bot.Resources.Database;
using System.Linq;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Bot.Core.Commands.Pet
{
    public class Pet : ModuleBase<SocketCommandContext>
    {
        [Group("pet"), Alias("petus", "petrus"), Summary("PetGroup")]
        public class PetGroup : ModuleBase<SocketCommandContext>
        {
            [Command("profile"), Alias("profilus")]
            public async Task petProfile(IUser user = null)
            {
                ulong userID = 0;

                if (user == null) { userID = (Context.Message.Author as IUser).Id; user = (Context.Message.Author as IUser); }
                else userID = user.Id;

                UserPets pet = Data.Data.returnPet(user.Id);

                if (pet == null) { await ReplyAsync(user.Mention + " doesnt own a pet!"); return; }

                EmbedBuilder embed = new EmbedBuilder();
                embed.WithTitle("**" + pet.PetName + "**");
                embed.WithFooter("Owner: " + user.Mention);

                embed.WithDescription("affection: " + pet.affection);

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }

            [Command("feed")]
            public async Task petFeed(string food)
            {
                ulong userID = 0;
                IUser user;

                userID = (Context.Message.Author as IUser).Id;
                user = (Context.Message.Author as IUser);

                using (var DBContext = new SqliteDbContext())
                {
                    int i =DBContext.items.Where(x=>x.IName==food).Count();
                    if (i == 0)
                    {
                        await ReplyAsync(user.Mention+" the item doesnt exist");
                        return;
                    }
                    Items item = DBContext.items.Where(x => x.IName == food).FirstOrDefault();
                    i = DBContext.userItems.Where(x => x.ItemID == item.ItemID).Count();
                    if (i == 0)
                    {
                        await ReplyAsync(user.Mention + " you dont have this item");
                        return;
                    }
                    UserItems usritem = DBContext.userItems.Where(x => x.ItemID == item.ItemID).FirstOrDefault(); ;
                    DBContext.userItems.Remove(usritem);
                    Data.Data.adjustStats(userID,"affection",item.Price);
                }
            }

            [Command("buy")]
            public async Task petBuy(string name)
            {
                ulong userID = 0;
                IUser user;

                userID = (Context.Message.Author as IUser).Id;
                user = (Context.Message.Author as IUser);

                using (var DBContext = new SqliteDbContext())
                {
                    int i = DBContext.userPets.Where(x => x.UserID == userID).Count();
                    if (i != 0)
                    {
                        await ReplyAsync(user.Mention + " you already own a pet!");
                    }
                    i = Data.Data.addPet(userID, name);
                    if (i == 1)
                    {
                        await ReplyAsync(user.Mention + " you dont have enough coins!");
                        return;
                    }
                    await ReplyAsync(user.Mention + " you bought "+name);
                }
            }

            [Command("rename")]
            public async Task petRename(string name)
            {
                ulong userID = 0;
                IUser user;

                userID = (Context.Message.Author as IUser).Id;
                user = (Context.Message.Author as IUser);

                using (var DBContext = new SqliteDbContext())
                {
                    int i = DBContext.userPets.Where(x => x.UserID == userID).Count();
                    if (i == 0)
                    {
                        await ReplyAsync(user.Mention + " dont own a pet!");
                    }
                    Data.Data.renamePet(userID,name);
                    await ReplyAsync(user.Mention + " you renamed your pet to "+name);
                }
            }
        }
    }
}
