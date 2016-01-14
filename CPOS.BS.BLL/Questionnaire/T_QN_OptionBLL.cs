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
    public partial class T_QN_OptionBLL
    {

        public void Update(T_QN_OptionEntity pEntity, bool pIsUpdateNullField)
        {
            _currentDAO.Update(pEntity, null, pIsUpdateNullField);
        }

        /// <summary>
        /// 获取所有题目的选项集合
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <returns></returns>
        public T_QN_OptionEntity[] GetListByQuestionID(string QuestionID)
        {
            return _currentDAO.GetListByQuestionID(QuestionID);
        }
    }
}