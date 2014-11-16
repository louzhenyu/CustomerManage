/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/2/27 11:48:18
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
    /// 实体： 拜访数据明细(参数定义) 
    /// </summary>
    public class VisitingParameterViewEntity : VisitingParameterEntity 
    {
        //use for listview
        public string ParameterTypeText { get; set; }
        public string ControlTypeText { get; set; }

        //use for select(step object)
        public Guid? MappingID { get; set; }
        public int? ParameterOrder { get; set; }

        //use for stepselect
        public Guid? VisitingTaskStepID { get; set; }

        //use for objectselect
        public Guid? VisitingObjectID { get; set; }
    }
}