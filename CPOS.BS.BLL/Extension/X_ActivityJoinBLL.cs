/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-6-18 14:44:14
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
    public partial class X_ActivityJoinBLL
    {  
        /// <summary>
        /// ��ȡ���ܳ齱��¼���ж��Ƿ����Ƿ�齱��
        /// </summary>
        /// <param name="vipId"></param>
        /// <param name="startWeekTime"></param>
        /// <param name="endWeekTime"></param>
        /// <returns></returns>
        public X_ActivityJoinEntity GetActivityJoinByWeek(string vipId, DateTime startWeekTime, DateTime endWeekTime)
        {
            return this._currentDAO.GetActivityJoinByWeek(vipId, startWeekTime, endWeekTime);
        }
           /// <summary>
        /// ��ȡ���ܽ�Ʒ�����д���
        /// </summary>
        /// <param name="prizesId"></param>
        /// <param name="startWeekTime"></param>
        /// <param name="endWeekTime"></param>
        /// <returns></returns>
        public int GetPrizesCountByWeek(Guid prizesId, DateTime startWeekTime, DateTime endWeekTime)
        {
            return this._currentDAO.GetPrizesCountByWeek(prizesId, startWeekTime, endWeekTime);
        }
    }
}