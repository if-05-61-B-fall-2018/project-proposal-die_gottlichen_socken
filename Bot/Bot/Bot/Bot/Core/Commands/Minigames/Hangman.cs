using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bot.Resources.Database;

using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;

namespace Bot.Core.Commands.Minigames
{
    public class Hangman : ModuleBase<SocketCommandContext>
    {
        [Command("hangman")]
        public async Task StartHangman()
        {
            IUser user = Context.Message.Author;

            Random rnd = new Random(DateTime.Now.Millisecond);
            string[] words = { "hallo", "fabi", "flo", "every","pride","job","raise","sale","he",
"book","square","until","mouth","bottom","guess",
"kitchen","discover","stems","pay","potatoes","see",
"opposite","rope","snake","film","share","tiny",
"he","been","begun","discussion","during","eaten",
"able","completely","aside","wrote","child","refused",
"cow","her","kept","crack","phrase","traffic"};

            int i = rnd.Next(0, words.Length);
            string word = words[i];
            Global.hangmanWord = word;
            Global.thisusr = user;
            string tmp = String.Empty;

            for (int l = 0; l < word.Length; l++)
            {
                tmp += "?";
            }
            Global.hangmanIsWord = tmp;
            Global.isHMgame = true;
            await ReplyAsync(""+tmp);
        }

        internal static async Task CheckChar(SocketCommandContext Context, string msg)
        {
            string word = Global.hangmanWord;
            string output = String.Empty;
            int j = 0;
            for (int k = 0; k < word.Length; k++)
            {
                if (word.Contains(msg))
                {
                    if (word[k].ToString() == msg)
                    {
                        for (int i = 0; i < word.Length; i++)
                        {
                            if (Global.hangmanIsWord[i] != '?')
                            {
                                output += Global.hangmanIsWord[i];
                            }
                            else if(msg == Global.hangmanWord[i].ToString())
                            {
                                output += word[k];
                                j = 1;
                            }
                            else
                            {
                                output += "?";
                            }                          
                        }                        
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync(Global.hangmanIsWord);
                    return;
                }
                if (j == 1) break;
            }
            Global.hangmanIsWord = output;
            await Context.Channel.SendMessageAsync(output);
            if (!output.Contains("?"))
            {
                await Context.Channel.SendMessageAsync(Global.thisusr.Mention + " you have won!");
                Global.isHMgame = false;
            }
            return;
        }
    }
}