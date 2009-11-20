using System;

namespace Tower_Defense
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (TD game = new TD())
            {
                game.Run();
            }
        }
    }
}

