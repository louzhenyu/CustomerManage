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
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="itemCategoryId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public PagedQueryResult<T_SuperRetailTraderItemMappingEntity> GetItemList(string itemName, string itemCategoryId,int pageSize,int pageIndex)
        {
            List<T_SuperRetailTraderItemMappingEntity> list = new List<T_SuperRetailTraderItemMappingEntity>();

            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();
            //分页
            pagedSql.AppendFormat(@" select *,x.rownum as DisplayIndex from ( 
                                        select distinct dense_rank() over(order by y.createtime desc) as rownum,* from  (
                                        select 
                                        a.item_id as ItemId,
                                        a.item_name as ItemName,
                                        a.item_code as ItemCode,
	                                    a.create_time as CreateTime
                                        from  vw_item_detail a  ");
            pagedSql.AppendFormat(@"  left join vw_sku_detail b on a.item_id = b.item_id and b.CustomerId = @CustomerId and b.status = 1 ");
            pagedSql.AppendFormat(@"  left join T_SuperRetailTraderSkuMapping d on b.sku_id = d.SkuId and d.CustomerId = @CustomerId and d.IsDelete = 0");
            if (!string.IsNullOrEmpty(itemCategoryId))
            {
                pagedSql.AppendFormat(" inner join [dbo].[fnGetChildCategoryByID](@ItemCategoryId,1) c on c.CategoryID = a.item_category_id ");
            }
            pagedSql.AppendFormat(@" where a.CustomerId = @CustomerId and a.status = 1 and d.SkuId is null and a.item_category_id <> '-1' ");
            if (!string.IsNullOrEmpty(itemName))
            {
                pagedSql.AppendFormat(" and a.item_name like @ItemName ");
            }
            pagedSql.AppendFormat(@" group by a.item_id,a.item_name,a.item_code,a.create_time) y ) x where x.rownum > {0} and x.rownum <= {1}", pageSize * (pageIndex - 1), pageSize * pageIndex);


            //记录总条数
            totalCountSql.AppendFormat(@"  select Count(1) from( 
                                           select a.item_Id
                                           from  vw_item_detail a  ");

            totalCountSql.AppendFormat(@"  left join vw_sku_detail b on a.item_id = b.item_id and b.CustomerId = @CustomerId and b.status = 1 ");
            totalCountSql.AppendFormat(@"  left join T_SuperRetailTraderSkuMapping d on b.sku_id = d.SkuId and d.CustomerId = @CustomerId and d.IsDelete = 0"); ;
            if (!string.IsNullOrEmpty(itemCategoryId))
            {
                totalCountSql.AppendFormat(" inner join [dbo].[fnGetChildCategoryByID](@ItemCategoryId,1) c on c.CategoryID = a.item_category_id ");
            }
            totalCountSql.AppendFormat(@" where a.CustomerId = @CustomerId and a.status = 1 and d.SkuId is null and a.item_category_id <> '-1' ");
            if (!string.IsNullOrEmpty(itemName))
            {
                totalCountSql.AppendFormat(" and a.item_name like @ItemName ");
            }
            totalCountSql.AppendFormat(@" group by a.item_id ) x");

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
        /// <summary>
        /// 获取分销商商品列表
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="itemCategoryId"></param>
        /// <param name="status"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public PagedQueryResult<T_SuperRetailTraderItemMappingEntity> GetSuperRetailTraderItemList(string itemName, string itemCategoryId, int status, int pageSize, int pageIndex)
        {
            List<T_SuperRetailTraderItemMappingEntity> list = new List<T_SuperRetailTraderItemMappingEntity>();

            StringBuilder pagedSql = new StringBuilder();
            StringBuilder totalCountSql = new StringBuilder();

            //分页
            pagedSql.AppendFormat(@" select * from (
                                    select distinct dense_rank() over(order by b.createtime desc) as rownum,
                                    a.*,
                                    b.item_name as ItemName,
                                    b.imageUrl as ImageUrl 
                                    from T_SuperRetailTraderItemMapping a 
                                    inner join vw_item_Detail b on a.ItemId = b.item_id and b.status = 1");
            if (!string.IsNullOrEmpty(itemCategoryId))
            {
                pagedSql.AppendFormat(" inner join [dbo].[fnGetChildCategoryByID](@ItemCategoryId,1) c on c.CategoryID = b.item_category_id ");
            }
            pagedSql.AppendFormat(@" where a.CustomerId = @CustomerId and a.IsDelete = 0 ");
            if (!string.IsNullOrEmpty(itemName))
            {
                pagedSql.AppendFormat(" and b.item_name like @ItemName ");
            }
            if (status == 10 || status == 90)
            {
                pagedSql.AppendFormat(" and a.Status = @Status");
            }
            pagedSql.AppendFormat(@" ) x where x.rownum > {0} and x.rownum <= {1}", pageSize * (pageIndex - 1), pageSize * pageIndex);

            //计算行数
            totalCountSql.AppendFormat(@" select Count(1)
                                    from T_SuperRetailTraderItemMapping a 
                                    inner join vw_item_Detail b on a.ItemId = b.item_id and b.status = 1");
            if (!string.IsNullOrEmpty(itemCategoryId))
            {
                totalCountSql.AppendFormat(" inner join [dbo].[fnGetChildCategoryByID](@ItemCategoryId,1) c on c.CategoryID = b.item_category_id ");
            }
            totalCountSql.AppendFormat(@" where a.CustomerId = @CustomerId and a.IsDelete = 0 ");
            if (!string.IsNullOrEmpty(itemName))
            {
                totalCountSql.AppendFormat(" and b.item_name like @ItemName ");
            }
            if (status == 10 || status == 90)
            {
                totalCountSql.AppendFormat(" and a.Status = @Status");
            }

            SqlParameter[] parameters = 
            {
			    new SqlParameter("@CustomerId",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.Int),
				new SqlParameter("@ItemName",SqlDbType.NVarChar),
                new SqlParameter("@ItemCategoryId",SqlDbType.VarChar),
            };
            parameters[0].Value = CurrentUserInfo.ClientID;
            parameters[1].Value = status;
            parameters[2].Value = "%" + itemName + "%";
            parameters[3].Value = itemCategoryId;

            //执行sql
            PagedQueryResult<T_SuperRetailTraderItemMappingEntity> result = new PagedQueryResult<T_SuperRetailTraderItemMappingEntity>();
            using (SqlDataReader rdr = SQLHelper.ExecuteReader(CommandType.Text, pagedSql.ToString(), parameters))
            {
                while (rdr.Read())
                {
                    T_SuperRetailTraderItemMappingEntity m = new T_SuperRetailTraderItemMappingEntity();
                    Load(rdr, out m);
                    if (rdr["ItemName"] != DBNull.Value)
                    {
                        m.ItemName = rdr["ItemName"].ToString();
                    }
                    if (rdr["ImageUrl"] != DBNull.Value)
                    {
                        m.ImageUrl = rdr["ImageUrl"].ToString();
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

        public T_SuperRetailTraderItemMappingEntity GetSuperRetailTraderItemDetail(string ItemId)
        {
            T_SuperRetailTraderItemMappingEntity entity = new T_SuperRetailTraderItemMappingEntity();
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" select top 1 a.*,b.item_name as ItemName,b.Prop1Name,b.Prop2Name,b.Price,b.ItemIntroduce
            from T_SuperRetailTraderItemMapping a inner join vw_item_detail b on a.ItemId = b.item_id where a.ItemId = @ItemId and a.IsDelete = 0 and b.status = 1 and a.status = 10 ");

            SqlParameter[] parameters = 
            {
			    new SqlParameter("@ItemId",SqlDbType.VarChar),
            };
            parameters[0].Value = ItemId;

            //执行sql

            using(SqlDataReader rdr = SQLHelper.ExecuteReader(CommandType.Text, sql.ToString(), parameters))
            {
                 T_SuperRetailTraderItemMappingEntity m = new T_SuperRetailTraderItemMappingEntity();
                 while(rdr.Read())
                 {
                     Load(rdr, out m);
                     if (rdr["ItemName"] != DBNull.Value)
                     {
                         m.ItemName = rdr["ItemName"].ToString();
                     }
                     if (rdr["Prop1Name"] != DBNull.Value)
                     {
                         m.Prop1Name = rdr["Prop1Name"].ToString();
                     }
                     if (rdr["Prop2Name"] != DBNull.Value)
                     {
                         m.Prop2Name = rdr["Prop2Name"].ToString();
                     }
                     if (rdr["Price"] != DBNull.Value)
                     {
                         m.Price = Convert.ToDecimal(rdr["Price"]);
                     }
                     if (rdr["ItemIntroduce"] != DBNull.Value)
                     {
                         m.ItemIntroduce = rdr["ItemIntroduce"].ToString();
                     }
                     entity = m;
                 }
            }
            return entity;
        }
    }
}
