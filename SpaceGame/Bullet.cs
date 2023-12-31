﻿using System;
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
        Normal, Special, Enemy, Menu
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

                //Balas del menu
                case BulletType.Menu:
                    Console.SetCursorPosition(x, y);
                    Console.Write("!");

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

        //Retorna verdadero si la bala colisiona con los limites de la nave
        public bool Move(int speed, int limit,List<Enemy> enemies)
        {
            //Si ya pasaron 30 milisegundos despues del ultimo movimiento, podremos volverla a mover
            if(DateTime.Now > time.AddMilliseconds(35))
            {
                Delete(); //Borrar posiciones anteriores

                //Determinamos que tipo de bala se creo
                switch (BulletTypeB)
                {

                    case BulletType.Normal:
                        Position = new Point(Position.X, Position.Y - speed);
                        if (Position.Y <= limit)
                            return true;

                        foreach(Enemy e in enemies) //Recorremos los enemigos
                        {
                            foreach(Point p in e.PositionsEnemy) //Recorremos sus posiciones
                            { 
                                if(p.X == Position.X && p.Y == Position.Y) //Bala de la nave, colisiona con el enemigo
                                {
                                    e.Health -= 7f; //Aqui se le baja la vida al enemigo
                                    Ship.SuperShot += 5f; //Cada que le demos a un enemigo, aumenta el superdisparo

                                    if (e.Health <= 0)
                                    {
                                        e.Health = 0;
                                        e.IsAlive = false;
                                        e.Death();
                                    }
                                    return true; //Retornamos verdadero porque hubo colision
                                }
                            }
                        }
                        break;


                    case BulletType.Special:
                        Position = new Point(Position.X, Position.Y - speed);
                        if (Position.Y <= limit)
                            return true;

                        foreach (Enemy e in enemies) //Recorremos los enemigos
                        {
                            foreach (Point p in e.PositionsEnemy) //Recorremos sus posiciones
                            {
                                foreach(Point pB in PositionsBullet) //Comparar posiciones de la bala especial con cada una de los enemigos
                                {
                                    if(p.X == pB.X && p.Y == pB.Y) //Colisiona bala especial con enemigo
                                    {
                                        e.Health -= 20f;
                                        Ship.SuperShot += 10f;
                                        if (e.Health <= 0)
                                        {
                                            e.Health = 0;
                                            e.IsAlive =false;
                                            e.Death();
                                        }
                                        return true;
                                    }
                                }
                            }
                        }
                        break;
                }

                Draw(); //Volvemos a dibujar las balas

                time = DateTime.Now; //Capturamos fecha y hora en que lo hizo
            }

            return false;
        }

        //Metodo exclusivo para balas enemigas
        public bool Move(int speed, int limit,Ship ship)
        {
            //Si ya pasaron 30 milisegundos despues del ultimo movimiento, podremos volverla a mover
            if (DateTime.Now > time.AddMilliseconds(20))
            {
                Delete(); //Borrar posiciones anteriores

                Position = new Point(Position.X, Position.Y + speed);
                if (Position.Y >= limit)
                    return true;

                //Recorremos la lista donde estan almacenadas todas las posiciones de los caracteres que forman a la nave
                foreach(Point p in ship.PositionsShip)
                {
                    if(p.X == Position.X && p.Y == Position.Y) //si chocan las balas de los enemigos en la nave
                    {
                        //Le restamos vida a la nave
                        ship.Health -= 4;
                        ship.ColorAux = Color; //Asignamos al colorAux, el color de la nave que lo colisiono
                        ship.TimeCollision = DateTime.Now; //Guardamos el momento en el que se hizo
                        return true; //Retornamos verdadero porque hubo colision
                    }
                }

                Draw(); //Volvemos a dibujar las balas

                time = DateTime.Now; //Capturamos fecha y hora en que lo hizo
            }

            return false;
        }

        //Metodo para las balas que se muestran en el MENU
        public bool Move(int speed, int limit)
        {
            //Si ya pasaron 30 milisegundos despues del ultimo movimiento, podremos volverla a mover
            if (DateTime.Now > time.AddMilliseconds(20))
            {
                Delete(); //Borrar posiciones anteriores
                                                 //Le restamos porque la bala ira de abajo a arriba
                Position = new Point(Position.X, Position.Y - speed);
                if (Position.Y <= limit)
                    return true;

                Draw(); //Volvemos a dibujar las balas

                time = DateTime.Now; //Capturamos fecha y hora en que lo hizo
            }

            return false;
        }


    }
}
