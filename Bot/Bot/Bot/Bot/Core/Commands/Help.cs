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
            embed.WithDescription($"``` \n{"!help",20} {"describes all functions"} \n" +
                "!play <Youtube Link>                  bot joins the channel and plays the requested music \n" +
                "!skip                  skips the current song \n" +
                "!clearqueue                  removes all queued songs from the music queue \n" +
                "!shop view                  displays all items which are available \n" +
                "!shop login                  user gets 25 coins for daily login \n" +
                "!shop buy <item name>                  this item will be added to your inventar \n" +
                "!profile                  displays username, coins and items of a user \n```");




            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}