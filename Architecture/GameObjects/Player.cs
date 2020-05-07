using System.Drawing;

namespace Tanks.Architecture.GameObjects
{
    public class Player : Tank
    {
        public override string GetImageFileName()
        {
            return "Player.png";
        }

        public override int GetDrawingPriority()
        {
            return 2;
        }

        public override TankCommand Act(int x, int y)
        {
            var command = new TankCommand();
            if (Game.Delta == Orientation)
            {
                if (IsAbleToStep(x + Game.Delta.X, y + Game.Delta.Y))
                {
                    command.DeltaX = Game.Delta.X;
                    command.DeltaY = Game.Delta.Y;
                    command.TransformTo = this;
                    if (Game.Map[x + Orientation.X, y + Orientation.Y] is Upgrade)
                        command.TransformTo = new PlayerUpgraded(Orientation);
                }

                Orientation = Game.Delta;
                Game.Delta.X = 0;
                Game.Delta.Y = 0;
            }
            else if (!Game.Delta.IsEmpty)
            {
                Orientation = Game.Delta;
                Game.Delta = Point.Empty;
                command.DeltaX = 0;
                command.DeltaY = 0;
                command.TransformTo = this;
            }

            if (!Game.IsShoot || !IsAbleToStep(Orientation.X + x, Orientation.Y + y) || Game.Map[Orientation.X + x, Orientation.Y + y] is Upgrade) return command;
            command.CreateTo = Game.Shells[this.GetType()](Orientation);
            Game.IsShoot = false;
            return command;
        }

        public override bool DeadInConflict(IGameObject conflictedObject)
        {
            return conflictedObject is DoubleShell || conflictedObject is Shell;
        }
    }
}