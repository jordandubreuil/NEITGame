using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using NEITGameEngine.SaveDataSystem;

namespace NEITGameEngine
{
    public static class Globals
    {
        public static Point WindowSize { get; set; }
        public static Point ScreenSize { get; set; }
        public static bool Paused { get; set; }
        public static SaveSystem GlobalSaveSystem { get; set; }
        public static GraphicsDeviceManager Graphics { get; set; }
    }
}
