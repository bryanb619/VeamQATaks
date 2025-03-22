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


        public void ErrorMesage(string errorMsg)
        {
            // display error message in console
            Console.WriteLine(errorMsg);
        }

        /// <summary>
        /// 
        /// </summary>
        public void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Sync Console APP. \n" +
            "Read and select the following options");
        }


    }


}