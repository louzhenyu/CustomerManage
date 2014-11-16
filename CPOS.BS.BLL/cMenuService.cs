using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace JIT.CPOS.BS.BLL
{
    public class CMenuService : BaseService
    {
        JIT.CPOS.BS.DataAccess.CMenuService cMenuService = null;
        #region 构造函数
        public CMenuService(LoggingSessionInfo loggingSessionInfo)
        {
            base.loggingSessionInfo = loggingSessionInfo;
            cMenuService = new DataAccess.CMenuService(loggingSessionInfo);
        }
        #endregion
       

        public bool SetMenuInfo(string strXML, string Customer_Id, bool IsTran)
        {
            try
            {
                if (strXML.Equals(""))
                {
                    throw new Exception("菜单字符窜为空失败");
                }
                if (Customer_Id.Equals(""))
                {
                    throw new Exception("客户标识为空为空失败");
                }
                IList<MenuModel> menuInfoList = new List<MenuModel>();
                //反序列化
                try
                {
                    menuInfoList = (IList<MenuModel>)cXMLService.Deserialize(strXML, typeof(List<MenuModel>));
                }
                catch (Exception ex1) { throw (ex1); }
                foreach (MenuModel menuInfo in menuInfoList)
                {
                    menuInfo.customer_id = Customer_Id;
                }
                //转成hash
                //var args = new Hashtable
                //       {
                //           {"MenuModels", menuInfoList}
                //       };
             
                try
                {
                    return cMenuService.SetMenuListInfo(menuInfoList);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }
}
