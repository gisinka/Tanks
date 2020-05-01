namespace Tanks.Architecture.GameObjects
{
    internal class PlayerUpgraded : Player
    {
        public override string GetImageFileName()
        {
            return "PlayerUpgraded.png";
        }

        public override TankCommand Act(int x, int y)
        {
            var command = new TankCommand();
            if (Game.Delta == Orientation)
            {
                command = new TankCommand { DeltaX = Game.Delta.X, DeltaY = Game.Delta.Y, TransformTo = this };
                Game.Delta.X = 0;
                Game.Delta.Y = 0;
            }

            if (!Game.IsShoot || !IsAbleToStep(Orientation.X + x, Orientation.Y + y)) return command;
            command.CreateTo = new DoubleShell(Orientation);
            Game.IsShoot = false;
            return command;
        }
    }
}