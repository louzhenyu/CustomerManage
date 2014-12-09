using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.Entity;
using JIT.Utility;
using System.Data;
using System.Data.SqlClient;
using JIT.Utility.DataAccess;

namespace JIT.CPOS.BS.DataAccess
{
    public class SkuPriceService : Base.BaseCPOSDAO
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        /// <param name="loggingSessionInfo">当前的用户信息</param>
        public SkuPriceService(LoggingSessionInfo loggingSessionInfo)
            : base(loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
        }
        #endregion

        #region 查询
        /// <summary>
        /// 获取对应sku价格集合
        /// </summary>
        /// <param name="skuId">sku标识</param>
        /// <returns></returns>
        public DataSet GetSkuPriceListBySkuId(string skuId)
        {
            DataSet ds = new DataSet();
            string sql = string.Format(@"select a.item_price_type_id,a.item_price_type_name,b.sku_price_id,
  isnull(b.sku_id,'{0}')as sku_id,isnull(b.sku_price,'-1')as sku_price,b.status,b.create_time,b.create_user_id,b.modify_time,
  b.modify_user_id,b.bat_id,b.if_flag,b.customer_id 
  from T_Item_Price_Type a left join t_sku_price b 
  on a.item_price_type_id=b.item_price_type_id and b.status='1' and b.sku_id='{0}' and b.customer_id='{1}'  
  where a.status='1'", skuId, this.CurrentUserInfo.CurrentLoggingManager.Customer_Id);
            ds = this.SQLHelper.ExecuteDataset(sql);
            return ds;
        }

        /// <summary>
        /// 根据skuid获取sku价格及人人销售价
        /// </summary>
        /// <param name="skuIds"></param>
        /// <returns></returns>
        public DataSet GetPriceListBySkuIds(string skuIds, string EventId)
        { 
            DataSet ds = new DataSet();

            //旧的sql不包含团购价
            //string sql = string.Format("select sku_id = a.sku_id,price= a.SalesPrice,EveryoneSalesPrice=a.EveryoneSalesPrice  from vw_sku_detail a where sku_id in ({0})", skuIds);

            //modify by donal 2014-11-13 13:43:08  新的sql，团购ID不为空时，查询团购价。
            string sql = @"
                SELECT  sku_id = a.sku_id ,
                        price = a.SalesPrice ,
                        EveryoneSalesPrice = a.EveryoneSalesPrice,
                        x.SalesPrice as groupPrice
                FROM    vw_sku_detail a
                LEFT JOIN ( SELECT 
                                    c.SalesPrice,c.SkuId
                          FROM      PanicbuyingEvent a
                                    LEFT JOIN PanicbuyingEventItemMapping b ON a.EventId = b.EventId
                                    LEFT JOIN dbo.PanicbuyingEventSkuMapping c ON b.EventItemMappingId = c.EventItemMappingId
                          WHERE     a.IsDelete = 0
                                    AND b.IsDelete = 0
                                    AND c.IsDelete = 0
                                    AND a.EventId = {0}
                        ) x ON x.SkuId =  a.sku_id
                WHERE   a.sku_id in({1})";
            string sEventId = string.IsNullOrWhiteSpace(EventId)? "NULL" : "'"+EventId.ToString()+"'";
            ds = this.SQLHelper.ExecuteDataset(string.Format(sql,sEventId,skuIds));         
            return ds;       
        }
        #endregion

        #region 删除sku相关价格
        /// <summary>
        /// 删除sku相关价格
        /// </summary>
        /// <param name="skuInfo">sku对象</param>
        /// <returns></returns>
        public bool DeleteSkuPriceInfo(SkuInfo skuInfo, IDbTransaction pTran = null)
        {
            try
            {
                string sql = "update T_Sku_Price "
                    + " set status= '-1' "
                    + " ,modify_user_id = '" + loggingSessionInfo.CurrentUser.User_Id + "' "
                    + " ,modify_time = '" + GetCurrentDateTime() + "'"
                    + " ,if_flag = '0' "
                    + " where sku_id = '" + skuInfo.sku_id + "';";


                if (pTran != null)
                {
                    this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
                }
                else
                {
                    this.SQLHelper.ExecuteNonQuery(sql);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        #endregion

        #region 修改sku相关价格信息
        /// <summary>
        /// 修改sku相关价格信息
        /// </summary>
        /// <param name="skuPriceInfo"></param>
        /// <param name="pTran"></param>
        public void UpdateSkuPrice(SkuPriceInfo skuPriceInfo, IDbTransaction pTran)
        {
            string sql = "update T_Sku_Price "
                    + " set sku_price = " + skuPriceInfo.sku_price
                    + " ,customer_id = '" + this.CurrentUserInfo.CurrentLoggingManager.Customer_Id + "' "
                    + " ,modify_user_id = '" + skuPriceInfo.create_user_id + "'  "
                    + " ,modify_time = '" + skuPriceInfo.create_time + "'  "
                    + " ,status = '1' "
                    + " ,if_flag = '0' where sku_price_id = '" + skuPriceInfo.sku_price_id + "'";

            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
        }
        #endregion

        #region 插入sku相关价格信息
        /// <summary>
        /// 插入sku相关价格信息
        /// </summary>
        /// <param name="skuPriceInfo"></param>
        /// <param name="pTran"></param>
        public void InsertSkuPrice(SkuPriceInfo skuPriceInfo, IDbTransaction pTran)
        {
            #region
            string sql = "insert into T_Sku_Price "
                    + " (sku_price_id "
                    + " ,sku_id "
                    + " ,item_price_type_id "
                    + " ,sku_price "
                    + " ,status "
                    + " ,create_user_id "
                    + " ,create_time "
                    + " ,modify_user_id "
                    + " ,modify_time "
                    + " ,if_flag "
                    + " ,customer_id "
                    + " ) "
                    + " select a.* From ( select '" + skuPriceInfo.sku_price_id + "' sku_price_id"
                    + ", '" + skuPriceInfo.sku_id + "' sku_id"
                    + ", '" + skuPriceInfo.item_price_type_id + "' item_price_type_id"
                    + ", " + skuPriceInfo.sku_price + " sku_price"
                    + ", '1' status"
                    + ", '" + skuPriceInfo.create_user_id + "' create_user_id"
                    + ", '" + skuPriceInfo.create_time + "' create_time"
                    + ", '" + skuPriceInfo.modify_user_id + "' modify_user_id"
                    + ", '" + skuPriceInfo.modify_time + "' modify_time"
                    + ", '0' if_flag"
                    + ", '" + this.CurrentUserInfo.CurrentLoggingManager.Customer_Id + "' customer_id"
                    + " ) a "
                    + " left join T_Sku_Price b on(a.sku_price_id = b.sku_price_id) where b.sku_price_id is null ;";
            #endregion
            if (pTran != null)
            {
                this.SQLHelper.ExecuteNonQuery((SqlTransaction)pTran, CommandType.Text, sql.ToString(), null);
            }
            else
            {
                this.SQLHelper.ExecuteNonQuery(sql);
            }
        }
        #endregion


    }
}
