using System;

namespace SyncTask
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            //
            Program prog = new Program();

            //
            prog.Run(args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        private void Run(string[] args)
        {
            // new Model
            Model model = new Model();

            // New abstract IView as concrete View
            IView view = new View();

            // new Controller
            Controller controller = new Controller(model, view);

            // start controller
            controller.Start();
        }
    }
}
