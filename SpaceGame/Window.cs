using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    internal class Window
    {
        //Aqui guardaremos las dimensiones de la ventana
        public int Width { get; set; }

        public int Height { get; set; }

        //Para guardar el color de la consola
        public ConsoleColor Color { get; set; }

        //Coordenadas con limites superiores e inferiores (Drawing)
        public Point UpperLimit { get; set; } //Limite superior
        public Point LowerLimit { get; set; } //Limite inferior

        //Enemigos del Menu
        private Enemy _enemy1;
        private Enemy _enemy2;

        //Lista de las balas del menu
        private List<Bullet> _bullets;

        public Window(int w,int h,ConsoleColor c,Point upperL, Point loweL) {
            
            this.Width = w;
            this.Height = h;
            this.Color = c;
            this.UpperLimit = upperL;
            this.LowerLimit = loweL;

            Init();
        }

        private void Init()
        {
            //Establecer el tamaño de la consola
            Console.SetWindowSize(Width, Height);
            Console.Title = "Space Game";
            Console.BackgroundColor = Color;
            Console.CursorVisible = false;
            Console.Clear(); //Limpiar buffer de la consola
            _enemy1 = new Enemy(new Point(50, 10), ConsoleColor.Cyan, this, TypeEnemy.Menu, null);
            _enemy2 = new Enemy(new Point(100, 30), ConsoleColor.Red, this, TypeEnemy.Menu, null);
            _bullets = new List<Bullet>();

            MakeBullets(); //Aqui ya tenemos las 20 balas creadas en al lista
        }

        public void DrawFrame()
        {
            Console.ForegroundColor = ConsoleColor.White; //Color de la letra
            //Marco top and bot
            for(int i = UpperLimit.X; i <= LowerLimit.X; i++)
            {
                //Ubicamos el cursor e imprimomos los caracteres

                //Marco de arriba
                Console.SetCursorPosition(i,UpperLimit.Y);
                Console.Write("═");

                //Marco de abajo
                Console.SetCursorPosition(i, LowerLimit.Y);
                Console.Write("═");
            }

            //Marco left and right
            for(int i = UpperLimit.Y; i <= LowerLimit.Y; i++)
            {
                //Marco de izquierda
                Console.SetCursorPosition(UpperLimit.X, i);
                Console.Write("║");

                //Marco de derecha
                Console.SetCursorPosition(LowerLimit.X, i);
                Console.Write("║");
            }

            //Esquina superior izquierda
            Console.SetCursorPosition(UpperLimit.X,UpperLimit.Y);
            Console.Write("╔");

            //Esquina inferior izquierda
            Console.SetCursorPosition(UpperLimit.X, LowerLimit.Y);
            Console.Write("╚");

            //Esquina superior derecha
            Console.SetCursorPosition(LowerLimit.X,UpperLimit.Y);
            Console.Write("╗");

            //Esquina inferior derecha
            Console.SetCursorPosition(LowerLimit.X, LowerLimit.Y);
            Console.Write("╝");
        }

        //Ventana de peligro cuando toca el boss
        public void Danger()
        {
            Console.Clear();
            Thread.Sleep(100);
            DrawFrame(); //TODO: checar porque no esta creando bien el marco a veces

            for (int i = 0; i < 6; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(LowerLimit.X / 2 - 5, LowerLimit.Y / 2);
                Console.Write("CUIDAO !!");
                Thread.Sleep(200);
                Console.SetCursorPosition(LowerLimit.X / 2 - 5, LowerLimit.Y / 2);
                Console.Write("         ");
                Thread.Sleep(200);



            }
        }

        //Mostrar menu del juego
        public void Menu()
        {
            _enemy1.Move();
            _enemy2.Move();
            MoveBullets();

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LowerLimit.X / 2 - 5, LowerLimit.Y / 2 - 1);
            Console.Write("[ENTER] Jugar");
            Console.SetCursorPosition(LowerLimit.X / 2 - 5, LowerLimit.Y / 2);
            Console.Write("[ESC] Salir");

        }

        //Para detectar las teclas del menu
        public void Keyboard(ref bool active, ref bool play)
        {
            if(Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if(key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    Thread.Sleep(100);
                    DrawFrame();
                    play = true;
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    active = false;
                }
            }
        }

        //Metodo para crear las balas del menu
        public void MakeBullets()
        {
            Bullet bullet1 = new Bullet(new Point(0, 0), ConsoleColor.DarkBlue, BulletType.Menu);
            _bullets.Add(bullet1);
            Bullet bullet2 = new Bullet(new Point(0, 0), ConsoleColor.DarkMagenta, BulletType.Menu);
            _bullets.Add(bullet2);
            Bullet bullet3 = new Bullet(new Point(0, 0), ConsoleColor.Cyan, BulletType.Menu);
            _bullets.Add(bullet3);
            Bullet bullet4 = new Bullet(new Point(0, 0), ConsoleColor.Gray, BulletType.Menu);
            _bullets.Add(bullet4);
            Bullet bullet5 = new Bullet(new Point(0, 0), ConsoleColor.Blue, BulletType.Menu);
            _bullets.Add(bullet5);
            Bullet bullet6 = new Bullet(new Point(0, 0), ConsoleColor.DarkRed, BulletType.Menu);
            _bullets.Add(bullet6);
            Bullet bullet7 = new Bullet(new Point(0, 0), ConsoleColor.Yellow, BulletType.Menu);
            _bullets.Add(bullet7);
            Bullet bullet8 = new Bullet(new Point(0, 0), ConsoleColor.White, BulletType.Menu);
            _bullets.Add(bullet8);
            Bullet bullet9 = new Bullet(new Point(0, 0), ConsoleColor.Green, BulletType.Menu);
            _bullets.Add(bullet9);
            Bullet bullet10 = new Bullet(new Point(0, 0), ConsoleColor.DarkCyan, BulletType.Menu);
            _bullets.Add(bullet10);
            Bullet bullet11 = new Bullet(new Point(0, 0), ConsoleColor.DarkGray, BulletType.Menu);
            _bullets.Add(bullet11);
            Bullet bullet12 = new Bullet(new Point(0, 0), ConsoleColor.DarkGreen, BulletType.Menu);
            _bullets.Add(bullet12);
            Bullet bullet13 = new Bullet(new Point(0, 0), ConsoleColor.Blue, BulletType.Menu);
            _bullets.Add(bullet13);
            Bullet bullet14 = new Bullet(new Point(0, 0), ConsoleColor.Cyan, BulletType.Menu);
            _bullets.Add(bullet14);
            Bullet bullet15 = new Bullet(new Point(0, 0), ConsoleColor.Yellow, BulletType.Menu);
            _bullets.Add(bullet15);
            Bullet bullet16 = new Bullet(new Point(0, 0), ConsoleColor.Green, BulletType.Menu);
            _bullets.Add(bullet16);
            Bullet bullet17 = new Bullet(new Point(0, 0), ConsoleColor.Magenta, BulletType.Menu);
            _bullets.Add(bullet17);
            Bullet bullet18 = new Bullet(new Point(0, 0), ConsoleColor.DarkMagenta, BulletType.Menu);
            _bullets.Add(bullet18);
            Bullet bullet19 = new Bullet(new Point(0, 0), ConsoleColor.White, BulletType.Menu);
            _bullets.Add(bullet19);
            Bullet bullet20 = new Bullet(new Point(0, 0), ConsoleColor.Magenta, BulletType.Menu);
            _bullets.Add(bullet20);

            Random random = new Random();

            for(int i = 0; i < _bullets.Count; i++)
            {
                RandomPositions(_bullets[i]);

                //Para evitar que todas las balas salgan de Y = 0
                int randomNumber = random.Next(UpperLimit.Y + 1, LowerLimit.Y);
                _bullets[i].Position = new Point(_bullets[i].Position.X, randomNumber);
            }

        }

        //Generar las posiciones de donde saldran las balas
        //Recibe la bala que cambiara de posicion, una vez colisione con el marco superipr
        public void RandomPositions(Bullet bullet)
        {
            Random random = new Random();

            int randomNumer = random.Next(UpperLimit.X + 1, LowerLimit.X);

            bullet.Position = new Point(randomNumer, LowerLimit.Y);
        }

        //Mover balas del menu
        public void MoveBullets()
        {
            for(int i = 0; i < _bullets.Count; i++)
            {
                if (_bullets[i].Move(1, UpperLimit.Y)) //Si regresa true, es porque la bala colisiono
                    RandomPositions(_bullets[i]); //Entonces le generamos una nueva posicion desde el inicio

            }
        }
    }
}
