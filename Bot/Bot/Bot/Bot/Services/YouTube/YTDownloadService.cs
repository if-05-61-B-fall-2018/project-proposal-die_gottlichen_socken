using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Bot.Services.YouTube
{
    public class YTDownloadService
    {
        public async Task<VideoData> DownloadVideo(string url)
        {
            var filename = Guid.NewGuid();

            var youtubeDl = StartYoutubeDl(
            $"-o Songs/{filename}.mp3 --restrict-filenames --extract-audio --no-overwrites --print-json --no-playlist --audio-format mp3 " +
             url);

            if (youtubeDl == null)
            {
               Console.WriteLine("Error: Unable to start process");
                return null;
            }

            var jsonOutput = await youtubeDl.StandardOutput.ReadToEndAsync();
            youtubeDl.WaitForExit();
            Console.WriteLine($"Download completed with exit code {youtubeDl.ExitCode}");

            return JsonConvert.DeserializeObject<VideoData>(jsonOutput);
        }

        public async Task<StreamData> GetLivestreamData(string url)
        {
            var youtubeDl = StartYoutubeDl("--print-json --skip-download " + url);
            var jsonOutput = await youtubeDl.StandardOutput.ReadToEndAsync();
            youtubeDl.WaitForExit();

            return JsonConvert.DeserializeObject<StreamData>(jsonOutput);
        }

        private Process StartYoutubeDl(string arguments)
        {
            ProcessStartInfo ffmpeg = new ProcessStartInfo
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                FileName = "youtube-dl",
                Arguments = arguments
            };
            return Process.Start(ffmpeg);
        }
    }
}
