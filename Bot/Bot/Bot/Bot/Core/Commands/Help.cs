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
            embed.WithDescription($"{"**!help**",20} {"describes all functions"} \n" +
                "**!play <Youtube Link>**                  {bot joins your channel and play your requested music, -20} \n" +
                "**!shop**                  {displays you all items which are available, -20}");
            


            await Context.Channel.SendMessageAsync("",false,embed.Build());
        }
    }
}
