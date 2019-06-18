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
            embed.WithDescription("**!help** describes all functions \n" +
                "**!profile** displays username, coins and items of a user \n" +
                "**!screenshare** sends a link that a user can stream in a channel \n" +
                "**!play** <Youtube Link> bot joins the channel and plays the requested music \n" +
                "**!skip** skips the current song \n" +
                "**!stream** plays a music stream \n" +
                "**!stop** removes all queued songs from the music queue \n" +
                "**!info** shows the current song \n" +
                "**!shop view** displays all items which are available \n" +
                "**!shop login** user gets 25 coins for daily login \n" +
                "**!shop buy <item name>** this item will be added to your inventar \n" +
                "**!coinflip** starts minigame coinflip \n" +
                "**!coinflipvs** starts minigame coinflip against a user \n" +
                "**!rps** starts minigame rock-paper-scissor \n" +
                "**!hangman** starts minigame hangman \n" +
                "**!pet profile** displays pet profile \n" +
                "**!pet feed <item name>** feeds a pet \n" + 
                "**!pet buy <pet>** this pet will be added to your profile \n" + 
                "**!pet rename** renames your pet \n" +
                "**!blacklist add** adds a user to the blacklist, admin function \n" +
                "**!blacklist remove** removes a user from the blacklist, admin function \n" +
                "**!meme** displays a random meme \n"
                );




            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }
        [Command("screenshare")]
        public async Task sc()
        {
            await ReplyAsync("https://discordapp.com/channels/" + Context.Guild.Id + "/" + (Context.User as IGuildUser).VoiceChannel.Id);
        }
    }
}