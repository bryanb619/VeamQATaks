using System;

namespace SyncTask
{
    /// <summary>
    /// Controls logic of program while interacting with the model and view classes.
    /// Checks if the arguments passed are valid and then starts the program.
    /// </summary>
    public class Controller
    {
        /// <summary>
        /// Reference to model class
        /// </summary>
        private readonly IFileHandler _fileHandler;

        /// <summary>
        /// Instance variable to reference view class
        /// </summary>
        private readonly IView _view;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="view"></param>
        public Controller(IFileHandler fileHandler, IView view)
        {
            // set the model class
            _fileHandler = fileHandler;

            // set the view class
            _view = view;
        }

        /// <summary>
        /// Starts the program by checking if the arguments passed are valid.
        /// </summary>
        /// <param name="args">Arguments passed in the console</param>
        public void Start(string[] args)
        {
            // local variables to hold arg values
            string folderPath = "", clonePath = "", logPath = "";
            int interval = 0;

            // display welcome message
            _view.WelcomeMessage();

            // Check if there are enough arguments
            if (args.Length < 4)
            {
                // display error message to user
                _view.ErrorMesage(
                    "the ammount of arguments passed was not enough."
                    + $"You've passed {args.Length} arguments");

                // terminate
                return;
            }

            else if (args.Length > 4)
            {
                // display error message to user
                _view.ErrorMesage(
                    "the ammount or arguments passed was too much."
                    + $"You've passed {args.Length} arguments");

                // terminate
                return;
            }

            try
            {
                // set the values of the arguments

                // source folder path
                folderPath = args[0];

                // destination folder path
                clonePath = args[1];

                // interval of sync task
                interval = int.Parse(args[2]);

                // log file path
                logPath = args[3];

                // check if interval is a positive number
                if (interval < 0)
                {
                    _view.ErrorMesage("\nInterval must be a positive number!\n"
                        + $"You've passed {interval} as interval of sync time...");
                    return;
                }


            }

            catch (Exception e)
            {
                _view.ErrorMesage(e.Message);
            }

            // while loop
            do
            {
                _view.Message("\n\nPress ENTER to exit\n\n", ConsoleColor.Gray);

                // Clone folder
                _fileHandler.CloneFolder(folderPath, clonePath, logPath);

                // loop iwth interval
                for (int i = 0; i < interval; i++)
                {
                    // display timer to user
                    _view.Message($"{interval - i}");

                    // wait for 1 second
                    System.Threading.Thread.Sleep(1000);
                }

            }
            // check if user pressed escape key (enter key)
            // if so, terminate the loop
            while (!_view.GetEscKey());

            // display final message to user
            _view.Message("\n\nThanks for using Sync Task!");
        }

    }
}