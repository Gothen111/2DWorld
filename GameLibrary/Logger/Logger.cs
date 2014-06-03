﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Logger
{
    public class Logger
    {
        private static int LogLevel = 2;
        private static bool OnlyError = false;

        public static void LogErr(String _Msg)
        {
            if (LogLevel >= 0 || OnlyError)
            {
                Console.ForegroundColor = ConsoleColor.Red; // DarkRed
                writeToConsole("Error: " + _Msg);
            }
        }

        public static void LogDeb(String _Msg, bool _UseSameRow)
        {
            if (LogLevel >= 1 && !OnlyError)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan; // DarkCyan
                if (_UseSameRow)
                {
                    writeToConsole(String.Format("Debug: {0}    \r", _Msg));
                }
                else
                {
                    writeToConsole("Debug: " + _Msg);
                }
            }
        }

        public static void LogDeb(String _Msg)
        {
            LogDeb(_Msg, false);
        }

        public static void LogInfo(String _Msg, bool _UseSameRow)
        {
            if (LogLevel >= 2 && !OnlyError)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                if (_UseSameRow)
                {
                    writeToConsole(String.Format("Info : {0}    \r", _Msg));
                }
                else
                {
                    writeToConsole("Info : " + _Msg);
                }
            }
        }

        public static void LogInfo(String _Msg)
        {
            LogInfo(_Msg, false);
        }

        public static void writeToConsole(string _String)
        {
            Console.WriteLine(_String);
        }
    }
}
