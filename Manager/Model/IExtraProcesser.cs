using System;
using System.Collections.Generic;
using System.Text;

namespace Manager
{
    internal interface IExtraProcesser
    {
        ExtraInformation[] Process(object instance);
    }
}
