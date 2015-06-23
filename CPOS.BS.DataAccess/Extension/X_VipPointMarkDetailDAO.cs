/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015-6-6 16:15:14
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
    /// ��X_VipPointMarkDetail�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class X_VipPointMarkDetailDAO : Base.BaseCPOSDAO, ICRUDable<X_VipPointMarkDetailEntity>, IQueryable<X_VipPointMarkDetailEntity>
    {
        /// <summary>
        /// ���ܻ�õ��������ж��Ƿ�ɼ������⣻����0��ʾ�����д��⣩
        /// </summary>
        /// <param name="vipId"></param>
        /// <param name="startWeek"></param>
        /// <param name="endWeek"></param>
        /// <returns></returns>
        public X_VipPointMarkDetailEntity GetPointMarkByWeek(string vipId, DateTime startWeek, DateTime endWeek)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [X_VipPointMarkDetail] where VipID='{0}' and CreateTime>'{1}' and CreateTime<'{2}' and Source=1 and isdelete=0 ", vipId, startWeek, endWeek);
            //��ȡ����
            X_VipPointMarkDetailEntity m = null;
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
    }
}
