/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/19 15:54:11
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
    /// ��R_WxO2OPanel_30Days�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class R_WxO2OPanel_30DaysDAO : Base.BaseCPOSDAO, ICRUDable<R_WxO2OPanel_30DaysEntity>, IQueryable<R_WxO2OPanel_30DaysEntity>
    {
        public R_WxO2OPanel_30DaysEntity GetEntityByDate(DateTime dateCode)
        {
            if (CurrentUserInfo == null)
            {
                return null;
            }
            string sql = "select top 1 * from R_WxO2OPanel_30Days where CustomerId=@customerId and DateCode=@dateCode";
            List<SqlParameter> pList = new List<SqlParameter>();
            pList.Add(new SqlParameter("@customerId", CurrentUserInfo.ClientID));
            pList.Add(new SqlParameter("@dateCode", dateCode));
            //��ȡ����
            R_WxO2OPanel_30DaysEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(CommandType.Text, sql.ToString(), pList.ToArray()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //����
            return m;
        }

    }
}
