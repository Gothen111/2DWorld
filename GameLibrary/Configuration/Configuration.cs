using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Commands;
using GameLibrary.Connection;

namespace GameLibrary.Configuration
{
    public class Configuration
    {
        public static bool isHost;
        public static CommandManager commandManager;
        public static NetworkManager networkManager;
    }
}
