/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/2/16 14:37:46
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
    /// 表PanicbuyingKJEventJoin的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class PanicbuyingKJEventJoinDAO : Base.BaseCPOSDAO, ICRUDable<PanicbuyingKJEventJoinEntity>, IQueryable<PanicbuyingKJEventJoinEntity>
    {
        public DataSet GetBuyerList(string EventId, string ItemId, int PageSize, int PageIndex)
        {
            string sql = @" declare @PageSize int
                            declare @PageIndex int 
                            declare @TotalCount int  
                            declare @TotalPage int

                            set @PageSize = " + PageSize + @"
                            set @PageIndex = " + PageIndex + @"


                            select b.VipId as BuyerId,b.VipName as BuyerName,b.HeadImgUrl,a.CreateTime,a.SalesPrice
                            from (select ROW_NUMBER() over(order by createtime desc) as rownum,* from PanicbuyingKJEventJoin) a inner join Vip b on a.VipId = b.VipId
                            where a.EventOrderMappingId is not null 
                            and a.EventId = '" + EventId + @"' 
                            and a.ItemId = '" + ItemId + @"'
                            and a.rownum >  @PageSize*@PageIndex 
                            and a.rownum <= @PageSize*(@PageIndex + 1)
                            
                            select @TotalCount = count(*) from (select ROW_NUMBER() over(order by createtime desc) as rownum,EventId,ItemId,VipId ,EventOrderMappingId from PanicbuyingKJEventJoin) a 
                            inner join Vip b on a.VipId = b.VipId
                            where a.EventOrderMappingId is not null
                            and a.EventId = '" + EventId + @"' 
                            and a.ItemId = '" + ItemId + @"'
                        
                            if @PageSize <> 0    
                            select   @TotalPage =  @TotalCount / @PageSize 
                            else select @TotalPage = 0
                            
                            select @TotalCount
                            select @TotalPage"
                                              ;

            return this.SQLHelper.ExecuteDataset(sql);
        }

        /// <summary>
        /// 获取会员砍价活动商品列表
        /// </summary>
        /// <param name="Vipid"></param>
        /// <param name="Index"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public DataSet GetKJEventJoinList(string Vipid, int Index, int Size)
        {
            int BeginIndex = 0;
            int EndIndex = 0;
            if (Index == 0)
            {
                BeginIndex = 0;
                EndIndex = Size;
            }
            else
            {
                BeginIndex = Index * Size;
                EndIndex = (Index + 1) * Size;
            }

            


            var para = new SqlParameter[4];
            para[0] = new SqlParameter("@VipId", System.Data.SqlDbType.VarChar) { Value = Vipid };
            para[1] = new SqlParameter("@CustomerId", System.Data.SqlDbType.VarChar) { Value = this.CurrentUserInfo.ClientID };
            para[2] = new SqlParameter("@BeginIndex", System.Data.SqlDbType.Int) { Value = BeginIndex };
            para[3] = new SqlParameter("@EndIndex", System.Data.SqlDbType.Int) { Value = EndIndex };

            return this.SQLHelper.ExecuteDataset(CommandType.StoredProcedure, "Proc_GetKJEventJoinByVipId", para);
        }

        /// <summary>
        /// 获取商品图片URL
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public string GetItemImageURL(string ItemID)
        {
            string ImageUrl=string.Empty;
            string sql = string.Format("select top 1 ImageURL from objectimages where ObjectId='{0}'  and isdelete=0 and Description != '自动生成的产品二维码' order by displayindex ", ItemID);
            var Result = this.SQLHelper.ExecuteScalar(sql);
            if (Result != null)
                ImageUrl = Result.ToString();
            return ImageUrl;
        }

    }
}
