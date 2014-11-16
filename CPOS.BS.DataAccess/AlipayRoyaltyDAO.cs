/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-08-27 15:33
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
    /// ��AlipayRoyalty�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class AlipayRoyaltyDAO : Base.BaseCPOSDAO, ICRUDable<AlipayRoyaltyEntity>, IQueryable<AlipayRoyaltyEntity>
    {
        /// <summary>
        /// �����̻���վΨһ�����Ż�ȡ������Ϣ
        /// </summary>
        /// <param name="outTradeNo">�̻���վΨһ������</param>
        public DataSet GetAlipayRoyalty(string outTradeNo)
        {
            string sql = " SELECT * FROM dbo.AlipayRoyalty "
                + " WHERE out_trade_no = '" + outTradeNo + "' AND result IS NULL ";

            //ִ�����
            return this.SQLHelper.ExecuteDataset(sql);
        }
    }
}
