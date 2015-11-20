/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/10/26 20:41:45
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
    /// ��ContactEvent�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class ContactEventDAO : Base.BaseCPOSDAO, ICRUDable<ContactEventEntity>, IQueryable<ContactEventEntity>
    {
        public string CreateSql()
        {
            string sql = string.Empty;
            sql = "SELECT    a.* ,ROW_NUMBER() OVER ( ORDER BY a.CreateTime DESC ) num ";
            sql += "INTO #tmp FROM (";
            sql += " SELECT  A.* , ";
            sql += " B.ContactTypeName ,";
            sql += " CASE WHEN PrizeType = 'Point' THEN '���ͻ���' + CAST(A.Integral AS VARCHAR(10))";
            sql += " WHEN PrizeType = 'Coupon' THEN '����' + c.CouponTypeName";
            sql += " WHEN PrizeType = 'Chance' THEN '���Ͳ���' +d.Title+CAST(a.ChanceCount AS NVARCHAR(10))+'��'";
            sql += " END Reward ,";
            sql += " CONVERT(NVARCHAR(10),BeginDate,120) + '��'+ CONVERT(NVARCHAR(10),EndDate,120) Date, ";
            sql += " CASE WHEN Status=1 THEN 'δ��ʼ'	WHEN Status=2 THEN '������'	WHEN Status=3 THEN '��ͣ'WHEN Status=4 THEN '����'	END StatusName,";
            sql += " t3.JoinCount";
            sql += " FROM    ContactEvent A";
            sql += " LEFT JOIN SysContactPointType B ON A.ContactTypeCode = B.ContactTypeCode ";
            sql += " LEFT JOIN dbo.CouponType c ON A.CouponTypeID = c.CouponTypeID AND C.CustomerId=A.CustomerId";
            sql += " LEFT JOIN dbo.LEvents d ON a.EventId=d.EventID AND A.CustomerId=D.CustomerId";
            sql += " LEFT JOIN (SELECT EventId,COUNT(DISTINCT VIPID) JoinCount FROM dbo.LLotteryLog	GROUP BY EventId) t3 ON a.EventID = t3.EventId";
            sql += " WHERE A.IsDelete=0 AND A.CustomerId = '" + CurrentUserInfo.ClientID + "') a";
            return sql;
        }
        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <returns></returns>
        public DataSet GetContactEventList(int pPageSize, int pCurrentPageIndex)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(CreateSql());
            sql.AppendFormat(" SELECT  *");
            sql.AppendFormat(" FROM   #tmp");
            sql.AppendFormat(" WHERE   num >{0}  AND num<{1}", pPageSize * (pCurrentPageIndex - 1), pPageSize * (pCurrentPageIndex));
            sql.AppendFormat("SELECT COUNT(1)TotalCount,(COUNT(1)/{0})+1 TotalPage FROM #tmp", pPageSize);

            var ds = this.SQLHelper.ExecuteDataset(sql.ToString());
            return ds;
        }
        /// <summary>
        /// �ı�״̬
        /// </summary>
        /// <param name="intStatus"></param>
        /// <param name="strContactEventId"></param>
        /// <returns></returns>
        public int ChangeStatus(int intStatus, string strContactEventId)
        {
            string sql = "UPDATE ContactEvent SET status=" + intStatus + " WHERE ContactEventId='" + strContactEventId + "'";
            return this.SQLHelper.ExecuteNonQuery(sql);
        }
        public int AddContactEventPrize(LPrizesEntity entity)
        {

            SqlParameter[] parameters = new SqlParameter[7] 
            { 
                new SqlParameter{ParameterName="@EventId",SqlDbType=SqlDbType.NVarChar,Value=entity.EventId},
                new SqlParameter{ParameterName="@PrizeName",SqlDbType=SqlDbType.NVarChar,Value=entity.PrizeName},
                new SqlParameter{ParameterName="@PrizeTypeId",SqlDbType=SqlDbType.NVarChar,Value=entity.PrizeTypeId},
                new SqlParameter{ParameterName="@Point",SqlDbType=SqlDbType.Int,Value=entity.Point},
                new SqlParameter{ParameterName="@CouponTypeID",SqlDbType=SqlDbType.NVarChar,Value=entity.CouponTypeID},
                new SqlParameter{ParameterName="@CountTotal",SqlDbType=SqlDbType.Int,Value=entity.CountTotal},
                new SqlParameter{ParameterName="@Creator",SqlDbType=SqlDbType.NVarChar,Value=entity.CreateBy}
            };
            return this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "[Proc_AddContactEventPrize]", parameters);
        }

        /// <summary>
        /// �����Ƿ��Ѵ���
        /// </summary>
        /// <param name="strContactType"></param>
        /// <param name="strShareEventId"></param>
        /// <returns></returns>
        public int ExistsContact(ContactEventEntity entityContact)
        {
            string strSql = string.Empty;
            if (entityContact.ShareEventId != null && entityContact.ShareEventId.Length > 0)
                strSql = string.Format("SELECT COUNT(1) FROM ContactEvent With(nolock) WHERE IsDelete=0 and [Status]=2 and CustomerId='{0}' and [ContactTypeCode] ='{1}' and ShareEventId='{2}'", CurrentUserInfo.ClientID, entityContact.ContactTypeCode, entityContact.ShareEventId);
            else
                strSql = string.Format("SELECT COUNT(1) FROM ContactEvent With(nolock) WHERE IsDelete=0 and [Status]=2  and CustomerId='{0}' and [ContactTypeCode] ='{1}' AND '{2}'>=BeginDate", CurrentUserInfo.ClientID, entityContact.ContactTypeCode, entityContact.BeginDate);
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(strSql));
        }

    }
}
