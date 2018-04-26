using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake
{
    class Screen
    {

        private Details[,] board;
        int[] foodPosition = new int[2];
        int difficulty = 0;
        int timesLooped = 0;

        Random rnd = new Random();
        public Player player;
        public Direction NextDirection { get; set; }

        public void StartGame()
        {
            board = new Details[21, 21];
            player = new Player();
            board[10, 10] = Details.PlayerHead;
            GenerateFood();

            //WaitForPlayer();
            Thread newThread = new Thread(WaitForPlayer);
            newThread.Start();
        }

        private void GenerateFood()
        {
            for (int i = 0; i < foodPosition.Length; i++)
            {
                foodPosition[i] = rnd.Next(3, 19);
            }
            Point FoodPoint = new Point(foodPosition[0], foodPosition[1], Details.Food);
        }

        public void Print()
        {
            string toPrint = string.Empty;
            int length = (int)Math.Sqrt(board.Length);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (i == 0 || i == length - 1)
                    {
                        toPrint += "-";
                    }
                    else if (j == 0 || j == length - 1)
                    {
                        toPrint += "|";
                    }
                    else if (board[i, j] == Details.Empty)
                    {
                        toPrint += " ";
                    }
                    else if (board[i, j] == Details.Food)
                    {
                        toPrint += "@";
                    }
                    else if (board[i, j] == Details.Player)
                    {
                        toPrint += "#";
                    }
                    else if (board[i, j] == Details.PlayerHead)
                    {
                        toPrint += "$";
                    }
                }

                if(i == 2)
                {
                    toPrint += "Score : ";
                }
                if(i == 4)
                {
                    toPrint += "Head Position : " + player.Head.Position[0] + "," + player.Head.Position[1];
                }
                if (i == 5)
                {
                    if(player.Tail != null)
                    toPrint += "Next Position : " + player.Tail.Position[0] + "," + player.Tail.Position[1];
                }
                toPrint += "\n";
            }

            Console.Clear();
            Console.WriteLine(toPrint);
        }

        public void WaitForPlayer()
        {
            while (true)
            {
                if (timesLooped <= 60)
                {
                    Thread.Sleep(800 - (difficulty * 10));
                    player.Move(NextDirection);
                    Update();
                    Print();
                }
                else
                {
                    difficulty++;
                    timesLooped = 0;
                }
            }
        }

        private void Update()
        {
            if (player.Head.Position[0] == foodPosition[0] && player.Head.Position[1] == foodPosition[1])
            {
                board[player.Head.Position[0], player.Head.Position[1]] = Details.PlayerHead;
                player.StartGrowing(new Point(foodPosition[1], foodPosition[0]));
                GenerateFood();
            }

            int length = (int)Math.Sqrt(board.Length);
            int playerSize = player.Size;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    board[i, j] = Details.Empty;
                    PlayerPart playerPart = player.Head;
                    do
                    {
                        if (playerPart.Position[0] == i && playerPart.Position[1] == j)
                        {
                            if (playerPart == player.Head)
                            {
                                board[i, j] = Details.PlayerHead;
                                break;
                            }
                            else
                            {
                                board[i, j] = Details.Player;
                                break;
                            }
                        }
                        if (playerPart.next != null)
                        {
                            playerPart = playerPart.next;
                        }
                        else
                        {
                            break;
                        }
                    }
                    while (true);

                    if (i == foodPosition[0] && j == foodPosition[1])
                    {
                        board[i, j] = Details.Food;
                    }

                    //if (board[i, j] == Details.PlayerHead)
                    //{
                    //    if (player.Head.Position[0] != i || player.Head.Position[1] != j)
                    //    {
                    //        board[i, j] = Details.Empty;
                    //    }
                    //}

                }
            }
        }

        
        public Screen()
        {
        }
    }
}
