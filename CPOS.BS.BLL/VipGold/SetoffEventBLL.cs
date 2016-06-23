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
using JIT.CPOS.DTO.Module.SuperRetailTraderProfit.RTExtend.Response;

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
        public void SetFailStatus(int Type, string customerId)
        {
            this._currentDAO.SetFailStatus(Type, customerId);
        }
        /// <summary>
        /// 判断添加集客工具是否重复
        /// </summary>
        /// <param name="SetoffEventID"></param>
        /// <param name="ObjectId"></param>
        /// <returns></returns>
        public bool IsToolsRepeat(string SetoffEventID, string ObjectId)
        {
            return this._currentDAO.IsToolsRepeat(SetoffEventID, ObjectId);
        }
        ///// <summary>
        ///// 得到扩展工具
        ///// </summary>
        ///// <returns></returns>
        //public GetExtendListRD GetSetoffToolList()
        //{
        //    //超级分销主数据
        //    var dbResult1 = QueryByEntity(new SetoffEventEntity() { Status = "10", SetoffType = 3, CustomerId = CurrentUserInfo.ClientID }, null).FirstOrDefault();
        //    if (dbResult1 == null)
        //        return null;
        //    var result = new GetExtendListRD();
        //    result.SetoffEventID = dbResult1.SetoffEventID;
        //    result.List = new List<GetExtendInfo>();
        //    var dbResult2 = _currentDAO.GetExtendList(dbResult1.SetoffEventID);
        //    if (dbResult2 != null)
        //        result.List = DataTableToObject.ConvertToList<GetExtendInfo>(dbResult2.Tables[0]);
        //    foreach (var m in result.List)
        //    {
        //        var tmpD1 = new DateTime();
        //        if (DateTime.TryParse(m.BeginData, out tmpD1))
        //        {
        //            m.BeginData = Convert.ToDateTime(m.BeginData).ToString("yyyy-MM-dd");
        //        }
        //        if (DateTime.TryParse(m.EndData, out tmpD1))
        //        {
        //            m.EndData = Convert.ToDateTime(m.EndData).ToString("yyyy-MM-dd");
        //        }
        //    }
        //    return result;
        //}
        /// <summary>
        /// 分销扩展工具数据集
        /// </summary>
        /// <param name="setoffEventID"></param>
        /// <returns></returns>
        public DataSet GetSetoffToolList(Guid? setoffEventID)
        {
            return _currentDAO.GetSetoffToolList(setoffEventID);
        }

    }
}