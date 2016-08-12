/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/3/19 11:39:44
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
    public partial class T_CTW_LEventTemplateBLL
    {
        public DataSet GetTemplateList(string strActivityGroupCode)
        {
            return this._currentDAO.GetTemplateList(strActivityGroupCode);
        }

        /// <summary>
        /// 获取创意仓库主题列表信息（包括主题列表，计划列表，Banner列表）
        /// </summary>
        /// <returns></returns>
        public int UpdateTemplateInfo(string pTemplateId, int pType)
        {
            return this._currentDAO.UpdateTemplateInfo(pTemplateId,pType);
        }
    }     
}