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

            if (args.Length < 3)
            {
                return;
            }

            //
            Program prog = new Program();

            prog.Run(args);
        }

        private void Run(string[] args)
        {
            // new Model
            Model model = new Model();

            // new Controller
            Controller controller = new Controller();

            // New abstract IView as concrete View
            IView view = new View();
        }
    }
}
