using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadBalls
{
    internal class Ball
    {
        // Properties to store ball's position, velocity, acceleration, radius, and movement status
        public int x, y;
        public int velocityX, velocityY;
        public int accelX, accelY;
        public int rad;
        public bool isMoving;
        public int WIDTH, HEIGHT;

        // Constructor to initialize ball properties
        public Ball(int x, int y, int speedX, int speedY, int accelX, int accelY, int radius)
        {
            this.x = x;
            this.y = y;
            this.velocityX = speedX;
            this.velocityY = speedY;
            this.rad = radius;
            this.accelX = accelX;
            this.accelY = accelY;
        }

        // Method to set ball's movement status and environment dimensions
        public void setEnv(bool isMoving, int width, int height)
        {
            this.isMoving = isMoving;
            this.WIDTH = width;
            this.HEIGHT = height;
        }

        // Method to simulate ball movement
        public void move(int n, Form form)
        {
            // Continuously update ball's position while it is moving
            while (isMoving)
            {
                // Update velocity based on acceleration
                velocityX += accelX;
                velocityY += accelY;

                // Update ball's position based on velocity
                x += velocityX;
                y += velocityY;

                // Check if the ball hits the boundaries, reverse its velocity if so
                if (x + rad > WIDTH || x - rad < 0)
                {
                    velocityX = -velocityX;
                }
                if (y + rad > HEIGHT || y - rad < 0)
                {
                    velocityY = -velocityY;
                }

                // Request form repainting to reflect updated ball position
                form.Invalidate();

                // Introduce a small delay for smoother animation (10 milliseconds)
                Thread.Sleep(10);
            }
        }
    }
}