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
            var command = new TankCommand {DeltaX = Orientation.X, DeltaY = Orientation.Y, TransformTo = this};
            if (!IsAbleToStep(x + Orientation.X, y + Orientation.Y))
                command.TransformTo = null;
            return command;

        }

        public virtual bool DeadInConflict(IGameObject conflictedObject)
        {
            return conflictedObject != null;
        }

        private static bool IsAbleToStep(int x, int y)
        {
            return x > -1 && y > -1 && x < Game.MapWidth && y < Game.MapHeight;
        }
    }
}
