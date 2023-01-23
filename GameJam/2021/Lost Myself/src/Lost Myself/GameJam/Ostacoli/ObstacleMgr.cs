using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    enum ObstacleType
    {
        FENCE,
        FILOSPINATOBIG,
        FILOSPINATO,
        LAST
    }

    static class ObstacleMgr
    {
        private static int queueSize = 16;
        private static float counterMax = 2f;
        private static float counter;
        private static bool isFirstObstacle;
        static Queue<Obstacle>[] obstacles;
        static List<Obstacle> activeObstacles;
        static Queue<HeartPiece> hearts;
        static List<HeartPiece> activeHearts;

        public static int ObstaclePassed;
        public static int HeartsAquired;

        public static void Init()
        {
            isFirstObstacle = true;
            ObstaclePassed = 0;
            HeartsAquired = 0;
            counter = counterMax;
            activeObstacles = new List<Obstacle>();
            activeHearts = new List<HeartPiece>();
            obstacles = new Queue<Obstacle>[(int)ObstacleType.LAST];
            for (int i = 0; i < obstacles.Length; i++)
            {
                obstacles[i] = new Queue<Obstacle>(queueSize);
                switch ((ObstacleType)i)
                {
                    case ObstacleType.FENCE:
                        for (int j = 0; j < queueSize; j++)
                        {
                            obstacles[i].Enqueue(new Fence());
                        }
                        break;
                    case ObstacleType.FILOSPINATO:
                        for (int j = 0; j < queueSize; j++)
                        {
                            obstacles[i].Enqueue(new FiloSpinato());
                        }
                        break;
                    case ObstacleType.FILOSPINATOBIG:
                        for (int j = 0; j < queueSize; j++)
                        {
                            obstacles[i].Enqueue(new FiloSpinatoBig());
                        }
                        break;
                }
            }
            hearts = new Queue<HeartPiece>(1);
            for (int i = 0; i < 1; i++)
            {
                hearts.Enqueue(new HeartPiece());
            }
        }

        public static void AddObstacle(Obstacle o)
        {
            o.Spawn();
            o.IsActive = true;
            o.Colliders.SetActive(true);
            activeObstacles.Add(o);
        }

        public static void AddHeart(HeartPiece h, Vector2 heartPos)
        {
            h.Spawn(heartPos);
            activeHearts.Add(h);
            h.IsActive = true;
        }

        public static void RestoreObstacle(Obstacle o)
        {
            o.IsActive = false;
            o.Colliders.SetActive(false);
            activeObstacles.Remove(o);
            obstacles[(int)o.Type].Enqueue(o);
            ObstaclePassed++;
        }

        public static void RestoreHeart(HeartPiece h)
        {
            h.IsActive = false;
            activeHearts.Remove(h);
            hearts.Enqueue(h);
        }

        public static void DeleteAllActiveObstacles()
        {
            for (int i = activeObstacles.Count - 1; i >= 0; i--)
            {
                RestoreObstacle(activeObstacles[i]);
            }
        }

        public static void ClearAllHearts()
        {
            for (int i = activeHearts.Count - 1; i >= 0; i--)
            {
                RestoreHeart(activeHearts[i]);
            }
        }

        public static void Update()
        {
            Console.SetCursorPosition(0, 5);
            Console.WriteLine(ObstaclePassed + "          ");
            //Calcolo tempo
            if (Game.IsStart || Game.EndGame)
            { 
            }
            else
            {
                counter -= Game.DeltaTime;
            }
            //Se tempo è minore di zero allora spawn random ostacolo
            if (counter <= 0)
            {
                //Decido l'ostacolo
                int queueIndex;
                queueIndex = RandomGenerator.GetRandomInt((int)ObstacleType.LAST);
                if (isFirstObstacle)
                {
                    while (queueIndex == (int)ObstacleType.FILOSPINATO)
                    {
                        queueIndex = RandomGenerator.GetRandomInt((int)ObstacleType.LAST);
                    }
                    isFirstObstacle = false;
                }
                AddObstacle(obstacles[queueIndex].Dequeue());

                //Vedo se ho superato abbastanza ostacoli
                if (ObstaclePassed >= 5)
                {
                    Vector2 heartPos = Vector2.Zero;
                    switch ((ObstacleType)queueIndex)
                    {
                        case ObstacleType.FENCE:
                            heartPos = new Vector2(Game.Win.Width + 50, 310);
                            break;
                        case ObstacleType.FILOSPINATO:
                            heartPos = new Vector2(Game.Win.Width + 100, 335);
                            break;
                        case ObstacleType.FILOSPINATOBIG:
                            heartPos = new Vector2(Game.Win.Width + 100, 465);
                            break;
                    }
                    ObstaclePassed = 0;
                    AddHeart(hearts.Dequeue(), heartPos);
                }
                counter = counterMax;
            }
        }

        public static bool CheckCollision(Colliders playerColliders)
        {
            bool ret = false;
            for (int i = 0; i < activeObstacles.Count; i++)
            {
                if (ret)
                {
                    break;
                }
                for (int j = 0; j < activeObstacles[i].Colliders.Count; j++)
                {
                    if (ret)
                    {
                        break;
                    }
                    for (int k = 0; k < playerColliders.Count; k++)
                    {
                        Vector2 dist = playerColliders.CircleColliders[k].Position - activeObstacles[i].Colliders.CircleColliders[j].Position;
                        float length = dist.Length;
                        if (length <= (playerColliders.CircleColliders[k].Radious + activeObstacles[i].Colliders.CircleColliders[j].Radious))
                        {
                            ret = true;
                            break;
                        }
                    }
                }
            }
            return ret;
        }
    }
}
