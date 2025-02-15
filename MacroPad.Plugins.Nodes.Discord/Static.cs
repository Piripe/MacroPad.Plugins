using DiscordRPC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroPad.Plugins.Nodes.Discord
{
    public static class Static
    {
        public static DiscordRpcClient client = new DiscordRpcClient("466217331270483969");

        public static async void Init()
        {
            client.Initialize();
            

        }
    }
}
