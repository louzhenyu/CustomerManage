/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014-8-25 11:44:14
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
    /// ��RechargeStrategy�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class RechargeStrategyDAO : Base.BaseCPOSDAO, ICRUDable<RechargeStrategyEntity>, IQueryable<RechargeStrategyEntity>
    {
        /// <summary>
        /// ��ȡ��������������Ʒ��Ϣ(֧���ص�ʹ��)
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataSet GetInoutOrderItems(string orderId)
        {
            string sql = "select *  from VwInoutOrderItems where OrderId='" + orderId + "'";
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql);
        }
    }
}
