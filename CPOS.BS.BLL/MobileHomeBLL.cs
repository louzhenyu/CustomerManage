
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.DataAccess.Base;

namespace JIT.CPOS.BS.BLL
{
    public partial class MobileHomeBLL
    {
        /// <summary>
        /// 更新商户下所有主页的状态为非激活状态
        /// </summary>
        /// <param name="pEntity"></param>
        public void UpdateIsActivate(MobileHomeEntity pEntity)
        {
            _currentDAO.UpdateIsActivate(pEntity);
        }
       /// <summary>
        /// 根据模板Id生成新实例数据
       /// </summary>
       /// <param name="strHomeId"></param>
        public void CreateStoreDataFromTemplate(string strHomeId, string strTemplateId)
        {
            _currentDAO.CreateStoreDataFromTemplate(strHomeId, strTemplateId);
        }
    }
}
