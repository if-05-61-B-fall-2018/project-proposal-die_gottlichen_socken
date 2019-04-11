using Discord;
using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using Discord.Audio;

namespace Bot.Services
{
    public class SongService
    {
        private IVoiceChannel _voiceChannel;
        private IMessageChannel _messageChannel;
        private BufferBlock<IPlayable> _songQueue;
        private IAudioClient _audio;

        public void SetAudio(IAudioClient voiceChannel)
        {
            this._audio = voiceChannel;
        }

        public IVoiceChannel GetChannel()
        {
            return this._voiceChannel;
        }

        public SongService()
        {
            _songQueue = new BufferBlock<IPlayable>();
        }

        public AudioService AudioPlaybackService { get; set; }

        public IPlayable NowPlaying { get; private set; }

        public void SetVoiceChannel(IVoiceChannel voiceChannel)
        {
            this._voiceChannel = voiceChannel;
            ProcessQueue();
        }

        public void SetMessageChannel(IMessageChannel messageChannel)
        {
            this._messageChannel = messageChannel;
        }

        public void Next()
        {
            NowPlaying = null;
            AudioPlaybackService.StopCurrentOperation();
        }

        public IList<IPlayable> Clear()
        {
            _songQueue.TryReceiveAll(out var skippedSongs);
            AudioPlaybackService.StopCurrentOperation();
            NowPlaying = null;
            return skippedSongs;
        }

        public void Queue(IPlayable video)
        {
            _songQueue.Post(video);
        }

        public int GetQueueLenght()
        {
            return _songQueue.Count;
        }

        public async void ProcessQueue()
        {
            while (await _songQueue.OutputAvailableAsync())
            {
                NowPlaying = await _songQueue.ReceiveAsync();
                try
                {
                     Console.WriteLine("Connecting to voice channel");
                     using (var audioClient = await _voiceChannel.ConnectAsync())
                     {
                         Console.WriteLine("Connected!");
                         await AudioPlaybackService.SendAsync(audioClient, NowPlaying.Uri, NowPlaying.Speed);
                     }
                    await AudioPlaybackService.SendAsync(_audio, NowPlaying.Uri, NowPlaying.Speed);
                    NowPlaying.OnPostPlay();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error while playing song: {e}");
                }
            }
        }
    }
}
