using System;

namespace Tanks.Architecture.Tanks
{
    class Shell : ITank
    {
        public string GetImageFileName()
        {
            return "Shell.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public TankCommand Act(int x, int y)
        {
            throw new NotImplementedException();
        }

        public bool DeadInConflict(ITank conflictedObject)
        {
            throw new NotImplementedException();
        }
    }
}
