using System;

namespace JumpMania
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (JM game = new JM())
            {
                game.Run();
            }
        }
    }
}

