/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/22 17:09:07
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
    /// ��R_Withdraw_Home�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class R_Withdraw_HomeDAO : Base.BaseCPOSDAO, ICRUDable<R_Withdraw_HomeEntity>, IQueryable<R_Withdraw_HomeEntity>
    {
        /// <summary>
        /// �����̻������ ��Ա���� ��ȡ���һ��ͳ����Ϣ
        /// </summary>
        /// <param name="CustomerId">�̻�����</param>
        /// <param name="VipTypeId">1=��Ա 2=Ա�� 3=�ɷ����� 4=����������</param>
        /// <returns></returns>
        public R_Withdraw_HomeEntity GetTopListByCustomer(string CustomerId, int VipTypeId)
        {
            string sql = @"  SELECT TOP 1 * FROM R_Withdraw_Home WHERE VipType=@VipTypeId  AND CustomerId=@customerId ORDER BY DateCode desc  ";
            List<SqlParameter> pList = new List<SqlParameter>();
            pList.Add(new SqlParameter("@customerId", CustomerId));
            pList.Add(new SqlParameter("@VipTypeId", VipTypeId));
            //ִ��SQL
            List<R_Withdraw_HomeEntity> list = new List<R_Withdraw_HomeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(CommandType.Text, sql, pList.ToArray()))
            {
                while (rdr.Read())
                {
                    R_Withdraw_HomeEntity m;
                    this.Load(rdr, out m);
                    return m;
                }
            }
            //���ؽ��
            return new R_Withdraw_HomeEntity();
        }
    }
}
