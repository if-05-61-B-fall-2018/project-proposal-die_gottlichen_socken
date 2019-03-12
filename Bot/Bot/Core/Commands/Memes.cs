using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace Bot.Core.Commands
{
    public class Memes:ModuleBase<SocketCommandContext>
    {
        [Command("DeusVult")]
        public async Task DeusVult()
        {
            await Context.Channel.SendMessageAsync("Deus Vult!");
        }

        [Command("Schlachtruf")]
        public async Task Schlachtruf()
        {
            await Context.Channel.SendMessageAsync("Döööööööt!");
        }

        [Command("ne")]
        public async Task Ne()
        {
            await Context.Channel.SendMessageAsync("Bot sagt nein!");
        }

        [Command("Detlef")]
        public async Task Detlef()
        {
            await Context.Channel.SendMessageAsync("Das klebt wie Schwanz!");
        }

        [Command("ja")]
        public async Task Ja()
        {
            await Context.Channel.SendMessageAsync("In der Tat!");
        }
    }
}
