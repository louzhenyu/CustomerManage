/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2014/3/26 18:33:15
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
    /// 表T_Inout_Detail的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class T_Inout_DetailDAO : Base.BaseCPOSDAO, ICRUDable<T_Inout_DetailEntity>, IQueryable<T_Inout_DetailEntity>
    {
        public T_Inout_DetailEntity[] GetByIDS(string[] IDS)
        {
            List<T_Inout_DetailEntity> list = new List<T_Inout_DetailEntity> { };
            StringBuilder sub = new StringBuilder();
            if (IDS.Length > 0)
            {
                StringBuilder sub2 = new StringBuilder();
                foreach (var item in IDS)
                {
                    sub2.AppendFormat("'{0}',", item);
                }
                sub.AppendFormat(" and a.order_id in({0})", sub2.ToString().Trim(','));
            }
            string sql = string.Format(@"select a.*,b.item_id,b.barcode,c.item_name,c.item_code,c.item_remark
                ,(select top 1 imageurl from ObjectImages where ObjectId=b.item_id and isdelete=0  and Description != '自动生成的产品二维码' order by displayindex) imageurl 
              , isnull(c.isGB,1) as isGB
                 from T_Inout_Detail a join t_sku b on a.sku_id=b.sku_id Join T_Item c on b.item_id=c.item_id where 1=1 {0}", sub);
            using (var rd = this.SQLHelper.ExecuteReader(sql))
            {
                while (rd.Read())
                {
                    T_Inout_DetailEntity m;
                    this.Load(rd, out m);
                    m.ItemID = rd["item_id"].ToString();//商品ID
                    m.ItemName = rd["item_name"].ToString();//商品名称
                    m.ItemCode = rd["item_code"].ToString();//商品编码
                    m.SKUID = rd["sku_id"].ToString();//SKUID
                    m.SkuCode = rd["barcode"].ToString();
                    m.Qty = Convert.ToInt32(rd["order_qty"]);//购买数量
                    m.SalesPrice = Convert.ToDecimal(rd["enter_price"]);//实际单价
                    m.ImageUrl = rd["imageurl"].ToString();//SKU图片
                    m.SpecificationDesc = rd["item_remark"].ToString();
                    m.ReturnCash = rd["ReturnCash"] == DBNull.Value ? 0.00m : Convert.ToDecimal(rd["ReturnCash"]);
                    m.isGB = Convert.ToInt32(rd["isGB"]);
                    list.Add(m);
                }
            }
            return list.ToArray();
        }
    }
}
