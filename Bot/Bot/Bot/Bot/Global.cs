using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot
{
    internal static class Global
    {
        internal static ulong messageIDtoTrack { get; set; }
        internal static string emoteMethod { get; set; }
        internal static IUser vsusr{get;set;}
        internal static IUser thisusr { get; set; }
        internal static string hangmanWord { get; set; }
        internal static string hangmanIsWord { get; set; }
        internal static string output { get; set; }
        internal static bool isHMgame { get; set; }
    }
}
