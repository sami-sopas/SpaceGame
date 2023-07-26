// .Net 7.0
using System.Drawing;
using System.Media;

namespace SpaceGame
{
    class Program
    {
        static Window window;
        static Ship ship;
        static Enemy enemy1;
        static Enemy enemy2;
        static Enemy enemy3;
        static Enemy boss;
        static bool play = false;
        static bool active = true;
        static bool showEnemies2And3 = false;
        static bool showFinalBoss = false;
        static void Main(string[] args)
        {
            /* NOTAS PARA LA REPRODUCCION DE SONIDOS, SOLO FUNCIONA EN WINDOWS
             * 
             * Pasos para agregar sonidos
             * 1. Descarga el archivo en formato WAV
             * 2. Click derecho a la solucion, agregar elemento existente y seleccionas el archivo
             * 3. En propiedades activas la casilla de Copiar siempre
             * 4. Instala System.Windows.Media mediante NuGet
             * 
             * 
             */

            Start();
            enemy1.IsAlive = false; enemy2.IsAlive = false; enemy3.IsAlive = false;
            showFinalBoss = true;
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
            //ship.Draw();

            //Creacion de enemigos
            enemy1 = new Enemy(new Point(50, 10), ConsoleColor.Cyan, window, TypeEnemy.Normal, ship);
            enemy2 = new Enemy(new Point(30, 12), ConsoleColor.Red, window, TypeEnemy.Normal, ship);
            enemy3 = new Enemy(new Point(40, 8), ConsoleColor.Gray, window, TypeEnemy.Normal, ship);
            boss = new Enemy(new Point(70, 10), ConsoleColor.Magenta, window, TypeEnemy.Boss,ship);


            //Dibujar enemigo
            //enemy1.Draw();
            //enemy2.Draw();
            //boss.Draw();

            //Agregamos los enemigos a la lista de enemigos de la nave
            ship.Enemies.Add(enemy1);
            ship.Enemies.Add(enemy2);
            ship.Enemies.Add(enemy3);
            ship.Enemies.Add(boss);


        }

        static void Game()
        {


            while (active) //EJECUCION DEL PROGRAMA
            {
                window.Menu(); //LLamar a menu mientras la aplicacion este activa
                window.Keyboard(ref active, ref play);

                while (play) // Ejecución del juego
                {
                    if (!enemy1.IsAlive && !showEnemies2And3) // Si el enemigo 1 muere y aún no mostramos a los enemigos 2 y 3
                    {
                        Enemy.DeathSound();
                        Thread.Sleep(1700);
                        showEnemies2And3 = true;

                        window.SecondLevel(); //Ventana para avisar sobre el segundo nivel


                        //y aumentaremos algunos atributos
                        ship.Health += 20;

                        if (ship.Health > 100) //En caso de que sobrepase, aqui hacemos que no aumente de 100
                        {
                            ship.Health = 100;
                        }

                        Ship.SuperShot += 25;

                        if (Ship.SuperShot > 100)
                        {
                            Ship.SuperShot = 100;
                        }

                        enemy2.Move(); // Muestra al enemigo 2 en la pantalla
                        enemy2.Information(80);
                        enemy3.Move();
                        enemy3.Information(100); // Muestra al enemigo 3 en la pantalla
                    }

                    if (!enemy2.IsAlive && !enemy3.IsAlive && !showFinalBoss) // Si los enemigos 2 y 3 mueren y aún no mostramos al jefe final
                    {
                        Enemy.DeathSound();
                        Thread.Sleep(1500);
                        showFinalBoss = true;
                        window.Danger();// Aqui se avisa que aparecera el jefe 

                        //y aumentaremos algunos atributos
                        ship.Health += 40;

                        if(ship.Health > 100) //En caso de que sobrepase, aqui hacemos que no aumente de 100
                        {
                            ship.Health = 100;
                        }

                        Ship.SuperShot += 30;

                        if(Ship.SuperShot > 100)
                        {
                            Ship.SuperShot = 100;
                        }

                     
                        boss.Move(); // Muestra al jefe final en la pantalla
                        boss.Information(100);
                    }

                    if (showFinalBoss) // Si se mostró al jefe final
                    {
                        boss.Move();
                        boss.Information(100);
                    }
                    else if (showEnemies2And3) // Si se mostraron los enemigos 2 y 3
                    {
                        enemy2.Move();
                        enemy2.Information(80);

                        enemy3.Move();
                        enemy3.Information(100);
                    }
                    else // Si no, seguimos mostrando al enemigo 1
                    {
                        enemy1.Move();
                        enemy1.Information(100);
                    }

                    ship.Move(2);
                    ship.Shoot();
                    //Thread.Sleep(50);

                    // Cuando muera la nave
                    if (ship.Health <= 0)
                    {
                        play = false;
                        ship.Dead();
                        Restart();
                    }

                    // Cuando muera el jefe final, termina el juego
                    if (!boss.IsAlive)
                    {
                        play = false;
                        // Cuando el jefe final muere, mostramos un final
                        window.End();
                        //Despues reiniciamos el juego
                        Restart();
                    }
                }
            }

            
        }

        //Reinciar el juego una vez terminado
        static void Restart()
        {
            Console.Clear();
            Thread.Sleep(100);
            window.DrawFrame();

            ship.Health = 100;
            Ship.SuperShot = 0;
            ship.Bullets.Clear();

            enemy1.Health = 100;
            enemy1.IsAlive = true;

            enemy2.Health = 100;
            enemy2.IsAlive = true;

            enemy3.Health = 100;
            enemy3.IsAlive = true;

            boss.Health = 100;
            boss.IsAlive = true;
            boss.PositionsEnemy.Clear();
        }




    }
}
