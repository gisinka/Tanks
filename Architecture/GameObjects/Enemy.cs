using System;
using System.Drawing;

namespace Tanks.Architecture.GameObjects
{
    public class Enemy : ITank
    {
        public Point Orientation { get; set; }

        public virtual string GetImageFileName()
        {
            return "Enemy.jpg";
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public TankCommand Act(int x, int y)
        {
           throw new NotImplementedException();
        }

        public bool DeadInConflict(IGameObject conflictedObject)
        {
            return conflictedObject is DoubleShell || conflictedObject is Shell;
        }

    }
}
