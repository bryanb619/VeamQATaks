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
            Console.ForegroundColor = color;

            Console.WriteLine(msg);

            Console.ResetColor();
        }


        public void ErrorMesage(string errorMsg)
        {

            Console.ForegroundColor = ConsoleColor.Red;

            // display error message in console
            Console.WriteLine(errorMsg + "\n");

            Console.ResetColor();

            Console.WriteLine("Program will now terminate...\n\n");
        }

        /// <summary>
        /// 
        /// </summary>
        public void WelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("\nWelcome to Sync Console APP!\n");

            Console.ResetColor();

            Console.WriteLine("Consult the README file placed in the root of the project\n"
                + "for further information.\n\n");
        }

        public ConsoleKey GetKey() => Console.ReadKey().Key;
    }


}