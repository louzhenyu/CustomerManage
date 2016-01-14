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
using JIT.CPOS.BS.DataAccess;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class T_QN_QuestionBLL
    {


       

        /// <summary>
        /// 根据问卷id获取数据集合
        /// </summary>
        /// <param name="QuestionnaireID">问卷id</param>
        /// <returns></returns>
        public T_QN_QuestionEntity[] getList(string QuestionnaireID)
        {
            return _currentDAO.getList(QuestionnaireID);
        }

        /// <summary>
        /// 根据问卷id获取单选和复选数据集合
        /// </summary>
        /// <param name="QuestionnaireID">问卷id</param>
        /// <returns></returns>
        public T_QN_QuestionEntity[] getOptionQuestionList(string QuestionnaireID)
        {
            return _currentDAO.getOptionQuestionList(QuestionnaireID);
        }


        public void Update(T_QN_QuestionEntity pEntity, bool pIsUpdateNullField)
        {
            _currentDAO.Update(pEntity, null, pIsUpdateNullField);
        }
    }
}