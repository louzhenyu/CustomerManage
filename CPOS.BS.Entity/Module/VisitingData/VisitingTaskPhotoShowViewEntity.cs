/*
 * Author		:tiansheng.zhu@jitmarketing.cn
 * EMail		:
 * Company		:JIT
 * Create On	:2013/5/29 10:51:59
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
    /// 实体： VisitingTaskPhotoShowViewEntity 
    /// </summary>
    public class VisitingTaskPhotoShowViewEntity : BaseEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingTaskPhotoShowViewEntity()
        {
        }
        #endregion

        #region 属性集

        /// <summary>
        /// ClientUserID
        /// </summary>
        public string ClientUserID { get; set; }

        /// <summary>
        /// InCoordinate
        /// </summary>
        public string InCoordinate { get; set; }

        /// <summary>
        /// ClientUserName
        /// </summary>
        public string ClientUserName { get; set; }


        /// <summary>
        /// PositionName
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// ObjectName
        /// </summary>
        public string ObjectName { get; set; } 

        /// <summary>
        /// StepName
        /// </summary>
        public string StepName { get; set; }    

        /// <summary>
        /// POPName
        /// </summary>
        public string POPName { get; set; } 

        /// <summary>
        /// 采集参数,照片类型名称,进店就是进店照，出店就是出店照
        /// </summary>
        public string PhotoName { get; set; }     

        /// <summary>
        /// OutCoordinate
        /// </summary>
        public string OutCoordinate { get; set; }  

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// PhotoDate
        /// </summary>
        public DateTime? PhotoDate { get; set; }

        /// <summary>
        /// PhotoDateTime
        /// </summary>
        public DateTime? PhotoDateTime { get; set; }

        /// <summary>
        /// PhotoTime
        /// </summary>
        public DateTime? PhotoTime { get; set; }

        /// <summary>
        /// PhotoUrl
        /// </summary>
        public string PhotoUrl { get; set; } 

         
        #endregion
    }

    /// <summary>
    /// 实体： VisitingTaskPhotoShowViewEntity 
    /// </summary>
    public class VisitingTaskPhotoDataViewEntity : BaseEntity
    {
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public VisitingTaskPhotoDataViewEntity()
        {
        }
        #endregion

        #region 属性集
        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Direction
        /// </summary>
        public int? Direction { get; set; }

        /// <summary>
        /// FileName
        /// </summary>
        public string FileName { get; set; }


        #endregion
    }
}