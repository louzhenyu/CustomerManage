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
using System.Data;
using System.Data.SqlClient;
using System.Text;

using JIT.Utility;
using JIT.Utility.Entity;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.Log;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��X_ActivityJoin�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class X_ActivityJoinDAO : Base.BaseCPOSDAO, ICRUDable<X_ActivityJoinEntity>, IQueryable<X_ActivityJoinEntity>
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
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [X_ActivityJoin] where VipID='{0}' and CreateTime>'{1}' and CreateTime<'{2}' and isdelete=0 ", vipId, startWeekTime, endWeekTime);
            //��ȡ����
            X_ActivityJoinEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            return m;
        }
        /// <summary>
        /// ��ȡ���ܽ�Ʒ�����д���
        /// </summary>
        /// <param name="prizesId"></param>
        /// <param name="startWeekTime"></param>
        /// <param name="endWeekTime"></param>
        /// <returns></returns>
        public int GetPrizesCountByWeek(Guid prizesId,DateTime startWeekTime,DateTime endWeekTime)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select count(*) from [X_ActivityJoin] where PrizesID='{0}' and CreateTime>'{1}' and CreateTime<'{2}' and isdelete=0 ", prizesId, startWeekTime, endWeekTime);
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql.ToString()));
        }

    }
}
