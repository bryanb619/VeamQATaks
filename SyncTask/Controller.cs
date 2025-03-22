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
        /// <param name="model">Reference to be passed for the model class
        /// </param>
        public Controller(Model model)
        {
            _model = model;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start(IView view)
        {


        }

    }
}