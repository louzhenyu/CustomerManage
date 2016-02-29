using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace JIT.CPOS.DTO.Module.Basic.Menu.Response
{
    public class GetMenuListRD:IAPIResponseData
    {
        public IList<MenuModel> MenuList;
    }
}
