using System;
using System.Linq;
using Tanks.Architecture.Tanks;

namespace Tanks.Architecture
{
    public class TankMapCreator
    {
        public static ITank[,] CreateMap(string map, string separator = "\r\n")
        {
            var rows = map.Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries);
            if (rows.Select(z => z.Length).Distinct().Count() != 1)
                throw new Exception($"Wrong test map '{map}'");
            var result = new ITank[rows[0].Length, rows.Length];
            for (var x = 0; x < rows[0].Length; x++)
            for (var y = 0; y < rows.Length; y++)
                result[x, y] = CreateCreatureBySymbol(rows[y][x]);
            return result;
        }

        private static ITank CreateCreatureBySymbol(char c)
        {
            switch (c)
            {
                case 'P':
                    return new Player();
                case 'E':
                    return new Enemy();
                case 'B':
                    return new Booster();
                case 'U':
                    return new Upgrade();
                case 'S':
                    return new Shell();
                case 'W':
                    return new Wall();
                case ' ':
                    return null;
                default:
                    throw new Exception($"wrong character for ITank {c}");
            }
        }
    }
}