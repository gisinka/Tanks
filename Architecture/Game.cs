using System.Drawing;
using System.Windows.Forms;

namespace Tanks.Architecture
{
    class Game
    {
        public static IGameObject[,] Map;
        public static bool IsOver;
        public static bool IsShoot;
        public static Point Delta;
        public static Keys KeyPressed;
        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);
        public static void CreateMap()
        {
            Map = TankMapCreator.CreateMap("");
        }
    }
}
