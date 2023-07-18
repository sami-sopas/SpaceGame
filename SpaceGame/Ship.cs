using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    internal class Ship
    {
        public float Health { get; set; }

        public Point Position { get; set; }

        public ConsoleColor Color { get; set; }

        public Window WindowC { get; set; }

        public Ship(Point p, ConsoleColor c, Window w)
        {
            this.Position = p;
            this.Color = c;
            this.WindowC = w;
            Health = 100;
        }

        //Dibujar la nave
        public void Draw()
        {
            Console.ForegroundColor = Color;
            int x = Position.X;
            int y = Position.Y;

            Console.SetCursorPosition(x + 3, y);
            Console.Write("A");
            Console.SetCursorPosition(x + 1, y + 1);
            Console.Write("<{x}>");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("± W W ±");

        }
    }
}
