/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/16 16:35:19
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
    /// ��PushIOSMessage�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class PushIOSMessageDAO : Base.BaseCPOSDAO, ICRUDable<PushIOSMessageEntity>, IQueryable<PushIOSMessageEntity>
    {
        #region �г��ʹ��APP������Ϣ
        /// <summary>
        /// �г��ʹ��APP������Ϣ
        /// </summary>
        /// <param name="MarketingId">���ʶ</param>
        /// <returns></returns>
        public bool SetMarketPushApp(string MarketEventId)
        {
            SqlParameter[] pars = new SqlParameter[] { 
                new SqlParameter("@MarketEventId",MarketEventId),
                new SqlParameter("@CustomerId",this.CurrentUserInfo.CurrentLoggingManager.Customer_Id.Trim())
            };

            this.SQLHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Proc_Set_MarketEventApp", pars);
            return true;

        }
        #endregion
    }
}
