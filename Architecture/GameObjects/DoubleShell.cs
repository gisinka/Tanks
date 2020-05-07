using System.Drawing;

namespace Tanks.Architecture.GameObjects
{
    public class DoubleShell : Shell
    {
        public DoubleShell()
        {
            Orientation = new Point(1, 0);
        }

        public DoubleShell(Point orientation)
        {
            Orientation = orientation;
        }

        public override string GetImageFileName()
        {
            return "DoubleShell.png";
        }
    }
}