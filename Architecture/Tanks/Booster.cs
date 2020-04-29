using System;

namespace Tanks.Architecture.Tanks
{
    class Booster : ITank
    {
        public string GetImageFileName()
        {
            return "Booster.png";
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public TankCommand Act(int x, int y)
        {
            return new TankCommand();
        }

        public bool DeadInConflict(ITank conflictedObject)
        {
            throw new NotImplementedException();
        }
    }
}
