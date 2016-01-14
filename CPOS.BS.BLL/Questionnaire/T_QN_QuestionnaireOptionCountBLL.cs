/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/12/15 11:34:37
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
    public partial class T_QN_QuestionnaireOptionCountBLL
    {

        /// <summary>
        /// 根据选项id和活动id查询是否存在数据
        /// </summary>
        /// <param name="OptionID"></param>
        /// <param name="ActivityID"></param>
        public T_QN_QuestionnaireOptionCountEntity isExist(string OptionID, string ActivityID)
        {
            return _currentDAO.isExist(OptionID, ActivityID);
        }

        /// <summary>
        /// 根据选项id和活动id查询数据
        /// </summary>
        /// <param name="QuestionnaireID"></param>
        /// <param name="ActivityID"></param>
        public T_QN_QuestionnaireOptionCountEntity[] GetList(string QuestionnaireID, string ActivityID)
        {
            return _currentDAO.GetList(QuestionnaireID, ActivityID);
        }
    }
}