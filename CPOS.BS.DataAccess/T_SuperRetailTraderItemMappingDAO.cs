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
using JIT.CPOS.BS.DataAccess.Utility;
using JIT.Utility.Log;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;


namespace JIT.CPOS.BS.DataAccess
{
    
    /// <summary>
    /// 数据访问：  
    /// 表T_SuperRetailTraderItemMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_SuperRetailTraderItemMappingDAO : BaseCPOSDAO, ICRUDable<T_SuperRetailTraderItemMappingEntity>, IQueryable<T_SuperRetailTraderItemMappingEntity>
    {
        public PagedQueryResult<T_SuperRetailTraderItemMappingEntity> GetItemList(string itemName, string itemCategoryId,int pageSize,int pageIndex)
        {
            List<T_SuperRetailTraderItemMappingEntity> list = new List<T_SuperRetailTraderItemMappingEntity>();

            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //分页
            pagedSql.AppendFormat(@" select a.item_id as ItemId,a.item_name as ItemName,a.item_code as ItemCode ,a.rownum as DisplayIndex,Case When b.ItemId is null Then 0 Else 1 End as IsSelected from (select ROW_NUMBER() over(order by createtime desc) as rownum,* from vw_item_detail where CustomerId = @CustomerId and status = 1 ) a 
                            left join T_SuperRetailTraderItemMapping b on a.item_id = b.ItemId 
                            and b.CustomerId = @CustomerId and b.IsDelete = 0 " );
            if (!string.IsNullOrEmpty(itemCategoryId))
            {
                pagedSql.AppendFormat(" inner join [dbo].[fnGetChildCategoryByID](@ItemCategoryId,1) c on c.CategoryID = a.item_category_id ");
            }

            pagedSql.AppendFormat(@" where a.rownum > {0} and a.rownum < {1}", pageSize * (pageIndex - 1), pageSize * pageIndex);
            if (!string.IsNullOrEmpty(itemName))
            {
                pagedSql.AppendFormat(" and a.item_name like @ItemName ");
            }

            

            //记录总条数
            totalCountSql.AppendFormat(@" select Count(1) from (select ROW_NUMBER() over(order by createtime desc) as rownum,* from vw_item_detail where CustomerId = @CustomerId and status = 1) a 
                            left join T_SuperRetailTraderItemMapping b on a.item_id = b.ItemId 
                            and b.CustomerId = @CustomerId and b.IsDelete = 0 ");
            if (!string.IsNullOrEmpty(itemCategoryId))
            {
                totalCountSql.AppendFormat(" inner join [dbo].[fnGetChildCategoryByID](@ItemCategoryId,1) c on c.CategoryID = a.item_category_id ");
            }

            if (!string.IsNullOrEmpty(itemName))
            {
                totalCountSql.AppendFormat(" where a.item_name like @ItemName ");
            }

            SqlParameter[] parameters = 
            {
			    new SqlParameter("@CustomerId",SqlDbType.VarChar),
				new SqlParameter("@ItemCategoryId",SqlDbType.VarChar),
				new SqlParameter("@ItemName",SqlDbType.NVarChar),
            };
            parameters[0].Value = CurrentUserInfo.ClientID;
            parameters[1].Value = itemCategoryId;
            parameters[2].Value = "%" + itemName + "%";

            //执行sql
            PagedQueryResult<T_SuperRetailTraderItemMappingEntity> result = new PagedQueryResult<T_SuperRetailTraderItemMappingEntity>();
            using (SqlDataReader rdr = SQLHelper.ExecuteReader(CommandType.Text, pagedSql.ToString(), parameters))
            {
                 while (rdr.Read())
                {
                    T_SuperRetailTraderItemMappingEntity m = new T_SuperRetailTraderItemMappingEntity();
                    if (rdr["ItemId"] != DBNull.Value)
		            {
                        m.ItemId = rdr["ItemId"].ToString();
		            }
                    if(rdr["ItemName"] != DBNull.Value)
                    {
                        m.ItemName = rdr["ItemName"].ToString();
                    }
                    if(rdr["ItemCode"] != DBNull.Value)
                    {
                        m.ItemCode = rdr["ItemCode"].ToString();
                    }
                    if(rdr["DisplayIndex"] != DBNull.Value)
                    {
                        m.DisplayIndex = Convert.ToInt32(rdr["DisplayIndex"]);
                    }
                    if(rdr["IsSelected"] != DBNull.Value)
                    {
                        m.IsSelected = Convert.ToInt32(rdr["IsSelected"]);
                    }

                    list.Add(m);
                }
            }
            result.Entities = list.ToArray();
            int totalCount = Convert.ToInt32(this.SQLHelper.ExecuteScalar(CommandType.Text, totalCountSql.ToString(), parameters));    //计算总行数
            result.RowCount = totalCount;
            int remainder = 0;
            result.PageCount = Math.DivRem(totalCount, pageSize, out remainder);
            if (remainder > 0)
                result.PageCount++;
            return result;
        } 

    }
}
