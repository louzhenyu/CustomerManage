
using JIT.CPOS.BS.BLL;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.DTO.Module.Event.Bargain.Request;
using JIT.CPOS.DTO.Module.Event.Bargain.Response;
using JIT.CPOS.Web.ApplicationInterface.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace JIT.CPOS.Web.ApplicationInterface.Module.WEvent.Bargain
{
    public class GetKJEventJoinListAH : BaseActionHandler<GetKJEventJoinListRP, GetKJEventJoinListRD>
    {
        protected override GetKJEventJoinListRD ProcessRequest(APIRequest<GetKJEventJoinListRP> pRequest)
        {
            var rp = pRequest.Parameters;
            var rd = new GetKJEventJoinListRD();
            var Bll = new PanicbuyingKJEventJoinBLL(CurrentUserInfo);

            DataSet ds = Bll.GetKJEventJoinList(pRequest.UserID, rp.PageIndex, rp.PageSize);
            if (ds.Tables[0].Rows.Count > 0)
            {
                rd.KJEventJoinInfoList = new List<KJEventJoinInfo>();
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    var Data = new KJEventJoinInfo();
                    Data.KJEventJoinId = dr["KJEventJoinId"].ToString();
                    Data.ItemName = dr["item_name"].ToString();
                    Data.Price = Convert.ToDecimal(dr["MinPrice"]);
                    Data.BasePrice = Convert.ToDecimal(dr["MinBasePrice"]);
                    Data.ItemId = dr["item_id"].ToString();
                    Data.Qty = Convert.ToInt32(dr["Qty"]) - Convert.ToInt32(dr["SoldQty"]);
                    Data.EventId=dr["EventId"].ToString();
                    Data.SkuID = dr["SkuID"].ToString();
                    Data.VipId = dr["VipId"].ToString();
                    if (dr["SalesPrice"] != DBNull.Value)
                        Data.SalesPrice = Convert.ToDecimal(dr["SalesPrice"]);
                    else
                        Data.SalesPrice = 0;
                    //
                    int Status = Convert.ToInt32(dr["EventStatus"]);
                    DateTime StareTime = Convert.ToDateTime(dr["BeginTime"]);
                    DateTime KJStareTime = Convert.ToDateTime(dr["CreateTime"]);
                    DateTime EndTime = Convert.ToDateTime(dr["EndTime"]);
                    int BargaingingInterval = Convert.ToInt32(dr["BargaingingInterval"]);
                    Data.Status = GetSatus(Status, StareTime, KJStareTime, EndTime, BargaingingInterval);
                    Data.ItemImageURL = Bll.GetItemImageURL(dr["item_id"].ToString());//获取商品图片URL
                    //
                    rd.KJEventJoinInfoList.Add(Data);
                }
            }

            int remainder = 0;
            rd.TotalCount = Convert.ToInt32(ds.Tables[1].Rows[0]["CountNum"]);
            rd.TotalPageCount = Math.DivRem(rd.TotalCount, rp.PageSize, out remainder);
            if (remainder > 0)
                rd.TotalPageCount += 1;

            return rd;
        }

        private int GetSatus(int pStatus, DateTime StareTime, DateTime KJStareTime, DateTime EndTime, int BargaingingInterval)
        {
            int status = 0;
            if (pStatus == 20)
            {
                var NowTime = DateTime.Now;
                if (NowTime >= StareTime && NowTime <= KJStareTime.AddHours(BargaingingInterval))
                {//进行中
                    status = 1;
                }
                if (NowTime >= KJStareTime.AddHours(BargaingingInterval) && NowTime <= EndTime)
                {//可砍时间已到
                    status = 2;
                }
                if (NowTime > EndTime)
                {//已结束
                    status = 3;
                }

            }
            else
            {//提前结束
                status = 3;
            }
            return status;
        }
    }
}