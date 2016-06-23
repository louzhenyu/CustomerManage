/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/5/28 10:12:43
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
    /// 表T_SuperRetailTraderSkuMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_SuperRetailTraderSkuMappingDAO : BaseCPOSDAO, ICRUDable<T_SuperRetailTraderSkuMappingEntity>, IQueryable<T_SuperRetailTraderSkuMappingEntity>
    {
        public List<T_SuperRetailTraderSkuMappingEntity> GetSkuList(string itemName,string itemCategoryId,int pageSize,int pageIndex)
        {
            List<T_SuperRetailTraderSkuMappingEntity> list = new List<T_SuperRetailTraderSkuMappingEntity>();

            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            pagedSql.AppendFormat(@" select a.item_id as ItemId,a.sku_id as SkuId,a.prop_1_detail_name as PropName1,a.prop_2_detail_name as PropName2,a.SalesPrice,Case When b.SkuId is null Then 0 Else 1 End as IsSelected from vw_Sku_Detail a 
                            left join T_SuperRetailTraderSkuMapping b on a.Sku_id = b.SkuId and b.CustomerId = @CustomerId and b.IsDelete = 0
                            inner join (select ROW_NUMBER() over(order by createtime desc) as rownum,* from vw_item_detail where CustomerId = @CustomerId and status = 1 ) d on d.item_id = a.item_id
                            ");
            if (!string.IsNullOrEmpty(itemCategoryId))
            {
                pagedSql.AppendFormat(" inner join [dbo].[fnGetChildCategoryByID](@ItemCategoryId,1) c on c.CategoryID = a.item_category_id ");
            }

            pagedSql.AppendFormat(" where a.CustomerId = @CustomerId and a.status = 1 ");
            if (!string.IsNullOrEmpty(itemName))
            {
                pagedSql.AppendFormat(" and a.item_name like @ItemName ");
            }
            pagedSql.AppendFormat(" and d.rownum > {0} and d.rownum < {1}", pageSize * (pageIndex - 1), pageSize * pageIndex);
            SqlParameter[] parameters = 
            {
			    new SqlParameter("@CustomerId",SqlDbType.VarChar),
				new SqlParameter("@ItemCategoryId",SqlDbType.VarChar),
				new SqlParameter("@ItemName",SqlDbType.NVarChar),
            };
            parameters[0].Value = CurrentUserInfo.ClientID;
            parameters[1].Value = itemCategoryId;
            parameters[2].Value = "%" + itemName + "%";

            using (SqlDataReader rdr = SQLHelper.ExecuteReader(CommandType.Text, pagedSql.ToString(), parameters))
            {
                while (rdr.Read())
                {
                    T_SuperRetailTraderSkuMappingEntity m = new T_SuperRetailTraderSkuMappingEntity();
                    if (rdr["ItemId"] != DBNull.Value)
                    {
                        m.ItemId = rdr["ItemId"].ToString();
                    }
                    if (rdr["SkuId"] != DBNull.Value)
                    {
                        m.SkuId = rdr["SkuId"].ToString();
                    }
                    if (rdr["PropName1"] != DBNull.Value)
                    {
                        m.PropName1 = rdr["PropName1"].ToString();
                    }
                    if (rdr["PropName2"] != DBNull.Value)
                    {
                        m.PropName2 = rdr["PropName2"].ToString();
                    }
                    if (rdr["SalesPrice"] != DBNull.Value)
                    {
                        m.SalesPrice = Convert.ToDecimal(rdr["SalesPrice"]);
                    }
                    if (rdr["IsSelected"] != DBNull.Value)
                    {
                        m.IsSelected = Convert.ToInt32(rdr["IsSelected"]);
                    }

                    list.Add(m);
                }
            }
            return list;
        } 
    }
}
