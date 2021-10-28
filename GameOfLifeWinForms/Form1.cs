using System;
using System.Drawing;
using System.Windows.Forms;
using CellularAutomatonEngine;

namespace GameOfLifeWinForms
{
    public partial class Form : System.Windows.Forms.Form
    {
        private Graphics graphics;
        private GameOfLifeEngine engine;
        private int resolution = 8;
        private int sparseness = 6;
        private bool worked = false;


        public Form()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            StartGame();    
        }

        private void StartGame()
        {
            engine = new GameOfLifeEngine
                (
                    rows: PictureBox.Height / resolution,
                    cols: PictureBox.Width / resolution
                );

            PictureBox.Image = new Bitmap(PictureBox.Width, PictureBox.Height);
            graphics = Graphics.FromImage(PictureBox.Image);
            graphics.Clear(Color.Black);

            Text = $"Generation {engine.Generation}";
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                if (!worked)
                {
                    Timer.Start();
                    worked = true;
                }
                else
                {
                    Timer.Stop();
                    worked = false;
                }

                if (Description.Visible)
                {
                    Description.Visible = false;
                }
            }

            if (e.KeyCode == Keys.R)
            {
                engine.RandomFilling(sparseness);
                if (!Timer.Enabled)
                {
                    DrawNextFrame();
                }
                PictureBox.Refresh();

                if (Description.Visible)
                {
                    Description.Visible = false;
                }
            }

            if (e.KeyCode == Keys.C)
            {
                engine.ClearField();
                if (!Timer.Enabled)
                {
                    DrawNextFrame();
                }
                PictureBox.Refresh();

                if (Description.Visible)
                {
                    Description.Visible = false;
                }
            }

            if (e.KeyCode == Keys.H)
            {
                if (Description.Visible)
                {
                    Description.Visible = false;
                }
                else
                {
                    Description.Visible = true;
                }
            }

            if (e.KeyCode == Keys.E)
            {
                Application.Exit();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DrawNextFrame();
        }

        private void DrawNextFrame()
        {
            graphics.Clear(Color.Black);

            var renderField = engine.GetCurrentGeneration();

            for (int x = 0; x < renderField.GetLength(0); x++)
            {
                for (int y = 0; y < renderField.GetLength(1); y++)
                {
                    if (renderField[x, y])
                    {
                        graphics.FillRectangle(Brushes.DarkOliveGreen, x * resolution, y * resolution, resolution - 1, resolution - 1);
                    }
                }
            }

            PictureBox.Refresh();
            Text = $"Generation {engine.Generation}";
            engine.CreateNextGeneration();
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            MouseEventHandling(e);
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            MouseEventHandling(e);
        }

        private void MouseEventHandling(MouseEventArgs e)
        {
            var x = e.Location.X / resolution;
            var y = e.Location.Y / resolution;

            if (e.Button == MouseButtons.Left)
            {
                engine.AddCell(x, y);
                if (!Timer.Enabled)
                {
                    graphics.FillRectangle(Brushes.DarkOliveGreen, x * resolution, y * resolution, resolution - 1, resolution - 1);
                    PictureBox.Refresh();
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                engine.RemoveCell(x, y);
                if (!Timer.Enabled)
                {
                    graphics.FillRectangle(Brushes.Black, x * resolution, y * resolution, resolution - 1, resolution - 1);
                    PictureBox.Refresh();
                }
            }
        }
    }
}
