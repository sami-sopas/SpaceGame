// .Net 7.0
using System.Drawing;

namespace SpaceGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //Inicializar ventana
            Window window = new Window(
                Console.LargestWindowWidth, //Largo consola
                Console.LargestWindowHeight, //Ancho consola
                ConsoleColor.Black, //Color de fondo
                new Point(3,1), //Coordenadas X para marco
                new Point(118,28)); //Coordenadas Y para marco

            //Dibujar marco
            window.DrawFrame();

            
            Console.ReadKey();
            
        }
    }
}
