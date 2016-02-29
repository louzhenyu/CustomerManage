using JIT.CPOS.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JIT.Utility.ExtensionMethod;
using System.Data;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;
/********************************************************************************

    * 创建时间: 2014-12-8 10:40:14
    * 作    者：donal
    * 说    明：我的小店
    * 修改时间：2014-12-8 10:40:14
    * 修 改 人：donal

*********************************************************************************/
namespace JIT.CPOS.Web.ApplicationInterface.VipStore
{
    /// <summary>
    /// 我的小店接口
    /// </summary>
    public class VipStoreGateway : BaseGateway
    {

        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "VipStoreAddItem":
                    rst = VipStoreAddItem(pRequest);
                     break;

                case "VipStoreDelItem":
                    rst = VipStoreDelItem(pRequest);
                    break;
                case "VipStoreInfo":
                    rst = VipStoreInfo(pRequest);
                    break;
                case "GetWeixinShareUrl":
                    rst = GetWeixinShareUrl(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的Action方法。", pAction));
            }
            return rst;
        }

       


        #region [ 方法 ]

        /// <summary>
        /// 我的小店增加商品
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string VipStoreAddItem(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<VipStoreRP>>();
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

           
            //先判断商品是否存在
            if (string.IsNullOrWhiteSpace(rp.Parameters.ItemID))
            {
                return new ErrorResponse(500, "没有添加商品信息").ToJSON();
            }

            //要传入的商品id组
            string[] items = rp.Parameters.ItemID.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries);

            
            //给用户增加商品
            VipStoreBLL vipStoreBlll = new VipStoreBLL(loggingSessionInfo);
           


            List<string> arrItem = new List<string>();

            for (int i = 0; i < items.Length; i++)
			{
                string itemID = items[i];
			    VipStoreEntity vipstoreEntityO = vipStoreBlll.QueryByEntity(
                    new VipStoreEntity()
                    {
                        ItemID = itemID,
                        VIPID = rp.UserID
                    },
                    null
                ).FirstOrDefault();
                
                //如果加入该商品就加入
                if (vipstoreEntityO == null&&!arrItem.Contains(itemID))
                {
                    arrItem.Add(itemID);
                }
			}

            IDbTransaction tran = new JIT.CPOS.BS.DataAccess.Base.TransactionHelper(loggingSessionInfo).CreateTransaction();

            foreach (var item in arrItem)
            {
                VipStoreEntity vipStoreEntity = new VipStoreEntity()
                {
                    vipStoreID = Guid.NewGuid().ToString(),
                    VIPID = rp.UserID,
                    ItemID = item,
                    SoldCount = 0
                };

                vipStoreBlll.Create(vipStoreEntity, tran);
            }            

            tran.Commit();

            //用户是否开通小店
            VipBLL vipBll = new VipBLL(loggingSessionInfo);

            VipEntity vipEntity = vipBll.GetByID(rp.UserID);
            vipEntity.IsSotre = vipEntity.IsSotre ?? 0;
            if (vipEntity !=null && vipEntity.IsSotre == 0)
            {
                vipEntity.IsSotre = 1;
                vipBll.Update(vipEntity);
            }

            return new SuccessResponse<IAPIResponseData>().ToJSON();
        }

        /// <summary>
        /// 删除小店商品
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string VipStoreDelItem(string pRequest)
        {
            //删除用户商品
            var rp = pRequest.DeserializeJSONTo<APIRequest<VipStoreRP>>();
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            VipStoreBLL vipStoreBlll = new VipStoreBLL(loggingSessionInfo);
            VipStoreEntity vipstoreEntity = vipStoreBlll.QueryByEntity(
                    new VipStoreEntity()
                    {
                        ItemID = rp.Parameters.ItemID,
                        VIPID=rp.UserID
                    },
                    null
                ).FirstOrDefault();
            if (vipstoreEntity!=null)
            {                
                vipStoreBlll.Delete(vipstoreEntity);
            }

            return new SuccessResponse<IAPIResponseData>().ToJSON();
        }


        /// <summary>
        /// 我的小店统计
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        private string VipStoreInfo(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            try
            {
                VipStoreBLL vipStoreBll = new VipStoreBLL(loggingSessionInfo);

                DataSet dt = vipStoreBll.VipStoreInfo(rp.UserID);
                var rd = DataTableToObject.ConvertToObject<VipStoreInfoRD>(dt.Tables[0].Rows[0]);
                var rsp = new SuccessResponse<IAPIResponseData>(rd);
                return rsp.ToJSON();
            }
            catch (Exception)
            {
                return new ErrorResponse() { ResultCode = 500, Message = "查询错误" }.ToJSON();
            }
        }


        private string GetWeixinShareUrl(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EmptyRequestParameter>>();
            LoggingSessionInfo loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");

            try
            {
                CustomerBasicSettingBLL CustomerBasicSettingBll = new CustomerBasicSettingBLL(loggingSessionInfo);

                string url = CustomerBasicSettingBll.GetSettingValueByCode("ShareWeixinPage");

                var rsp = new SuccessResponse<IAPIResponseData>();
                rsp.Message = url;
                return rsp.ToJSON();
            }
            catch (Exception)
            {
                return new ErrorResponse() { ResultCode = 500, Message = "查询错误" }.ToJSON();
            }
        }

        #endregion
    }

    #region [ 请求/返回参数 ]
    public class VipStoreRP : IAPIRequestParameter
    {
        public string ItemID { get; set; }

        public void Validate()
        {
        }
    }

    public class VipStoreInfoRD : IAPIResponseData
    {
        /// <summary>
        /// 小店商品
        /// </summary>
        public Int64 StoreCount { get; set; }
        /// <summary>
        /// 新增商品
        /// </summary>
        public Int64 RecentStoreCount { get; set; }
        /// <summary>
        /// 我的订单
        /// </summary>
        public Int64 OrderCount { get; set; }
        /// <summary>
        /// 我的收入
        /// </summary>
        public Int64 AmountCount { get; set; }
        /// <summary>
        /// 邀请小伙伴
        /// </summary>
        public Int64 SetoffUserCount { get; set; }
        /// <summary>
        /// 榜单
        /// </summary>
        public Int64 Ranking { get; set; }
    }

    #endregion
}