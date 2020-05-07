using System.Drawing;

namespace Tanks.Architecture.GameObjects
{
    public class EnemyUpgraded : Enemy
    {
        public EnemyUpgraded(Point orientation)
        {
            Orientation = orientation;
        }

        public override string GetImageFileName()
        {
            return "EnemyUpgraded.png";
        }
    }
}