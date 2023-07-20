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

        //Lista para guardar las balas y poder darle movimiento
        public List<Bullet> Bullets { get; set; }

        public float OverLoad { get; set; }

        public bool OverLoadLimit { get; set; }

        public float SuperShot { get; set; }

        //Listas para guardar las posiciones de los enemigos (para hacerles daño)
        public List<Enemy> Enemies { get; set; }

        public Ship(Point p, ConsoleColor c, Window w)
        {
            this.Position = p;
            this.Color = c;
            this.WindowC = w;
            Health = 100;
            PositionsShip = new List<Point>();
            Bullets = new List<Bullet>();
            Enemies = new List<Enemy>();
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

            //Creacion y posicionamiento de la bala
            if(key.Key == ConsoleKey.RightArrow)
            {
                if (!OverLoadLimit)
                {
                    Bullet bullet = new Bullet(
                        new Point(Position.X + 6, Position.Y + 2),
                        ConsoleColor.White,
                        BulletType.Normal);

                    Bullets.Add(bullet);

                    OverLoad += 0.8f; //Cada que se dispara, aumenta la sobrecarga

                    if (OverLoad >= 100)
                    {
                        OverLoadLimit = true;
                        OverLoad = 100;
                    }
                }

            }

            if (key.Key == ConsoleKey.LeftArrow)
            {
                if (!OverLoadLimit)
                {
                    Bullet bullet = new Bullet(
                        new Point(Position.X, Position.Y + 2),
                        ConsoleColor.White,
                        BulletType.Normal);

                    Bullets.Add(bullet);

                    OverLoad += 0.8f; //Cada que se dispara, aumenta la sobrecarga

                    if (OverLoad >= 100)
                    {
                        OverLoadLimit = true;
                        OverLoad = 100;
                    }
                }

            }
            if (key.Key == ConsoleKey.UpArrow)
            {
                if(SuperShot >= 100) //Solo la podremos disparar cuando tenga el 100
                {
                    Bullet bullet = new Bullet(
                        new Point(Position.X + 2, Position.Y - 2),
                        ConsoleColor.White,
                        BulletType.Special);

                    Bullets.Add(bullet);

                    //Una vez disparada, la colocamos en 0
                    SuperShot = 0;
                }
            }

        }

        //Mostrar informacion de la nave
        public void Information()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(WindowC.UpperLimit.X, WindowC.UpperLimit.Y - 1);
            Console.Write("VIDA: " + (int)Health + " %  ");

            //Disminuir sobrecarga progresivamente
            if (OverLoad <= 0) //No tener numero negativo en el super disparo
                OverLoad = 0;
            else //En cada iteracion va bajando
                OverLoad -= 0.009f;

            if (OverLoad <= 50) //Para poder disparar una vez baje a 50% la sobrecarga
                OverLoadLimit = false;

            if (OverLoadLimit)
                Console.ForegroundColor = ConsoleColor.Red;
            else
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(WindowC.UpperLimit.X + 13, WindowC.UpperLimit.Y - 1);
            Console.Write("SOBRECARGA: " + (int)OverLoad + " %  ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(WindowC.UpperLimit.X + 32, WindowC.UpperLimit.Y - 1);
            Console.Write("SUPER DISPARO: " + (int)SuperShot + " %  ");
            if (SuperShot >= 100)
                SuperShot = 100;
            else
                SuperShot += 0.009f; //Aqui va aumentando constantemente

        }

        //Funcion para disparar las balas
        public void Shoot()
        {
            for(int i = 0; i < Bullets.Count; i++)
            {
                //Recordar que este metodo retorna true cuando colisiona con un limite
                if(Bullets[i].Move(1,WindowC.UpperLimit.Y,Enemies))
                {
                    Bullets.Remove(Bullets[i]); //Eliminamos esa bala que colisiona
                }
            }
        }

        //Funcion para mover la nave
        //La velocidad se refiere a la cantidad de caracteres que se movera la nave
        public void Move(int speed)
        {
            if(Console.KeyAvailable) //Si se presiono una tecla...
            {
                Delete(); //Borramos los caracteres de la nave

                Point distance = new Point(); //Distancia que se movera
                Keyboard(ref distance,speed); //Tecla presionada y punto a donde se movera
                Collisions(distance); //En caso de colisiones estro lo arregla

                Draw(); //Las volvemos a dibujar para que de el efecto de movimiento
            }

            Information();
        }

        //Funcion para que la nave no rompa los marcos
        public void Collisions(Point distance)
        {
            //Almacenamos la posicion que se supone que debe tener la nave en un auxiliar
            Point positionAux = new Point(Position.X + distance.X, Position.Y + distance.Y);

            //Validaciones para que no supere los marcos, la nave
            if (positionAux.X <= WindowC.UpperLimit.X)
                positionAux.X = WindowC.UpperLimit.X + 1;
            if (positionAux.X + 6 >= WindowC.LowerLimit.X)
                positionAux.X = WindowC.LowerLimit.X -7;
            if(positionAux.Y <= (WindowC.UpperLimit.Y) + 15) //Le aumentamos 15 para que no pase de la mitad del mapa
                positionAux.Y = (WindowC.UpperLimit.Y + 1) + 15;
            if (positionAux.Y + 2 >= WindowC.LowerLimit.Y)
                positionAux.Y = WindowC.LowerLimit.Y - 3;

            //Igualamos 
            Position = positionAux; 
        }

        //Animacion de muerte
        public void Dead()
        {
            Console.ForegroundColor = Color;

            //Primero recorremos todos los caracteres de la nave 
            foreach(Point p in PositionsShip)
            {
                Console.SetCursorPosition(p.X, p.Y);
                Console.Write("X");
                Thread.Sleep(200);
            }

            foreach (Point p in PositionsShip)
            {
                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(" ");
                Thread.Sleep(200);
            }
        }
    }
}
