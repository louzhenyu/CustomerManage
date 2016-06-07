/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/16 13:59:39
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
    /// 表SetoffEvent的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class SetoffEventDAO : Base.BaseCPOSDAO, ICRUDable<SetoffEventEntity>, IQueryable<SetoffEventEntity>
    {
        /// <summary>
        /// 设置集客行动状态为失效状态
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="customerId">商户ID</param>
        public void SetFailStatus(int Type,string customerId)
        {
            string sql = string.Format("update SetoffEvent set Status='90' where SetoffType={0} and customerID={1}", Type,customerId);
            this.SQLHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        /// <summary>
        /// 判断添加集客工具是否重复
        /// </summary>
        /// <param name="SetoffEventID"></param>
        /// <param name="ObjectId"></param>
        /// <returns></returns>
        public bool IsToolsRepeat(string SetoffEventID, string ObjectId)
        {
            bool flag = false;

            string sql = string.Format("select * from SetoffTools where SetoffEventID='{0}' and ObjectId='{1}' and Status='10' and IsDelete=0", SetoffEventID, ObjectId);

            var Result = this.SQLHelper.ExecuteScalar(sql);
            if (Result != null)
                flag = true;

            return flag;
        }
    }
}
