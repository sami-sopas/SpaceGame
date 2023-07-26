using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGame
{
    public enum TypeEnemy
    {
        Normal, Boss, Menu
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

        //Constantes a donde se dirigira el enemigo
        enum Direction
        {
            Right,Left,Up,Down
        }

        //Guardara la posicion actual a donde se dirige el enemigo
        private Direction direction;

        //Guardar el tiempo para cambiar de direccion
        private DateTime timeDirection;

        //Para generar el tiempo en el que se cambiara de direccion de manera aleatoria
        private float timeDirectionRandom;

        //Para que no se muevan tan rapido y se logren ver bien los enemigos
        private DateTime timeMovement;

        //Lista para guardar las balas
        public List<Bullet> Bullets { get; set; }

        //Guardar tiempo de creacion de balas enemigas
        private DateTime timeShoot;

        //Tiempo de disparo aleatorio de balas enemigas
        private float timeShootRandom;

        //Para solucionar colisiones de balas con la nave
        private Ship ShipC { get; set; }


        public Enemy (Point p, ConsoleColor c, Window w, TypeEnemy t, Ship shipC)
        {
            this.Position = p;
            this.Color = c;
            this.TypeEnemyE = t;
            this.WindowC = w;
            IsAlive = true;
            Health = 100;
            direction = Direction.Right; //Direccion inicial
            timeDirection = DateTime.Now;
            timeDirectionRandom = 1000;
            timeMovement = DateTime.Now;
            PositionsEnemy = new List<Point>();
            Bullets = new List<Bullet>();
            timeShoot = DateTime.Now;
            timeShootRandom = 200;
            this.ShipC = shipC;
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

                case TypeEnemy.Menu: //Enemigos que saldran en el menu
                    DrawNormal();
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

        //Funcion para mover al enemigo
        public void Move()
        {
            //Validamos que el enemigo este vivo
            if(!IsAlive)
            {
                Death();
                return;
            }

            int time = 30; //Tiempo para enemigo normales

            if (TypeEnemyE == TypeEnemy.Boss)
                time = 20;

            //Si ya paso x tiempo desde el ultimo movimiento, ejecutamos el siguiente
            if(DateTime.Now > timeMovement.AddMilliseconds(time))
            {
                //Tenemos que borrar sus posiciones y volverlas a dibujar
                Delete();

                RandomDirection(); //Generamos direcciona leatoria cada cierto tiempo

                Point positionAux = Position;
                Movement(ref positionAux); //Mover el enemigo segun la direccion a la que va
                Collisions(positionAux);

                Draw();

                //Capturamos fecha y hora cuando se realiza un movimiento
                timeMovement = DateTime.Now;
            }

            //CREACION DE BALAS PARA LOS ENEMIGOS QUE NO SON LOS DEL MENU (NO DISPARAN ESOS)
            if(TypeEnemyE != TypeEnemy.Menu) //Si el enemigo a crear, no es del menu, entonces si disparara
            {
                CreateBullets();
                Shoot();
            }



        }

        public void Movement(ref Point positionAux)
        {
            //Evaluamos la direccion del enemigo para que el enemigo se mueva segun la direccion a la que va
            switch(direction)
            {
                case Direction.Right:
                    positionAux.X += 1;
                    break;
                case Direction.Left:
                    positionAux.X -= 1;
                    break;
                case Direction.Up:
                    positionAux.Y -= 1;
                    break;
                case Direction.Down:
                    positionAux.Y += 1;
                    break;
            }
        }

        //Funcion para resolver colisiones con el marco
        public void Collisions(Point positionAux)
        {
            int width = 3; //ancho enemigo normal
            if(TypeEnemyE == TypeEnemy.Boss)
                width = 7; //ancho de boss

            int lowerLimit = WindowC.UpperLimit.Y + 15; //Le asignamos el limite que tenia  (hasta la mitad)

            if (TypeEnemyE == TypeEnemy.Menu) //Si el enemigo es del menu, entonces cambiamos su limite
                lowerLimit = WindowC.LowerLimit.Y - 1; //Para que este pueda moverse por toda la pantalla
 

            //si la posicion de la nave supera el de los limites de la ventana, hacemos que cambie de direccion
            if (positionAux.X <= WindowC.UpperLimit.X) //Colision sobrepasa izquierda
            {
                direction = Direction.Right;
                positionAux.X = WindowC.UpperLimit.X + 1;
            }
            if (positionAux.X + width >= WindowC.LowerLimit.X) //Colision sobrepasa derecha
            {
                direction = Direction.Left;
                positionAux.X = WindowC.LowerLimit.X - 1 - width;
            }
            if (positionAux.Y <= WindowC.UpperLimit.Y) //Colision sobrepasa arriba
            {
                direction = Direction.Down;
                positionAux.Y = WindowC.UpperLimit.Y + 1;
            }
            if (positionAux.Y + 2 >= lowerLimit) //Colision sobrepasa abajo (le sumamos 15 porque el limite esta a la mitad)
            {
                direction = Direction.Up;
                positionAux.Y = lowerLimit - 2;
            }

            Position = positionAux; //Igualamos a la auxiliar donde hicimos los cambios
            
        }

        //Generamos las direcciones de manera aleatoria
        public void RandomDirection()
        {
            //Verificamos si ya paso 1 segundo desde la ultima direccion aleatoria
            if(DateTime.Now > timeDirection.AddMilliseconds(timeDirectionRandom)
                && (direction == Direction.Right || direction == Direction.Left))
            {
                //Si es asi, entonces generamos la nueva direccion
                Random random = new Random();
                int randomNumer = random.Next(1, 5);

                switch (randomNumer) //Cambiamos las direccioens
                {
                    case 1: direction = Direction.Right; break;

                    case 2: direction = Direction.Left; break;

                    case 3: direction = Direction.Up; break;

                    case 4: direction = Direction.Down; break;
                }

                //Guardamos el tiempo en el que se creo
                timeDirection = DateTime.Now;

                //Generamos el tiempo aleatorio para cambiar de direccion
                timeDirectionRandom = random.Next(1000, 2000);

            }

            //Cuando la nave vaya hacia arriba o abajo, la nave ira a la izquierda o derecha (evitar rebotes)
            if (DateTime.Now > timeDirection.AddMilliseconds(50) 
                && (direction == Direction.Up || direction == Direction.Down))
            {
                //Si es asi, entonces generamos la nueva direccion
                Random random = new Random();
                int randomNumer = random.Next(1, 3);

                switch (randomNumer) //Cambiamos las direccioens
                {
                    case 1: direction = Direction.Right; break;

                    case 2: direction = Direction.Left; break;
                }

                //Guardamos el tiempo en el que se creo
                timeDirection = DateTime.Now;

            }


        }

        //Crear balas de los enemigos
        public void CreateBullets()
        {
            //Si ya pasaron 200 mls desde la ultima bala creada, creamos una
            if(DateTime.Now  > timeShoot.AddMilliseconds(timeShootRandom))
            {
                Random random = new Random();

                if (TypeEnemyE == TypeEnemy.Normal)
                {
                    Bullet bullet = new Bullet(
                        new Point(Position.X + 1, Position.Y + 2),
                        Color,
                        BulletType.Enemy);

                    Bullets.Add(bullet);
                    timeShootRandom = random.Next(200, 500); //Tiempo de generacion aleatorio
                }
                if (TypeEnemyE == TypeEnemy.Boss)
                {
                    Bullet bullet = new Bullet(
                        new Point(Position.X + 4, Position.Y + 2),
                        Color,
                        BulletType.Enemy);

                    Bullets.Add(bullet);
                    timeShootRandom = random.Next(100, 150); //Tiempo de generacion ale
                }

                timeShoot = DateTime.Now;
            }

        }
        
        public void Shoot()
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                //Devuelve true cuando supera el limite del marco, ya le enviamos la nave para las colisiones
                if(Bullets[i].Move(1, WindowC.LowerLimit.Y,ShipC))
                {
                    Bullets.Remove(Bullets[i]);
                }
            }
        }

        //Mostrar informacion de los enemigos
        public void Information(int distanceX)
        {
            //Recibimos la distancia en X desde el punto inicial del marco superior izquierdo
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(WindowC.UpperLimit.X + distanceX, WindowC.UpperLimit.Y - 1);
            Console.Write("ENEMIGO: " + (int)Health + " %  ");
        }

        //Animacion de muerte
        public void Death()
        {
            if(TypeEnemyE == TypeEnemy.Normal)
            {
                NormalDeath();
            }
            if (TypeEnemyE == TypeEnemy.Boss)
            {
                BossDeath();
            }
        }

        //Animacion de muerte para enemigos normales
        public void NormalDeath()
        {
            Console.ForegroundColor = ConsoleColor.White;
            
            int x = Position.X;
            int y = Position.Y;

            Console.SetCursorPosition(x + 1, y);
            Console.Write("▄▄Zzz");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("████");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("▀  ▀");

            //Borramos sus psosicioens
            PositionsEnemy.Clear();

            //Borramos las balas que hayan quedado del enemigo
            foreach(Bullet b in Bullets)
            {
                b.Delete();
            }

            Bullets.Clear();
        }

        //Animacion de muerte del boss
        public void BossDeath()
        {
            Console.ForegroundColor = Color;
            DeathSoundBoss();

            //Recorremos las posiciones del enemigo
            foreach(Point p in PositionsEnemy)
            {
                Console.SetCursorPosition(p.X, p.Y);
                Console.Write("▓");
                Thread.Sleep(200);
            }
            foreach(Point p in PositionsEnemy)
            {
                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(" ");
                Thread.Sleep(200);
            }
            //Eliminamos sus posiciones
            PositionsEnemy.Clear();

            //Eliminamos las balas que dejo
            foreach(Bullet b in Bullets)
            { 
                b.Delete(); 
            }

            Console.ForegroundColor = ConsoleColor.White;


        }


        //Audio a reproducir cuando muera un enemigo
        public static void DeathSound()
        {
            if (OperatingSystem.IsWindows())
            {
                SoundPlayer song = new SoundPlayer("Death.wav");

                song.Load();
                song.Play();
            }
        }

        //Audio a reproducir cuando muera el boss
        public static void DeathSoundBoss()
        {
            if (OperatingSystem.IsWindows())
            {
                SoundPlayer song = new SoundPlayer("FlyMeToTheMoon.wav");

                song.Load();
                song.Play();
            }
        }
    }
}
