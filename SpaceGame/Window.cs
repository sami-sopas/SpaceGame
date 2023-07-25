using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
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

        //Ventana de peligro cuando toca pelear contra el boss
        public void Danger()
        {
            Console.Clear();
            Thread.Sleep(100);
            DrawFrame();

            Thread.Sleep(500);
            WrittingSound();

            Console.SetCursorPosition(LowerLimit.X / 2 - 6, LowerLimit.Y - 18);
            Title("Nivel completado",100);

            Console.SetCursorPosition(LowerLimit.X / 2 - 5, LowerLimit.Y - 16);
            Title("Haz recibido:", 100);

            Thread.Sleep(500);
            BonusSound();

            Console.SetCursorPosition(LowerLimit.X / 2 - 6, LowerLimit.Y - 13);
            Console.Write("+ 40 de Vida");

            Thread.Sleep(250);
            Console.SetCursorPosition(LowerLimit.X / 2 - 6, LowerLimit.Y - 11);
            Console.Write("+ 30 de SuperDisparo");



            Thread.Sleep(2000);
            for (int i = 0; i < 6; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(LowerLimit.X / 2 - 2, LowerLimit.Y - 7);
                Console.Write("CUIDAO !!");
                Thread.Sleep(250);
                Console.SetCursorPosition(LowerLimit.X / 2 - 2, LowerLimit.Y - 7);
                Console.Write("         ");
                Thread.Sleep(200);



            }

            Console.Clear();
            Thread.Sleep(100);
            DrawFrame();
        }

        //Mostrar menu del juego
        public void Menu()
        {
            _enemy1.Move();
            _enemy2.Move();
            MoveBullets();

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(LowerLimit.X / 2 - 30, LowerLimit.Y / 2 - 10);
            Console.Write("  ____                             ____                          ");
            Console.SetCursorPosition(LowerLimit.X / 2 - 30, LowerLimit.Y / 2 - 9);
            Console.Write(" / ___|  _ __    __ _   ___  ___  / ___|  __ _  _ __ ___    ___  ");
            Console.SetCursorPosition(LowerLimit.X / 2 - 30, LowerLimit.Y / 2 - 8);
            Console.Write(" \\___ \\ | '_ \\  / _` | / __|/ _ \\| |  _  / _` || '_ ` _ \\  / _ \\ ");
            Console.SetCursorPosition(LowerLimit.X / 2 - 30, LowerLimit.Y / 2 - 7);
            Console.Write("  ___) || |_) || (_| || (__|  __/| |_| || (_| || | | | | ||  __/ ");
            Console.SetCursorPosition(LowerLimit.X / 2 - 30, LowerLimit.Y / 2 - 6);
            Console.Write(" |____/ | .__/  \\__,_| \\___|\\___| \\____| \\__,_||_| |_| |_| \\___| ");
            Console.SetCursorPosition(LowerLimit.X / 2 - 30, LowerLimit.Y / 2 - 5);
            Console.Write("        |_|                                                      ");

 // ____                             ____                          
 /// ___|  _ __    __ _   ___  ___  / ___|  __ _  _ __ ___    ___  
 //\___ \ | '_ \  / _` | / __|/ _ \| |  _  / _` || '_ ` _ \  / _ \ 
 // ___) || |_) || (_| || (__|  __/| |_| || (_| || | | | | ||  __/ 
 //|____/ | .__/  \__,_| \___|\___| \____| \__,_||_| |_| |_| \___| 
 //       |_|                                                      
            Console.SetCursorPosition(LowerLimit.X / 2 - 6, LowerLimit.Y / 2);
            Console.Write("[ENTER] Jugar");
            Console.SetCursorPosition(LowerLimit.X / 2 - 6, LowerLimit.Y / 2 + 2);
            Console.Write("[TAB]   Instrucciones");
            Console.SetCursorPosition(LowerLimit.X / 2 - 6, LowerLimit.Y / 2 + 4);
            Console.Write("[ESC]   Salir");



        }

        //Para detectar las teclas del menu
        public void Keyboard(ref bool active, ref bool play)
        {
            if(Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if(key.Key == ConsoleKey.Enter)
                {
                    ClickSound();
                    Thread.Sleep(1000);

                    Console.Clear();
                    Thread.Sleep(150);
                    DrawFrame();
                    Introduction();
                    play = true;
                   


                }
                if (key.Key == ConsoleKey.Escape)
                {
                    active = false;
                }
                if(key.Key == ConsoleKey.Tab)
                {
                    ClickSound();
                    play = false;
                    Instructions();
                    Console.Clear();
                    Thread.Sleep(100);
                    DrawFrame();
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

        //Mostrar instrucciones
        public void Instructions()
        {
            Console.Clear();

            Thread.Sleep(100);

            DrawFrame();

            Console.SetCursorPosition(LowerLimit.X / 2 - 19, LowerLimit.Y - 24);
            Console.Write("Tu mision no es solo eliminar amenazas externas,");
            Console.SetCursorPosition(LowerLimit.X / 2 - 24, LowerLimit.Y - 23);
            Console.Write("sino también descubrir la esencia misma de tu existencia");
            Console.SetCursorPosition(LowerLimit.X / 2 - 35, LowerLimit.Y - 22);
            Console.Write("¿Acaso tus enemigos no son reflejos de tus propios deseos, temores y pasiones?");
            Console.SetCursorPosition(LowerLimit.X / 2 - 30, LowerLimit.Y - 21);
            Console.Write("¿Qué significado tiene la guerra en el inmenso lienzo del cosmos?");

            Console.SetCursorPosition(LowerLimit.X / 2 - 32, LowerLimit.Y - 16);
            Console.Write("[W] Arriba");
            Console.SetCursorPosition(LowerLimit.X / 2 - 32, LowerLimit.Y - 14);
            Console.Write("[S] Abajo");
            Console.SetCursorPosition(LowerLimit.X / 2 - 32, LowerLimit.Y - 12);
            Console.Write("[A] Izquierda");
            Console.SetCursorPosition(LowerLimit.X / 2 - 32, LowerLimit.Y - 10);
            Console.Write("[D] Derecha");

            Console.SetCursorPosition(LowerLimit.X - 35, LowerLimit.Y - 15);
            Console.Write("[^] SuperDisparo");
            Console.SetCursorPosition(LowerLimit.X - 35, LowerLimit.Y - 13);
            Console.Write("[>] Disparo derecha");
            Console.SetCursorPosition(LowerLimit.X - 35, LowerLimit.Y - 11);
            Console.Write("[<] Disparo izquierda");

            Console.SetCursorPosition(LowerLimit.X - 56, LowerLimit.Y - 14);
            Console.Write("A");
            Console.SetCursorPosition(LowerLimit.X - 58, LowerLimit.Y - 13);
            Console.Write("<{x}>");
            Console.SetCursorPosition(LowerLimit.X - 59, LowerLimit.Y - 12);
            Console.Write("± W W ±");

            Console.SetCursorPosition(LowerLimit.X / 2 - 16, LowerLimit.Y - 4);
            Console.Write("Presiona cualquier tecla para regresar...");
            

            Console.ReadKey();
            ClickSound();


        }

        //Sonido al hacer click en una de las teclas
        public void ClickSound()
        {
            if (OperatingSystem.IsWindows())
            {
                SoundPlayer song = new SoundPlayer("Sound.wav");

                song.Load();
                song.Play();
            }
        }

        //Sonido de escritura
        public void WrittingSound()
        {
            if (OperatingSystem.IsWindows())
            {
                SoundPlayer song = new SoundPlayer("Writting.wav");

                song.Load();
                song.Play();
            }
        }

        //Sonido de bonus
        public void BonusSound()
        {
            if (OperatingSystem.IsWindows())
            {
                SoundPlayer song = new SoundPlayer("Bonus.wav");

                song.Load();
                song.Play();
            }
        }

        //Ventana a mostrar antes de iniciar el juego
        public void Introduction()
        {
            WrittingSound();

            Console.SetCursorPosition(LowerLimit.X / 2 - 10, LowerLimit.Y - 17);
            Title("In my restless dreams,", 80);
            Console.SetCursorPosition(LowerLimit.X / 2 - 9, LowerLimit.Y - 15);
            Title("I see that planet...", 80);
            Thread.Sleep(1500);

            Console.SetCursorPosition(LowerLimit.X / 2 - 10, LowerLimit.Y - 17);
            Console.Write("                       ");      
            Console.SetCursorPosition(LowerLimit.X / 2 - 9, LowerLimit.Y - 15);
            Console.Write("                       ");


        }

        //Ventana a mostrar despues de derrotar al primer enemigo
        public void SecondLevel()
        {
            Console.Clear();
            Thread.Sleep(100);
            DrawFrame();

            Thread.Sleep(500);
            WrittingSound();

            Console.SetCursorPosition(LowerLimit.X / 2 - 6, LowerLimit.Y - 18);
            Title("Nivel completado", 100);

            Console.SetCursorPosition(LowerLimit.X / 2 - 5, LowerLimit.Y - 16);
            Title("Haz recibido:", 100);

            Thread.Sleep(500);
            BonusSound();

            Console.SetCursorPosition(LowerLimit.X / 2 - 6, LowerLimit.Y - 13);
            Console.Write("+ 20 de Vida");

            Thread.Sleep(250);
            Console.SetCursorPosition(LowerLimit.X / 2 - 6, LowerLimit.Y - 11);
            Console.Write("+ 25 de SuperDisparo");



            Thread.Sleep(2000);
            Console.Clear();
            Thread.Sleep(100);
            DrawFrame();

        }

        //Para escribir lentamente
        public void Title(string text, int speed)
        {
            foreach (char caracter in text)
            {
                Console.Write(caracter);
                Thread.Sleep(speed);
            }
        }

    }
}
