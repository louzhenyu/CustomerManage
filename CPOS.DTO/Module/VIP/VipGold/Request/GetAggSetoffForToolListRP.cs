using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class GetAggSetoffForToolListRP : IAPIRequestParameter
    {
        /// <summary>
        /// （1=近7天；2=近30天）
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 门店编号
        /// </summary>
        public string unitid { get; set; }

        /// <summary>
        ///CTW：创意仓库、 Coupon=优惠券 、SetoffPoster=集客报 、Goods=商品
        /// </summary>
        public string SetoffRoleId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string starttime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string endtime { get; set; }

        /// <summary>
        /// 排序名称
        /// </summary>
        public string SortName { get; set; }
        /// <summary>
        /// 排序方式
        /// </summary>
        public string SortOrder { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int PageSize { get; set; }
        public void Validate()
        {
        }
    }
}
