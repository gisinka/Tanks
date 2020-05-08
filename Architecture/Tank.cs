using System.Drawing;
using Tanks.Architecture.GameObjects;

namespace Tanks.Architecture
{
    public abstract class Tank : IGameObject
    {
        protected Tank()
        {
            Orientation = new Point(0, -1);
        }

        public Point Orientation { get; set; }
        public abstract string GetImageFileName();
        public abstract int GetDrawingPriority();
        public abstract TankCommand Act(int x, int y);
        public abstract bool DeadInConflict(IGameObject conflictedObject);

        protected virtual bool IsAbleToStep(int x, int y)
        {
            return Game.IsInMap(x, y) && (Game.Map[x, y] == null || Game.Map[x, y] is Upgrade);
        }
    }
}