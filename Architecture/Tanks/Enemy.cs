using System;
using System.Drawing;

namespace Tanks.Architecture.Tanks
{
    public class Enemy :ITank
    {
        public Point Orientation = new Point(1, 0);
        public string GetImageFileName()
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

        public bool DeadInConflict(ITank conflictedObject)
        {
            throw new NotImplementedException();
        }

        private static bool IsAbleToStep(int x, int y)
        {
            return !(Game.Map[x, y] is Enemy || Game.Map[x, y] is Player || Game.Map[x, y] is Wall);
        }
    }
}
