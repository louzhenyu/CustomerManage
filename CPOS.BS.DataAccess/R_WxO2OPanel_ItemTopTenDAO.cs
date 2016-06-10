/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/19 18:30:11
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
    /// 数据访问：  
    /// 表R_WxO2OPanel_ItemTopTen的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class R_WxO2OPanel_ItemTopTenDAO : Base.BaseCPOSDAO, ICRUDable<R_WxO2OPanel_ItemTopTenEntity>, IQueryable<R_WxO2OPanel_ItemTopTenEntity>
    {
        public List<R_WxO2OPanel_ItemTopTenEntity> GetListByDate()
        {
            if (CurrentUserInfo == null)
            {
                return null;
            }
            //string sql = "select * from R_WxO2OPanel_ItemTopTen where CustomerId=@customerId and DateCode=@dateCode order by sortIndex";
            string sql = "select * from R_WxO2OPanel_ItemTopTen where DateCode=(select DISTINCT top 1 DateCode from R_WxO2OPanel_ItemTopTen  order by DateCode desc) and CustomerId=@customerId order by sortIndex";
            List<SqlParameter> pList = new List<SqlParameter>();
            pList.Add(new SqlParameter("@customerId", CurrentUserInfo.ClientID));

            List<R_WxO2OPanel_ItemTopTenEntity> list = new List<R_WxO2OPanel_ItemTopTenEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(CommandType.Text, sql.ToString(), pList.ToArray()))
            {
                while (rdr.Read())
                {
                    R_WxO2OPanel_ItemTopTenEntity m;
                    this.Load(rdr, out m);
                    list.Add(m);
                }
            }
            //返回
            return list;
        }
    }
}
