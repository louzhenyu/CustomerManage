/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/10/25 14:38:12
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
    public partial class LNewsTagBLL
    {
        #region �������Ż�ȡ���ż���
        public IList<LNewsTagEntity> GetNewsTagsList(string NewsId)
        {
            IList<LNewsTagEntity> list = new List<LNewsTagEntity>();
            DataSet ds = new DataSet();
            ds = _currentDAO.GetNewsTagsList(NewsId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                list = DataTableToObject.ConvertToList<LNewsTagEntity>(ds.Tables[0]);
            }
            return list;
        }
        #endregion
    }
}