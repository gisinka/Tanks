using System.Drawing;

namespace Tanks.Architecture
{
    public interface IGameObject
    {
        Point Orientation { get; set; }
        string GetImageFileName();
        int GetDrawingPriority();
        TankCommand Act(int x, int y);
        bool DeadInConflict(IGameObject conflictedObject);
    }
}
