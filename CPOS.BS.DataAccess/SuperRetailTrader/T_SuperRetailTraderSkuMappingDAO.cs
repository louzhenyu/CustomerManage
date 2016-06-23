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
        /// <summary>
        /// 获取商品Sku
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="itemCategoryId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public List<T_SuperRetailTraderSkuMappingEntity> GetSkuList(string itemName,string itemCategoryId,int pageSize,int pageIndex)
        {
            List<T_SuperRetailTraderSkuMappingEntity> list = new List<T_SuperRetailTraderSkuMappingEntity>();

            StringBuilder pagedSql = new StringBuilder();

            //临时表
            pagedSql.AppendFormat(@"Create Table Temp (ItemId varchar(50))");
            pagedSql.AppendFormat(@"Insert into Temp 
                                    select ItemId from (
                                    Select 
                                    distinct dense_rank() over(order by y.createtime desc) as rownum,* from (
                                    select
                                    a.item_id  as ItemId ,a.create_time as createtime from vw_item_detail a
                                    left join vw_sku_detail b on a.item_id = b.item_id and b.CustomerId = @CustomerId and b.status = 1
                                    left join T_SuperRetailTraderSkuMapping d on b.sku_id = d.SkuId and d.CustomerId = @CustomerId and d.IsDelete = 0 ");
            if (!string.IsNullOrEmpty(itemCategoryId))
            {
                pagedSql.AppendFormat(" inner join [dbo].[fnGetChildCategoryByID](@ItemCategoryId,1) c on c.CategoryID = a.item_category_id ");
            }
            pagedSql.AppendFormat(@" where a.CustomerId = @CustomerId and a.status = 1 and d.SkuId is null and a.item_category_id <> '-1' ");
            if (!string.IsNullOrEmpty(itemName))
            {
                pagedSql.AppendFormat(" and a.item_name like @ItemName ");
            }
            pagedSql.AppendFormat(@" group by a.item_id,a.create_time ) y ) x where x.rownum > {0} and x.rownum <= {1} ", pageSize * (pageIndex - 1), pageSize * pageIndex);

            //查询
            pagedSql.AppendFormat(@"select * from ( 
                                         select distinct
                                         a.item_id as ItemId,
                                         a.sku_id as SkuId,
                                         a.prop_1_detail_name as PropName1,
                                         a.prop_2_detail_name as PropName2,
                                         a.SalesPrice
                                         from vw_Sku_Detail a 
                                         left join T_SuperRetailTraderSkuMapping b on a.Sku_id = b.SkuId and b.CustomerId = @CustomerId and b.IsDelete = 0
                                         inner join vw_item_detail x on x.item_id = a.item_id and  x.CustomerId = @CustomerId and x.status = 1
                                         inner join temp d on a.item_id = d.ItemId ");

            if (!string.IsNullOrEmpty(itemName))
            {
                pagedSql.AppendFormat(" and x.item_name like @ItemName ");
            }
            if (!string.IsNullOrEmpty(itemCategoryId))
            {
                pagedSql.AppendFormat(" inner join [dbo].[fnGetChildCategoryByID](@ItemCategoryId,1) c on c.CategoryID = x.item_category_id ");
            }
            pagedSql.AppendFormat(" where a.CustomerId = @CustomerId and a.status = 1 and b.itemid is null ) y");
            pagedSql.AppendFormat(" drop table temp");

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

                    list.Add(m);
                }
            }
            return list;
        }
        /// <summary>
        /// 获取分销商Sku列表
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public List<T_SuperRetailTraderSkuMappingEntity> GetSuperRetailTraderSkuList(string itemCategoryId,string itemName,int status,int pageSize,int pageIndex)
        {
            List<T_SuperRetailTraderSkuMappingEntity> list = new List<T_SuperRetailTraderSkuMappingEntity>();

            StringBuilder pagedSql = new StringBuilder();
            
            //临时表
            pagedSql.AppendFormat(@"Create Table Temp (ItemId varchar(50))");
            pagedSql.AppendFormat(@"insert into  Temp 
                                    select ItemId from (
                                    select distinct dense_rank() over(order by createtime desc) as rownum, ItemId from ( 
                                    select  
                                    a.ItemId,
                                    a.createtime
                                    from T_SuperRetailTraderItemMapping a 
                                    inner join vw_item_Detail b on a.ItemId = b.item_id and b.status = 1
                                    inner join T_SuperRetailTraderSkuMapping x on x.ItemId = a.ItemId ");
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
                pagedSql.AppendFormat(" and x.Status = @Status");
            }
            pagedSql.AppendFormat(@" Group by a.ItemId, a.createtime) z ");
            pagedSql.AppendFormat(@"  ) x where x.rownum > {0} and x.rownum <= {1} ", pageSize * (pageIndex - 1), pageSize * pageIndex);


            pagedSql.AppendFormat(@"
                                        select distinct
			                            a.item_id as ItemId,
			                            d.item_name as ItemName,
			                            a.sku_id as SkuId,
			                            a.prop_1_detail_name as PropName1,
			                            a.prop_2_detail_name as PropName2,
			                            a.SalesPrice,
			                            b.DistributerStock,
			                            b.DistributerCostPrice,
			                            b.DistributerPrice,
			                            b.Status,
                                        b.SalesQty
			                            from vw_Sku_Detail a 
			                            inner join T_SuperRetailTraderSkuMapping b 
			                            on a.Sku_id = b.SkuId and b.CustomerId = @CustomerId and b.IsDelete = 0
			                            inner join vw_item_detail d
			                            on d.item_id = a.item_id and d.CustomerId = @CustomerId and d.status = 1 
                                        inner join temp x on a.item_id = x.ItemId ");
                            
           // pagedSql.AppendFormat(" and d.rownum > {0} and d.rownum <= {1}", pageSize * (pageIndex - 1), pageSize * pageIndex);
            pagedSql.AppendFormat(@" where a.CustomerId = @CustomerId and a.status = 1 ");
            if (status == 10 || status == 90)
            {
                pagedSql.AppendFormat(" and b.Status = @Status");
            }
            if (!string.IsNullOrEmpty(itemName))
            {
                pagedSql.AppendFormat(" and d.item_name like @ItemName");
            }

            pagedSql.AppendFormat(@" drop table Temp ");
            
            SqlParameter[] parameters = 
            {
			    new SqlParameter("@CustomerId",SqlDbType.VarChar),
                new SqlParameter("@Status",SqlDbType.Int),
                new SqlParameter(@"ItemName",SqlDbType.NVarChar),
                new SqlParameter("@ItemCategoryId",SqlDbType.VarChar)
            };
            parameters[0].Value = CurrentUserInfo.ClientID;
            parameters[1].Value = status;
            parameters[2].Value = "%" + itemName + "%";
            parameters[3].Value = itemCategoryId;


            using (SqlDataReader rdr = SQLHelper.ExecuteReader(CommandType.Text, pagedSql.ToString(), parameters))
            {
                while (rdr.Read())
                {
                    T_SuperRetailTraderSkuMappingEntity m = new T_SuperRetailTraderSkuMappingEntity();
                    if (rdr["ItemId"] != DBNull.Value)
                    {
                        m.ItemId = rdr["ItemId"].ToString();
                    }
                    if (rdr["ItemName"] != DBNull.Value)
                    {
                        m.ItemName = rdr["ItemName"].ToString();
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
                    if (rdr["DistributerStock"] != DBNull.Value)
                    {
                        m.DistributerStock = Convert.ToInt32(rdr["DistributerStock"]);
                    }
                    if (rdr["DistributerCostPrice"] != DBNull.Value)
                    {
                        m.DistributerCostPrice = Convert.ToDecimal(rdr["DistributerCostPrice"]);
                    }
                    if (rdr["DistributerPrice"] != DBNull.Value)
                    {
                        m.DistributerPrice = Convert.ToDecimal(rdr["DistributerPrice"]);
                    }
                    if (rdr["Status"] != DBNull.Value)
                    {
                        m.Status = Convert.ToInt32(rdr["Status"]);
                    }
                    if (rdr["SalesQty"] != DBNull.Value)
                    {
                        m.SalesQty = Convert.ToInt32(rdr["SalesQty"]);
                    }
                    list.Add(m);
                }
            }
            return list;
        }
        /// <summary>
        /// 获取分销商Sku详情
        /// </summary>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        public List<T_SuperRetailTraderSkuMappingEntity> GetSuperRetailTraderSkuDetail(string ItemId,string CustomerId)
        {
            List<T_SuperRetailTraderSkuMappingEntity> list = new List<T_SuperRetailTraderSkuMappingEntity>();
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@" select  a.*,b.prop_1_detail_name as PropName1,b.prop_2_detail_name as PropName2 from T_SuperRetailTraderSkuMapping a 
            inner join vw_sku_detail b on a.SkuId = b.Sku_id and b.CustomerId = @CustomerId where a.ItemId = @ItemId and a.Status = 10 and a.Isdelete = 0 and b.Status = 1");

            SqlParameter[] parameters = 
            {
			    new SqlParameter("@ItemId",SqlDbType.VarChar),
                new SqlParameter("@CustomerId",SqlDbType.VarChar)
            };
            parameters[0].Value = ItemId;
            parameters[1].Value = CustomerId;

            //执行sql
            using (SqlDataReader rdr = SQLHelper.ExecuteReader(CommandType.Text, sql.ToString(), parameters))
            { 
                while (rdr.Read())
                {
                    T_SuperRetailTraderSkuMappingEntity m = new T_SuperRetailTraderSkuMappingEntity();
                    Load(rdr, out m);
                    if (rdr["PropName1"] != DBNull.Value)
                    {
                        m.PropName1 = rdr["PropName1"].ToString();
                    }
                    if (rdr["PropName2"] != DBNull.Value)
                    {
                        m.PropName2 = rdr["PropName2"].ToString();
                    }
                    list.Add(m);
                }
            }
            return list;
        }

    
    }
}
