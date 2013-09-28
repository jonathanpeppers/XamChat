using System;
using System.Collections.Generic;
using System.Text;

namespace XamChat.Core
{
    public class BaseViewModel
    {
        public event EventHandler IsBusyChanged = delegate { };

        private bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                IsBusyChanged(this, EventArgs.Empty);
            }
        }
    }
}

