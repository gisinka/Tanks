using System.Drawing;

namespace Tanks.Architecture.GameObjects
{
    internal class Wall : IGameObject
    {
        public Point Orientation { get; set; }

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

        public bool DeadInConflict(IGameObject conflictedObject)
        {
            return conflictedObject is Shell;
        }
    }
}