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

        //Lista donde guardaremos las posiciones de la nave para poder eliminarlas cuando se mueva
        public List<Point> PositionsShip { get; set; } 

        public Ship(Point p, ConsoleColor c, Window w)
        {
            this.Position = p;
            this.Color = c;
            this.WindowC = w;
            Health = 100;
            PositionsShip = new List<Point>();
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

            PositionsShip.Clear(); //Eliminar todos los elementos de la lista para que solo guarde los actuales

            //Posiciones de los caracteres de la nave
            PositionsShip.Add(new Point(x + 3, y)); //Coordenada de la A

            PositionsShip.Add(new Point(x + 1, y + 1)); //Coordenada <
            PositionsShip.Add(new Point(x + 2, y + 1)); //Coordenada {
            PositionsShip.Add(new Point(x + 3, y + 1)); //Coordenada x
            PositionsShip.Add(new Point(x + 4, y + 1)); //Coordenada }
            PositionsShip.Add(new Point(x + 5, y + 1)); //Coordenada >

            PositionsShip.Add(new Point(x, y + 2)); //Coordenada ±
            PositionsShip.Add(new Point(x + 2, y + 2)); //Coordenada W
            PositionsShip.Add(new Point(x + 4, y + 2)); //Coordenada W
            PositionsShip.Add(new Point(x + 6, y + 2)); //Coordenada ±

        }

        //Funcion para borrar los caracteres de la consola mientras se mueve
        public void Delete()
        {
            foreach(Point p in PositionsShip)
            {
                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(" ");
            }
        }

        public void Keyboard(ref Point distance, int speed)
        {
            ConsoleKeyInfo key = Console.ReadKey(true); //Capturamos la tecla presionada
            
            if (key.Key == ConsoleKey.W) //Si se presiona la W (arriba), modificamos la distancia
                distance = new Point(0, -1);
            if (key.Key == ConsoleKey.S)
                distance = new Point(0, 1);
            if (key.Key == ConsoleKey.D)
                distance = new Point(1, 0);
            if (key.Key == ConsoleKey.A)
                distance = new Point(-1, 0);

            distance.X *= speed;
            distance.Y *= speed;

            //Ya tenemos la distancia, ahora modificamos la posicion para que pueda moverse
            Position = new Point(Position.X+distance.X,Position.Y+distance.Y);
        }

        //Funcion para mover la nave
        //La velocidad se refiere a la cantidad de caracteres que se movera la nave
        public void Move(int speed)
        {
            if(Console.KeyAvailable) //Si se presiono una tecla...
            {
                Delete(); //Borramos los caracteres de la nave

                Point distance = new Point(); //Distancia que se movera
                Keyboard(ref distance,speed);

                Draw(); //Las volvemos a dibujar para que de el efecto de movimiento
            }
        }
    }
}
