/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/8/13 9:26:57
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
    public partial class WMaterialVoiceBLL
    {
        #region Web�б��ȡ
        /// <summary>
        /// Web�б��ȡ
        /// </summary>
        public IList<WMaterialVoiceEntity> GetWebList(WMaterialVoiceEntity entity, int Page, int PageSize)
        {
            if (PageSize <= 0) PageSize = 15;

            IList<WMaterialVoiceEntity> list = new List<WMaterialVoiceEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetWebList(entity, Page, PageSize);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<WMaterialVoiceEntity>(ds.Tables[0]);
            }
            return list;
        }
        /// <summary>
        /// �б�������ȡ
        /// </summary>
        public int GetWebListCount(WMaterialVoiceEntity entity)
        {
            return _currentDAO.GetWebListCount(entity);
        }
        #endregion
    }
}