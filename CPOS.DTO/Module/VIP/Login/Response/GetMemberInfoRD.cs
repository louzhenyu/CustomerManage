using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.DTO.Module.VIP.Login.Response
{
    public class GetMemberInfoRD : IAPIResponseData
    {
        /// <summary>
        /// 会员信息
        /// </summary>
        public MemberInfo MemberInfo { get; set; }
        public TagsInfo[] IdentityTagsList { get; set; }

        public MemberControlInfo[] MemberControlList { get; set; }
        public List<JIT.CPOS.DTO.Module.VIP.Login.Response.OrderInfo> OrderList { get; set; }
    }

    public class MemberControlInfo
    {
        public string ColumnDesc { get; set; }
        public string ColumnName { get; set; }
        public string ColumnValue { get; set; }
        public int ControlType { get; set; }
        //public int DisplayIndex { get; set; }
    }

    public class TagsInfo
    {
        public string TagsId { get; set; }
        public string TagsName { get; set; }
        public string TagsDesc { get; set; }
        public string TagsFormula { get; set; }
        public string TypeId { get; set; }
        public string TypeName { get; set; }

    }
    public class OrderInfo
    {
        public string order_date { get; set; }
        public string order_id { get; set; }
        public string order_no { get; set; }


        public IList<InoutDetailInfo> DetailList { get; set; }

    }


}
