// .Net 7.0
using System.Drawing;

namespace SpaceGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Start();

            Console.ReadKey();
            
        }

        static void Start()
        {
            Window window;
            Ship ship;

            //Inicializar ventana
            window = new Window(
                Console.LargestWindowWidth, //Largo consola
                Console.LargestWindowHeight, //Ancho consola
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
