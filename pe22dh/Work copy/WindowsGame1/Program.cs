using System;

namespace CombatLand
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (CLand game = new CLand())
            {
                game.Run();
            }
        }
    }
}

