using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Manager
{
    class AppChangeEventArgs : EventArgs
    {
        public AppChangeEventArgs(Application app)
        {
            TargetApp = app;
        }

        public Application TargetApp { get; private set; }
    }
}
