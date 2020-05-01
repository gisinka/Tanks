using System.Drawing;

namespace Tanks.Architecture.GameObjects
{
    public class Shell : IGameObject
    {
        public Point Orientation { get; set; }

        public Shell()
        {
            Orientation = new Point(1, 0);
        }
        public Shell(Point orientation)
        {
            Orientation = orientation;
        }
        public virtual string GetImageFileName()
        {
            return "Shell.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public virtual TankCommand Act(int x, int y)
        {
            var command = new TankCommand();
            if (!IsAbleToStep(x + Orientation.X, y + Orientation.Y)) return null;
            command.DeltaX = Orientation.X;
            command.DeltaY = Orientation.Y;
            command.TransformTo = this;
            return command;

        }

        public bool DeadInConflict(IGameObject conflictedObject)
        {
            return conflictedObject != null;
        }

        private bool IsAbleToStep(int x, int y)
        {
            return x > -1 && y > -1 && x < Game.MapWidth && y < Game.MapHeight;
        }
    }
}
