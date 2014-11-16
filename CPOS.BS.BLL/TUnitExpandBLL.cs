/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/7/17 10:52:37
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class TUnitExpandBLL
    {
        /// <summary>
        /// 获取最新的订单号
        /// </summary>
        /// <param name="loggingSesssionInfo">登录信息</param>
        /// <param name="unitId">门店ID</param>
        /// <param name="count">订单号增加数量</param>
        /// <returns>返回最新的订单号</returns>
        public int GetOrderNo(LoggingSessionInfo loggingSesssionInfo, string unitId, int count)
        {
            TUnitExpandBLL service = new TUnitExpandBLL(loggingSesssionInfo);
            var unitExpandEntity = new TUnitExpandEntity() { UnitId = unitId };
            var unitExpandEntitys = this.QueryByEntity(unitExpandEntity, null);

            int orderNo = 0;    //订单号
            if (unitExpandEntitys != null && unitExpandEntitys.Length > 0)
            {
                //更新
                orderNo = Convert.ToInt32(unitExpandEntitys.FirstOrDefault().OrderNo) + 1;
                unitExpandEntity.OrderNo = Convert.ToInt32(unitExpandEntitys.FirstOrDefault().OrderNo) + count;

                service.Update(unitExpandEntity, false);
            }
            else
            {
                //新增
                orderNo = 1;
                unitExpandEntity.OrderNo = count;

                service.Create(unitExpandEntity);
            }
            return orderNo;
        }

        #region 
        /// <summary>
        /// 获取当前单据号
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public string GetNowOrderNo(LoggingSessionInfo loggingSessionInfo, string unitId)
        {
            int count = GetOrderNo(loggingSessionInfo, unitId, 1);
            string orderNo = string.Empty;
            switch (count.ToString().Length) { 
                case 1:
                    orderNo = "000" + count.ToString();
                    break;
                case 2:
                    orderNo = "00" + count.ToString();
                    break;
                case 3:
                    orderNo = "0" + count.ToString();
                    break;
                case 4:
                    orderNo = count.ToString();
                    break;
                default:
                    orderNo = "";
                    break;
            }
            return orderNo;
        }
        /// <summary>
        /// 获取门店订单号码 Jermyn20130906
        /// </summary>
        /// <param name="loggingSessionInfo"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public string GetUnitOrderNo(LoggingSessionInfo loggingSessionInfo, string unitId)
        {
            string OrderNo = GetNowOrderNo(loggingSessionInfo, unitId);
            UnitService unitServer = new UnitService(loggingSessionInfo);
            UnitInfo unitInfo = new UnitInfo();
            unitInfo = unitServer.GetUnitById(unitId);
            if (unitInfo != null && unitInfo.Code != null && !unitInfo.Code.Equals("")) {
                OrderNo = unitInfo.Code + OrderNo;
            }
            return OrderNo;
        }
        #endregion
    }
}