using System.Drawing;

namespace Tanks.Architecture.GameObjects
{
    public class DoubleShell : Shell
    {
        public DoubleShell()
        {
            Orientation = new Point(1, 0);
        }

        public DoubleShell(Point orientation)
        {
            Orientation = orientation;
        }
        public override string GetImageFileName()
        {
            return "DoubleShell.png";
        }

        public override TankCommand Act(int x, int y)
        {
            var command = new TankCommand {DeltaX = Orientation.X, DeltaY = Orientation.Y};
            if (Game.Map[x + Orientation.X, y + Orientation.Y] != null)
                command.TransformTo = new Shell(Orientation);
            return command;
        }

        public override bool DeadInConflict(IGameObject conflictedObject)
        {
            return conflictedObject is DoubleShell;
        }
    }
}
