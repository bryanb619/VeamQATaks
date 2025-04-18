﻿namespace SyncTask
{
    /// <summary>
    /// Class defines root of C# program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Starting point of the program.
        /// Like Main in Python! 
        /// </summary>
        /// <param name="args">Set of arguments to be passed in console</param>
        private static void Main(string[] args)
        {

            // New abstract IView as concrete View
            IView view = new View();

            // new Model
            IFileHandler model = new SyncService(view);

            // new Controller
            // accepts a model and a view
            Controller controller = new Controller(model, view);

            // start controller
            controller.Start(args);
        }
    }
}
