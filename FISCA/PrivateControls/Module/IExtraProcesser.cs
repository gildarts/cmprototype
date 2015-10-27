using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.PrivateControls
{
    internal interface IExtraProcesser
    {
        ExtraInformation[] Process(object instance);
    }
}
