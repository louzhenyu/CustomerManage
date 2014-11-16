/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/22 20:06:22
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
    /// ��EclubSetUp�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class EclubSetUpDAO : Base.BaseCPOSDAO, ICRUDable<EclubSetUpEntity>, IQueryable<EclubSetUpEntity>
    {
        /// <summary>
        /// ��ȡ�ͻ����ù���,����CustomerId��ѯ
        /// </summary>
        /// <param name="customerId">�ͻ�ID</param>
        /// <returns></returns>
        public DataSet GetSetUpListByCustomerId(string customerId)
        {
            //Create SQL Text
            StringBuilder sb = new StringBuilder();
            sb.Append("select SetUpID, Name, Code, Value, Sequence, CustomerId, CreateBy, CreateTime, LastUpdateBy, LastUpdateTime, IsDelete ");
            sb.Append("from EclubSetUp ");
            sb.AppendFormat("where CustomerId='{0}';", customerId);

            //Return Select Result
            return this.SQLHelper.ExecuteDataset(sb.ToString());
        }
    }
}
