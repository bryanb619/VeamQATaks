using System;

namespace SyncTask
{
    /// <summary>
    /// Interface for the view of the application.
    /// Defines all the methods that the view should implement. 
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Displays the welcome message
        /// </summary>
        void WelcomeMessage();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        void LineMessage(string msg);

        /// <summary>
        /// Displays a message
        /// </summary>
        /// <param name="msg">text to be displayed</param>
        void Message(string msg);

        /// <summary>
        /// Displays a message with a specific color
        /// </summary>
        /// <param name="msg">text to be displayed</param>
        /// <param name="color">color of text</param>
        void Message(string msg, ConsoleColor color);

        /// <summary>
        /// Displays a error message.
        /// Color of text is red by default
        /// </summary>
        /// <param name="errorMsg">text of error</param>
        void ErrorMesage(string errorMsg);

        /// <summary>
        /// Method to get the user input.
        /// Waits for the user to press enter returning true if so.
        /// Else returns false even if the user presses any key.  
        /// </summary>
        /// <returns>returns the state of a defined key press</returns>
        bool GetEscKey();
    }
}