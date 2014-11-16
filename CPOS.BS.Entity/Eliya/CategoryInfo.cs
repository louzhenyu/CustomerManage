using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity.Eliya
{
    public class CategoryInfo
    {
        public string CagegoryID { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string TargetUrl { get; set; }
        public ItemInfo[] ShowItems { get; set; }
    }
}
