using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace DiscordNew.Models
{
    public class ApplicationSettings
    {
        public string DiscordBotActivity { get; set; }
        public string Token { get; set; }
        public string COMMAND_PREFIX { get; set; }
    }
}
