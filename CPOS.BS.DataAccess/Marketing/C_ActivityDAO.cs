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
                if (string.IsNullOrWhiteSpace(strTargetGroups))
                    strTargetGroups = "ȫ��";
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
                sql.Append("select count(1) from VipCardVipMapping as vm ");
                sql.Append("inner join VipCard as vc on vm.VipCardID=vc.VipCardID and vc.VipCardStatusId=1 and vc.IsDelete=0 ");
                sql.Append("inner join vip as vp on vm.vipid=vp.vipid and vp.IsDelete=0 ");
                sql.AppendFormat("where vm.IsDelete=0 and vc.VipCardTypeID={0} ", int.Parse(VipCardTypeID));
            }
            else
            {
                sql.Append("select count(1) from VipCardVipMapping as vm ");
                sql.Append("inner join VipCard as vc on vm.VipCardID=vc.VipCardID and vc.VipCardStatusId=1 and vc.IsDelete=0 ");
                sql.Append("inner join vip as vp on vm.vipid=vp.vipid and vp.IsDelete=0 ");
                sql.Append("where vm.IsDelete=0 ");
            }

            sql.AppendFormat("and vm.customerid='{0}' ", CurrentUserInfo.ClientID);
            //if (!string.IsNullOrWhiteSpace(ActivityID))
            //{
            //    sql.Append("select count(*) from VipCard where VipCardStatusId=5 or VipCardStatusId=1 and IsDelete=0 ");
            //    sql.Append("and VipCardTypeID=(select t.ObjectID from C_Activity as c left join C_TargetGroup as t on c.ActivityID=t.ActivityID ");
            //    sql.AppendFormat("where c.ActivityID='{0}')",ActivityID);
            //}

            count = Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql.ToString()));


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
                ", activityId, prizesId);
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


        #region ����Ӫ�����ȯ����Ϣҵ��
        /// <summary>
        /// ��ȡ�����̻���Ϣ
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetALLCustomerInfo(string sql)
        {
            DataSet ds = new DataSet();
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        /// <summary>
        /// ������ȯҵ��
        /// </summary>
        /// <param name="MinCouponNum"></param>
        /// <param name="ActivityID"></param>
        /// <param name="ActivityType"></param>
        /// <param name="CouponTypeID"></param>
        /// <param name="VipCardTypeID"></param>
        /// <returns></returns>
        public int BatchAddSendCoupon(int MinCouponNum, string ActivityID, int ActivityType, string CouponTypeID, int VipCardTypeID)
        {
            int Count = 0;
            //�Ƿ�������Ӫ���
            bool IsBirthdayMarketing = false;
            if (ActivityType == 1)
                IsBirthdayMarketing = true;


            var parm = new SqlParameter[7];
            parm[0] = new SqlParameter("@Num", System.Data.SqlDbType.Int) { Value = MinCouponNum };
            parm[1] = new SqlParameter("@IsBirthdayMarketing", System.Data.SqlDbType.Bit) { Value = IsBirthdayMarketing };
            parm[2] = new SqlParameter("@ActivityID", System.Data.SqlDbType.UniqueIdentifier) { Value = new Guid(ActivityID) };
            parm[3] = new SqlParameter("@CouponTypeID", System.Data.SqlDbType.NVarChar) { Value = CouponTypeID };
            parm[4] = new SqlParameter("@VipCardTypeID", System.Data.SqlDbType.Int) { Value = VipCardTypeID };
            parm[5] = new SqlParameter("@UserID", System.Data.SqlDbType.NVarChar) { Value = CurrentUserInfo.UserID };
            parm[6] = new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar) { Value = CurrentUserInfo.ClientID };

            var Result = this.SQLHelper.ExecuteScalar(CommandType.StoredProcedure, "BatchAddSendCoupon", parm);
            if (Result != null)
                Count = Convert.ToInt32(Result);

            return Count;
        }
        /// <summary>
        /// �������ɻ��Ϣҵ��
        /// </summary>
        /// <param name="ResultConunt"></param>
        /// <param name="ActivityID"></param>
        /// <param name="VipCardTypeID"></param>
        /// <param name="MessageID"></param>
        /// <param name="MessageType"></param>
        /// <param name="Content"></param>
        /// <param name="SendTime"></param>
        /// <param name="StrCouponName"></param>
        /// <param name="ActivityType"></param>
        public void BatchAddMessageSend(int ResultConunt, string ActivityID, int VipCardTypeID, string MessageID, string MessageType, string Content, DateTime SendTime, int ActivityType)
        {
            //�Ƿ�������Ӫ���
            bool IsBirthdayMarketing = false;
            if (ActivityType == 1)
                IsBirthdayMarketing = true;


            var parm = new SqlParameter[10];
            parm[0] = new SqlParameter("@Num", System.Data.SqlDbType.Int) { Value = ResultConunt };
            parm[1] = new SqlParameter("@IsBirthdayMarketing", System.Data.SqlDbType.Bit) { Value = IsBirthdayMarketing };
            parm[2] = new SqlParameter("@ActivityID", System.Data.SqlDbType.UniqueIdentifier) { Value = new Guid(ActivityID) };
            parm[3] = new SqlParameter("@VipCardTypeID", System.Data.SqlDbType.Int) { Value = VipCardTypeID };
            parm[4] = new SqlParameter("@MessageID", System.Data.SqlDbType.UniqueIdentifier) { Value = new Guid(MessageID) };
            parm[5] = new SqlParameter("@MessageType", System.Data.SqlDbType.NVarChar) { Value = MessageType };
            parm[6] = new SqlParameter("@Content", System.Data.SqlDbType.NVarChar) { Value = Content };
            parm[7] = new SqlParameter("@SendTime", System.Data.SqlDbType.DateTime) { Value = SendTime };
            parm[8] = new SqlParameter("@UserID", System.Data.SqlDbType.NVarChar) { Value = CurrentUserInfo.UserID };
            parm[9] = new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar) { Value = CurrentUserInfo.ClientID };

            var Result = this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "BatchAddMessageSend", parm);

        }
        /// <summary>
        /// ���������������Ϣ
        /// </summary>
        /// <param name="ResultConunt"></param>
        /// <param name="ActivityID"></param>
        /// <param name="VipCardTypeID"></param>
        /// <param name="ActivityType"></param>
        public void BatchAddPrizeReceive(int ResultConunt, string ActivityID, int VipCardTypeID, int ActivityType)
        {
            //�Ƿ�������Ӫ���
            bool IsBirthdayMarketing = false;
            if (ActivityType == 1)
                IsBirthdayMarketing = true;

            var parm = new SqlParameter[6];
            parm[0] = new SqlParameter("@Num", System.Data.SqlDbType.Int) { Value = ResultConunt };
            parm[1] = new SqlParameter("@IsBirthdayMarketing", System.Data.SqlDbType.Bit) { Value = IsBirthdayMarketing };
            parm[2] = new SqlParameter("@ActivityID", System.Data.SqlDbType.UniqueIdentifier) { Value = new Guid(ActivityID) };
            parm[3] = new SqlParameter("@VipCardTypeID", System.Data.SqlDbType.Int) { Value = VipCardTypeID };
            parm[4] = new SqlParameter("@UserID", System.Data.SqlDbType.NVarChar) { Value = CurrentUserInfo.UserID };
            parm[5] = new SqlParameter("@CustomerID", System.Data.SqlDbType.NVarChar) { Value = CurrentUserInfo.ClientID };

            var Result = this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "BatchAddPrizeReceive", parm);
        }
        #endregion
		/// <summary>
        /// ��ȡ��Ա��Ϣ
        /// </summary>
        /// <param name="vipCardTypeID"></param>
        /// <returns></returns>
        public DataSet GetAllVipInfoList(string vipCardTypeID)
        {
            StringBuilder Str = new StringBuilder();
            Str.Append("SELECT top 10 m.VipID,vp.VipCode,vp.WeiXinUserId,vp.Phone,vp.Email FROM VipCardVipMapping m ");
            Str.Append("INNER JOIN VipCard vc ON vc.VipCardID = m.VipCardID and vc.IsDelete=0 ");
            if (!string.IsNullOrWhiteSpace(vipCardTypeID))
                Str.AppendFormat("and vc.VipCardTypeID={0} ", vipCardTypeID);
            Str.Append("INNER JOIN Vip as vp on m.VIPID=vp.VIPID and vp.IsDelete=0 where m.IsDelete=0 ");

            return this.SQLHelper.ExecuteDataset(Str.ToString());
        }
    }
}
