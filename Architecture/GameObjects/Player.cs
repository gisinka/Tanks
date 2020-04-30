using System.Drawing;

namespace Tanks.Architecture.GameObjects
{
    internal class Player : ITank
    {
        public Point Orientation { get; set; }

        public virtual string GetImageFileName()
        {
            return "Player.jpg";
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public virtual TankCommand Act(int x, int y)
        {
            var command = new TankCommand();
            if (Game.Delta == Orientation)
                command = new TankCommand {DeltaX = Game.Delta.X, DeltaY = Game.Delta.Y};
            if (!Game.IsShoot || !IsAbleToStep(Orientation.X + x, Orientation.Y + y)) return command;
            Game.Map[Orientation.X + x, Orientation.Y + y] = new Shell(Orientation);
            Game.IsShoot = false;

            return command;
        }

        public bool DeadInConflict(IGameObject conflictedObject)
        {
            return conflictedObject is DoubleShell || conflictedObject is Shell;
        }

        protected static bool IsAbleToStep(int x, int y)
        {
            return x > -1 && y > -1 && x < Game.MapWidth && y < Game.MapHeight && !(Game.Map[x, y] is Wall);
        }
    }
}