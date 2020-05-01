﻿using System.Drawing;
using Tanks.Architecture.GameObjects;

namespace Tanks.Architecture
{
    public abstract class Tank : IGameObject
    {
        public Point Orientation { get; set; }
        public abstract string GetImageFileName();

        public abstract int GetDrawingPriority();
        public abstract TankCommand Act(int x, int y);

        public abstract bool DeadInConflict(IGameObject conflictedObject);

        protected static bool IsAbleToStep(int x, int y)
        {
            return x > -1 && y > -1 && x < Game.MapWidth && y < Game.MapHeight &&
                   !(Game.Map[x, y] is Wall && !(Game.Map[x, y] is Tank));
        }
    }
}