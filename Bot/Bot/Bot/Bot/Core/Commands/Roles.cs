using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace Bot.Core.Commands
{
    public class Roles : ModuleBase<SocketCommandContext>
    {
        [Command("roles")]
        public async Task Rolesc() 
        {
            EmbedBuilder embed = new EmbedBuilder();
            
            embed.WithColor(Color.LightGrey);
            embed.WithAuthor("Roles");
            embed.WithDescription("**DJ**                  gives the user the rights to skip the Songs \n" +
                "**Custom Role**                  gives the user a role without bonus rights, where he can decide the name and the color itself \n" +
                "**Color**                  grants the user a Color which he can decide himself");

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
    }
}
