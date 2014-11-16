using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;

namespace JIT.CPOS.BS.BLL.CS
{
    public class CSPublicInterface
    {
        private LoggingSessionInfo loggingSessionInfo;
        public CSPublicInterface(LoggingSessionInfo loggingSessionInfo)
        {
            this.loggingSessionInfo = loggingSessionInfo;
        }
        /// <summary>
        /// 用户取消关注时调用的程序
        /// </summary>
        /// <param name="vipID"></param>
        public void UnSubscribe(string vipID)
        {
            ISQLHelper sqlHelper = new DefaultSQLHelper(loggingSessionInfo.Conn);
            sqlHelper.ExecuteNonQuery("DELETE FROM TimingPushMessage WHERE MemberID='" + vipID + "'");
            sqlHelper.ExecuteNonQuery("DELETE FROM TimingPushMessageVipLastRecord WHERE VIPID='" + vipID + "'");
        }
        /// <summary>
        /// 判断用户是否为活动用户，是否可以发送客服消息
        /// </summary>
        /// <param name="vipID">会员ID</param>
        /// <returns></returns>
        public bool IsWeixinCSMessageActiveUser(string vipID)
        {
            return true;
        }
    }
}
