using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Bot.Data;

namespace Bot.Core.Commands
{
    public class Blacklist : ModuleBase<SocketCommandContext>
    {
        [Group("blacklist")]
        public class BlackListGroup : ModuleBase<SocketCommandContext>
        {
            [Command("add")]
            public async Task addBlackList(IUser user = null)
            {
                ulong userID = 0;
                if (user == null) { userID = (Context.Message.Author as IUser).Id; user = (Context.Message.Author as IUser); }
                else { userID = user.Id; }
                if (Data.Data.addUserToBlacklist(userID) == 0)
                {
                    await ReplyAsync("User successfully added to blacklist");
                }
                else
                {
                    await ReplyAsync("User is allready in the blacklist");
                }
            }

            [Command("remove")]
            public async Task removeBlacklist(IUser user = null)
            {
                ulong userID = 0;
                if (user == null) { userID = (Context.Message.Author as IUser).Id; user = (Context.Message.Author as IUser); }
                else { userID = user.Id; }
                if (Data.Data.removeUserfromBlacklist(userID) == 0)
                {
                    await ReplyAsync("User successfully removed from blacklist");
                }
                else
                {
                    await ReplyAsync("User is not in the blacklist");
                }
            }
        }
    }
}
