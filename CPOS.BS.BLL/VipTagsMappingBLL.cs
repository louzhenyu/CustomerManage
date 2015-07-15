/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/12/5 11:09:10
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
    /// ҵ������  
    /// </summary>
    public partial class VipTagsMappingBLL
    {  
        public IList<TagsEntity> GetList(VipTagsMappingEntity qEntity)
        {
           DataSet ds =_currentDAO.GetList(qEntity);
           return DataTableToObject.ConvertToList<TagsEntity>(ds.Tables[0]);
        }

        public bool DeleteByVipID(string itemID)
        {
            return _currentDAO.DeleteByVipID(itemID);
        }
    }
}