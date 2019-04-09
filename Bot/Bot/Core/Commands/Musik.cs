using System;
using System.Threading.Tasks;

using Bot.Services;
using Bot.Services.YouTube;

using Discord;
using Discord.Commands;

namespace Bot.Core.Commands
{
    public class Musik:ModuleBase<SocketCommandContext>
    {
        public YTDownloadService YoutubeDownloadService { get; set; }

        public SongService SongService { get; set; }


        [Command("Join")]
        public async Task JoinChannel(IVoiceChannel channel = null)
        {
            channel = channel ?? (Context.Message.Author as IGuildUser)?.VoiceChannel;
            SongService.SetMessageChannel(Context.Message.Channel); 
            if (channel == null) { await Context.Channel.SendMessageAsync("User must be in a voice channel"); return; }
            var audioClient = await channel.ConnectAsync();
            
            SongService.SetAudio(audioClient);
        }

        [Command("Play")]
        public async Task Request(string url ,IVoiceChannel channel =null)
        {
            await PlayMusic(url, 48);
        }

        private async Task PlayMusic(string url, int speedModifier)
        {
            try
            {
                if(SongService.GetChannel()!=(Context.Message.Author as IGuildUser).VoiceChannel)
                    SongService.SetVoiceChannel((Context.Message.Author as IGuildUser).VoiceChannel);
                var downloadAnnouncement = await ReplyAsync($"{Context.User.Mention} attempting to download {url}");
                var video = await YoutubeDownloadService.DownloadVideo(url);
                await downloadAnnouncement.DeleteAsync();

                if (video == null)
                {
                    await ReplyAsync($"{Context.User.Mention} unable to queue song");
                    return;
                }

                video.Requester = Context.User.Mention;
                video.Speed = speedModifier;
                
                await ReplyAsync($"{Context.User.Mention} queued **{video.Title}** | `{TimeSpan.FromSeconds(video.Duration)}` | {url}");

                SongService.Queue(video);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while processing song request: {e}");
            }
        }

        [Command("stream", RunMode = RunMode.Async)]
        public async Task Stream(string url)
        {
            try
            {
                var downloadAnnouncement = await ReplyAsync($"{Context.User.Mention} attempting to stream {url}");
                var stream = await YoutubeDownloadService.GetLivestreamData(url);
                await downloadAnnouncement.DeleteAsync();

                if (stream == null)
                {
                    await ReplyAsync($"{Context.User.Mention} unable to stream");
                    return;
                }

                stream.Requester = Context.User.Mention;
                stream.Url = url;


                await ReplyAsync($"{Context.User.Mention} queued **{stream.Title}** | `{stream.DurationString}` | {url}");

                SongService.Queue(stream);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while processing song requet: {e}");
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

        [Command("info")]
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