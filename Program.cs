using System;
using System.Windows.Forms;
using Tanks.Architecture;

namespace Tanks
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Game.CreateMap(Maps.FirstMap);
            Application.Run(new TanksWindow());
        }
    }
}