using JIT.CPOS.BS.BLL.RedisOperationBLL.OrderReward;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL.CodeGeneration.Order
{
    /// <summary>
    /// 订单奖励
    /// </summary>
    public class OrderRewardBLL
    {
        private LoggingSessionInfo _loggingSessionInfo;
        private T_InoutDAO _inoutDAO;


        public OrderRewardBLL(LoggingSessionInfo loggingSessionInfo)
        {
            _loggingSessionInfo = loggingSessionInfo;
            _inoutDAO = new T_InoutDAO(loggingSessionInfo);
        }
        /// <summary>
        /// 订单奖励
        /// </summary>
        /// <param name="orderId"></param>
        public void RewardOrder(string orderId)
        {
            if (_loggingSessionInfo == null)
            {
                throw new APIException("登录信息不合法");
            }
            if (_loggingSessionInfo.CurrentUser == null || string.IsNullOrWhiteSpace(_loggingSessionInfo.CurrentUser.customer_id))
            {
                throw new APIException("登录信息不合法");
            }
            var entity = _inoutDAO.GetByID(orderId);
            if (entity == null)
            {
                throw new APIException("订单不存在");
            }
            new SendOrderRewardMsgBLL().OrderReward(entity, _loggingSessionInfo, null);
        }
    }
}
