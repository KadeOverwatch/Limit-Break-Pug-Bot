using System;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using Microsoft.Extensions.Configuration;

/* You need to add your own config.json file to the project. Contact Kade. */

namespace Limit_Break_Pug_Bot
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new Bot();
            bot.Start().GetAwaiter().GetResult();
        }
    }
}
