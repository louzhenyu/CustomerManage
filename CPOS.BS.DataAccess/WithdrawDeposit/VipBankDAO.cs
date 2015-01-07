/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014-12-28 11:40:41
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
    /// 表VipBank的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class VipBankDAO : Base.BaseCPOSDAO, ICRUDable<VipBankEntity>, IQueryable<VipBankEntity>
    {
        /// <summary>
        /// 获取银行卡列表
        /// </summary>
        /// <param name="vipID"></param>
        /// <returns></returns>
        public DataSet GetVipBankList(string vipID)
        {
            string sql =string.Format(@"
                            SELECT  
                                vb.VipBankID ,
                                vb.BankID ,
                                vb.AccountName ,
                                vb.CardNo ,
                                b.BankName ,
                                b.LogoUrl
                            FROM    VipBank vb
                                    INNER JOIN Bank b ON b.BankID = vb.BankID
                            WHERE   vb.VipID = '{0}' and vb.IsDelete=0
                        ", vipID);
            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 根据标识符获取实例[包含已删除]
        /// </summary>
        /// <param name="pID">标识符的值</param>
        public VipBankEntity GetVipBankByID(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [VipBank] where VipBankID='{0}' ", id.ToString());
            //读取数据
            VipBankEntity m = null;
            using (SqlDataReader rdr = this.SQLHelper.ExecuteReader(sql.ToString()))
            {
                while (rdr.Read())
                {
                    this.Load(rdr, out m);
                    break;
                }
            }
            //返回
            return m;
        }
    }
}
