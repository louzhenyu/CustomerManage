using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Response
{
    public class GetAggSetoffForSourceListRD : IAPIResponseData
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
        public List<SetOffRoleSources> roletoolsources { get; set; }
        /// <summary>
        /// 下面柱状图
        /// </summary>
        public List<SetOffSourcesDate> lst { get; set; }

        public GetAggSetoffForSourceListRD()
        {
            roletoolsources = new List<SetOffRoleSources>();
            lst = new List<SetOffSourcesDate>();
        }
    }

    public class GetAggSetoffForSourceByConditionListRD : IAPIResponseData
    {
        public int TotalPageCount { get; set; }
        public int TotalCount { get; set; }

        public List<AggSetoffForSourceByConditionList> aggsetoffforSourcelist { get; set; }
    }

    public class AggSetoffForSourceByConditionList
    {
        public string ID { get; set; }
        public string SetoffRole { get; set; }
        public string UnitName { get; set; }
        public string UserName { get; set; }
        public string PushMessageCount { get; set; }
        public string ShareCount { get; set; }
        public string SourcesName { get; set; }

        public string SetoffCount { get; set; }

        public string OrderAmount { get; set; }
    }

    public class SetOffRoleSources
    {
        /// <summary>
        /// 1=员工, 2=客服, 3=会员
        /// </summary>
        public int SetoffRole { get; set; }
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
        /// <param name="setoffrole">角色类型  1=员工, 2=客服, 3=会员</param>
        /// <param name="sharecount">分享次数</param>
        /// <param name="setoffcount">集客个数</param>
        public SetOffRoleSources(int setoffrole, int? sharecount, int? setoffcount, int? diffcount)
        {
            this.SetoffRole = setoffrole;
            this.ShareCount = sharecount;
            this.SetoffCount = setoffcount;
            this.DiffCount = diffcount;
        }
        public SetOffRoleSources()
        {

        }
    }
    public class SetOffSourcesDate
    {
        /// <summary>
        /// 日期
        /// </summary>
        public string datetime { get; set; }
        /// <summary>
        /// 柱状图列表 数据
        /// </summary>
        public List<SetOffSourcesContent> RoleContent { get; set; }
        public SetOffSourcesDate()
        {
            RoleContent = new List<SetOffSourcesContent>();
        }
    }
    /// <summary>
    /// 柱状图 1个时间下面有三条信息
    /// </summary>
    public class SetOffSourcesContent
    {
        /// <summary>
        /// 1：员工  2：客服 3：会员
        /// </summary>
        public int SetoffRoleId { get; set; }

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
