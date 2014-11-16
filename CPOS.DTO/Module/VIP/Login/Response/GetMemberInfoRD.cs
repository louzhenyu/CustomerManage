using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;

namespace JIT.CPOS.DTO.Module.VIP.Login.Response
{
    public class GetMemberInfoRD : IAPIResponseData
    {
        /// <summary>
        /// 会员信息
        /// </summary>
        public MemberInfo MemberInfo { get; set; }

        public MemberControlInfo[] MemberControlList { get; set; }
    }

    public class MemberControlInfo
    {
        public string ColumnDesc { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
        public int ControlType { get; set; }
        //public int DisplayIndex { get; set; }
    }
}
