using System;
using System.Collections.Generic;
using System.Data;
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
    public class GetImageAH : BaseActionHandler<GetImageRP, GetImageRD>
    {
        protected override GetImageRD ProcessRequest(APIRequest<GetImageRP> pRequest)
        {

            var rd = new GetImageRD();
            ObjectImagesBLL bllImage = new ObjectImagesBLL(this.CurrentUserInfo);
            LEventsBLL bllEvent = new LEventsBLL(this.CurrentUserInfo);
            var image = bllImage.QueryByEntity(new ObjectImagesEntity() { ObjectId = pRequest.Parameters.EventId ,IsDelete=0}, null).ToList();
            var eventInfo = bllEvent.GetByID(pRequest.Parameters.EventId);
            
            if (image != null)
            {
                foreach (var i in image)
                {
                    if (i.BatId == "BackGround")
                        rd.BackGround = i.ImageURL;
                    if (i.BatId == "BeforeGround")
                        rd.BeforeGround = i.ImageURL;
                    if (i.BatId == "Logo")
                        rd.Logo = i.ImageURL;
                    if (i.BatId == "Rule")
                        rd.Rule = i.ImageURL;
                    if (i.BatId == "LT_kvPic")
                        rd.LT_kvPic = i.ImageURL;
                    if (i.BatId == "LT_Rule")
                        rd.LT_Rule = i.ImageURL;
                    if (i.BatId == "LT_bgpic1")
                        rd.LT_bgpic1 = i.ImageURL;
                    if (i.BatId == "LT_bgpic2")
                        rd.LT_bgpic2 = i.ImageURL;
                    if (i.BatId == "LT_regularpic")
                        rd.LT_regularpic = i.ImageURL;
                };



                rd.ImageList = bllImage.QueryByEntity(new ObjectImagesEntity() { ObjectId = pRequest.Parameters.EventId, BatId = "list", IsDelete = 0 }, null).ToList();
            }

            rd.EventTitle = eventInfo.Title;
            rd.EventContent = eventInfo.Content;
            rd.BootUrl = eventInfo.BootURL;
            rd.ShareRemark = eventInfo.ShareRemark;
            rd.PosterImageUrl = eventInfo.PosterImageUrl;
            rd.OverRemark = eventInfo.OverRemark;
            rd.ShareLogoUrl = eventInfo.ShareLogoUrl;
            rd.IsShare = eventInfo.IsShare == null ? 0 : (int)eventInfo.IsShare;

            //LLotteryLogEntity lotteryEntity = null;
            //LLotteryLogBLL bllLottery = new LLotteryLogBLL(this.CurrentUserInfo);

            LPrizeWinnerBLL bllPrizeWinner = new LPrizeWinnerBLL(this.CurrentUserInfo);
            LPrizeWinnerEntity entityPrizeWinner = null;

            List<OrderBy> lstOrder = new List<OrderBy> { };
            lstOrder.Add(new OrderBy() { FieldName = " createtime", Direction = OrderByDirections.Desc });

            entityPrizeWinner = bllPrizeWinner.QueryByEntity(new LPrizeWinnerEntity() {VipID = CurrentUserInfo.UserID }, lstOrder.ToArray()).FirstOrDefault();

            switch (eventInfo.PersonCount)
            {
                case 1://仅能参加一次抽奖
                    if (entityPrizeWinner == null)
                    {
                        rd.Qualification = 1;
                    }
                    else
                    {
                        rd.Qualification = 0;
                        rd.PrizeName = entityPrizeWinner.PrizeName;
                    }
                    break;
                case 2://每天一次
                    if ((entityPrizeWinner != null && Convert.ToDateTime(entityPrizeWinner.CreateTime.ToString()).Date < DateTime.Now.Date) || entityPrizeWinner == null)
                    {
                        rd.Qualification = 1;
                    }
                    else
                    {
                        rd.Qualification = 0;
                        rd.PrizeName = entityPrizeWinner.PrizeName;
                    }
                    break;
                case 3://每周一次  最后一次获奖时间 小于周一即可
                    if ((entityPrizeWinner != null && DateTime.Now.AddDays(1 - (int)DateTime.Now.DayOfWeek) < Convert.ToDateTime(entityPrizeWinner.CreateTime.ToString()).Date) || entityPrizeWinner == null)
                    {
                        rd.Qualification = 1;
                    }
                    else
                    {
                        rd.Qualification = 0;
                        rd.PrizeName = entityPrizeWinner.PrizeName;
                    }
                    break;
                case 4://无限
                        rd.Qualification = 1;
                    break;

            }


            return rd;
        }
    }
}