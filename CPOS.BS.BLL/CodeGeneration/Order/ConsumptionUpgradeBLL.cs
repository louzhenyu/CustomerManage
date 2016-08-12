using JIT.CPOS.BS.BLL.RedisOperationBLL.Order;
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
    /// 消费升级
    /// </summary>
    public class ConsumptionUpgradeBLL
    {
        private LoggingSessionInfo _loggingSessionInfo;
        private T_InoutDAO _inoutDAO;

        public ConsumptionUpgradeBLL(LoggingSessionInfo loggingSessionInfo)
        {
            _loggingSessionInfo = loggingSessionInfo;
            _inoutDAO = new T_InoutDAO(loggingSessionInfo);
        }

        /// <summary>
        /// 消费升级(如果每个方法都有相同验证，建议抽出)
        /// </summary>
        /// <param name="orderId">订单Id</param>
        public void UpgradeConsumption(string orderId)
        {
            if (_loggingSessionInfo == null) 
            {
                throw new APIException("登录信息不合法");
            }
            if (_loggingSessionInfo.CurrentUser == null || string.IsNullOrWhiteSpace(_loggingSessionInfo.CurrentUser.customer_id)) 
            {
                throw new APIException("登录信息不合法");
            }
            var entity= _inoutDAO.GetByID(orderId);
            if (entity == null) 
            {
                throw new APIException("订单不存在");
            }
            RedisCalculateVipConsumeForUpgrade redisCalculateVipConsumeForUpgrade = new RedisCalculateVipConsumeForUpgrade();
            redisCalculateVipConsumeForUpgrade.SetVipConsumeForUpgradeList(_loggingSessionInfo, entity);
        }
    }
}

