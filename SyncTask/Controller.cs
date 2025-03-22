using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public void Start()
        {

            if (_model is null)
            {
                _view.ErrorMesage("Model is invalid");
            }

            // display welcome message
            _view.WelcomeMessage();

            // display options menu for cloning



        }

    }
}