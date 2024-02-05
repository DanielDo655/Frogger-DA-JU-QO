using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frogger
{
    public class DriveBy : Hindernis
    {

        private int timer;

        public DriveBy(int X, int Y, int Width, int Height, int Speed, Color Color) : base(X, Y, Width, Height, Speed, Color)
        {
            base.X = X;
            base.Y = Y;
            base.Width = Width;
            base.Height = Height;
            base.Speed = Speed;
            base.Color = Color;

        }
    }
}
