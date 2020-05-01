using System.Drawing;
using NUnit.Framework;
using Tanks.Architecture;
using Tanks.Architecture.GameObjects;

namespace Tanks
{
    [TestFixture]
    internal static class TanksShould
    {
        private const string MapWithPlayerEnemy = @"
P E
   
";

        [Test]
        public static void ShootingTest()
        {
            Game.CreateMap(MapWithPlayerEnemy);
            for (var i = 0; i < 10; i++)
            for (var x = 0; x < Game.MapWidth; x++)
            for (var y = 0; y < Game.MapHeight; y++)
            {
                if (Game.Map[x, y] == null) continue;
                var creature = Game.Map[x, y];
                if (creature == null) continue;
                var command = creature.Act(x, y);
                if (command == null)
                {
                    Game.Map[x, y] = null;
                    continue;
                }
                if (command.TransformTo != null && (command.DeltaX != 0 || command.DeltaY != 0) && IsInMap(x + command.DeltaX, y + command.DeltaY))
                {
                    Game.Map[x + command.DeltaX, y + command.DeltaY] = command.TransformTo;
                    Game.Map[x, y] = null;
                }

                if (command.CreateTo != null && command.CreateTo.Orientation != new Point(0, 0) && IsInMap(x + command.CreateTo.Orientation.X, y + command.CreateTo.Orientation.Y))
                    Game.Map[x + command.CreateTo.Orientation.X, y + command.CreateTo.Orientation.Y] = command.CreateTo;
            }

            Assert.AreEqual(true, Game.Map[0, 0] is Enemy);
        }

        private static bool IsInMap(int x, int y)
        {
            return x > -1 && y > -1 && x < Game.MapWidth && y < Game.MapHeight;
        }
    }
}