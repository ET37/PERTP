#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace NoNamedGame
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /* Como el constructor de Game1 ya no es accesible 
             * por su visibilidad, usandola como Singleton accedemos
             * a la clase por su instancia
             * */
            using (var game = Game1.Instance)
                 game.Run();
        }
    }
#endif
}
