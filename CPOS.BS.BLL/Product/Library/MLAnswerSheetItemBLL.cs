/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/8/5 9:34:34
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
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class MLAnswerSheetItemBLL
    {
        #region ��ȡ�û������б�
        /// <summary>
        /// ��ȡ�û������б�
        /// </summary>
        /// <param name="pAnswerSheetId"></param>
        /// <param name="pUserID"></param>
        /// <returns></returns>
        public DataTable GetAnswerSheetItem(string pAnswerSheetId, string pUserID)
        {
            return this._currentDAO.GetAnswerSheetItem(pAnswerSheetId, pUserID);
        }
        #endregion
    }
}