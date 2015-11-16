/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/6/20 11:22:27
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
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��SysVipCardType�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class SysVipCardTypeDAO : Base.BaseCPOSDAO, ICRUDable<SysVipCardTypeEntity>, IQueryable<SysVipCardTypeEntity>
    {
        /// <summary>
        /// ��ȡ��Ա���б�
        /// </summary>
        /// <param name="pWhereConditions">ɸѡ����</param>
        /// <param name="pOrderBys">����</param>
        /// <returns></returns>
        public SysVipCardTypeEntity[] GetVipCardList(IWhereCondition[] pWhereConditions, OrderBy[] pOrderBys)
        {
            //��֯SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(" select vct.*,vcr.CardDiscount,vcr.RuleID,vcr.PointsMultiple,vcr.ChargeFull,vcr.ChargeGive from [SysVipCardType] vct ");
            sql.AppendFormat(" left join [VipCardRule] vcr on vcr.VipCardTypeID=vct.VipCardTypeID ");

            sql.AppendFormat(" where 1=1  and vct.isdelete=0 ");
            if (pWhereConditions != null)
            {
                foreach (var item in pWhereConditions)
                {
                    sql.AppendFormat(" and {0}", item.GetExpression());
                }
            }
            if (pOrderBys != null && pOrderBys.Length > 0)
            {
                sql.AppendFormat(" order by ");
                foreach (var item in pOrderBys)
                {
                    sql.AppendFormat(" {0} {1},", item.FieldName, item.Direction == OrderByDirections.Asc ? "asc" : "desc");
                }
                sql.Remove(sql.Length - 1, 1);
            }
            //ִ��SQL
            List<SysVipCardTypeEntity> list = new List<SysVipCardTypeEntity>();
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    SysVipCardTypeEntity m;
                    this.Load(rdr, out m);

                    if (rdr["RuleID"] != DBNull.Value)
                    {
                        m.RuleID = Convert.ToInt32(rdr["VipCardTypeID"]);
                    }
                    if (rdr["CardDiscount"] != DBNull.Value)
                    {
                        m.CardDiscount = Convert.ToDecimal(rdr["CardDiscount"]);
                    }
                    if (rdr["PointsMultiple"] != DBNull.Value)
                    {
                        m.PointsMultiple = Convert.ToInt32(rdr["PointsMultiple"]);
                    }
                    if (rdr["ChargeFull"] != DBNull.Value)
                    {
                        m.ChargeFull = Convert.ToDecimal(rdr["ChargeFull"]);
                    }
                    if (rdr["ChargeGive"] != DBNull.Value)
                    {
                        m.ChargeGive = Convert.ToDecimal(rdr["ChargeGive"]);
                    }

                    list.Add(m);
                }
            }
            //���ؽ��
            return list.ToArray();
        }
        
    }
}
