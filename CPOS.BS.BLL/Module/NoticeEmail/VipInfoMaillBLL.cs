using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.BLL.NoticeEmail;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;

namespace JIT.CPOS.BS.BLL.Module.NoticeEmail
{
    public class VipInfoMaillBLL
    {
        private LoggingSessionInfo CurrentUserInfo;
        private VipDAO _currentDAO;

        public VipInfoMaillBLL(LoggingSessionInfo pUserInfo)
        {
            this.CurrentUserInfo = pUserInfo;
            this._currentDAO = new VipDAO(pUserInfo);
        }

        public bool SendNoticeMail(string pEmail,string pContent)
        {
            return NoticeMailSender<MailDetailTemplate>.SendMail(pEmail, "会员注册通知",pContent);
        }
    }
}
