﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using GameLibrary.Setting;

namespace GameLibrary.Logger
{
    public class Logger
    {
        private static int LogLevel = 2;
        private static bool OnlyError = false;
        //private static string filePath = "log.txt";

        public static void LogErr(String _Msg)
        {
            if (LogLevel >= 0 || OnlyError)
            {
                Console.ForegroundColor = ConsoleColor.Red; // DarkRed
                writeToConsole("Error", _Msg);
            }
        }

        public static void LogDeb(String _Msg, bool _UseSameRow)
        {
            if (LogLevel >= 1 && !OnlyError)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan; // DarkCyan
                if (_UseSameRow)
                {
                    writeToConsole("Debug", String.Format("{0}    \r", _Msg));
                }
                else
                {
                    writeToConsole("Debug", _Msg);
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
                    writeToConsole("Info", String.Format("{0}    \r", _Msg));
                }
                else
                {
                    writeToConsole("Info", _Msg);
                }
            }
        }

        public static void LogInfo(String _Msg)
        {
            LogInfo(_Msg, false);
        }

        private static void writeToConsole(String _Type, String _Message)
        {
            /*if (Setting.Setting.logInstance.Equals("") || !GameLibrary.Configuration.Configuration.isHost)
            {
                Console.WriteLine(_Type + " : " + _Message);
            }
            else
            {
                saveToFile(_Type, _Message);
            }*/
            saveToFile(_Type, _Message);
        }

        private static void saveToFile(String _Type, String _Message)
        {
            if (!File.Exists(Setting.Setting.logInstance))
            {
                String[] directories = Setting.Setting.logInstance.Split('/');
                String result = "";
                for (int x = 0; x < directories.Length - 1; x++)
                    result += directories[x];
                Directory.CreateDirectory(result);
                File.Create(Setting.Setting.logInstance).Close();
            }
            StreamWriter writer = new StreamWriter(File.Open(Setting.Setting.logInstance, FileMode.Append));
            writer.WriteLine("<Date>" + DateTime.Now.TimeOfDay + "</Date>");
            writer.WriteLine("<Type>" + _Type + "</Type>");
            writer.WriteLine("<Message>" + _Message + "</Message>");
            writer.Flush();
            writer.Close();
        }
    }
}
