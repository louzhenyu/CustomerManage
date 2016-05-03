/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/23 10:45:49
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

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class T_CTW_LEventInteractionBLL
    {
        /// <summary>
        /// 获取促销活动的开始时间和结束时间
        /// </summary>
        /// <param name="strCTWEventId"></param>
        /// <returns></returns>
        public DataSet GetPanicbuyingEventDate(string strCTWEventId)
        {
            return this._currentDAO.GetPanicbuyingEventDate(strCTWEventId);
        }
        /// <summary>
        /// 获取相关主题下的所有促销活动id
        /// </summary>
        /// <param name="strCTWEventId"></param>
        /// <returns></returns>
        public DataSet GetPanicbuyingEventId(string strCTWEventId)
        {
            return this._currentDAO.GetPanicbuyingEventId(strCTWEventId);
        }
        public void DeleteByCTWEventID(string strCTWEventId)
        {
            this._currentDAO.DeleteByCTWEventID(strCTWEventId);
        }
        public DataSet GetCTWLEventInteraction(string strObjectId)
        {
            return this._currentDAO.GetCTWLEventInteraction(strObjectId);

        }
    }
}