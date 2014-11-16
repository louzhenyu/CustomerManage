/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/21 18:19:53
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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表HotelsOrder的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class HotelsOrderDAO : Base.BaseCPOSDAO, ICRUDable<HotelsOrderEntity>, IQueryable<HotelsOrderEntity>
    {
        /// <summary>
        /// 我的【酒店订单列表】 Add by changjian.tian 2014-7-22
        /// </summary>
        /// <param name="pVipId">会员ID</param>
        /// <param name="DataRange">时间范围【1.近三个月的订单，2.三个月前的订单。】</param>
        /// <returns></returns>
        public DataSet GetMyHotelsOrderList(string pVipId,int pDataRange)
        {
            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            if (pDataRange==1)  //近三个月的订单
                sbWhere.Append(" and L.ReservationsTime>DATEADD(MONTH,-3,GETDATE())");
            if (pDataRange == 2) //三个月前的订单
                sbWhere.Append(" and L.ReservationsTime<DATEADD(MONTH,-3,GETDATE())");
            sbSQL.Append("SELECT L.OrderId,L.ReservationsVipId, R.UnitName,(case when L.OrderStatus='100' then '已成交' end) as OrderStatus,RetailTotalAmount,R.Address, L.BeginDate,L.EndDate,L.CheckDaysQty,L.RoomQty,(case when  S.IsBreakfast=1 then '大床房' end) IsBreakfast,(case when S.IsKing=1 then '含早' end) as IsKing FROM HotelsOrder L,VwHotelUnit R ,VwHotelRoomSku S,HotelsOrderDetail H WHERE L.UnitId=R.UnitID AND L.OrderId=H.OrderId AND S.RoomID=H.RoomId ");
            sbSQL.Append(string.Format(" and L.ReservationsVipId='{0}' "+sbWhere.ToString()+" ",pVipId));
            return SQLHelper.ExecuteDataset(sbSQL.ToString());
        }

        /// <summary>
        /// 
        /// 我的【酒店】订单列表明细
        /// </summary>
        /// <param name="pVipId">会员ID</param>
        /// <param name="pOrderId">订单ID</param>
        /// <returns></returns>
        public DataSet GetMyHotelsOrderListDetails(string pVipId, string pOrderId)
        {
            StringBuilder sbSQL = new StringBuilder();

            sbSQL.Append(" select * from  VwGetHotelsOrder where 1=1 ");
            sbSQL.Append(string.Format(" and Orderid='{0}' ", pOrderId));
            return SQLHelper.ExecuteDataset(sbSQL.ToString());
        }
    }
}
