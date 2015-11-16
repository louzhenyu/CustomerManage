/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/9/9 17:12:31
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
    /// ��C_Activity�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class C_ActivityDAO : Base.BaseCPOSDAO, ICRUDable<C_ActivityEntity>, IQueryable<C_ActivityEntity>
    {
        /// <summary>
        /// ���ݻID��ȡĿ��Ⱥ������
        /// </summary>
        /// <param name="ActivityID"></param>
        /// <returns></returns>
        public string GetTargetGroups(string ActivityID)
        {
            string strTargetGroups = "";
            if (!string.IsNullOrWhiteSpace(ActivityID))
            {
                string sql = string.Format("select b.VipCardTypeName from C_TargetGroup as a left join SysVipCardType as b on a.ObjectID=b.VipCardTypeID where a.ActivityID='{0}'", ActivityID);
                strTargetGroups = Convert.ToString(this.SQLHelper.ExecuteScalar(sql));

            }
            return strTargetGroups;
        }
        /// <summary>
        /// ������ȡ��ȡ�ֿ�����
        /// </summary>
        /// <returns></returns>
        public int GetholderCardCount(string VipCardTypeID, string ActivityID)
        {
            int count = 0;
            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(VipCardTypeID))
            {
                sql.AppendFormat("select count(*) from VipCard as a where IsDelete=0 and VipCardStatusId=1  and VipCardTypeID={0}", Convert.ToInt32(VipCardTypeID));
            }
            else {
                sql.Append("select count(*) from VipCard as a where IsDelete=0 and VipCardStatusId=1");
            }
            //if (!string.IsNullOrWhiteSpace(ActivityID))
            //{
            //    sql.Append("select count(*) from VipCard where VipCardStatusId=5 or VipCardStatusId=1 and IsDelete=0 ");
            //    sql.Append("and VipCardTypeID=(select t.ObjectID from C_Activity as c left join C_TargetGroup as t on c.ActivityID=t.ActivityID ");
            //    sql.AppendFormat("where c.ActivityID='{0}')",ActivityID);
            //}

            count= Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql.ToString()));


            return count;
        }
        /// <summary>
        /// ˵������ȡ�δ�ͽ�Ʒ�Ļ�Ա��Ϣ(�Ŀ��Ⱥ�������л�Ա���û�)
        /// ʹ�ã���ʱ��ȯʹ��
        /// </summary>
        /// <param name="activityId">�ID</param>
        /// <param name="prizesId">��ƷID</param>
        /// <returns></returns>
        public DataSet GetSendPrizeVipID(Guid activityId, Guid prizesId)
        {
            string sql = string.Format(@"
                    SELECT  m.VipID
                    FROM    VipCardVipMapping m
                            INNER JOIN VipCard vc ON vc.VipCardID = m.VipCardID
                            LEFT JOIN C_PrizeReceive pr ON pr.VipID = m.VipID
                            LEFT JOIN C_Activity a ON a.ActivityID = pr.ActivityID
                            LEFT JOIN C_Prizes p ON p.PrizesID = pr.PrizesID
                    WHERE   m.IsDelete = 0
                            AND vc.IsDelete = 0
                            AND vc.VipCardStatusId = 1
                            AND ( pr.VipID IS NULL
                                  OR ( pr.vipID IS NOT NULL
                                       AND a.ActivityID = '{0}'
                                       AND p.PrizesID = '{1}'
                                     )
                                )
                    GROUP BY m.VipID
                ",activityId,prizesId);
            return this.SQLHelper.ExecuteDataset(sql);
        }
        /// <summary>
        /// ˵������ȡ�����δ�ͽ�Ʒ�Ļ�Ա��Ϣ���Ŀ��Ⱥ����ĳ��������û���
        /// ʹ�ã���ʱ��ȯʹ��
        /// </summary>
        /// <param name="activityId">�ID</param>
        /// <param name="prizesId">��ƷID</param>
        /// <param name="VipCardTypeID">������ID</param>
        /// <returns></returns>
        public DataSet GetSendPrizeVipID(Guid activityId, Guid prizesId, string vipCardTypeID)
        {
            string sql = string.Format(@"
                    SELECT  m.VipID
                    FROM    VipCardVipMapping m
                            INNER JOIN VipCard vc ON vc.VipCardID = m.VipCardID
                            LEFT JOIN C_PrizeReceive pr ON pr.VipID = m.VipID
                            LEFT JOIN C_Activity a ON a.ActivityID = pr.ActivityID
                            LEFT JOIN C_Prizes p ON p.PrizesID = pr.PrizesID
                            LEFT JOIN C_TargetGroup tg ON tg.ObjectID = vc.VipCardTypeID
                                                          AND tg.GroupType = 1
                    WHERE   m.IsDelete = 0
                            AND vc.IsDelete = 0
                            AND vc.VipCardStatusId = 1
                            AND ( pr.VipID IS NULL
                                  OR ( pr.vipID IS NOT NULL
                                       AND a.ActivityID = '{0}'
                                       AND p.PrizesID = '{1}'
                                     )
                                )
                            AND tg.ObjectID = {2}
                    GROUP BY m.VipID
                ", activityId, prizesId, vipCardTypeID);
            return this.SQLHelper.ExecuteDataset(sql);
        }
        
    }
}
