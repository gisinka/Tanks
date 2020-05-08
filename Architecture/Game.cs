using System;
using System.Collections.Generic;
using System.Drawing;
using Tanks.Architecture.GameObjects;

namespace Tanks.Architecture
{
    internal static class Game
    {
        public static readonly Dictionary<Type, Func<Point, Shell>> Shells= new Dictionary<Type, Func<Point, Shell>>()
        {
            [typeof(Player)] = x => new Shell(x),
            [typeof(PlayerUpgraded)] = x => new DoubleShell(x),
            [typeof(Enemy)] = x => new Shell(x),
            [typeof(EnemyUpgraded)] = x => new DoubleShell(x)

        };
        public static readonly Random Rnd = new Random();
        public static IGameObject[,] Map;
        public static byte Scores;
        public static bool IsShoot;
        public static Point Delta = new Point(0, 0);
        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);
        public static void CreateMap()
        {
            Map = TankMapCreator.CreateMap(Maps.FirstMap);
        }

        public static void CreateMap(string map)
        {
            IsShoot = false;
            Scores = 0;
            Delta = new Point();
            Map = TankMapCreator.CreateMap(map);
        }

        public static bool IsInMap(int x, int y)
        {
            return x > -1 && y > -1 && x < Game.MapWidth && y < Game.MapHeight;
        }
    }
}
