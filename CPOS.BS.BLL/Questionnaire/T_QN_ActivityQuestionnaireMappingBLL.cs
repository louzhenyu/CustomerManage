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
    /// ҵ������  
    /// </summary>
    public partial class T_QN_ActivityQuestionnaireMappingBLL
    {
        /// <summary>
        /// ���ݻid����ȡʵ��
        /// </summary>
        /// <param name="AID">�id</param>
        /// <returns></returns>
        public T_QN_ActivityQuestionnaireMappingEntity GetByAID(object AID)
        {
            return _currentDAO.GetByAID(AID);
        }
       
    }
}