/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/4 16:23:21
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
    /// ��TCustomerWeiXinMapping�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class TCustomerWeiXinMappingDAO : Base.BaseCPOSDAO, ICRUDable<TCustomerWeiXinMappingEntity>, IQueryable<TCustomerWeiXinMappingEntity>
    {
        public string GetCustomerIdByAppId(string appId)
        {
            var sql =
                string.Format(
                    "select CustomerId from cpos_ap..TCustomerWeiXinMapping where AppId = '{0}' and IsDelete = 0 ",
                    appId);
            var result = this.SQLHelper.ExecuteScalar(sql.ToString());

            if (result == null || result.ToString() == "" || string.IsNullOrEmpty(result.ToString()))
            {
                return "";
            }
            else
            {
                return result.ToString();
            }
        }
    }
}
