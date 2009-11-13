using System;

namespace MeCloidGame
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MeCloid game = new MeCloid())
            {
                game.Run();
            }
        }
    }
}

