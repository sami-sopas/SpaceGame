// .Net 7.0
using System.Drawing;

namespace SpaceGame
{
    class Program
    {
        static Window? window;
        static Ship? ship;
        static bool play = true;
        static void Main(string[] args)
        {
            Start();
            Game();



            Console.ReadKey();
            
        }

        static void Game()
        {
            while (play)
            {
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
                new Point(58,23),
                ConsoleColor.White,
                window);

            //Dibujar nave
            ship.Draw();

        }


    }
}
