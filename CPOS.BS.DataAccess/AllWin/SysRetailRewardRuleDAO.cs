/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/6/1 16:12:03
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
    /// ���ݷ��ʣ� ��Ϊ�����˫���� 
    /// ��SysRetailRewardRule�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class SysRetailRewardRuleDAO : Base.BaseCPOSDAO, ICRUDable<SysRetailRewardRuleEntity>, IQueryable<SysRetailRewardRuleEntity>
    {
          public void UpdateSysRetailRewardRule(int IsTemplate, string CooperateType, string RetailTraderID, string CustomerID)
        {
            List<SqlParameter> ls = new List<SqlParameter>();
            ls.Add(new SqlParameter("@CustomerId", CustomerID));
            ls.Add(new SqlParameter("@EndTime", DateTime.Now));

            string sql = @"update SysRetailRewardRule set isdelete=1,status='0',endTime=@EndTime where customerid=@CustomerId";
            ls.Add(new SqlParameter("@IsTemplate", IsTemplate));
            sql += " and IsTemplate=@IsTemplate";

            if (IsTemplate == 1)//�����ģ��
            {
                ls.Add(new SqlParameter("@CooperateType", CooperateType));
                sql += " and CooperateType=@CooperateType";
            }
            else {
                ls.Add(new SqlParameter("@RetailTraderID", RetailTraderID));  //����Ƿ����̣�������֮ǰ�õ�ʲô�������򶼸�ɾ����
                sql += " and RetailTraderID=@RetailTraderID";
            }

      
            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql, ls.ToArray());    //����������
           
        }
    }
}
