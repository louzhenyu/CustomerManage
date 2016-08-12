/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/25 14:47:15
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
    /// ��VipCardProfitRule�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class VipCardProfitRuleDAO : Base.BaseCPOSDAO, ICRUDable<VipCardProfitRuleEntity>, IQueryable<VipCardProfitRuleEntity>
    {
        /// <summary>
        /// ��ȡ��Ա�����ѳ�ֵ������� ��Ϣ {�������ݴ�������}
        /// </summary>
        /// <param name="CustomerId">�̻����</param>
        /// <returns></returns>
        public DataSet GetVipCardReRechargeProfitRuleList(string CustomerId)
        {
            string sql = @"SELECT *
                            FROM   VipCardProfitRule AS vcpr
                                   RIGHT JOIN VipCardReRechargeProfitRule AS vcrrp
                                        ON  vcpr.CardBuyToProfitRuleId = vcrrp.CardBuyToProfitRuleId
                            WHERE  vcpr.IsDelete = 0
                                   AND vcrrp.IsDelete = 0
                                   AND vcpr.CustomerID = @CustomerId
                                   AND vcrrp.CustomerID = @CustomerId
                                   ORDER BY LimitAmount ASC";
            SqlParameter[] parameter = new SqlParameter[]{
                new SqlParameter("@CustomerId",CustomerId)
            };
            return this.SQLHelper.ExecuteDataset(CommandType.Text, sql, parameter);
        }
        /// <summary>
        /// ��ȡ ĳ������ ���ѳ�ֵ��ʽ
        /// </summary>
        /// <param name="CustomerId">�̻����</param>
        /// <param name="CardTypeId">�����</param>
        /// <returns></returns>
        public string[] GetRechargeProfitRuleByIsPrepaid(string CustomerId, int ? CardTypeId)
        {
            List<string> lst = new List<string>();
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT vcrrp.ReRechargeProfitRuleId
                            FROM   VipCardReRechargeProfitRule   AS vcrrp --���ѳ�ֵ��ʽ��
       
                                   INNER JOIN VipCardProfitRule  AS vcpr --�������
                                        ON  vcrrp.CardBuyToProfitRuleId = vcpr.CardBuyToProfitRuleId
                                   INNER JOIN SysVipCardType     AS svct --��Ա����Ϣ��
                                        ON  svct.VipCardTypeID = vcpr.VipCardTypeID
                            WHERE  svct.IsDelete = 0 --δɾ��
                                                     AND vcpr.IsDelete = 0  --δɾ��
                                                     AND vcrrp.IsDelete = 0  --δɾ��
                                                     AND svct.VipCardTypeID=@VipCardTypeID  --�����
                                                     AND svct.CustomerID=@CustomerId
                                                     AND vcpr.CustomerID=@CustomerId
                                                     AND vcrrp.CustomerID=@CustomerId
                                  -- AND svct.IsPrepaid = 0 ���ܼ��� ");
            SqlParameter[] parameter = new SqlParameter[]{
                 new SqlParameter("@VipCardTypeID",CardTypeId),
                 new SqlParameter("@CustomerId",CustomerId)
            };

            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(CommandType.Text, sb.ToString(), parameter))
            {
                while (rdr.Read())
                {

                    if (rdr["ReRechargeProfitRuleId"] != DBNull.Value)
                    {
                        lst.Add(Convert.ToString(rdr["ReRechargeProfitRuleId"]));
                    }
                }
            }
            return lst.ToArray();
        }
    }
}
