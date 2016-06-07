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
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class SetoffEventBLL
    {
        /// <summary>
        /// 事务
        /// </summary>
        /// <returns></returns>
        public SqlTransaction GetTran()
        {
            return this._currentDAO.GetTran();
        }
        /// <summary>
        /// 设置集客行动状态为失效状态
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="customerId">商户ID</param>
        public void SetFailStatus(int Type,string customerId)
        {
            this._currentDAO.SetFailStatus(Type,customerId);
        }
        /// <summary>
        /// 判断添加集客工具是否重复
        /// </summary>
        /// <param name="SetoffEventID"></param>
        /// <param name="ObjectId"></param>
        /// <returns></returns>
        public bool IsToolsRepeat(string SetoffEventID,string ObjectId)
        {
            return this._currentDAO.IsToolsRepeat(SetoffEventID, ObjectId);
        }

    }
}