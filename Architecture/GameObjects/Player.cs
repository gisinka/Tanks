namespace Tanks.Architecture.GameObjects
{
    internal class Player : Tank
    {
        public override string GetImageFileName()
        {
            return "Player.jpg";
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
                command = new TankCommand {DeltaX = Game.Delta.X, DeltaY = Game.Delta.Y, TransformTo = this};
                Game.Delta.X = 0;
                Game.Delta.Y = 0;
            }

            if (!Game.IsShoot || !IsAbleToStep(Orientation.X + x, Orientation.Y + y)) return command;
            command.CreateTo = new Shell(Orientation);
            Game.IsShoot = false;
            return command;
        }

        public override bool DeadInConflict(IGameObject conflictedObject)
        {
            return conflictedObject is DoubleShell || conflictedObject is Shell;
        }
    }
}