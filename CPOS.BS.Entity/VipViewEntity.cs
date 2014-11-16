using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 会员表
    /// </summary>
    public partial class VipViewEntity : VipEntity
    {
        #region 构造函数
        public VipViewEntity() { }
        #endregion

        #region 属性集
        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 城市编号
        /// </summary>
        public string CityCode { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 会员下单状态
        /// </summary>
        public string OrdersStatus { get; set; }

        /// <summary>
        /// 订单备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 大使编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 会员爱好
        /// </summary>
        public string Hobby { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// 课程名称
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 会员审核不通过原因
        /// </summary>
        public string NotApproveReson { get; set; }

        /// <summary>
        /// 会员状态
        /// </summary>
        public string OptionText { get; set; }

        /// <summary>
        /// 前段显示会员状态
        /// </summary>
        public string OptionTextEn { get; set; }

        /// <summary>
        /// 会员状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 会员前段要跳转的页面
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 会员查询次数
        /// </summary>
        public string SearchCount { get; set; }

        /// <summary>
        /// 会员学校
        /// </summary>
        public string VipSchool { get; set; }

        /// <summary>
        /// 会员课程
        /// </summary>
        public string VipClass { get; set; }

        /// <summary>
        /// 活动会员关联ID
        /// </summary>
        public string SignUpID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Createtime { set; get; }
        #endregion
    }
}
