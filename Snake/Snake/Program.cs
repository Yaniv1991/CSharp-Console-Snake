using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            bool gameOn;

            Console.WriteLine("Press Any Key to start");
            Console.Read();
            Screen screen = new Screen();

            screen.StartGame();
            gameOn = true;

            while (gameOn)
            {
                screen.NextDirection = (Direction)(Console.ReadKey().Key-37);

                
            }
        }
    }
}
