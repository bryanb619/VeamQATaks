using System;

namespace SyncTask
{
    /// <summary>
    /// Class defines root of C# program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args">Set of arguments to be passed in console</param>
        private static void Main(string[] args)
        {
            // new Model
            Model model = new Model();

            // New abstract IView as concrete View
            IView view = new View();

            // new Controller
            // accepts a model and a view
            Controller controller = new Controller(model, view);

            // start controller
            controller.Start(args);
        }
    }
}
