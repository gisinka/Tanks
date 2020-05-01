using System.Drawing;

namespace Tanks.Architecture.GameObjects
{
    public class Enemy : Tank
    {
        public override string GetImageFileName()
        {
            return "Enemy.jpg";
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
                var correctOrientation = FindCorrectOrientation(new Point(x, y), coords);
                if (correctOrientation == Orientation)
                {
                    command.CreateTo = new Shell(Orientation);
                }
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
            return conflictedObject is DoubleShell || conflictedObject is Shell;
        }

        protected static Point FindPlayer(int x, int y)
        {
            for (var i = x; i < Game.MapWidth; i++)
            {
                if (Game.Map[i, y] is Wall || Game.Map[i, y] is Shell)
                    break;
                if (Game.Map[i, y] is Player)
                    return new Point(i, y);
            }

            for (var i = x; i > -1; i--)
            {
                if (Game.Map[i, y] is Wall || Game.Map[i, y] is Shell)
                    break;
                if (Game.Map[i, y] is Player)
                    return new Point(i, y);
            }

            for (var i = y; i < Game.MapHeight; i++)
            {
                if (Game.Map[x, i] is Wall || Game.Map[i, y] is Shell)
                    break;
                if (Game.Map[x, i] is Player)
                    return new Point(x, i);
            }

            for (var i = y; i > -1; i--)
            {
                if (Game.Map[x, i] is Wall || Game.Map[i, y] is Shell)
                    break;
                if (Game.Map[x, i] is Player)
                    return new Point(x, i);
            }

            return new Point(-1, -1);
        }

        protected static Point FindCorrectOrientation(Point thisCoords, Point playerCoords)
        {
            return new Point
            {
                X = playerCoords.X - thisCoords.X > 0 ? 1 : playerCoords.X - thisCoords.X == 0 ? 0 : -1,
                Y = playerCoords.Y - thisCoords.Y > 0 ? 1 : playerCoords.Y - thisCoords.Y == 0 ? 0 : -1,
            };
        }

        protected static Point FindNewOrientation(int x, int y)
        {
            for (var dy = -1; dy <= 1; dy++)
            for (var dx = -1; dx <= 1; dx++)
            {
                if (dx != 0 && dy != 0) continue;
                if (IsAbleToStep(x + dx, y + dy))
                    return new Point(dx, dy);
            }
            return new Point(1, 0);
        }
    }
}