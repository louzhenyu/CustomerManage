﻿using System;
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
            LCoverBLL bllCover = new LCoverBLL(CurrentUserInfo);

            string strEventId = pRequest.Parameters.EventId;
            string strCTWEventId = string.Empty;

            rd.EventId = strEventId;
            rd.IsCTW = 0;
            //if (!string.IsNullOrEmpty(pRequest.Parameters.CTWEventId))
            //{
                T_CTW_LEventInteractionBLL bllLEventInteraction = new T_CTW_LEventInteractionBLL(this.CurrentUserInfo);
                DataSet ds = bllLEventInteraction.GetCTWLEventInteraction(strEventId);
                if (ds != null && ds.Tables[0].Rows.Count>0)
                {
                    strEventId = ds.Tables[0].Rows[0]["LeventId"].ToString();
                    strCTWEventId=ds.Tables[0].Rows[0]["CTWEventId"].ToString();
                    ContactEventBLL bllContact = new ContactEventBLL(this.CurrentUserInfo);
                    var contactList = bllContact.QueryByEntity(new ContactEventEntity() { EventId = strCTWEventId, IsCTW = 1, IsDelete = 0 }, null).ToList();
                    if(contactList.Count>0)
                    {
                        T_CTW_SpreadSettingBLL bllSpreadSetting = new T_CTW_SpreadSettingBLL(this.CurrentUserInfo);

                        if (contactList.Where(a => a.ContactTypeCode == "Reg").Count() > 0)
                        {
                            ButtonInfo reg = new ButtonInfo();
                            reg.Text = "注册有奖";
                            rd.Reg = reg;
                        }
                        if (contactList.Where(a => a.ContactTypeCode == "Share").Count() > 0)
                        {
                            ButtonInfo share = new ButtonInfo();
                            DataSet dsSpreadSetting = bllSpreadSetting.GetSpreadSettingQRImageByCTWEventId(strCTWEventId, "Share");
                            if (dsSpreadSetting != null && dsSpreadSetting.Tables.Count > 0)
                            {
                                share.Title = dsSpreadSetting.Tables[0].Rows[0]["Title"].ToString(); 
                                share.Summary = dsSpreadSetting.Tables[0].Rows[0]["Summary"].ToString();
                                share.BGImageUrl = dsSpreadSetting.Tables[0].Rows[0]["BGImageUrl"].ToString(); 

                            } 
                            share.Text = "分享有奖";
                            
                            rd.Share = share;
                        }
                        if (contactList.Where(a => a.ContactTypeCode == "Focus").Count() > 0)
                        {
                            DataSet dsSpreadSetting = bllSpreadSetting.GetSpreadSettingQRImageByCTWEventId(strCTWEventId, "Focus");
                            if (dsSpreadSetting != null && dsSpreadSetting.Tables.Count > 0)
                            {
                                ButtonInfo focus = new ButtonInfo();

                                focus.BGImageUrl = dsSpreadSetting.Tables[0].Rows[0]["BGImageUrl"].ToString();
                                focus.LeadPageQRCodeImageUrl = dsSpreadSetting.Tables[0].Rows[0]["LeadPageQRCodeImageUrl"].ToString();
                                rd.Focus = focus;
                            }
                        }
                    }
                    rd.IsCTW = 1;
                    rd.CTWEventId = strCTWEventId;
                    rd.EventId = strEventId;

                }
            //}

            var image = bllImage.QueryByEntity(new ObjectImagesEntity() { ObjectId = strEventId ,IsDelete=0}, null).ToList();
            var eventInfo = bllEvent.GetByID(strEventId);
            
            if (image.Count != 0)
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
                rd.RuleContent = image.FirstOrDefault().RuleContent;
                rd.RuleId = image.FirstOrDefault().RuleId??0;
                rd.ImageList = bllImage.QueryByEntity(new ObjectImagesEntity() { ObjectId = strEventId, BatId = "list", IsDelete = 0 }, null).ToList();
            }
            rd.EventTitle = eventInfo.Title;
            rd.EventContent = eventInfo.Content;
            rd.BootUrl = eventInfo.BootURL;
            rd.ShareRemark = eventInfo.ShareRemark;
            rd.PosterImageUrl = eventInfo.PosterImageUrl;
            rd.OverRemark = eventInfo.OverRemark;
            rd.ShareLogoUrl = eventInfo.ShareLogoUrl;
            rd.IsShare = eventInfo.IsShare == null ? 0 : (int)eventInfo.IsShare;

            var entityCover = bllCover.QueryByEntity(new LCoverEntity() { EventId = strEventId, IsDelete = 0,IsShow=1 }, null).FirstOrDefault();
            if (entityCover!=null)
            {
                rd.CoverInfo = entityCover;
            }

            return rd;
        }
    }
}