using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class GetAggSetoffForToolListRD : IAPIResponseData
    {

        /// <summary>
        /// 分享总计
        /// </summary>
        public int? ShareTotal { get; set; }
        /// <summary>
        /// 集客总计
        /// </summary>
        public int? TotalSetOff { get; set; }
        /// <summary>
        /// 新增集客总计
        /// </summary>
        public int? AddTotalSetOff { get; set; }
        /// <summary>
        /// 上面放块列表
        /// </summary>
        public List<SetOffTool> roletoolsources { get; set; }
        /// <summary>
        /// 下面柱状图
        /// </summary>
        public List<SetOffToolDate> lst { get; set; }

        public GetAggSetoffForToolListRD()
        {
            roletoolsources = new List<SetOffTool>();
            lst = new List<SetOffToolDate>();
        }
    }

    public class GetAggSetoffForToolByConditionListRD : IAPIResponseData
    {
        public int TotalPageCount { get; set; }
        public int TotalCount { get; set; }

        public List<AggSetoffForToolByConditionList> aggsetoffforToollist { get; set; }
    }

    public class AggSetoffForToolByConditionList
    {
        public string ID { get; set; }
        public string SetoffRole { get; set; }
        public string ObjectName { get; set; }
        public string ShareCount { get; set; }

        public string SetoffCount { get; set; }

        public string OrderAmount { get; set; }
    }

    public class SetOffTool
    {
        /// <summary>
        /// (CTW=创意仓库、Coupon=优惠券、SetoffPoster=集客报、Goods=商品)
        /// </summary>
        public string SetoffRole { get; set; }
        /// <summary>
        /// 分享次数
        /// </summary>
        public int? ShareCount { get; set; }
        /// <summary>
        /// 集客数量
        /// </summary>
        public int? SetoffCount { get; set; }
        /// <summary>
        /// 与上一个时间节点的集客数量差
        /// </summary>
        public int? DiffCount { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="setoffrole">角色类型  (CTW=创意仓库、Coupon=优惠券、SetoffPoster=集客报、Goods=商品)</param>
        /// <param name="sharecount">分享次数</param>
        /// <param name="setoffcount">集客个数</param>
        public SetOffTool(string setoffrole, int? sharecount, int? setoffcount, int? diffcount)
        {
            this.SetoffRole = setoffrole;
            this.ShareCount = sharecount;
            this.SetoffCount = setoffcount;
            this.DiffCount = diffcount;
        }
        public SetOffTool()
        {

        }
    }
    public class SetOffToolDate
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string datetime { get; set; }
        /// <summary>
        /// 柱状图列表 数据
        /// </summary>
        public List<SetOffToolContent> RoleContent { get; set; }
        public SetOffToolDate()
        {
            RoleContent = new List<SetOffToolContent>();
        }
    }
    /// <summary>
    /// 柱状图 1个时间下面有三条信息
    /// </summary>
    public class SetOffToolContent
    {
        /// <summary>
        /// （CTW：创意仓库、Coupon：优惠券、SetoffPoster：集客报、Goods：商品）
        /// </summary>
        public string SetoffRoleId { get; set; }

        /// <summary>
        /// 集客人数
        /// </summary>
        public int? PeopleCount { get; set; }
        /// <summary>
        /// 分享次数
        /// </summary>
        public int? ShareCount { get; set; }
    }
}