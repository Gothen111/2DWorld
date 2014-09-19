using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Setting
{
    public class Setting
    {
        public static String logInstance;

        public static bool drawWorld = true;
        public static bool drawBlocks = true;
        public static int blockDrawRange = 40;
        public static bool drawObjects = true;
        public static bool drawPreEnvironmentObjects = true;

        public static bool debugMode = true;

        public static int resolutionX = 1024;
        public static int resolutionY = 768;
    }
}
