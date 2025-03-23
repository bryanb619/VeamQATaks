namespace SyncTask
{
    public interface IView
    {
        /// <summary>
        /// 
        /// </summary>
        void WelcomeMessage();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        void LineMessage(string msg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        void Message(string msg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMsg"></param>
        void ErrorMesage(string errorMsg);

    }
}