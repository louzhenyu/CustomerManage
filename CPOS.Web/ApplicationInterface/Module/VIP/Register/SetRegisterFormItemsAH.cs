using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.DTO.Module.VIP.Register.Request;
using JIT.CPOS.DTO.Module.VIP.Register.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System.Data;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;

namespace JIT.CPOS.Web.ApplicationInterface.Module.VIP.Register
{
    public class SetRegisterFormItemsAH : BaseActionHandler<SetRegisterFormItemsRP, SetRegisterFormItemsRD>
    {
        const int ERROR_ColumnValue_IsMustDo = 120;
        protected override SetRegisterFormItemsRD ProcessRequest(DTO.Base.APIRequest<SetRegisterFormItemsRP> pRequest)
        {
            //var validFlag = pRequest.Parameters.ValidFlag;
            //#region 需要验证


            //#region 根据userid、phone分别查询vip表，如果存在两条记录，且两条记录不相同，进行合并，以userid为主
            ////如果一条都不存在，新增一条vip信息
            ////如果两条记录相同，继续
            //#endregion
            //#region 根据表单数据，更新vip表

            //#endregion
            //#endregion

            //#region 不需要验证 
            //#region 根据userid 取vip，如果有记录更新，没有记录，反之新增
            //#endregion
            //#endregion

            SetRegisterFormItemsRD rd = new SetRegisterFormItemsRD();

            var itemlist = pRequest.Parameters.ItemList;
            //var objectId = pRequest.Parameters.ObjectId;
            //if (string.IsNullOrEmpty(objectId) || objectId == "")
            //{
            //    throw new APIException("参数【ObjectId】不能为空") { ErrorCode = 121 };
            //}


            MobileBussinessDefinedBLL bll = new MobileBussinessDefinedBLL(this.CurrentUserInfo);
            var vipBll = new VipBLL(CurrentUserInfo);
            var vip = vipBll.GetByID(pRequest.UserID);

            if (itemlist == null) return rd;
            if (vip != null)
            {
                string sql = "";
                foreach (var item in itemlist)
                {
                    if (Convert.ToBoolean(item.IsMustDo) && string.IsNullOrEmpty(item.Value))
                    {
                        throw new APIException("必填字段不能为空") {ErrorCode = ERROR_ColumnValue_IsMustDo};
                    }
                    sql += bll.GetColumnName(item.ID) + "='" + item.Value + "',";


                }
                var tableName = "vip";
                ////根据objectid获取表明
                //var tableName = bll.GetTableNameByObjectId(objectId, pRequest.CustomerID);
                //if (tableName == "" || string.IsNullOrEmpty(tableName))
                //{
                //    throw new APIException("无效的ObjectId") { ErrorCode = 122 };
                //}
                //根据表更新字段

                // bll.UpdateDynamicColumnValue(sql.Trim(','), "003e30e7121741beb749a230eebecfe0");
                bll.UpdateDynamicColumnValue(sql.Trim(','), pRequest.UserID,tableName);
            }
            else
            {
                throw new APIException("用户ID无效") {ErrorCode = 121};
            }
            return rd;
        }

    }
}