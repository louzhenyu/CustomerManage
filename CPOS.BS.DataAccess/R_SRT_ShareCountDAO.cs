/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/6/3 13:49:01
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
    /// ��R_SRT_ShareCount�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class R_SRT_ShareCountDAO : Base.BaseCPOSDAO, ICRUDable<R_SRT_ShareCountEntity>, IQueryable<R_SRT_ShareCountEntity>
    {
        public List<R_SRT_ShareCountEntity> GetStatisticList()
        {

            string sql = @"SELECT * FROM [dbo].[R_SRT_ShareCount] where DateCode=(
select DISTINCT top 1  datecode from [dbo].[R_SRT_ShareCount]  order by DateCode desc) and CustomerId=@customerId";
            List<SqlParameter> pList=new List<SqlParameter>();
            pList.Add(new SqlParameter("@customerId",CurrentUserInfo.ClientID));

            //��ȡ����
            List<R_SRT_ShareCountEntity> list = new List<R_SRT_ShareCountEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(CommandType.Text, sql, pList.ToArray()))
            {
                while (rdr.Read())
                {
                    R_SRT_ShareCountEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //����
            return list;
        }
    }
}
