using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace Bot.Core.Commands
{
    public class Musik:ModuleBase<SocketCommandContext>
    {
        [Command("Play")]
        public async Task JoinChannel(IVoiceChannel channel = null)
        {
            
            //channel = channel ?? (msg.Author as IGuildUser)?.VoiceChannel;
            //if (channel == null) { await msg.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }
			
            //var audioClient = await channel.ConnectAsync();
        }
    }
}
