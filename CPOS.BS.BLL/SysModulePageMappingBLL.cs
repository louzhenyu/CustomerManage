/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/5/21 16:06:37
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
    public partial class SysModulePageMappingBLL
    {
        /// <summary>
        /// 查询数据库表SysModulePageMapping 中是否存在该MappingId和PageId的数据 Add By changjian.tian 2014-05-26
        /// </summary>
        /// <param name="pPageKey"></param>
        /// <returns></returns>
        public DataSet GetExistsVocaVerMappingIDandPageId(string MappingId,string PageId)
        {
            return _currentDAO.GetExistsVocaVerMappingIDandPageId(MappingId,PageId);
        }

        /// <summary>
        /// 查询数据库表SysModulePageMapping 返回次序
        /// </summary>
        /// <param name="pPageKey"></param>
        /// <returns></returns>
        public object GetModulePageMappingBySequence(string MappingId)
        {
            return _currentDAO.GetModulePageMappingBySequence(MappingId);
        }
    }
}