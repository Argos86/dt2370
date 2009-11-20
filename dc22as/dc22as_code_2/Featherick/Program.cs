using System;

namespace Featherick
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (FeatherickGame game = new FeatherickGame())
            {
                game.Run();
            }
        }
    }
}

