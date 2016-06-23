/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/31 14:12:49
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
    public partial class R_SRT_HomeBLL
    {
        public DataSet GetSevenDaySalesAndPersonCount()
        {
            return this._currentDAO.GetSevenDaySalesAndPersonCount();
        }
        /// <summary>
        /// 近30天新增分销商 这个数据 最近一天的数据
        /// </summary>
        /// <returns></returns>
        public R_SRT_HomeEntity GetNearest1DayEntity() {
            return this._currentDAO.GetNearest1DayEntity();
        }
    }
}