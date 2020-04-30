using System.Drawing;

namespace Tanks.Architecture.GameObjects
{
    class Upgrade : IGameObject
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
                conflictedObject = new PlayerUpgraded() {Orientation = conflictedObject.Orientation};
            }
            if (conflictedObject is Enemy)
            {
                conflictedObject = new EnemyUpgraded() { Orientation = conflictedObject.Orientation };
            }
            return conflictedObject != null;
        }
    }
}
