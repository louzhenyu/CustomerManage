using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.Entity
{
    public class QueryUserEntity
    {
        public string QUserName { set; get; }
        public string QUnitID { set; get; }
        public string QJobFunctionID { set; get; }
        public string QRoleID { set; get; }//默认employee的roleId

    }
}
