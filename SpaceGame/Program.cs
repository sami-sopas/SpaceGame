// .Net 7.0
using System.Drawing;

namespace SpaceGame
{
    class Program
    {
        static Window? window;
        static Ship? ship;
        static Enemy? enemy1;
        static Enemy? enemy2;
        static Enemy? boss;
        static bool play = true;
        static void Main(string[] args)
        {
            Start();
            Game();



            
            
        }

        static void Start()
        {

            //Inicializar ventana
            window = new Window(
                Console.LargestWindowWidth, //Ancho consola maximo (120)
                Console.LargestWindowHeight, //Altura consola maximo (30)
                ConsoleColor.Black, //Color de fondo
                new Point(3, 1), //Coordenadas X para marco
                new Point(118, 28)); //Coordenadas Y para marco

            //Dibujar marco
            window.DrawFrame();

            //Inicializar nave
            ship = new Ship(
                new Point(58, 23),
                ConsoleColor.White,
                window);

            //Dibujar nave
            ship.Draw();

            //Creacion de enemigos
            enemy1 = new Enemy(new Point(50, 10), ConsoleColor.Cyan, window, TypeEnemy.Normal, ship);
            enemy2 = new Enemy(new Point(30, 12), ConsoleColor.Red, window, TypeEnemy.Normal, ship);
            boss = new Enemy(new Point(70, 10), ConsoleColor.Magenta, window, TypeEnemy.Boss,ship);

            //Dibujar enemigo
            enemy1.Draw();
            enemy2.Draw();
            boss.Draw();

            //Agregamos los enemigos a la lista de enemigos de la nave
            ship.Enemies.Add(enemy1);
            ship.Enemies.Add(enemy2);
            ship.Enemies.Add(boss);


        }

        static void Game()
        {
            while (play)
            {
                boss.Move();
                boss.Information(100);
                enemy1.Move();
                enemy1.Information(60);
                enemy2.Move();
                enemy2.Information(80);
                ship.Move(2);
                ship.Shoot();
                //Thread.Sleep(50);

                //cuando muera
                if(ship.Health <= 0)
                {
                    play = false;
                    ship.Dead();
                }
            }
        }



    }
}
