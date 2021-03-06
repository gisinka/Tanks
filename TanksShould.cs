﻿using System.Drawing;
using NUnit.Framework;
using Tanks.Architecture;
using Tanks.Architecture.GameObjects;

namespace Tanks
{
    [TestFixture]
    internal static class TanksShould
    {
        private const string MapWithEnemy = @"
E  
";

        private const string MapWithEnemyShell = @"
S
 
E
";

        private const string MapWithPlayerEnemy = @"
E P
";

        private const string MapWithPlayerUpgradedEnemy = @"
Y P
";

        private const string MapWithUpgradeEnemy = @"
E U
";

        private const string MapWithWallEnemy = @"
  
  
EW
";

        private static void DoActions()
        {
            for (var i = 0; i < 2; i++)
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

                if (command.TransformTo != null && (command.DeltaX != 0 || command.DeltaY != 0) &&
                    Game.IsInMap(x + command.DeltaX, y + command.DeltaY))
                {
                    Game.Map[x + command.DeltaX, y + command.DeltaY] = command.TransformTo;
                    Game.Map[x, y] = null;
                }

                if (command.CreateTo != null && command.CreateTo.Orientation != new Point(0, 0) &&
                    Game.IsInMap(x + command.CreateTo.Orientation.X, y + command.CreateTo.Orientation.Y))
                    Game.Map[x + command.CreateTo.Orientation.X, y + command.CreateTo.Orientation.Y] =
                        command.CreateTo;
            }
        }

        [Test]
        public static void KillingTest()
        {
            Game.CreateMap(MapWithEnemyShell);
            DoActions();
            Assert.AreEqual(true, Game.Map[0, 2] == null);
        }

        [Test]
        public static void RidingTest()
        {
            Game.CreateMap(MapWithEnemy);
            DoActions();
            Assert.AreEqual(true, Game.Map[2, 0] is Enemy);
        }

        [Test]
        public static void ShootingTest()
        {
            Game.CreateMap(MapWithPlayerEnemy);
            DoActions();
            Assert.AreEqual(true, Game.Map[2, 0] is Shell);
        }

        [Test]
        public static void UpgradedShootingTest()
        {
            Game.CreateMap(MapWithPlayerUpgradedEnemy);
            DoActions();
            Assert.AreEqual(true, Game.Map[2, 0] is DoubleShell);
        }

        [Test]
        public static void UpgradeTest()
        {
            Game.CreateMap(MapWithUpgradeEnemy);
            DoActions();
            Assert.AreEqual(true, Game.Map[2, 0] is EnemyUpgraded);
        }

        [Test]
        public static void WallTest()
        {
            Game.CreateMap(MapWithWallEnemy);
            DoActions();
            Assert.AreEqual(true, Game.Map[0, 0] is Enemy);
        }
    }
}