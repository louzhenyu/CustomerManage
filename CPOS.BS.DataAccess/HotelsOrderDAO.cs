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
    /// ���ݷ��ʣ�  
    /// ��HotelsOrder�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class HotelsOrderDAO : Base.BaseCPOSDAO, ICRUDable<HotelsOrderEntity>, IQueryable<HotelsOrderEntity>
    {
        /// <summary>
        /// �ҵġ��Ƶ궩���б� Add by changjian.tian 2014-7-22
        /// </summary>
        /// <param name="pVipId">��ԱID</param>
        /// <param name="DataRange">ʱ�䷶Χ��1.�������µĶ�����2.������ǰ�Ķ�������</param>
        /// <returns></returns>
        public DataSet GetMyHotelsOrderList(string pVipId,int pDataRange)
        {
            StringBuilder sbSQL = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            if (pDataRange==1)  //�������µĶ���
                sbWhere.Append(" and L.ReservationsTime>DATEADD(MONTH,-3,GETDATE())");
            if (pDataRange == 2) //������ǰ�Ķ���
                sbWhere.Append(" and L.ReservationsTime<DATEADD(MONTH,-3,GETDATE())");
            sbSQL.Append("SELECT L.OrderId,L.ReservationsVipId, R.UnitName,(case when L.OrderStatus='100' then '�ѳɽ�' end) as OrderStatus,RetailTotalAmount,R.Address, L.BeginDate,L.EndDate,L.CheckDaysQty,L.RoomQty,(case when  S.IsBreakfast=1 then '�󴲷�' end) IsBreakfast,(case when S.IsKing=1 then '����' end) as IsKing FROM HotelsOrder L,VwHotelUnit R ,VwHotelRoomSku S,HotelsOrderDetail H WHERE L.UnitId=R.UnitID AND L.OrderId=H.OrderId AND S.RoomID=H.RoomId ");
            sbSQL.Append(string.Format(" and L.ReservationsVipId='{0}' "+sbWhere.ToString()+" ",pVipId));
            return SQLHelper.ExecuteDataset(sbSQL.ToString());
        }

        /// <summary>
        /// 
        /// �ҵġ��Ƶ꡿�����б���ϸ
        /// </summary>
        /// <param name="pVipId">��ԱID</param>
        /// <param name="pOrderId">����ID</param>
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
