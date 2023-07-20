using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    //Tipos de bala que tendremos
    public enum BulletType
    {
        Normal, Special, Enemy
    }
    internal class Bullet
    {
        public Point Position { get; set; }

        public ConsoleColor Color { get; set; }

        public BulletType BulletTypeB { get; set; }

        //Aqui guardaremos las posiciones de las balas
        public List<Point> PositionsBullet { get; set; }

        //Controlar el tiempo
        private DateTime time;

        public Bullet(Point p, ConsoleColor c,BulletType b)
        {
            this.Position = p;
            this.Color = c;
            this.BulletTypeB = b;
            PositionsBullet = new List<Point>();
            time = DateTime.Now; //Regresa fehca y hora actual
        }

        public void Draw()
        {
            //Posicion de la bala
            Console.ForegroundColor = Color;
            int x = Position.X;
            int y = Position.Y;

            //Limpiar la lista antes de agregar nuevos puntos
            PositionsBullet.Clear();

            //Determinar tipo de bala a dibujar
            switch(BulletTypeB)
            {
                case BulletType.Normal:
                    Console.SetCursorPosition(x, y);
                    Console.Write("o");
                    PositionsBullet.Add(new Point(x, y));
                    break;

                case BulletType.Special:
                    Console.SetCursorPosition(x + 1, y);
                    Console.Write("_");
                    Console.SetCursorPosition(x, y + 1);
                    Console.Write("( )");
                    Console.SetCursorPosition(x + 1, y + 2);
                    Console.Write("W");

                    PositionsBullet.Add(new Point(x + 1, y));
                    PositionsBullet.Add(new Point(x, y + 1));
                    PositionsBullet.Add(new Point(x + 2, y + 1));
                    PositionsBullet.Add(new Point(x + 1, y + 2));
                    break;

                //Bala de enemigo
                case BulletType.Enemy:
                    Console.SetCursorPosition(x, y);
                    Console.Write("█");

                    PositionsBullet.Add(new Point(x, y));
                    break;

            }
        }

        public void Delete()
        {
            foreach(Point p in PositionsBullet)
            {
                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(" ");
            }
        }

        //Retorna verdadero si la bala colisiona con los limites
        public bool Move(int speed, int limit)
        {
            //Si ya pasaron 30 milisegundos despues del ultimo movimiento, podremos volverla a mover
            if(DateTime.Now > time.AddMilliseconds(30))
            {
                Delete(); //Borrar posiciones anteriores

                //Determinamos que tipo de bala se creo
                switch (BulletTypeB)
                {

                    case BulletType.Normal:
                        Position = new Point(Position.X, Position.Y - speed);
                        if (Position.Y <= limit)
                            return true;
                        break;


                    case BulletType.Special:
                        Position = new Point(Position.X, Position.Y - speed);
                        if (Position.Y <= limit)
                            return true;
                        break;

                    case BulletType.Enemy: //Le aumentamos a Y su velocidad ya que iran de arriba a abajo
                        Position = new Point(Position.X, Position.Y + speed);
                        if(Position.Y >= limit)
                            return true;
                        break;
                }

                Draw(); //Volvemos a dibujar las balas

                time = DateTime.Now; //Capturamos fecha y hora en que lo hizo
            }

            return false;
        }


    }
}
