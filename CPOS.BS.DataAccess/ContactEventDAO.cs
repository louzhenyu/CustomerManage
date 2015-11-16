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
        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <returns></returns>
        public DataSet GetContactEventList()
        {
            string sql = string.Empty;
            sql += " SELECT  A.ContactEventId , ";
            sql += " A.ContactEventName , ";
            sql += " B.ContactTypeName ,";
            sql += " CASE WHEN RewardType = 1 THEN '���ͻ���' + CAST(A.Integral AS VARCHAR(10))";
            sql += " WHEN RewardType = 2 THEN '����' + c.CouponTypeName";
            sql += " WHEN RewardType = 3 THEN '���Ͳ���' +d.Title+CAST(a.ChanceCount AS NVARCHAR(10))+'��'";
            sql += " END Reward ,";
            sql += " RewardType, ";
            sql += " CAST(A.BeginDate AS NVARCHAR(30)) + '-'+ CAST(A.EndDate AS NVARCHAR(30)) Data, ";
            sql += " status";
            sql += " FROM    ContactEvent A";
            sql += " LEFT JOIN SysContactPointType B ON A.ContactTypeCode = B.ContactTypeCode ";
            sql += " LEFT JOIN dbo.CouponType c ON A.CouponTypeID = c.CouponTypeID AND C.CustomerId=A.CustomerId";
            sql += " LEFT JOIN dbo.LEvents d ON a.EventId=d.EventID AND A.CustomerId=D.CustomerId";
            sql += " WHERE A.IsDelete=0 AND A.CustomerId = '" + CurrentUserInfo.ClientID + "' ORDER BY A.CreateTime DESC";

            var ds = this.SQLHelper.ExecuteDataset(sql);
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
        
    }
}
