using System.Drawing;

namespace Tanks.Architecture.GameObjects
{
    public class Upgrade : IGameObject
    {
        public Point Orientation { get; set; }

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

        public bool DeadInConflict(IGameObject conflictedObject)
        {
            if (conflictedObject is Player)
            {
                conflictedObject = new PlayerUpgraded(conflictedObject.Orientation);
            }
            if (conflictedObject is Enemy)
            {
                conflictedObject = new EnemyUpgraded(conflictedObject.Orientation);
            }
            return conflictedObject != null;
        }
    }
}
