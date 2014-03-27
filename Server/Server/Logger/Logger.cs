using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Logger
{
    class Logger
    {
        private static int LogLevel = 0;
        private static bool OnlyError = false;

        public static void LogErr(String _Msg)
        {
            if (LogLevel >= 0 || OnlyError)
                Console.WriteLine(_Msg);
        }

        public static void LogDeb(String _Msg)
        {
            if (LogLevel >= 1 && !OnlyError)
                Console.WriteLine(_Msg);
        }

        public static void LogInfo(String _Msg)
        {
            if (LogLevel >= 2 && !OnlyError)
                Console.WriteLine(_Msg);
        }
    }
}
