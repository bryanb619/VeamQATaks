using System;

namespace SyncTask
{
    public class Controller
    {
        /// <summary>
        /// Reference to model class
        /// </summary>
        private readonly Model _model;

        /// <summary>
        /// 
        /// </summary>
        private readonly IView _view;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model">Reference to be passed for the model class
        /// </param>
        public Controller(Model model, IView view)
        {
            _model = model;
            _view = view;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public void Start(string[] args)
        {
            // local variables to hold arg values
            string folderPath = "", clonePath = "", logPath;
            int interval;

            // display welcome message
            _view.WelcomeMessage();

            // Check if there are enough arguments
            if (args.Length < 4)
            {
                // display error message to user
                _view.ErrorMesage("the ammount or args passed was not enough."
                    + $"You passed {args.Length} arguments");

                // terminate
                return;
            }

            else if (args.Length > 4)
            {
                // display error message to user
                _view.ErrorMesage("the ammount or args passed was too much."
                    + $"You passed {args.Length} arguments");

                // terminate
                return;
            }

            try
            {

                folderPath = args[0];
                clonePath = args[1];
                interval = int.Parse(args[2]);
                logPath = args[3];
            }

            catch (Exception e)
            {
                _view.ErrorMesage(e.Message);
            }


            //
            _model.replicateFolder(folderPath, clonePath);
        }

    }
}