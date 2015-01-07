/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/12/8 10:35:42
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
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��VipStore�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipStoreDAO : Base.BaseCPOSDAO, ICRUDable<VipStoreEntity>, IQueryable<VipStoreEntity>
    {
     
        /// <summary>
        /// �ҵ�С����Ϣ
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public DataSet VipStoreInfo(string userID)
        {
            string sql =string.Format(@" SELECT ( SELECT    COUNT(*)
                      FROM      vipstore
                      WHERE     vipid = '{0}'
                                AND IsDelete = 0
                    ) AS StoreCount ,
                    ( SELECT    COUNT(*)
                      FROM      vipstore
                      WHERE     vipid = '{0}'
                                AND IsDelete = 0
                                AND DATEDIFF(day, CreateTime, GETDATE()) < 7
                    ) AS RecentStoreCount ,
                    ( SELECT    COUNT(*)
                      FROM      T_Inout
                      WHERE     sales_user = '{0}'
                                AND field7!='-99'
                    ) AS OrderCount ,
                    ( SELECT    COUNT(*)
                      FROM      VipAmountDetail
                      WHERE     AmountSourceId = 10
                                AND IsDelete = 0
                                AND VipId='{0}'
                    ) AS AmountCount ,
                    ( SELECT    COUNT(*)
                      FROM      vip
                      WHERE     SetoffUserId = '{0}'
                                AND IsDelete = 0
                    ) AS SetoffUserCount ,
                    ISNULL(( SELECT z.rownum
                             FROM   ( SELECT    rownum = ROW_NUMBER() OVER ( ORDER BY ISNULL(SUM(Amount),
                                                                          0) ) ,
                                                VipId = VipId ,
                                                COUNT = ISNULL(SUM(Amount), 0)
                                      FROM      VipAmountDetail
                                      WHERE     AmountSourceId = 10
                                      GROUP BY  VipId
                                    ) Z
                             WHERE  Z.VipId = '{0}'
                           ), -1) AS Ranking", userID);

            return SQLHelper.ExecuteDataset(sql);
        }
    }
}
