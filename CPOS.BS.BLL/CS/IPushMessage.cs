using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL.CS
{
    public interface IPushMessage
    {
        void PushMessage(string memberID, string messageContent);
    }
}
