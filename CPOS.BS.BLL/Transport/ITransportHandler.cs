using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity.Transport;

namespace JIT.CPOS.BS.BLL.Transport
{
    public interface ITransportHandler
    {
        /// <summary>
        /// 获取物流信息
        /// </summary>
        /// <returns></returns>
        List<TransportEntity> SearchTransport();
    }
}
