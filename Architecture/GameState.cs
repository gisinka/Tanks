using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Tanks.Architecture.GameObjects;

namespace Tanks.Architecture
{
    internal class GameState
    {
        public const int ElementSize = 64;
        public List<TankAnimation> Animations = new List<TankAnimation>();

        public void BeginAct()
        {
            Animations.Clear();
            for (var x = 0; x < Game.MapWidth; x++)
            for (var y = 0; y < Game.MapHeight; y++)
            {
                var gameObject = Game.Map[x, y];
                if (gameObject == null) continue;
                var command = gameObject.Act(x, y);
                if (x + command.DeltaX < 0 || x + command.DeltaX >= Game.MapWidth || y + command.DeltaY < 0 ||
                    y + command.DeltaY >= Game.MapHeight)
                    Game.Map[x, y] = null;
                Animations.Add(
                    new TankAnimation
                    {
                        Command = command,
                        GameObject = gameObject,
                        Location = new Point(x * ElementSize, y * ElementSize),
                        TargetLogicalLocation = new Point(x + command.DeltaX, y + command.DeltaY)
                    });
            }

            Animations = Animations.OrderByDescending(z => z.GameObject.GetDrawingPriority()).ToList();
        }

        public void EndAct()
        {
            var gameObjectsPerLocation = GetCandidatesPerLocation();
            for (var x = 0; x < Game.MapWidth; x++)
            for (var y = 0; y < Game.MapHeight; y++)
                Game.Map[x, y] = SelectWinnerCandidatePerLocation(gameObjectsPerLocation, x, y);
        }

        private static IGameObject SelectWinnerCandidatePerLocation(List<IGameObject>[,] creatures, int x, int y)
        {
            var candidates = creatures[x, y];
            var aliveCandidates = candidates.ToList();
            foreach (var candidate in candidates)
            {
                if (candidate is DoubleShell && aliveCandidates.Count > 1)
                {
                    aliveCandidates.Clear();
                    aliveCandidates.Add(new Shell(candidate.Orientation));
                    break;
                }

                foreach (var rival in candidates)
                {
                    if (rival != candidate && candidate.DeadInConflict(rival))
                    {
                        aliveCandidates.Remove(candidate);
                        if (candidate is Enemy)
                            Game.Scores += 10;
                    }
                    if (rival != candidate && rival.DeadInConflict(candidate))
                        aliveCandidates.Remove(rival);
                }
            }

            if (aliveCandidates.Count > 1)
                throw new Exception(
                    $"GameObjects {aliveCandidates[0].GetType().Name} and {aliveCandidates[1].GetType().Name} claimed the same map cell");
            return aliveCandidates.FirstOrDefault();
        }

        private List<IGameObject>[,] GetCandidatesPerLocation()
        {
            var gameObjects = new List<IGameObject>[Game.MapWidth, Game.MapHeight];
            for (var x = 0; x < Game.MapWidth; x++)
            for (var y = 0; y < Game.MapHeight; y++)
                gameObjects[x, y] = new List<IGameObject>();
            foreach (var e in Animations)
            {
                var x = e.TargetLogicalLocation.X;
                var y = e.TargetLogicalLocation.Y;
                var nextCreature = e.Command.TransformTo ?? e.GameObject;
                if (Game.IsInMap(x, y))
                    gameObjects[x, y].Add(nextCreature);
                if (e.Command.CreateTo != null && Game.IsInMap(x + e.Command.CreateTo.Orientation.X,
                    y + e.Command.CreateTo.Orientation.Y))
                    gameObjects[x + e.Command.CreateTo.Orientation.X, y + e.Command.CreateTo.Orientation.Y]
                        .Add(e.Command.CreateTo);
            }

            return gameObjects;
        }
    }
}