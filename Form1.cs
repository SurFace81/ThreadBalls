using System;
using System.Threading;
using System.Windows.Forms;

namespace ThreadBalls
{
    public partial class Form1 : Form
    {
        // Variables declaration
        private const int ballsCntr = 10;
        private Ball[] balls = new Ball[ballsCntr];
        private Thread[] threads = new Thread[ballsCntr];

        public Form1()
        {
            InitializeComponent();

            // Enable double buffering for smooth rendering
            this.DoubleBuffered = true;

            // Randomly initialize balls with positions, velocities, and radius
            Random rnd = new Random();
            for (int i = 0; i < balls.Length; i++)
            {
                balls[i] = new Ball(rnd.Next(30, ClientSize.Width - 30), rnd.Next(30, ClientSize.Height - 30),
                                    rnd.Next(1, 5), rnd.Next(1, 5),
                                    0, 0, 15);

                // Set environment for each ball and create corresponding threads
                balls[i].setEnv(true, ClientSize.Width, ClientSize.Height);
                int index = i;
                threads[i] = new Thread(() => balls[index].move(index, this));
            }

            // Start threads to move balls
            foreach (Thread t in threads)
            {
                t.Start();
            }
        }

        // Override OnPaint method to draw balls on the form
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Brush for drawing balls
            Brush brush = new SolidBrush(Color.FromArgb(125, 125, 125));

            // Draw each ball on the form
            for (int i = 0; i < balls.Length; i++)
            {
                e.Graphics.FillEllipse(brush, balls[i].x - balls[i].rad,
                                              balls[i].y - balls[i].rad,
                                              2 * balls[i].rad,
                                              2 * balls[i].rad);
            }
        }

        // Override OnFormClosing method to stop ball movement threads upon form closing
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Stop ball movement by setting isMoving to false for each ball
            foreach (Ball b in balls)
            {
                b.isMoving = false;
            }

            // Wait for all threads to finish before closing the form
            foreach (Thread t in threads)
            {
                t.Join();
            }
        }
    }
}