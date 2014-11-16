/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/23 17:47:45
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
using System.Linq;

namespace JIT.CPOS.BS.DataAccess
{

    /// <summary>
    /// ���ݷ��ʣ�  
    /// ��ShoppingCart�����ݷ����� 
    /// TODO:
    /// 1.ʵ��ICRUDable�ӿ�
    /// 2.ʵ��IQueryable�ӿ�
    /// 3.ʵ��Load����
    /// </summary>
    public partial class ShoppingCartDAO : Base.BaseCPOSDAO, ICRUDable<ShoppingCartEntity>, JIT.Utility.DataAccess.IQueryable<ShoppingCartEntity>
    {
        #region ��ȡ�б�
        /// <summary>
        /// ��ȡ�б�����
        /// </summary>
        public int GetListCount(ShoppingCartEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select count(*) as icount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        public DataSet GetList(ShoppingCartEntity entity, int Page, int PageSize)
        {
            int beginSize = (Page - 1) * PageSize + 1;
            int endSize = beginSize + PageSize - 1;
            DataSet ds = new DataSet();
            string sql = GetListSql(entity);
            sql += " select * From #tmp a where 1=1 and a.DisplayIndex between '" +
                beginSize + "' and '" + endSize + "' order by  a.DisplayIndex ";
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }
        private string GetListSql(ShoppingCartEntity entity)
        {
            string sql = string.Empty;
            sql = "select a.* ";
            sql += " ,DisplayIndex = row_number() over(order by a.CreateTime desc) ";
            sql += " ,b.item_id ItemId, b.prop_1_detail_name GG, b.salesPrice,c.ItemCategoryName,isnull(datediff(day,a.beginDate,a.endDate),0) DayCount ";
            sql += " into #tmp ";
            sql += " from [ShoppingCart] a ";
            sql += " left join vw_sku_detail b on a.skuId=b.sku_id ";
            sql += " left join Vw_Item_Detail c on c.item_id=b.item_id ";
            sql += " where a.IsDelete='0' ";
            if (entity.VipId != null)
            {
                sql += " and a.VipId = '" + entity.VipId + "' ";
            }
            return sql;
        }
        /// <summary>
        /// ��ȡ�б�����
        /// </summary>
        public decimal GetListAmount(ShoppingCartEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select isnull(sum(salesPrice*qty*(case when DayCount = 0 then 1 else DayCount end)),0) as iamount From #tmp; ";
            return Convert.ToDecimal(this.SQLHelper.ExecuteScalar(sql));
        }
        /// <summary>
        /// ��ȡ�б�����
        /// </summary>
        public int GetListQty(ShoppingCartEntity entity)
        {
            string sql = GetListSql(entity);
            sql = sql + " select isnull(sum(qty),0) as iamount From #tmp; ";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion

        #region ���������Ӧ�Ĺ��ﳵ��Ϣ
        /// <summary>
        /// ���������Ӧ�Ĺ��ﳵ��Ϣ
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public bool SetCancelShoppingCartByOrderId(string OrderId, string VipId, string[] pSkuIDs)
        {
            StringBuilder sub = new StringBuilder();
            if (pSkuIDs.Length > 0)
            {
                sub.Append(" and skuid in({0})");
                var temp = pSkuIDs.Aggregate("", (i, j) => i + string.Format("'{0}',", j)).Trim(',');
                sub = new StringBuilder(string.Format(sub.ToString(), temp));
            }
            string sql = "UPDATE dbo.ShoppingCart "
                        + " SET Qty = '0' "
                        + " ,IsDelete = '1' "
                        + " FROM ShoppingCart x "
                        + " INNER JOIN dbo.T_Inout_Detail y "
                        + " ON(x.SkuId = y.sku_id) "
                        + " WHERE x.VipId = '" + VipId + "' "
                        + " AND y.order_id = '" + OrderId + "' {0}";
            this.SQLHelper.ExecuteNonQuery(string.Format(sql, sub.ToString()));
            return true;
        }
        #endregion

        #region ���ﳵ����
        /// <summary>
        /// ��ȡ��Ա�Ĺ��ﳵ����
        /// </summary>
        /// <param name="vipId"></param>
        /// <returns></returns>
        public int GetShoppingCartByVipId(string vipId)
        {
            string sql = "SELECT ISNULL(SUM(Qty),0) FROM dbo.ShoppingCart WHERE IsDelete = '0' AND VipId = '" + vipId + "'";
            return Convert.ToInt32(this.SQLHelper.ExecuteScalar(sql));
        }
        #endregion

        #region ɾ�����ﳵ
        public void DeleteShoppingCart(string pVipId, string[] pIDs)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update shoppingcart set isdelete=1 where vipid='{0}' {1}");
            StringBuilder sub = new StringBuilder();
            if (pIDs != null && pIDs.Length > 0)
            {
                foreach (var item in pIDs)
                {
                    sub.AppendFormat("'{0}',", item);
                }
                sub = new StringBuilder(string.Format(" and skuid in ({0})", sub.ToString().Trim(',')));
            }
            sql = new StringBuilder(string.Format(sql.ToString(), pVipId, sub.ToString()));
            this.SQLHelper.ExecuteNonQuery(sql.ToString());
        }

        public void DeleteShoppingCart(string pOrderId)
        {
            string sql = string.Format(@"UPDATE dbo.ShoppingCart  SET Qty = '0',IsDelete = '1' 
                         FROM ShoppingCart x 
                         INNER JOIN dbo.T_Inout_Detail y 
                         ON(x.SkuId = y.sku_id) 
                         WHERE  y.order_id = '{0}'", pOrderId);
            this.SQLHelper.ExecuteNonQuery(sql.ToString());
        }
        #endregion


        #region ��ȡ���ﳵ�б��е���Ʒ���
        public DataSet GetShoppingGgBySkuId(string skuIdList)
        {
            var sql = new StringBuilder();
            sql.Append(" select isnull(b.sku_id,'')sku_id,");
            sql.Append(" isnull(b.prop_1_name,'') prop_1_name,isnull(b.prop_1_detail_name,'') prop_1_detail_name");
            sql.Append(" ,isnull(b.prop_2_name,'') prop_2_name,isnull(b.prop_2_detail_name,'') prop_2_detail_name");
            sql.Append(" ,isnull(b.prop_3_name,'') prop_3_name,isnull(b.prop_3_detail_name,'') prop_3_detail_name");
            sql.Append(" ,isnull(b.prop_4_name,'') prop_4_name,isnull(b.prop_4_detail_name,'') prop_4_detail_name");
            sql.Append(" ,isnull(b.prop_5_name,'') prop_5_name,isnull(b.prop_5_detail_name,'') prop_5_detail_name");
            sql.Append("  from vw_sku b");
           
            sql.AppendFormat("  where b.sku_id in ({0})",skuIdList);
            return this.SQLHelper.ExecuteDataset(sql.ToString());
        }
        #endregion

    }
}
