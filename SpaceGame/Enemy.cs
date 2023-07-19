using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public enum TypeEnemy
    {
        Normal, Boss
    }
    internal class Enemy
    {
        //Saber si esta vivo
        public bool IsAlive { get; set; }

        //Vida
        public float Health { get; set; }

        //Positcion
        public Point Position { get; set; }

        //Tamaño de ventana
        public Window WindowC { get; set; }

        //Almacenar color de varios enemigos mediante enum
        public ConsoleColor Color { get; set; }

        //Almacenar que tipo de enmigo se ha creado
        public TypeEnemy TypeEnemyE { get; set; }

        //Lista para guardar las posiciones y estar agregando / borrando
        public List<Point> PositionsEnemy { get; set; }

        public Enemy (Point p, ConsoleColor c, Window w, TypeEnemy t)
        {
            this.Position = p;
            this.Color = c;
            this.TypeEnemyE = t;
            this.WindowC = w;
            IsAlive = true;
            Health = 100;
            PositionsEnemy = new List<Point>();
        }

        public void Draw()
        {
            //Primero determinamos el tipo de enemigo
            switch(TypeEnemyE)
            {
                case TypeEnemy.Normal:
                    DrawNormal();
                    break;

                case TypeEnemy.Boss:
                    DrawBoss();
                    break;
            }
        }

        //Dibujar enemigos normales
        public void DrawNormal()
        {
            Console.ForegroundColor = Color;
            int x = Position.X;
            int y = Position.Y;

            Console.SetCursorPosition(x + 1, y);
            Console.Write("▄▄");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("████");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("▀  ▀");

            PositionsEnemy.Clear(); //Eliminamos posiciones anteriores

            PositionsEnemy.Add(new Point(x + 1, y));
            PositionsEnemy.Add(new Point(x + 2, y));
            PositionsEnemy.Add(new Point(x, y + 1));
            PositionsEnemy.Add(new Point(x + 1, y + 1));
            PositionsEnemy.Add(new Point(x + 2, y + 1));
            PositionsEnemy.Add(new Point(x + 3, y + 1));
            PositionsEnemy.Add(new Point(x, y + 2));
            PositionsEnemy.Add(new Point(x + 3, y + 2));
        }

        //Dibujar boss
        public void DrawBoss()
        {
            Console.ForegroundColor = Color;
            int x = Position.X;
            int y = Position.Y;

            Console.SetCursorPosition(x + 1, y);
            Console.Write("█▄▄▄▄█");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("██ ██ ██");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("█▀▀▀▀▀▀█");

            PositionsEnemy.Clear();

            PositionsEnemy.Add(new Point(x + 1, y));
            PositionsEnemy.Add(new Point(x + 2, y));
            PositionsEnemy.Add(new Point(x + 3, y));
            PositionsEnemy.Add(new Point(x + 4, y));
            PositionsEnemy.Add(new Point(x + 5, y));
            PositionsEnemy.Add(new Point(x + 6, y));

            PositionsEnemy.Add(new Point(x, y + 1));
            PositionsEnemy.Add(new Point(x + 1, y + 1));
            PositionsEnemy.Add(new Point(x + 3, y + 1));
            PositionsEnemy.Add(new Point(x + 4, y + 1));
            PositionsEnemy.Add(new Point(x + 6, y + 1));
            PositionsEnemy.Add(new Point(x + 7, y + 1));

            PositionsEnemy.Add(new Point(x, y + 2));
            PositionsEnemy.Add(new Point(x + 1, y + 2));
            PositionsEnemy.Add(new Point(x + 2, y + 2));
            PositionsEnemy.Add(new Point(x + 3, y + 2));
            PositionsEnemy.Add(new Point(x + 4, y + 2));
            PositionsEnemy.Add(new Point(x + 5, y + 2));
            PositionsEnemy.Add(new Point(x + 6, y + 2));
            PositionsEnemy.Add(new Point(x + 7, y + 2));

        }

        //Borrar enemigo
        public void Delete()
        {
            //Recorremos la lista donde estan las posiciones
            foreach(Point p in PositionsEnemy)
            {
                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(" ");
            }
        }

    }
}
