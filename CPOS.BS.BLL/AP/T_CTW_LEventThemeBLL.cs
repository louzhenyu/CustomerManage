/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/2/22 9:51:44
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
    /// ҵ����  
    /// </summary>
    public partial class T_CTW_LEventThemeBLL
    {  
        /// <summary>
        /// ��ȡ������б�
        /// </summary>
        /// <returns></returns>
         public DataSet GetInSeasonThemeList()
        {
            return this._currentDAO.GetInSeasonThemeList();
        }
        /// <summary>
        /// ��ȡ�¼���б�
        /// </summary>
        /// <returns></returns>
        public DataSet GetNextSeasonThemeList()
         {
             return this._currentDAO.GetNextSeasonThemeList();

         }
        /// <summary>
        /// ��ȡap���̻���Ϣ
        /// </summary>
        /// <returns></returns>
        public DataSet GetCustomerInfo()
        {
            return this._currentDAO.GetCustomerInfo();
        }
    }
}