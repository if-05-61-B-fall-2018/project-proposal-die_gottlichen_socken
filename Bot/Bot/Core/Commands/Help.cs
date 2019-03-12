using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace Bot.Core.Commands
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task Helpc()
        {
            EmbedBuilder embed = new EmbedBuilder();

            embed.WithColor(Color.LightGrey);
            embed.WithAuthor("Help");
            embed.WithDescription("**!help**                  describes all functions");

            await Context.Channel.SendMessageAsync("",false,embed.Build());
        }
    }
}
