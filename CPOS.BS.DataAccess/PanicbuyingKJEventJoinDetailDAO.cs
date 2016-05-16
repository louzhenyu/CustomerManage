/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2016/2/16 14:37:47
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
    /// 表PanicbuyingKJEventJoinDetail的数据访问类 
    /// TODO:
    /// 1.实现ICRUDable接口
    /// 2.实现IQueryable接口
    /// 3.实现Load方法
    /// </summary>
    public partial class PanicbuyingKJEventJoinDetailDAO : Base.BaseCPOSDAO, ICRUDable<PanicbuyingKJEventJoinDetailEntity>, IQueryable<PanicbuyingKJEventJoinDetailEntity>
    {
        public DataSet GetHelperList(string EventId, string KJEventJoinId, string SkuId, int PageSize, int PageIndex)
        {
            string sql = @" declare @PageSize int
                            declare @PageIndex int 
                            declare @TotalCount int  
                            declare @TotalPage int

                            set @PageSize = " + PageSize + @"
                            set @PageIndex = " + PageIndex + @"

                            select b.VipId as HelperId,b.VipName as HelperName,b.HeadImgUrl,a.CreateTime,a.BargainPrice
                            from (select ROW_NUMBER() over(order by createtime desc) as rownum,* from PanicbuyingKJEventJoinDetail) a 
                            inner join Vip b on a.VipId = b.VipId
                            inner join PanicbuyingKJEventJoin c on a.KJEventJoinId = c.KJEventJoinId
                            where c.KJEventJoinId = '" + KJEventJoinId + @"'
                            and a.EventId = '" + EventId + @"' 
                            and a.SkuId = '" + SkuId + @"'
                            and a.rownum >  @PageSize*@PageIndex  
                            and a.rownum <= @PageSize*(@PageIndex + 1)

                            select @TotalCount = 
                            count(*) from (select ROW_NUMBER() over(order by createtime desc) as rownum,* from		PanicbuyingKJEventJoinDetail) a 
                            inner join Vip b on a.VipId = b.VipId
                            inner join PanicbuyingKJEventJoin c on a.KJEventJoinId = c.KJEventJoinId
                            where c.KJEventJoinId = '" + KJEventJoinId + @"'
                            and a.EventId = '" + EventId + @"' 
                            and a.SkuId = '" + SkuId + @"'
                            
                                                        if @PageSize <> 0    
                            select   @TotalPage =  @TotalCount / @PageSize 
                            else select @TotalPage = 0
                            
                            select @TotalCount
                            select @TotalPage
";

            return this.SQLHelper.ExecuteDataset(sql);
        }


        ///// <summary>
        ///// 获取砍价参与表最小的金额
        ///// </summary>
        ///// <param name="KJEventJoinId"></param>
        ///// <returns></returns>
        //public decimal GetMinMomentSalesPrice(string KJEventJoinId)
        //{
        //    return 0;
        //}
        
    }
}
