using System;
using System.Windows.Forms;
using System.Drawing;

namespace Tanks.Architecture.Tanks
{
    class Player :ITank
    {
        public Point Orientation = new Point(1, 0);
        public string GetImageFileName()
        {
            return "Player.jpg";
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public TankCommand Act(int x, int y)
        {
            var command = new TankCommand();
            if (Game.KeyPressed == Keys.Right && IsAbleToStep(x + 1, y))
            {
                command.DeltaX = 1;
            }
            else if (Game.KeyPressed == Keys.Left && IsAbleToStep(x - 1, y))
            {
                command.DeltaX = -1;
            }
            else if (Game.KeyPressed == Keys.Up && IsAbleToStep(x, y - 1))
            {
                command.DeltaY = -1;
            }
            else if (Game.KeyPressed == Keys.Down && IsAbleToStep(x, y + 1))
            {
                command.DeltaY = 1;
            }
            else if (Game.KeyPressed == Keys.Space && IsAbleToStep(Orientation.X, Orientation.Y))
            {
            }

            return command;
        }

        public bool DeadInConflict(ITank conflictedObject)
        {
            throw new NotImplementedException();
        }

        private static bool IsAbleToStep(int x, int y)
        {
            return x > -1 && y > -1 && x < Game.MapWidth && y < Game.MapHeight && !(Game.Map[x, y] is Wall);
        }
    }
}
