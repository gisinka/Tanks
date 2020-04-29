namespace Tanks.Architecture
{
    public interface ITank
    {
        string GetImageFileName();
        int GetDrawingPriority();
        TankCommand Act(int x, int y);
        bool DeadInConflict(ITank conflictedObject);
    }
}
