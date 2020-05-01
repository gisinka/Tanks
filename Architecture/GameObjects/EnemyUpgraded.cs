using System.Drawing;

namespace Tanks.Architecture.GameObjects
{
    internal class EnemyUpgraded : Enemy
    {
        public override string GetImageFileName()
        {
            return "EnemyUpgraded.png";
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
                    command.CreateTo = new DoubleShell(Orientation);
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
    }
}