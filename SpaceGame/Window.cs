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
            //Console.Clear();

            for(int i = 0; i < 6; i++)
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
    }
}
