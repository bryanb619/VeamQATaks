using System;


namespace SyncTask
{
    public class View : IView
    {

        /// <summary>
        /// Method to be used when displaying messages without enter in the end
        /// for user in console
        /// </summary>
        /// <param name="msg">
        /// String with message content to be displayed to user</param>
        public void LineMessage(string msg)
        {
            // display message in console
            Console.Write(msg);
        }


        /// <summary>
        /// Method to be used when displaying messages for user in console
        /// </summary>
        /// <param name="msg">
        /// String with message content to be displayed to user</param>
        public void Message(string msg)
        {
            // display message in console
            Console.WriteLine(msg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="color"></param>
        public void Message(string msg, ConsoleColor color)
        {
            // set text color to given color
            Console.ForegroundColor = color;

            Console.WriteLine(msg);

            // reset text color to default
            Console.ResetColor();
        }


        public void ErrorMesage(string errorMsg)
        {
            // set text color to red
            Console.ForegroundColor = ConsoleColor.Red;

            // display error message in console
            Console.WriteLine(errorMsg + "\n");

            // reset text color to default
            Console.ResetColor();

            // display final message to user
            Console.WriteLine("Program will now terminate...\n\n");
        }

        /// <summary>
        /// 
        /// </summary>
        public void WelcomeMessage()
        {
            // set text color to green
            Console.ForegroundColor = ConsoleColor.Green;

            // display welcome message to user
            Console.WriteLine("\nWelcome to Sync Console APP!\n");

            // reset text color to default
            Console.ResetColor();

            // display message to user
            Console.WriteLine(
                "Pres enter to pause!"
                + "\nConsult the README file placed in the root of the project"
                + " for further information.\n\n");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>returns state of key press in bool form</returns>
        public bool GetEscKey()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    return true;
                }

                else return false;
            }

            else return false;
        }
    }
}