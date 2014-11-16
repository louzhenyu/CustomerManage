/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/3/21 14:52:49
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
    /// 实体： 拜访明细数据 
    /// </summary>
    public partial class VisitingTaskDetailDataEntity : BaseEntity 
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingTaskDetailDataEntity()
        {
        }
        #endregion     

        #region 属性集
		/// <summary>
		/// 自动编号
		/// </summary>
		public Guid? VisitingTaskDetailDataID { get; set; }

		/// <summary>
		/// 拜访数据编号(关联VisitingTaskData表)
		/// </summary>
		public Guid? VisitingTaskDataID { get; set; }

		/// <summary>
		/// 拜访步骤编号(关联VisitingTaskStep表)
		/// </summary>
		public Guid? VisitingTaskStepID { get; set; }

		/// <summary>
		/// 拜访对象编号(关联VisitingTaskStepObject表)
		/// </summary>
		public Guid? ObjectID { get; set; }

		/// <summary>
		/// 拜访对象编号(关联门店/产品/品牌/类别等表,拜访对象为一个时值存于该字段)
		/// </summary>
		public string Target1ID { get; set; }

		/// <summary>
		/// 拜访对象编号(当对象为两个时,存放第二个对象)
		/// </summary>
		public string Target2ID { get; set; }

		/// <summary>
		/// 拜访参数编号(关联VisitingParameter表)
		/// </summary>
		public Guid? VisitingParameterID { get; set; }

		/// <summary>
		/// 拜访参数值
		/// </summary>
		public string Value { get; set; }

		/// <summary>
		/// 数据提交时间
		/// </summary>
		public DateTime? SubmitTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string ClientID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? ClientDistributorID { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string CreateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string LastUpdateBy { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public DateTime? LastUpdateTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public int? IsDelete { get; set; }


        #endregion

    }
}