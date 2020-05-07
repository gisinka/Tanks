using System.Drawing;

namespace Tanks.Architecture.GameObjects
{
    public class PlayerUpgraded : Player
    {
        public PlayerUpgraded(Point orientation)
        {
            Orientation = orientation;
        }
        public override string GetImageFileName()
        {
            return "PlayerUpgraded.png";
        }
    }
}