using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Tanks.Architecture;

namespace Tanks
{
    public partial class TanksWindow : Form
    {
        private static GameState _gameState;
        private readonly Dictionary<string, Bitmap> _bitmaps = new Dictionary<string, Bitmap>();
        private readonly ToolStripLabel _scoresLabel = new ToolStripLabel();
        private int _tickCount;

        public TanksWindow(DirectoryInfo imagesDirectory = null)
        {
            InitializeComponent();
            statusStrip.Items.Add(_scoresLabel);
            InitializeMenu();
            _gameState = new GameState();
            ClientSize = new Size(
                GameState.ElementSize * Game.MapWidth + 4 * GameState.ElementSize,
                GameState.ElementSize * Game.MapHeight + statusStrip.Size.Height + menuStrip.Size.Height);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            if (imagesDirectory == null)
                imagesDirectory = new DirectoryInfo("Images");
            foreach (var e in imagesDirectory.GetFiles("*.png"))
                _bitmaps[e.Name] = (Bitmap) Image.FromFile(e.FullName);
            var timer = new Timer {Interval = 15};
            timer.Tick += TimerTick;
            timer.Start();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "Tanks";
            DoubleBuffered = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    Game.Delta = new Point(-1, 0);
                    break;
                case Keys.Right:
                    Game.Delta = new Point(1, 0);
                    break;
                case Keys.Up:
                    Game.Delta = new Point(0, -1);
                    break;
                case Keys.Down:
                    Game.Delta = new Point(0, 1);
                    break;
                case Keys.Space:
                    Game.IsShoot = true;
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(2 * GameState.ElementSize, statusStrip.Size.Height + menuStrip.Size.Height);
            e.Graphics.FillRectangle(
                Brushes.Black, 0, 0, GameState.ElementSize * Game.MapWidth,
                GameState.ElementSize * Game.MapHeight);
            foreach (var a in _gameState.Animations)
            {
                var bitmapGameObject = (Bitmap) _bitmaps[a.GameObject.GetImageFileName()].Clone();
                bitmapGameObject.RotateFlip(RotateBitmap(a.GameObject.Orientation));
                e.Graphics.DrawImage(bitmapGameObject, a.Location);
            }

            e.Graphics.ResetTransform();
        }

        private void TimerTick(object sender, EventArgs args)
        {
            if (_tickCount == 0) _gameState.BeginAct();
            foreach (var e in _gameState.Animations)
                e.Location = new Point(e.Location.X + 4 * e.Command.DeltaX, e.Location.Y + 4 * e.Command.DeltaY);
            if (_tickCount == 7)
                _gameState.EndAct();
            _tickCount++;
            if (_tickCount == 8) _tickCount = 0;
            _scoresLabel.Text = $"Scores: {Game.Scores}";
            Invalidate();
        }

        private static RotateFlipType RotateBitmap(Point orientation)
        {
            if (orientation == new Point(-1, 0))
                return RotateFlipType.Rotate270FlipNone;
            if (orientation == new Point(0, -1))
                return RotateFlipType.RotateNoneFlipNone;
            return orientation == new Point(1, 0) ? RotateFlipType.Rotate90FlipNone : RotateFlipType.Rotate180FlipNone;
        }

        private void InitializeMenu()
        {
            toolStripMenuItem.DropDownItems.Add("Карта 1", null, Map1Click);
            toolStripMenuItem.DropDownItems.Add("Карта 2", null, Map2Click);
            toolStripMenuItem.DropDownItems.Add("Карта 3", null, Map3Click);
        }

        private void Map1Click(object sender, EventArgs e)
        {
            Game.CreateMap(Maps.FirstMap);
            UpdateFormWithNewMap();
        }

        private void Map2Click(object sender, EventArgs e)
        {
            Game.CreateMap(Maps.SecondMap);
            UpdateFormWithNewMap();
        }

        private void Map3Click(object sender, EventArgs e)
        {
            Game.CreateMap(Maps.ThirdMap);
            UpdateFormWithNewMap();
        }

        private void UpdateFormWithNewMap()
        {
            _gameState = new GameState();
            _gameState.BeginAct();
            ClientSize = new Size(
                GameState.ElementSize * Game.MapWidth + 4 * GameState.ElementSize,
                GameState.ElementSize * Game.MapHeight + statusStrip.Size.Height + menuStrip.Size.Height);
            Refresh();
        }
    }
}