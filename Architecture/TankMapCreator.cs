using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tanks.Architecture.GameObjects;

namespace Tanks.Architecture
{
    public static class TankMapCreator
    {
        private static readonly Dictionary<char, Func<IGameObject>> GameObjectCreator =
            new Dictionary<char, Func<IGameObject>>
            {
                ['P'] = () => new Player(),
                ['I'] = () => new PlayerUpgraded(new Point(0, -1)),
                ['E'] = () => new Enemy(),
                ['Y'] = () => new EnemyUpgraded(new Point(0, -1)),
                ['D'] = () => new DoubleShell(),
                ['U'] = () => new Upgrade(),
                ['S'] = () => new Shell(),
                ['W'] = () => new Wall(),
                [' '] = () => null
            };

        public static IGameObject[,] CreateMap(string map, string separator = "\r\n")
        {
            var rows = map.Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries);
            if (rows.Select(z => z.Length).Distinct().Count() != 1)
                throw new Exception($"Wrong test map '{map}'");
            var result = new IGameObject[rows[0].Length, rows.Length];
            for (var x = 0; x < rows[0].Length; x++)
            for (var y = 0; y < rows.Length; y++)
                result[x, y] = GameObjectCreator[rows[y][x]]();
            return result;
        }
    }
}