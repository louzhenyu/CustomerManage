/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/12/15 11:34:36
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
    public partial class T_QN_QuestionnaireAnswerRecordBLL
    {

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="AID">活动id</param>
        /// <param name="QNID">问卷id</param>
        /// <returns></returns>
        public T_QN_QuestionnaireAnswerRecordEntity[] GetModelList(object AID, object QNID)
        {
            return _currentDAO.GetModelList(AID,QNID);
        }


        /// <summary>
        /// 获取所有用户数据
        /// </summary>
        /// <param name="AID">活动id</param>
        /// <param name="QNID">问卷id</param>
        /// <returns></returns>
        public string[] GetUserModelList(object AID, object QNID)
        {
            return _currentDAO.GetUserModelList(AID, QNID);
        }

        /// <summary>
        /// 按照每个用户答题标识批量删除
        /// </summary>
        /// <param name="vipIDs">用户答题标识数组</param>
        /// <param name="pTran">事务实例,可为null,如果为null,则不使用事务来更新</param>
        public void DeletevipIDs(object[] vipIDs)
        {
            _currentDAO.DeletevipIDs(vipIDs, null);
            
        }

    }
}