/*
 * Author		:Yuangxi
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/11 14:18:56
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
using System.Text;
using JIT.Utility;
using JIT.Utility.Entity;

namespace JIT.CPOS.BS.Entity
{
    /// <summary>
    /// 实体： 拜访执行明细查看 (一个人一天所有的走店信息明细)
    /// </summary>
    public class VisitingTaskDataViewEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingTaskDataViewEntity()
        {
        }
        #endregion     

        #region 属性集
        /// <summary>
        /// 自动编号
        /// </summary>
        public string VisitingTaskDataID { get; set; }

        /// <summary>
        /// 用户标识
        /// </summary>
        public string ClientUserID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string ClientUserName { get; set; }
        
        /// <summary>
        /// 拜访对象编号(关联门店/经销商等)
        /// </summary>
        public string POPID { get; set; }
        /// <summary>
        /// 终端类型
        /// </summary>
        public int POPType { get; set; }
        /// <summary>
        /// 门店名称(拜访对象名称)
        /// </summary>
        public string POPName { get; set; }
        /// <summary>
        /// 进店时间
        /// </summary>
        public DateTime? InTime { get; set; }

        /// <summary>
        /// 出店时间
        /// </summary>
        public DateTime? OutTime { get; set; }
        
        /// <summary>
        /// 走店日期(dayof(进店时间))
        /// </summary>
        public DateTime? ExecutionTime { get; set; }

        /// <summary>
        /// 店内时间(分钟)
        /// </summary>
        public int? WorkingHoursIndoor { get; set; }

        /// <summary>
        /// 路途时间(本次进店-上次出店)
        /// </summary>
        public int? WorkingHoursJourneyTime { get; set; }

        /// <summary>
        /// 总时间(店内时间+路途时间)
        /// </summary>
        public int? WorkingHoursTotal { get; set; }

        /// <summary>
        /// 定位坐标信息:进店定位坐标
        /// </summary>
        public string InCoordinate { get; set; }

        /// <summary>
        /// 定位坐标信息:出店定位坐标
        /// </summary>
        public string OutCoordinate { get; set; }

        /// <summary>
        /// 进店照片
        /// </summary>
        public string InPic { get; set; }

        /// <summary>
        /// 出店照片
        /// </summary>
        public string OutPic { get; set; }

        #endregion

    }
}