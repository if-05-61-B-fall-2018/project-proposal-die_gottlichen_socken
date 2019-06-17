using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace Bot.Core.Commands
{
    public class Memes:ModuleBase<SocketCommandContext>
    {
        [Command("meme")]
        public async Task meme()
        {
            string path = Data.Data.getrandomMeme();
            await Context.Channel.SendFileAsync(path, "lustig haha");
        }
    }
}
