using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.DSAUtil
{
    public class StateChangeEventArgs : EventArgs
    {
        private DSConnectionState _state;

        public StateChangeEventArgs(DSConnectionState newState)
        {
            _state = newState;
        }

        public DSConnectionState NewState
        {
            get { return _state; }
        }
    }
}
