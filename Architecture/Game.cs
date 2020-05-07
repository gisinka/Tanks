using System;
using System.Collections.Generic;
using System.Drawing;
using Tanks.Architecture.GameObjects;

namespace Tanks.Architecture
{
    internal static class Game
    {
        private const string MapWithPlayerEnemy = @"
P   W  E U
          
          
";
        public static readonly Dictionary<Type, Func<Point, Shell>> Shells= new Dictionary<Type, Func<Point, Shell>>()
        {
            [typeof(Player)] = x => new Shell(x),
            [typeof(PlayerUpgraded)] = x => new DoubleShell(x),
            [typeof(Enemy)] = x => new Shell(x),
            [typeof(EnemyUpgraded)] = x => new DoubleShell(x)

        };
        public static readonly Random Rnd = new Random();
        public static IGameObject[,] Map;
        public static bool IsOver;
        public static bool IsShoot = false;
        public static Point Delta = new Point(0, 0);
        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);
        public static void CreateMap()
        {
            Map = TankMapCreator.CreateMap(MapWithPlayerEnemy);
        }

        public static void CreateMap(string map)
        {
            Map = TankMapCreator.CreateMap(map);
        }

        public static bool IsInMap(int x, int y)
        {
            return x > -1 && y > -1 && x < Game.MapWidth && y < Game.MapHeight;
        }
    }
}
