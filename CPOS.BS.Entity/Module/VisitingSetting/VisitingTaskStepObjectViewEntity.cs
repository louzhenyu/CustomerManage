/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/2/27 11:48:20
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
    /// 实体： 拜访步骤中的对象 
    /// </summary>
    public class VisitingTaskStepObjectViewEntity : VisitingTaskStepObjectEntity 
    {
        //brand category
        public string Remark { get; set; }

        public string ParentBrandName { get; set; }
        public string BrandName { get; set; }
        public string Firm { get; set; }

        public string ParentCategoryName { get; set; }
        public string CategoryName { get; set; }

        //position
        public string PositionName { get; set; }

        //sku
        public int? SKUID { get; set; }

        public string BrandCompany { get; set; }
    }
}