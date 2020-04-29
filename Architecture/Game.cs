using System.Windows.Forms;

namespace Tanks.Architecture
{
    class Game
    {
        public static ITank[,] Map;
        public static bool IsOver;
        public static Keys KeyPressed;
        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);
        public static void CreateMap()
        {
            Map = TankMapCreator.CreateMap("");
        }
    }
}
