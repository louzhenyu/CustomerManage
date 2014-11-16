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
    /// 实体： 拜访执行明细查看 (某人某天在某个店的产品检查明细)
    /// </summary>
    public class VisitingTaskDetailDataViewEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingTaskDetailDataViewEntity()
        {
        }
        #endregion     

        #region 属性集
        /// <summary>
        /// 自动编号
        /// </summary>
        public string VisitingTaskDetailDataID { get; set; }

        /// <summary>
        /// 人员标识
        /// </summary>
        public int? ClientUserID { get; set; } 

        /// <summary>
        /// 进店时间
        /// </summary>
        public DateTime? InTime { get; set; }

        /// <summary>
        /// 拜访步骤标识
        /// </summary>
        public string VisitingTaskStepID { get; set; }

        /// <summary>
        /// 拜访步骤类别
        /// </summary>
        public int? StepType { get; set; }

        /// <summary>
        /// 拜访步骤名称
        /// </summary>
        public string StepName { get; set; }

        /// <summary>
        /// 拜访参数标识
        /// </summary>
        public string VisitingParameterID { get; set; }

        /// <summary>
        /// 拜访参数类型(1-产品相关,2-品牌相关等)
        /// </summary>
        public int? ParameterType { get; set; }

        /// <summary>
        /// 拜访参数名称
        /// </summary>
        public string ParameterName { get; set; }

        /// <summary>
        /// 拜访参数英文名称
        /// </summary>
        public string ParameterNameEn { get; set; }

        /// <summary>
        /// 参数序号
        /// </summary>
        public int? ParameterOrder { get; set; }

        /// <summary>
        /// 控件类型
        /// </summary>
        public int? ControlType { get; set; }

        /// <summary>
        /// 控件名称
        /// </summary>
        public string ControlName { get; set; }

        /// <summary>
        /// 参数所属对象标识
        /// </summary>
        public string ObjectID { get; set; }

        /// <summary>
        /// 拜访对象编号(关联门店/产品/品牌/类别等表)
        /// </summary>
        public string Target1ID { get; set; }

        /// <summary>
        /// 拜访对象编号(用于拜访对象为品类的步骤:品牌/类别等表)
        /// </summary>
        public string Target2ID { get; set; }

        /// <summary>
        /// 参数采集到的值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 参数所属对象的名称
        /// 取决于StepType：
        /// 1：SKUName
        /// 2：BrandName
        /// 3：CategoryName
        /// 4：ClientPositionName
        /// </summary>
        public string ObjectName1 { get; set; }

        /// <summary>
        /// 参数所属对象的名称2
        /// 取决于StepType：
        /// 1：【空值】
        /// 2：CategoryName或者【空值】
        /// 3：BrandName或者【空值】
        /// 4：ClientUserName
        /// </summary>
        public string ObjectName2 { get; set; }
        #endregion

    }
}