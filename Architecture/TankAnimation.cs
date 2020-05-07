using System.Drawing;

namespace Tanks.Architecture
{
    class TankAnimation
    {
        public TankCommand Command;
        public IGameObject GameObject;
        public Point Location;
        public Point TargetLogicalLocation;
    }
}
