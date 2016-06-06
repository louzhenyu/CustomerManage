using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.DTO.Module.VIP.VipGold.Request
{
    public class SendNoticeRP : IAPIRequestParameter
    {
        public List<NoticeInfo> NoticeInfoList { get; set; }
        public void Validate()
        {

        } 
    }

    public class NoticeInfo{
        /// <summary>
        /// 1：微信用户 2：APP员工
        /// </summary>
        public int NoticePlatformType{get;set;}
        public string SetoffEventID{get;set;}
        public string Title { get; set; }
        public string Text{get;set;}
    }
}
