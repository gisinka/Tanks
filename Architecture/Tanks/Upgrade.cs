using System;

namespace Tanks.Architecture.Tanks
{
    class Upgrade :ITank
    {
        public string GetImageFileName()
        {
            return "Upgrade.png";
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
