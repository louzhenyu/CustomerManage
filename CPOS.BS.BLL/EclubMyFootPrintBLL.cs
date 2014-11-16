/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/6/5 17:14:24
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
    /// 业务处理：  
    /// </summary>
    public partial class EclubMyFootPrintBLL
    {  
        /// <summary>
        /// 记录足迹
        /// </summary>
        /// <param name="pageCode">页编号</param>
        /// <param name="userID">用户ID</param>
        /// <param name="objectID">浏览对象ID</param>
        /// <param name="footType">0，其他，1.资讯,  2.视频 ,3.活动, 4.课程, 5.校友</param>
        /// <param name="operationType">1.查询，2.修改，3.新增,4.删除,5登陆，6收藏</param>
        public void RecordSpoorInfo(string pageCode, string userID, string objectID, int footType, int operationType)
        {
            _currentDAO.RecordSpoorInfo(pageCode, userID, objectID, footType, operationType);
        }
    }
}