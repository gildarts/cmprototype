using System;
using System.Collections.Generic;
using System.Text;

namespace FISCA.DSAUtil
{
    public interface IRenewableToken : ISecurityToken
    {
        void RenewToken();
    }
}
