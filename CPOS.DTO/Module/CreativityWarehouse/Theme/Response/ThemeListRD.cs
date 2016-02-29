using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.CreativityWarehouse.Theme.Response
{
    public class ThemeListRD: IAPIResponseData
    {
        public int TotalCount { get;set;}
        public int PageCount { get; set; } 
        public List<T_CTW_LEventThemeEntity> ThemeList { get; set; }
    }

}
