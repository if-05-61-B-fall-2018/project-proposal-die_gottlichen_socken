using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Bot.Services;
using Bot.Services.YouTube;

using Discord;
using Discord.Commands;
using Discord.Audio;

namespace Bot.Core.Commands
{
    public class Musik:ModuleBase<SocketCommandContext>
    {
        public YouTubeDownloadService YoutubeDownloadService { get; set; }

        public SongService SongService { get; set; }


        [Command("Join")]
        public async Task JoinChannel(IVoiceChannel channel = null)
        {
            channel = channel ?? (Context.Message.Author as IGuildUser)?.VoiceChannel;
            SongService.SetMessageChannel(Context.Message.Channel); 
            //channel = channel ?? (msg.Author as IGuildUser)?.VoiceChannel;
            //if (channel == null) { await msg.Channel.SendMessageAsync("User must be in a voice channel, or a voice channel must be passed as an argument."); return; }
            if (channel == null) { await Context.Channel.SendMessageAsync("User must be in a voice channel"); return; }
            var audioClient = await channel.ConnectAsync();
            
            SongService.SetAudio(audioClient);
        }

        /*private Process CreateStream(string path)
         {
             return Process.Start(new ProcessStartInfo
             {
                 FileName = "ffmpeg",
                 Arguments = $"-hide_banner -loglevel panic -i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1",
                 UseShellExecute = false,
                 RedirectStandardOutput = true,
             });
         }

         private async Task SendAsync(IAudioClient client, string path)
         {
             // Create FFmpeg using the previous example
             using (var ffmpeg = CreateStream(path))
             using (var output = ffmpeg.StandardOutput.BaseStream)
             using (var discord = client.CreatePCMStream(AudioApplication.Mixed))
             {
                 try { await output.CopyToAsync(discord); }
                 finally { await discord.FlushAsync(); }
             }
         }*/

        [Command("Play")]
        public async Task Request(string url ,IVoiceChannel channel =null)
        {
            //await JoinChannel();
            await PlayMusic(url, 48);
        }

        private async Task PlayMusic(string url, int speedModifier)
        {
            try
            {
                /*if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    await ReplyAsync($"{Context.User.Mention} please provide a valid song URL");
                    return;
                }*/
                if(SongService.GetChannel()!=(Context.Message.Author as IGuildUser).VoiceChannel)
                    SongService.SetVoiceChannel((Context.Message.Author as IGuildUser).VoiceChannel);
                var downloadAnnouncement = await ReplyAsync($"{Context.User.Mention} attempting to download {url}");
                var video = await YoutubeDownloadService.DownloadVideo(url);
                await downloadAnnouncement.DeleteAsync();

                if (video == null)
                {
                    await ReplyAsync($"{Context.User.Mention} unable to queue song, make sure its is a valid supported URL or contact a server admin.");
                    return;
                }

                video.Requester = Context.User.Mention;
                video.Speed = speedModifier;
                
                await ReplyAsync($"{Context.User.Mention} queued **{video.Title}** | `{TimeSpan.FromSeconds(video.Duration)}` | {url}");

                SongService.Queue(video);
               /* if (SongService.GetQueueLenght() == 1)
                {
                    SongService.ProcessQueue();
                }*/
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while processing song request: {e}");
            }
        }

        [Command("stop")]
        public async Task ClearQueue()
        {
            SongService.Clear();
            await ReplyAsync("Queue cleared");
        }

        [Command("skip")]
        public async Task SkipSong()
        {
            SongService.Next();
            await ReplyAsync("Skipped song");
        }

        [Command("cursong")]
        public async Task NowPlaying()
        {
            if (SongService.NowPlaying == null)
            {
                await ReplyAsync($"{Context.User.Mention} current queue is empty");
            }
            else
            {
                await ReplyAsync($"{Context.User.Mention} now playing `{SongService.NowPlaying.Title}` requested by {SongService.NowPlaying.Requester}");
            }
        }
    }
}