﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.Web.ApplicationInterface.Base;
using JIT.CPOS.DTO.Module.Event.Lottery.Request;
using JIT.CPOS.DTO.Module.Event.Lottery.Response;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
using JIT.Utility.Log;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;
namespace JIT.CPOS.Web.ApplicationInterface.Module.Event.Lottery
{
    public class ShareAH : BaseActionHandler<LotteryRP, LotteryRD>
    {
        protected override LotteryRD ProcessRequest(DTO.Base.APIRequest<LotteryRP> pRequest)
        {
            var rd = new LotteryRD();//返回值
            var bllPrize=new LPrizesBLL(this.CurrentUserInfo);
            var para = pRequest.Parameters;

            if(para.EventId!=null && para.EventId.ToString()!="")
            {
                try
                {

                    rd = bllPrize.CheckIsWinnerForShare(pRequest.UserID, para.EventId);

                }
                catch (Exception ex)
                {

                    rd.ResultMsg = ex.Message.ToString();
                }
            }
            else
            {
                rd.ResultMsg = "参数EventId有误";

            }



            return rd;
        }
    }
}