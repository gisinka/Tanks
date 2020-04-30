namespace Tanks.Architecture.GameObjects
{
    class PlayerUpgraded : Player
    {
        public override string GetImageFileName()
        {
            return "PlayerUpgraded.png";
        }

        public override TankCommand Act(int x, int y)
        {
            var command = new TankCommand();
            if (Game.Delta == Orientation)
                command = new TankCommand { DeltaX = Game.Delta.X, DeltaY = Game.Delta.Y };
            if (!Game.IsShoot || !IsAbleToStep(Orientation.X + x, Orientation.Y + y)) return command;
            Game.Map[Orientation.X + x, Orientation.Y + y] = new DoubleShell(Orientation);
            Game.IsShoot = false;

            return command;
        }
    }
}
