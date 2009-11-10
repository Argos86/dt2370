using System;

namespace ZombieHoards
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Zombie game = new Zombie())
            {
                game.Run();
            }
        }
    }
}

