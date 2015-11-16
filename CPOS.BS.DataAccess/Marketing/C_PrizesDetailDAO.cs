/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/9/9 17:12:31
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
    /// 数据访问：  
    /// 表C_PrizesDetail的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class C_PrizesDetailDAO : Base.BaseCPOSDAO, ICRUDable<C_PrizesDetailEntity>, IQueryable<C_PrizesDetailEntity>
    {
        /// <summary>
        /// 根据奖品ID获取奖品明细集合
        /// </summary>
        /// <param name="PrizesID"></param>
        /// <returns></returns>
        public List<C_PrizesDetailEntity> GetPrizesDetailList(string PrizesID)
        {
            List<C_PrizesDetailEntity> List = new List<C_PrizesDetailEntity>();

            if (!string.IsNullOrWhiteSpace(PrizesID))
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select a.*,b.CouponTypeName,b.EndTime,b.IssuedQty,b.IsVoucher,b.CouponTypeDesc from C_PrizesDetail as a ");
                sql.Append("left join CouponType as b on a.CouponTypeID=b.CouponTypeID and b.IsDelete=0 ");
                sql.AppendFormat("where a.IsDelete=0 and a.PrizesID='{0}'", PrizesID);

                using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
                {
                    while (rdr.Read())
                    {
                        C_PrizesDetailEntity m;
                        this.Load(rdr, out m);
                        //
                        if (rdr["CouponTypeName"] != DBNull.Value)
                        {
                            m.CouponTypeName = Convert.ToString(rdr["CouponTypeName"]);
                        }
                        if (rdr["EndTime"] != DBNull.Value)
                        {
                            m.EndTime = Convert.ToDateTime(rdr["EndTime"]);
                        }
                        if (rdr["IssuedQty"] != DBNull.Value)
                        {
                            m.IssuedQty = Convert.ToInt32(rdr["IssuedQty"]);
                        }
                        if (rdr["IsVoucher"] != DBNull.Value)
                        {
                            m.IsVoucher = Convert.ToInt32(rdr["IsVoucher"]);
                        }
                        if (rdr["CouponTypeDesc"] != DBNull.Value)
                        {
                            m.CouponTypeDesc = Convert.ToString(rdr["CouponTypeDesc"]);
                        }
                        List.Add(m);
                    }
                }
            }

            return List;
        }
    }
}
