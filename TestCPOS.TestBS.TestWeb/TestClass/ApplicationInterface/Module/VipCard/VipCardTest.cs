using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VIPCard;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Module.VIP.VIPCardManage.Response;
namespace JIT.TestCPOS.TestBS.TestWeb.TestClass.ApplicationInterface.Module.VipCard
{
    [TestFixture]
    public class VipCardTest
    {

        VipCardBLL c_VipCardBLL;
        List<IWhereCondition> c_complexCondition;
        List<OrderBy> c_lstOrder;
        LoggingSessionInfo c_LoginSession;
        GetVipCardListAH c_GetVipCardListAH;
        [SetUp]
        public void SetUp()
        {
            #region 用户登陆信息
            c_LoginSession = new LoggingSessionInfo();
            c_LoginSession.Conn = @"user id=dev;password=JtLaxT7668;data source=182.254.219.83,3433;database=cpos_bs_alading;";
            LoggingManager m_Data = new LoggingManager();
            m_Data.Connection_String = @"user id=dev;password=JtLaxT7668;data source=182.254.219.83,3433;database=cpos_bs_alading;";
            c_LoginSession.CurrentLoggingManager = m_Data;
            #endregion

            c_GetVipCardListAH = new GetVipCardListAH();
            //c_VipCardBLL = new VipCardBLL(c_LoginSession);
            //c_complexCondition = new List<IWhereCondition>();
            //c_lstOrder = new List<OrderBy>();
        }
        /// <summary>
        /// 测试会员卡管理列表查询
        /// </summary>
        [Test]
        public void GetVipCardListTest()
        {
            //var m_Result = c_VipCardBLL.GetVipCardList(c_complexCondition.ToArray(), c_lstOrder.ToArray(), 10, 1);
        }
    }
}
