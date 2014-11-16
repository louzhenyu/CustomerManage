using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL;

namespace JIT.CPOS.Web.ApplicationInterface.Product.MobileLibrary.ActionHandler
{
    /// <summary>
    /// SetFavorite的摘要说明
    /// </summary>
    [Export(typeof(ILibraryRequestHandler))]
    [ExportMetadata("Action", "SetFavorite")]
    public class SetFavoriteHandler : ILibraryRequestHandler
    {
        public string DoAction(string pRequest)
        {
            return SetFavorite(pRequest);
        }

        public string SetFavorite(string pRequest)
        {
            var rd = new APIResponse<SetFavoriteRD>();
            var rdData = new SetFavoriteRD();
            var rp = pRequest.DeserializeJSONTo<APIRequest<SetFavoriteRP>>();

            if (rp.Parameters == null)
                throw new ArgumentException();

            if (rp.Parameters != null)
                rp.Parameters.Validate();

            var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, rp.UserID);
            try
            {
                ItemKeepBLL itemBll = new ItemKeepBLL(loggingSessionInfo);
                ItemKeepEntity entity = itemBll.GetItemKeepByUser(rp.Parameters.OnlineCourseID, rp.UserID);
                //加入收藏
                if (rp.Parameters.IsFavorite.Equals("1"))
                {
                    if (entity == null)
                    {
                        entity = new ItemKeepEntity()
                        {
                            ItemKeepId = Guid.NewGuid().ToString().Replace("-", ""),
                            ItemId = rp.Parameters.OnlineCourseID,
                            VipId = rp.UserID,
                            KeepStatus = 1,
                            ItemType = 1//默认1，保留字段
                        };
                        itemBll.Create(entity);
                    }
                    else
                    {
                        if (entity.KeepStatus != 1)
                        {
                            entity.KeepStatus = 1;
                            itemBll.Update(entity);
                        }
                    }
                }
                else //取消收藏
                {
                    if (entity != null)
                    {
                        entity.KeepStatus = 0;
                        itemBll.Update(entity);
                    }
                }
                rd.ResultCode = 0;
                rdData.IsSuccess = true;
            }
            catch (Exception ex)
            {
                rd.ResultCode = 103;
                rd.Message = ex.Message;
            }
            rd.Data = rdData;
            return rd.ToJSON();
        }
    }

    #region 设置收藏
    public class SetFavoriteRP : IAPIRequestParameter
    {
        public string OnlineCourseID { set; get; }
        public string IsFavorite { set; get; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(OnlineCourseID)) throw new APIException("OnlineCourseID不能为空") { ErrorCode = 102 };
            if (string.IsNullOrEmpty(IsFavorite)) throw new APIException("IsFavorite不能为空") { ErrorCode = 102 };
        }
    }
    public class SetFavoriteRD : IAPIResponseData
    {
        public bool IsSuccess { set; get; }
    }
    #endregion
}