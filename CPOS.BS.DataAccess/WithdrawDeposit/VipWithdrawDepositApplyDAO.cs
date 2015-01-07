/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014-12-28 11:40:41
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
    /// ��VipWithdrawDepositApply�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipWithdrawDepositApplyDAO : Base.BaseCPOSDAO, ICRUDable<VipWithdrawDepositApplyEntity>, IQueryable<VipWithdrawDepositApplyEntity>
    {
        /// <summary>
        /// ��ȡ����������
        /// </summary>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public VipWithdrawDepositApplyEntity[] GetVipWDApplyByToday(string vipId)
        {
            //��֯SQL
            string sql =string.Format(@"
            SELECT [ApplyID]
              ,[VipID]
              ,[WithdrawNo]
              ,[Amount]
              ,[Status]
              ,[ApplyDate]
              ,[ConfirmDate]
              ,[VipBankID]
              ,[CustomerID]
              ,[CreateTime]
              ,[CreateBy]
              ,[LastUpdateTime]
              ,[LastUpdateBy]
              ,[IsDelete]
          FROM [cpos_bs_alading].[dbo].[VipWithdrawDepositApply]
          WHERE VipID='{0}' AND  CONVERT(varchar(100),ApplyDate,23)=CONVERT(varchar(100),GETDATE(),23) AND IsDelete=0
            ",vipId);
            //ִ��SQL
            List<VipWithdrawDepositApplyEntity> list = new List<VipWithdrawDepositApplyEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    VipWithdrawDepositApplyEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //���ؽ��
            return list.ToArray();
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this.SQLHelper.CreateTransaction();
        }
    }
}
