namespace Tanks.Architecture.Tanks
{
    internal class Wall : ITank
    {
        public string GetImageFileName()
        {
            return "Wall.png";
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public TankCommand Act(int x, int y)
        {
            return new TankCommand();
        }

        public bool DeadInConflict(ITank conflictedObject)
        {
            return conflictedObject is Shell;
        }
    }
}