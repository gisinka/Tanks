using System.Drawing;

namespace Tanks.Architecture.GameObjects
{
    public class Enemy : Tank
    {
        public override string GetImageFileName()
        {
            return "Enemy.png";
        }

        public override int GetDrawingPriority()
        {
            return 2;
        }

        public override TankCommand Act(int x, int y)
        {
            var command = new TankCommand();
            var coords = FindPlayer(x, y);
            if (coords != new Point(-1, -1))
            {
                var correctOrientation = new Point
                {
                    X = CalculateAxisDirection(x, coords.X),
                    Y = CalculateAxisDirection(y, coords.Y)
                };
                if (correctOrientation == Orientation &&
                    IsAbleToStep(x + correctOrientation.X, y + correctOrientation.Y))
                    command.CreateTo = Game.Shells[GetType()](Orientation);
                else
                    Orientation = correctOrientation;
            }
            else
            {
                if (IsAbleToStep(x + Orientation.X, y + Orientation.Y))
                {
                    command.DeltaX = Orientation.X;
                    command.DeltaY = Orientation.Y;
                    command.TransformTo = this;
                    if (Game.Map[x + Orientation.X, y + Orientation.Y] is Upgrade)
                        command.TransformTo = new EnemyUpgraded(Orientation);
                }
                else
                {
                    Orientation = FindNewOrientation(x, y);
                }
            }

            return command;
        }

        public override bool DeadInConflict(IGameObject conflictedObject)
        {
            return conflictedObject is DoubleShell || conflictedObject is Shell || conflictedObject is Enemy;
        }

        private static Point FindPlayer(int x, int y)
        {
            for (var i = x + 1; i < Game.MapWidth; i++)
            {
                if (Game.Map[i, y] is Wall || Game.Map[i, y] is Shell || Game.Map[i, y] is Enemy)
                    break;
                if (Game.Map[i, y] is Player)
                    return new Point(i, y);
            }

            for (var i = x - 1; i > -1; i--)
            {
                if (Game.Map[i, y] is Wall || Game.Map[i, y] is Shell || Game.Map[i, y] is Enemy)
                    break;
                if (Game.Map[i, y] is Player)
                    return new Point(i, y);
            }

            for (var i = y + 1; i < Game.MapHeight; i++)
            {
                if (Game.Map[x, i] is Wall || Game.Map[x, i] is Shell || Game.Map[x, i] is Enemy)
                    break;
                if (Game.Map[x, i] is Player)
                    return new Point(x, i);
            }

            for (var i = y - 1; i > -1; i--)
            {
                if (Game.Map[x, i] is Wall || Game.Map[x, i] is Shell || Game.Map[x, i] is Enemy)
                    break;
                if (Game.Map[x, i] is Player)
                    return new Point(x, i);
            }

            return new Point(-1, -1);
        }

        private static int CalculateAxisDirection(int thisCoords, int playerCoords)
        {
            return playerCoords - thisCoords > 0 ? 1 : playerCoords - thisCoords == 0 ? 0 : -1;
        }

        private Point FindNewOrientation(int x, int y)
        {
            for (var dy = -1; dy <= 1; dy++)
            for (var dx = -1; dx <= 1; dx++)
            {
                if (dx != 0 && dy != 0 || dx == 0 && dy == 0) continue;
                if (Game.Rnd.Next(2) == 1) continue;
                if (IsAbleToStep(x + dx, y + dy))
                    return new Point(dx, dy);
            }

            return new Point(1, 0);
        }

        protected override bool IsAbleToStep(int x, int y)
        {
            return x > -1 && y > -1 && x < Game.MapWidth && y < Game.MapHeight &&
                   (Game.Map[x, y] == null || Game.Map[x, y] is Upgrade) &&
                   !(Game.IsInMap(x + Orientation.X, y + Orientation.Y) &&
                     Game.Map[x + Orientation.X, y + Orientation.Y] is Enemy);
        }
    }
}