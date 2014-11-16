/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/30 16:00:39
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
    public partial class MarketTemplateBLL
    {
        #region Jermyn+20130531 ��ȡģ���������
        /// <summary>
        /// ��ȡģ���������
        /// </summary>
        /// <param name="templateType"></param>
        /// <returns></returns>
        public IList<MarketTemplateEntity> GetTemplateListByType(string templateType)
        {
            IList<MarketTemplateEntity> list = new List<MarketTemplateEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetTemplateListByType(templateType);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<MarketTemplateEntity>(ds.Tables[0]);
            }

            return list;
        }
        #endregion
    }
}