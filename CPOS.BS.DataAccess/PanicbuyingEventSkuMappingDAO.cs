/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/7/25 13:51:20
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
    /// 数据访问：  
    /// 表PanicbuyingEventSkuMapping的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class PanicbuyingEventSkuMappingDAO : Base.BaseCPOSDAO, ICRUDable<PanicbuyingEventSkuMappingEntity>, IQueryable<PanicbuyingEventSkuMappingEntity>
    {
        #region 获取活动商品
        /// <summary>
        /// 获取 活动商品
        /// </summary>
        /// <param name="EventId"></param>
        /// <returns></returns>
        public DataSet GetEventMerchandise(string pEventId)
        {
            var paras = new List<SqlParameter>{
            new SqlParameter{ ParameterName="@EventId",Value=pEventId },
            new SqlParameter{ParameterName="@pCustomerId",Value=this.CurrentUserInfo.ClientID}
            };
            StringBuilder strb = new StringBuilder();
            strb.Append(@"
SELECT item_id ItemID,item_name ItemName ,isnull(DisplayIndex,0)displayindex,ISNULL(SinglePurchaseQty,0) AS SinglePurchaseQty 
            ,(CASE  WHEN ObjectURL IS NULL OR ObjectURL='' THEN Imageurl  END)Imageurl , EventItemMappingId,buycount ,SalesPersonCount
  ,SalesPrice
  ,Price
            FROM
              (SELECT item_id,item_name,DisplayIndex,EventItemMappingId,SinglePurchaseQty,
                 (SELECT top 1 imageUrl
                  FROM ObjectImages AS img
                  WHERE T.item_id=img.ObjectId
                    AND IsDelete=0
                    AND DisplayIndex IS NOT NULL
                  ORDER BY displayindex) Imageurl,
                 (SELECT top 1 ImageURL
                  FROM ObjectImages
                  WHERE ObjectImages.ObjectId=cast(Pe.EventItemMappingId AS nvarchar(200))
                  ORDER BY createtime DESC) ObjectURL,
  (select  count(1) from    PanicbuyingEventOrderMapping x inner join t_inout_Detail y on x.orderid=y.order_id
  inner join t_sku z on y.sku_id=z.sku_id where  x.EventId=pe.EventId and z.item_id=pe.ItemId )  as buycount,  ----购买次数
 (select  count(DISTINCT za.vip_no) from    PanicbuyingEventOrderMapping x inner join t_inout za on x.OrderId = za.order_id inner join t_inout_Detail y on x.orderid=y.order_id
  inner join t_sku z on y.sku_id=z.sku_id where  x.EventId=pe.EventId and z.item_id=pe.ItemId )+isnull(pe.KeepQty,0)     as SalesPersonCount  ----购买次数


 , Pe.SalesPrice  --抢购价格  
  ,pe.Price   --商品价格        



               FROM PanicbuyingEventItemMapping AS Pe
               INNER JOIN T_Item AS T ON T.item_id=Pe.ItemId
               WHERE Pe.EventId=@EventId
                 AND T.CustomerId=@pCustomerId
                 AND Pe.IsDelete=0)X
            ORDER BY DisplayIndex ASC");
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, strb.ToString(), paras.ToArray());
            return ds;


    //        , ISNULL(                    
    //(SELECT COUNT(DISTINCT z.vip_no)                     
    // FROM PanicbuyingEventOrderMapping y                    
    //  INNER JOIN T_Inout z ON y.orderid = z.order_id                    
    //  inner join t_inout_detail aa on z.order_id=aa.order_id                    
    //  inner join T_Sku bb on aa.sku_id=bb.sku_id                    
    // where bb.item_id=T.item_id and y.EventID=pe.EventID and y.IsDelete=0)                    
    //, 0)+isnull(pe.KeepQty,0) AS SalesPersonCount ---购买人数 
        }
        /// <summary>
        /// 获取活动
        /// </summary>
        /// <param name="EventItemMappingId"></param>
        /// <returns></returns>
        public DataSet GetGetEventMerchandiseSku(string pEventItemMappingId)
        {
            var paras = new List<SqlParameter>{
            new SqlParameter{ ParameterName="@EventItemMappingId",Value=pEventItemMappingId }
            };

            StringBuilder strb = new StringBuilder();
            strb.Append(@"
               select 
                    sm.MappingId,vwsku.sku_id SkuID,isnull(vp.price,0) price,sm.SalesPrice
                    ,(vwsku.prop_1_detail_name+vwsku.prop_2_detail_name+vwsku.prop_3_detail_name+vwsku.prop_4_detail_name+vwsku.prop_5_detail_name) SkuName
                    ,sm.Qty,sm.KeepQty,sm.SoldQty,(isnull(sm.Qty,0)-isnull(sm.SoldQty,0)) InverTory
                    ,(case when (isnull(sm.Qty,0)-isnull(sm.SoldQty,0))<=0 then '售罄' else '在售' end)StatusName
                    ,(case when (isnull(sm.Qty,0)-isnull(sm.SoldQty,0))<=0 then 'false' else 'true' end)Status
                    from  PanicbuyingEventItemMapping as im
                    inner join PanicbuyingEventSkuMapping as sm on im.EventItemMappingId=sm.EventItemMappingId
                    inner join vw_sku as vwsku on vwsku.sku_id=sm.SkuId
                    left join vw_sku_price vp on vp.sku_id=vwsku.sku_id and  vp.item_price_type_id='77850286E3F24CD2AC84F80BC625859D'
                    where im.EventItemMappingId=@EventItemMappingId
                    and im.IsDelete=0 and sm.IsDelete=0 order by vwsku.sku_id desc");

            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, strb.ToString(), paras.ToArray());
            return ds;
        }
        #endregion


        #region 获取活动商品规格
        public DataSet GetItemSku(string EventId, string ItemId, string EventItemMappingId)
        {

            var paras = new List<SqlParameter>{
            new SqlParameter{ ParameterName="@EventId",Value=EventId },
            new SqlParameter{ParameterName="@ItemId",Value=ItemId},
            new SqlParameter{ParameterName="@pCustomerId",Value=this.CurrentUserInfo.ClientID},
            new SqlParameter{ParameterName="@EventItemMappingId",Value=EventItemMappingId}
            };
            //js和更新时的操作都是正确的，查询时出错了
            StringBuilder strb = new StringBuilder();
            strb.Append(@"
                        select   
                            MappingId,vw.sku_id  as SkuID 
                            ,isnull(vp.price,0)price ,ISNULL(pe.SalesPrice,0)SalesPrice
                            ,(CASE WHEN LEN(vw.prop_1_detail_name) > 0
                                   THEN vw.prop_1_detail_name
                                        + CASE WHEN LEN(vw.prop_2_detail_name) > 0
                                               THEN ',' + vw.prop_2_detail_name
                                               ELSE ''
                                          END + CASE WHEN LEN(vw.prop_3_detail_name) > 0
                                                     THEN ',' + vw.prop_3_detail_name
                                                     ELSE ''
                                                END
                                        + CASE WHEN LEN(vw.prop_4_detail_name) > 0
                                               THEN ',' + vw.prop_4_detail_name
                                               ELSE ''
                                          END + CASE WHEN LEN(vw.prop_5_detail_name) > 0
                                                     THEN ',' + vw.prop_5_detail_name
                                                     ELSE ''
                                                END
                                   ELSE ''
                              END ) SkuName
                            ,ISNULL(pe.Qty,0) Qty,ISNULL(pe.KeepQty,0) KeepQty,ISNULL(pe.SoldQty,0) SoldQty,
                            (ISNULL(pe.Qty,0)-ISNULL(pe.KeepQty,0)-ISNULL(pe.SoldQty,0)) InverTory,
                            (case when MappingId is null or CONVERT(NVARCHAR(50),MappingId)='' then 'false' else 'true' end)IsSelected
                            from vw_sku  as vw
                            left join  PanicbuyingEventSkuMapping as pe on pe.SkuId=vw.sku_id  and convert(nvarchar(50),pe.EventItemMappingId)=@EventItemMappingId and pe.IsDelete=0
                            left join  PanicbuyingEventItemMapping ps on ps.EventItemMappingId=pe.EventItemMappingId and ps.ItemId=@ItemId and ps.EventId=@EventId and ps.IsDelete=0 
                            left join vw_sku_price vp on vp.sku_id=vw.sku_id and  vp.item_price_type_id='77850286E3F24CD2AC84F80BC625859D'
                            where vw.item_id=@ItemId  and vw.status='1'
                            order by SkuID desc");
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, strb.ToString(), paras.ToArray());
            return ds;
        
        }
        #endregion


        public void DeleteEventItemSku(string pEventItemMappingId)
        {

            string sql = "update PanicbuyingEventSkuMapping set isdelete=1 where EventItemMappingId='" + pEventItemMappingId + "'";

            this.SQLHelper.ExecuteNonQuery(sql);
        }


        public PanicbuyingEventSkuMappingEntity GetEventItemSku(object pID)
        {
            //参数检查
            if (pID == null)
                return null;
            string id = pID.ToString();
            //组织SQL
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat("select * from [PanicbuyingEventSkuMapping] where MappingId='{0}' and IsDelete=1 ", id.ToString());
            //读取数据
            PanicbuyingEventSkuMappingEntity m = null;
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


        

              #region 获取活动商品规格
        public DataSet GetTCTWPanicbuyingEventKV(string CTWEventId)
        {

            var paras = new List<SqlParameter>{
            new SqlParameter{ ParameterName="@CTWEventId",Value=CTWEventId },
        
            };
            //js和更新时的操作都是正确的，查询时出错了
            StringBuilder strb = new StringBuilder();
            strb.Append(@"                       
select  a.*,ImageURL from  T_CTW_PanicbuyingEventKV  a inner join objectimages b
on a.imageid=b.imageid   where CONVERT(varchar(50), CTWEventId)=@CTWEventId  ");
            DataSet ds = this.SQLHelper.ExecuteDataset(CommandType.Text, strb.ToString(), paras.ToArray());
            return ds;
        
        }
        #endregion

  
    }
}
