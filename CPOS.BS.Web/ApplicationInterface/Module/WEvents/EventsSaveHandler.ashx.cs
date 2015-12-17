using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

using System.Configuration;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Base;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.Log;
using Aspose.Cells;
using JIT.Utility;
using JIT.CPOS.BS.Web.Base.Excel;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Entity;
using System.Web.Script.Serialization;
using JIT.CPOS.BS.Web.ApplicationInterface.Vip;
using JIT.CPOS.Common;
using JIT.CPOS.BS.Web.Module.WEvents.Handler;

namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.WEvents
{
    /// <summary>
    /// EventsSaveHandler 的摘要说明
    /// </summary>
    public class EventsSaveHandler : BaseGateway
    {

        #region 错误码
        const int ERROR_AUTHCODE_NOTEXISTS = 330;
        const int ERROR_AUTHCODE_INVALID = 331;
        const int ERROR_AUTHCODE_NOT_EQUALS = 333;
        const int ERROR_AUTHCODE_WAS_USED = 332;
        const int ERROR_LACK_MOBILE = 312;
        const int ERROR_LACK_VIP_SOURCE = 315;
        const int ERROR_AUTO_MERGE_MEMBER_FAILED = 313;
        const int ERROR_MEMBER_REGISTERED = 314;

        const int ERROR_VIPCARD_EXISTS = 340;   //会员已办卡
        #endregion


        protected override string ProcessAction(string pType, string pAction, string pRequest)
        {
            string rst;
            switch (pAction)
            {
                case "GetDrawMethodByType":  //根据活动类型取活动方式
                    rst = this.GetDrawMethodByType(pRequest);
                    break;
                case "GetVipCardTypeList"://卡类型
                    rst = this.GetVipCardTypeList(pRequest);
                    break;
                case "GetVipCardGradeList"://卡级别
                    rst = this.GetVipCardGradeList(pRequest);
                    break;
                case "DoGetOptionListByName"://奖品等级
                    rst = this.DoGetOptionListByName(pRequest);
                    break;
                case "GetPrizeType"://奖品类型
                    rst = this.GetPrizeType(pRequest);
                    break;
                case "GetEventBaseData"://获取活动基础数据
                    rst = this.GetEventBaseData(pRequest);
                    break;
                case "GetCouponTypeInfo"://优惠券列表类型
                    rst = this.GetCouponTypeInfo(pRequest);
                    break;
                case "SaveEventStep1": //保存，更新活动第一步
                    rst = this.SaveEventStep1(pRequest);
                    break;
                case "GetStep1Info"://根据活动id获取活动信息
                    rst = this.GetStep1Info(pRequest);
                    break;
                case "SavePrize": //保存奖品
                    rst = this.SavePrize(pRequest);
                    break;
                case "AppendPrize"://追加奖品
                    rst = this.AppendPrize(pRequest);
                    break;
                case "DeletePrize":
                    rst = this.DeletePrize(pRequest);
                    break;
                case "GetStep2Info"://获取第二步信息
                    rst = this.GetStep2Info(pRequest);
                    break;
                case "SaveEventStep2": //保存，更新活动第二步
                    rst = this.SaveEventStep2(pRequest);
                    break;
                case "GetStep3Info"://获取第三步信息，包括前面选的是红包还是大转盘，对应相应的页面数据，还有图文素材的信息、还有活动的信息
                    rst = this.GetStep3Info(pRequest);
                    break;
                case "SaveEventStep3": //保存，更新活动第一步
                    rst = this.SaveEventStep3(pRequest);
                    break;
                case "SavePrizeLocation": //保存奖品位置
                    rst = this.SavePrizeLocation(pRequest);
                    break;
                case "GetPrizeLocationList": //获取奖品位置列表
                    rst = this.GetPrizeLocationList(pRequest);
                    break;
                default:
                    throw new APIException(string.Format("找不到名为：{0}的action处理方法.", pAction))
                    {
                        ErrorCode = ERROR_CODES.INVALID_REQUEST_CAN_NOT_FIND_ACTION_HANDLER
                    };
            }
            //HttpContext.Current.Response.ContentType = "text/html;charset=UTF-8";  
            return rst;
        }
        #region  根据活动类型获取活动方式
        public string GetDrawMethodByType(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetDrawMethodByTypeRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var rd = new GetDrawMethodByTypeRD();//返回值
            LEventDrawMethodBLL bll = new LEventDrawMethodBLL(loggingSessionInfo);
            List<LEventDrawMethodEntity> ls = bll.GetAll().Where(p => p.EventTypeId == rp.Parameters.EventTypeId).ToList();
            //LEventDrawMethodEntity entity = new LEventDrawMethodEntity();
            //entity.DrawMethodID = 0;   //
            //entity.DrawMethodName = "----请选择-----";
            //ls.Add(entity);


            rd.LEventDrawMethodList = ls;

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();

        }
        #endregion
        #region 获取会员卡类型
        public string GetVipCardTypeList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipCardTypeListRP>>();
            //   var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new VipCardBLL(loggingSessionInfo);
            var rd = new GetVipCardTypeListRD();
            var ds = bll.GetVipCardTypeList(loggingSessionInfo.ClientID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rd.VipCardTypeList = DataTableToObject.ConvertToList<VipCardTypeInfo>(ds.Tables[0]).ToArray();//直接根据所需要的字段反序列化
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion
        #region 获取会员卡等级
        public string GetVipCardGradeList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipCardGradeListRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new SysVipCardGradeBLL(loggingSessionInfo);
            var rd = new GetVipCardGradeListRD();

            SysVipCardGradeEntity en = new SysVipCardGradeEntity();
            en.CustomerID = loggingSessionInfo.ClientID;
            var ds = bll.QueryByEntity(en, null);
            rd.VipCardGradeList = ds;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion
        #region 获取活动基础数据（卡类型、卡等级、活动类型、参与活动次数）
        public string GetEventBaseData(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipCardGradeListRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var rd = new GetEventBaseDataRD();
            //卡级别
            var bll = new SysVipCardGradeBLL(loggingSessionInfo);
            SysVipCardGradeEntity en = new SysVipCardGradeEntity();
            en.CustomerID = loggingSessionInfo.ClientID;
            var ds = bll.QueryByEntity(en, null);
            rd.VipCardGradeList = ds;
            //卡类型
            var bll2 = new VipCardBLL(loggingSessionInfo);

            var ds2 = bll2.GetVipCardTypeList(loggingSessionInfo.ClientID);
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                rd.VipCardTypeList = DataTableToObject.ConvertToList<VipCardTypeInfo>(ds2.Tables[0]).ToArray();//直接根据所需要的字段反序列化
            }
            //活动类型
            var bll3 = new JIT.CPOS.BS.BLL.Control.ControlBLL(loggingSessionInfo);
            var ds3 = bll3.GetLEventsType2();
            if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
            {
                rd.EventsTypeList = DataTableToObject.ConvertToList<LEventsTypeEntity>(ds3.Tables[0]).ToArray();//直接根据所需要的字段反序列化
            }

            List<PersonCountEntity> list = new List<PersonCountEntity>(){
               
                 new PersonCountEntity{ Id="1",Name="仅一次"}, 
                 new PersonCountEntity{ Id="2",Name="每天一次"},
                   new PersonCountEntity{ Id="3",Name="每周一次"},
                   new PersonCountEntity{ Id="4",Name="无限制"}
            };
            rd.PersonCountList = list.ToArray();


            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion
        #region 获取优惠券类型列表
        /// <summary>
        /// 获取优惠券类型列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetCouponTypeInfo(string pRequest)
        {
            GetCouponTypeListRD rd = new GetCouponTypeListRD();
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<GetCouponTypeListRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            CouponTypeBLL bll = new CouponTypeBLL(loggingSessionInfo);
            DataSet ds = bll.GetCouponTypeList();
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                rd.CouponTypeList = DataTableToObject.ConvertToList<CouponType>(ds.Tables[0]);
            }
            //DataSet dsCouponTypeCount = bll.GetCouponTypeCount();
            //if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            //{
            //    rd.CouponTypeCount = DataTableToObject.ConvertToList<CouponTypeCount>(ds.Tables[0]);
            //}
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion
        #region 保存活动第一步
        public string SaveEventStep1(string pRequest)
        {
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<EventRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            LEventsBLL bll = new LEventsBLL(loggingSessionInfo);
            var eventEntity = new LEventsEntity();
            string strGuid = string.Empty;
            if (reqObj.Parameters.EventId != null && reqObj.Parameters.EventId != "")
                strGuid = reqObj.Parameters.EventId;
            else
                strGuid = Guid.NewGuid().ToString();
            eventEntity.EventID = strGuid;
            eventEntity.Title = reqObj.Parameters.Title;
            eventEntity.BeginTime = reqObj.Parameters.BeginTime;
            eventEntity.EndTime = reqObj.Parameters.EndTime;
            eventEntity.Content = reqObj.Parameters.Content;
            eventEntity.VipCardType = reqObj.Parameters.VipCardType;
            eventEntity.VipCardGrade = reqObj.Parameters.VipCardGrade;
            eventEntity.EventTypeID = reqObj.Parameters.EventTypeID;
            eventEntity.DrawMethodId = reqObj.Parameters.DrawMethodId;
            eventEntity.PersonCount = reqObj.Parameters.PersonCount;
            eventEntity.PointsLottery = reqObj.Parameters.PointsLottery;
            if (DateTime.Compare(Convert.ToDateTime(reqObj.Parameters.BeginTime), Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) <= 0 && DateTime.Compare(Convert.ToDateTime(reqObj.Parameters.EndTime), Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))) >= 0)
            {
                eventEntity.EventStatus = 20;//10=未开始,20=运行中,30=暂停,40=结束

            }
            else
            {
                eventEntity.EventStatus = 10;//10=未开始,20=运行中,30=暂停,40=结束

            }
            eventEntity.CustomerId = loggingSessionInfo.ClientID;

            var methodBll = new LEventDrawMethodMappingBLL(loggingSessionInfo);


            if (reqObj.Parameters.EventId != null && reqObj.Parameters.EventId != "")
            {
                eventEntity.LastUpdateBy = loggingSessionInfo.UserID;
                eventEntity.LastUpdateTime = DateTime.Now;
                bll.Update(eventEntity);
            }
            else
            {
                eventEntity.CreateBy = loggingSessionInfo.UserID;
                bll.Create(eventEntity);

                #region 生成二维码

                var wqrentity = new WQRCodeTypeBLL(loggingSessionInfo).QueryByEntity(

                    new WQRCodeTypeEntity { TypeCode = "EventQrcode" }

                    , null).FirstOrDefault();

                var wapentity = new WApplicationInterfaceBLL(loggingSessionInfo).QueryByEntity(new WApplicationInterfaceEntity
                {
                            
                    CustomerId = loggingSessionInfo.ClientID,
                    IsDelete = 0

                }, null).FirstOrDefault();

                var wxCode = CretaeWxCode();

                var WQRCodeManagerbll = new WQRCodeManagerBLL(loggingSessionInfo);

                Guid QRCodeId = Guid.NewGuid();

                if (!string.IsNullOrEmpty(wxCode.ImageUrl))
                {
                    WQRCodeManagerbll.Create(new WQRCodeManagerEntity
                    {
                        QRCodeId = QRCodeId,
                        QRCode = wxCode.MaxWQRCod.ToString(),
                        QRCodeTypeId = wqrentity.QRCodeTypeId,
                        IsUse = 1,
                        ObjectId = strGuid,
                        CreateBy = loggingSessionInfo.UserID,
                        ApplicationId = wapentity.ApplicationId,
                        IsDelete = 0,
                        ImageUrl = wxCode.ImageUrl,
                        CustomerId = loggingSessionInfo.ClientID

                    });
                }
                #endregion

            }


            var rd = new EventRD();
            rd.EventId = strGuid;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion
        #region 根据活动id获取活动第一步信息
        public string GetStep1Info(string pRequest)
        {
            var reqObj = pRequest.DeserializeJSONTo<APIRequest<EventRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            LEventsBLL bll = new LEventsBLL(loggingSessionInfo);
            var eventEntity = new LEventsEntity();
            eventEntity = bll.GetByID(reqObj.Parameters.EventId);
            var rd = new EventRD();
            rd.EventInfo = eventEntity;
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion
        #region 添加奖品
        public string SavePrize(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SavePrizesRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new LPrizesBLL(loggingSessionInfo);
            var entity = new LPrizesEntity();

            entity.EventId = rp.Parameters.EventId;
            entity.PrizeLevel = rp.Parameters.PrizeLevel;
            entity.PrizeTypeId = rp.Parameters.PrizeTypeId;
            entity.Point = rp.Parameters.Point;
            entity.CouponTypeID = rp.Parameters.CouponTypeID;
            entity.PrizeName = rp.Parameters.PrizeName;
            entity.CountTotal = rp.Parameters.CountTotal;
            entity.ImageUrl = rp.Parameters.ImageUrl;
            //entity.Probability = rp.Parameters.Probability == null ? 0 : rp.Parameters.Probability;
            entity.CreateBy = loggingSessionInfo.UserID;
            entity.PrizesID = Guid.NewGuid().ToString();
            bll.SavePrize(entity);

            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion 
        #region 追加奖品数量
        public string AppendPrize(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<AppendPrizeRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new LPrizesBLL(loggingSessionInfo);
            var entity = new LPrizesEntity();
            entity.EventId = rp.Parameters.EventId;
            entity.CountTotal = rp.Parameters.CountTotal;
            entity.PrizesID = rp.Parameters.PrizesId;
            entity.LastUpdateBy = loggingSessionInfo.UserID;
            bll.AppendPrize(entity);

            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion
        #region 删除奖品
        public string DeletePrize(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<AppendPrizeRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new LPrizesBLL(loggingSessionInfo);
            var entity = new LPrizesEntity();

            entity.EventId = rp.Parameters.EventId;
            entity.PrizesID = rp.Parameters.PrizesId;
            entity.LastUpdateBy = loggingSessionInfo.UserID;

            bll.DeletePrize(entity);

            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion
        #region 保存活动第二步
        public string SaveEventStep2(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<ImageObjectRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var imageEntity = new ObjectImagesEntity();
            var imageBll = new ObjectImagesBLL(loggingSessionInfo);
            if (rp.Parameters.ItemImageList.Count > 0)
            {
                imageBll.DeleteByObjectID(rp.Parameters.EventId);

                foreach (var i in rp.Parameters.ItemImageList)
                {
                    imageEntity.ImageURL = i.ImageURL;
                    imageEntity.ObjectId = rp.Parameters.EventId;
                    imageEntity.CreateBy = loggingSessionInfo.UserID;
                    imageEntity.ImageId = Guid.NewGuid().ToString();
                    imageEntity.BatId = i.BatId;
                    imageEntity.RuleId = rp.Parameters.RuleId?? 1;
                    imageEntity.RuleContent = rp.Parameters.RuleContent;
                    imageEntity.IsDelete = 0;
                    imageEntity.CustomerId = loggingSessionInfo.ClientID;
                    imageBll.Create(imageEntity);

                }

            }
            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion


        #region 根据活动id获取活动第二步信息
        public string GetStep2Info(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SavePrizesRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            var bll = new LPrizesBLL(loggingSessionInfo);
            var bllImage = new ObjectImagesBLL(loggingSessionInfo);
            var entity = new LPrizesEntity();
            var rd = new PrizeRD();
            if (rp.Parameters != null)
            {
                DataSet ds = bll.GetPirzeList(rp.Parameters.EventId);
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    rd.PrizeList = DataTableToObject.ConvertToList<Prize>(ds.Tables[0]);
                }

                DataSet dsImage = bllImage.GetObjectImagesByEventId(rp.Parameters.EventId);
                if (dsImage.Tables != null && dsImage.Tables.Count > 0 && dsImage.Tables[0] != null && dsImage.Tables[0].Rows.Count > 0)
                {
                    rd.ImageList = DataTableToObject.ConvertToList<ObjectImagesEntity>(dsImage.Tables[0]);

                    rd.RuleId = Convert.ToInt32(dsImage.Tables[0].Rows[0]["RuleId"].ToString() == "" ? "0" : dsImage.Tables[0].Rows[0]["RuleId"].ToString());
                    rd.RuleContent = dsImage.Tables[0].Rows[0]["RuleContent"].ToString();
                }
            }

            


            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion

        #region 根据活动id获取活动第三步信息
        public string GetStep3Info(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<EventRP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            LEventsBLL bll = new LEventsBLL(loggingSessionInfo);
            //var eventInfo = new LEventsEntity();
            //eventEntity = bll.GetByID(reqObj.Parameters.EventId);
            var rd = new GetStep3InfoRD();
            var ds = bll.GetNewEventInfo(rp.Parameters.EventId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rd.EventInfo = DataTableToObject.ConvertToObject<LEventsInfo>(ds.Tables[0].Rows[0]);//直接根据所需要的字段反序列化
            }
            var ds2 = bll.GetMaterialTextInfo(rp.Parameters.EventId);
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                rd.MaterialText = DataTableToObject.ConvertToObject<WMaterialTextEntity>(ds2.Tables[0].Rows[0]);//直接根据所需要的字段反序列化
                rd.MappingId=ds2.Tables[0].Rows[0]["MappingId"].ToString();
            }

            //获取图文素材的信息
            var sysPageBLL = new SysPageBLL(loggingSessionInfo);
            var list = sysPageBLL.GetPagesByCustomerID(loggingSessionInfo.ClientID);  //增加根据customer_id查询 update by Henry 2014-11-19
            var temp = list.GroupBy(t => new { t.ModuleName, t.PageCode, t.PageID, t.URLTemplate,t.PageKey }).Select(t => new ModulePageInfo()
            {
                ModuleName = t.Key.ModuleName,
                PageCode = t.Key.PageCode,
                PageID = t.Key.PageID.Value.ToString("N"),
                URLTemplate = t.Key.URLTemplate,
               PageKey= t.Key.PageKey
            }).Distinct().ToArray();
            var SysModuleList = temp;     

            //获取相应页面的信息
            //根据活动的抽奖方式获取：“HB” “BigDial“
            switch (rd.EventInfo.DrawMethodCode)
            { 
                case "HB":
                    rd.ModulePage = SysModuleList.Where(p => p.PageKey == "RedPacket").SingleOrDefault();//红包-新
                    break;
                case "DZP":
                    rd.ModulePage = SysModuleList.Where(p => p.PageKey == "BigDial").SingleOrDefault();//大转盘
                    break;
                case "GGK":
                    rd.ModulePage = SysModuleList.Where(p => p.PageKey == "BigDial").SingleOrDefault();//大转盘
                    break;
                default:
                    rd.ModulePage = SysModuleList.Where(p => p.PageKey == "RedPacket").SingleOrDefault();
                    break;
            }

            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion

        #region 保存活动第三步
        public string SaveEventStep3(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<SaveEventStep3RP>>();
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            //微信 公共平台
            var wapentity = new WApplicationInterfaceBLL(loggingSessionInfo).QueryByEntity(new WApplicationInterfaceEntity
            {
                CustomerId = loggingSessionInfo.ClientID,
                IsDelete = 0
            }, null).FirstOrDefault();//取默认的第一个微信

            #region 生成图文素材
            var para = rp.Parameters;

            #region 获取Page信息
            var pageBll = new SysPageBLL(loggingSessionInfo);
            var textBll = new WMaterialTextBLL(loggingSessionInfo);

            //组织图文实体
            var entity = new WMaterialTextEntity()
            {
                ApplicationId = wapentity.ApplicationId,//用自己取出来的
                CoverImageUrl = para.MaterialText.ImageUrl,//图拍你地址
                PageId = para.MaterialText.PageID,  //页面模块的标识
                PageParamJson = para.MaterialText.PageParamJson,//这个比较重要
                Text = para.MaterialText.Text,
                TextId = para.MaterialText.TextId,//为空时在后面保存时生成
                Title = para.MaterialText.Title,
                TypeId = para.MaterialText.TypeId
            };
            #endregion
            #region 生成URL
            var Domain = ConfigurationManager.AppSettings["interfacehost"].Replace("http://", "");
            var Domain1 = ConfigurationManager.AppSettings["interfacehost1"].Replace("http://", "");
            string URL = string.Empty;

            #region 系统模块
            var pages = pageBll.GetPageByID(para.MaterialText.PageID);//通过pageid查找syspage信息
            if (pages.Length == 0)
                throw new APIException("未找到Page:" + para.MaterialText.PageID) { ErrorCode = 341 };
            SysPageEntity CurrentPage;
            string path = string.Empty;//要替换的路径
            string urlTemplate = pages[0].URLTemplate;//模板URL
            string json = pages[0].JsonValue;// JSON体
            var jsonDic = json.DeserializeJSONTo<Dictionary<string, object>>();//转换后的字典
            var htmls = jsonDic["htmls"].ToJSON().DeserializeJSONTo<Dictionary<string, object>[]>().ToList();//所有的Html模板
            Dictionary<string, object> html = null;//选择的html信息
            var defaultHtmlId = jsonDic["defaultHtml"].ToString();
            html = htmls.Find(t => t["id"].ToString() == defaultHtmlId);//默认的htmlid*****
            if (html != null)
                path = html["path"].ToString();
            //判断高级oauth认证
            var scope = "snsapi_base";
            if(jsonDic.ContainsKey("scope"))//必须要判断key里是否包含scope
            {
                  scope = (jsonDic["scope"] == null || jsonDic["scope"] == "") ? "snsapi_base" : "snsapi_userinfo";
            }

            //判断是否有定制,没有则取JSON体中的默认
            //找出订制内容
            var customerPages = pages.ToList().FindAll(t => t.CustomerID == loggingSessionInfo.ClientID);
            if (customerPages.Count > 0)
            {
                //看是否有htmls的定制(Node值=2)
                CurrentPage = customerPages.Find(t => t.Node == "2");
                if (CurrentPage != null)
                {
                    var nodeValue = CurrentPage.NodeValue;
                    //在Json解析后的集合中找到path
                    html = htmls.Find(t => t["id"].ToString() == nodeValue);
                    if (html != null)
                    {
                        path = html["path"].ToString();
                    }
                }
                else
                {
                    CurrentPage = pages[0];
                }
            }
            else
            {
                CurrentPage = pages[0];
            }
            //读取配置信息域名,检查不用http://开头,如果有则去除
            var IsAuth = false;
            //TODO:判断页面是否需要Auth认证,如果页面需要证则再判断这个客户有没有Auth认证,Type=3
            if (CurrentPage.IsAuth == 1)
            {
                //判断这个客户是否是认证客户,type=3
                var applicationBll = new WApplicationInterfaceBLL(loggingSessionInfo);
                var application = applicationBll.GetByID(wapentity.ApplicationId);//取默认的第一个
                if (application.WeiXinTypeId == "3")
                {
                    IsAuth = true;
                }
            }

            //替换URL模板
            #region 替换URL模板
            urlTemplate = urlTemplate.Replace("_pageName_", path);
            var paraDic = para.MaterialText.PageParamJson.DeserializeJSONTo<Dictionary<string, object>[]>();
            foreach (var item in paraDic)   //这里key和value对于活动来说，其实就是活动的eventId，和eventId的值
            {
                if (item.ContainsKey("key") && item.ContainsKey("value"))
                    urlTemplate = urlTemplate.Replace("{" + item["key"] + "}", item["value"].ToString());
            }
            #endregion

            //根据规则组织URL
            #region 组织URL
            //读取配置文件中的域名

            if (string.IsNullOrEmpty(Domain))
                throw new APIException("微信管理:未配置域名,请在web.config中添加<add key='host' value=''/>") { ErrorCode = 342 };
            if (IsAuth)
            {
                //需要认证
                URL = string.Format("http://{0}/WXOAuth/AuthUniversal.aspx?customerId={1}&applicationId={2}&goUrl={3}&scope={4}", Domain.Trim('/'), loggingSessionInfo.ClientID, wapentity.ApplicationId, HttpUtility.UrlEncode(string.Format("{0}{1}", Domain.Trim('/'), urlTemplate)), scope);
            }
            else
            {
                //不需要认证
                URL = string.Format("http://{0}/WXOAuth/NoAuthGoto.aspx?customerId={1}&goUrl={2}", Domain.Trim('/'), loggingSessionInfo.ClientID, HttpUtility.UrlEncode(string.Format("{0}{1}", Domain.Trim('/'), urlTemplate.Trim('/'))));
            }
            entity.IsAuth = Convert.ToInt32(IsAuth);
            entity.PageParamJson = para.MaterialText.PageParamJson;
            #endregion
            #endregion

            entity.OriginalUrl = URL;//图文素材
            #endregion



            #region 保存
            var unionMappingBll = new WModelTextMappingBLL(loggingSessionInfo);
            if (string.IsNullOrEmpty(entity.TextId))
            {
                entity.TextId = Guid.NewGuid().ToString("N");
                #region 图文详情要对占位符#TextId#和#customerId#进行替换
                entity.OriginalUrl = entity.OriginalUrl.Replace("#TextId#", entity.TextId).Replace("#cutomerId#", loggingSessionInfo.ClientID);
                #endregion
                textBll.Create(entity);//创建图文素材
                //var mapping = new WModelTextMappingEntity()
                //{
                //    MappingId = Guid.NewGuid().ToString("N"),
                //    ModelId = entity.TypeId,
                //    TextId = entity.TextId,
                //};
                //unionMappingBll.Create(mapping);
            }
            else
            {
                #region 图文详情要对占位符#textId和#customerId进行替换
                entity.OriginalUrl = entity.OriginalUrl.Replace("#TextId#", entity.TextId).Replace("#cutomerId#", loggingSessionInfo.ClientID);
                #endregion
                textBll.Update(entity);
            }
            /**
            #region 图文分类和图文Mapping关系,先删除再增加
            var modelMappingBll = new WModelTextMappingBLL(loggingSessionInfo);
            modelMappingBll.DeleteByTextId(entity.TextId);
            var modelMapping = new WModelTextMappingEntity()
            {
                MappingId = Guid.NewGuid().ToString("N"),
                ModelId = entity.TypeId,   //模板的标识竟然是和图文类型的标识
                TextId = entity.TextId
            };
            modelMappingBll.Create(modelMapping);
            #endregion
            **/
            #endregion

            //  rd.MaterialTextId = entity.TextId;


            #endregion




            //活动的二维码自己查找QRCodeId
            var wqrCodeManagerEntity = new WQRCodeManagerBLL(loggingSessionInfo).QueryByEntity(new WQRCodeManagerEntity() { ObjectId = rp.Parameters.EventId }, null).FirstOrDefault();
            if (wqrCodeManagerEntity == null)
            {
                throw new APIException("活动没有生成二维码！") { ErrorCode = 342 };
            }
            var QRCodeId = wqrCodeManagerEntity.QRCodeId;//活动二维码的标识
            //根据二维码标识查找是否有他的关键字回复
            var WKeywordReplyentity = new WKeywordReplyBLL(loggingSessionInfo).QueryByEntity(new WKeywordReplyEntity()
            {
                Keyword = QRCodeId.ToString()  //二维码的标识

            }, null).FirstOrDefault();
            var ReplyBLL = new WKeywordReplyBLL(loggingSessionInfo);
            var ReplyId = Guid.NewGuid().ToString();//创建临时
            if (WKeywordReplyentity == null)
            {
                ReplyBLL.Create(new WKeywordReplyEntity()
                {
                    ReplyId = ReplyId,
                    Keyword = QRCodeId.ToString(),
                    ReplyType = 3,  //用图文素材
                    KeywordType = 4,//标识
                    IsDelete = 0,
                    CreateBy = loggingSessionInfo.UserID,
                    ApplicationId = wapentity.ApplicationId,
                });

            }
            else
            {
                ReplyId = WKeywordReplyentity.ReplyId; //用取出来的数据查看           
                WKeywordReplyentity.Text = "";
                WKeywordReplyentity.ReplyType = 3;//图文素材
                ReplyBLL.Update(WKeywordReplyentity);
            }
            #region 添加图文消息

            WMenuMTextMappingBLL MenuMTextMappingServer = new WMenuMTextMappingBLL(loggingSessionInfo);
            //MenuMTextMappingServer.Delete(new WMenuMTextMappingEntity
            //{
            //    MappingId = item.MappingId  //先删除，然后再保存，在这里做，就没必要了，直接修改就可以了。
            //}, null);
            if (string.IsNullOrEmpty(rp.Parameters.MappingId))//如果没有才添加，如果有，之前的关系就已经定好了，不需要更改
            {
                WMenuMTextMappingEntity MappingEntity = new WMenuMTextMappingEntity();
                MappingEntity.MenuId = ReplyId;
                MappingEntity.TextId = entity.TextId;   // 用图文素材标识******
                MappingEntity.DisplayIndex = 1;//排列顺序
                MappingEntity.CustomerId = loggingSessionInfo.ClientID;
                if (string.IsNullOrEmpty(rp.Parameters.MappingId))
                {
                    MappingEntity.MappingId = Guid.NewGuid();
                    MenuMTextMappingServer.Create(MappingEntity);
                }

            }



            #endregion


            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion

        #region 1.1 查询数据表 Options
        protected string DoGetOptionListByName(string pRequest)
        {
            //1.反序列化请求参数对象
            var rp = pRequest.DeserializeJSONTo<APIRequest<getOptionListByNameRP>>();

            //2.对请求参数进行验证
            if (rp.Parameters != null)
                rp.Parameters.Validate();

            //4.拼装响应结果
            var rd = new APIResponse<getOptionListByNameRD>(new getOptionListByNameRD());

            try
            {
                // var loggingSessionInfo = Default.GetBSLoggingSession(rp.CustomerID, "1");
                var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
                var ds = new OptionsBLL(loggingSessionInfo).GetOptionByName(rp.Parameters.OptionName, rp.Parameters.IsSort);

                if (ds != null && ds.Tables.Count > 0)
                {
                    rd.Data.Options = DataTableToObject.ConvertToList<Option>(ds.Tables[0]).ToArray();
                }
            }
            catch (Exception ex)
            {
                Loggers.Exception(new ExceptionLogInfo(ex));
                throw new Exception(ex.Message, ex);
            }

            //5.将响应结果序列化为JSON并返回
            return rd.ToJSON();
        }

        #endregion

        #region 奖品类型
        public string GetPrizeType(string pRequest)
        {
            //  var rp = pRequest.DeserializeJSONTo<APIRequest<GetVipCardGradeListRP>>();           
            //var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            //var bll = new SysVipCardGradeBLL(loggingSessionInfo);
            var rd = new GetPrizeTypeRD();

            List<PrizeTypeInfo> list = new List<PrizeTypeInfo>(){
               
                 new PrizeTypeInfo{ PrizeTypeCode="Coupon",PrizeTypeName="优惠券"} ,
                 new PrizeTypeInfo{ PrizeTypeCode="Point",PrizeTypeName="积分"}
                
            };
            rd.PrizeTypeList = list.ToArray();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);

            return rsp.ToJSON();
        }
        #endregion

        #region new 生成活动二维码
        public WxCode CretaeWxCode()
        {
            var responseData = new WxCode();
            responseData.success = false;
            responseData.msg = "二维码生成失败!";
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            try
            {
                //微信 公共平台
                var wapentity = new WApplicationInterfaceBLL(loggingSessionInfo).QueryByEntity(new WApplicationInterfaceEntity
                {

                    CustomerId = loggingSessionInfo.ClientID,
                    IsDelete = 0

                }, null).FirstOrDefault();

                //获取当前二维码 最大值
                var MaxWQRCod = new WQRCodeManagerBLL(loggingSessionInfo).GetMaxWQRCod() + 1;

                if (wapentity == null)
                {
                    responseData.success = false;
                    responseData.msg = "无设置微信公众平台!";
                    return responseData;
                }

                string imageUrl = string.Empty;

                #region 生成二维码
                JIT.CPOS.BS.BLL.WX.CommonBLL commonServer = new JIT.CPOS.BS.BLL.WX.CommonBLL();
                imageUrl = commonServer.GetQrcodeUrl(wapentity.AppID.ToString().Trim()
                                                          , wapentity.AppSecret.Trim()
                                                          , "1", MaxWQRCod
                                                          , loggingSessionInfo);

                if (!string.IsNullOrEmpty(imageUrl))
                {

                    string host = ConfigurationManager.AppSettings["DownloadImageUrl"];
                    CPOS.Common.DownloadImage downloadServer = new DownloadImage();
                    imageUrl = downloadServer.DownloadFile(imageUrl, host);
                }
                #endregion
                responseData.success = true;
                responseData.msg = "二维码生成成功!";
                responseData.ImageUrl = imageUrl;
                responseData.MaxWQRCod = MaxWQRCod;


                return responseData;
            }
            catch (Exception ex)
            {
                //throw new APIException(ex.Message);
                return responseData;
            }

        }
        #endregion

        #region 大转盘相关
        /// <summary>
        /// 保存奖品位置
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string SavePrizeLocation(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<PrizeLocationRP>>().Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var entityPrizeLocation = new LPrizeLocationEntity();
            var bllPrizeLocation = new LPrizeLocationBLL(loggingSessionInfo);

            if(rp.PrizeLocationList.Count>0)
            {
                foreach(var item in rp.PrizeLocationList)
                {
                    var entity = new LPrizeLocationEntity()
                    {
                        EventID = rp.EventID,
                        PrizeID = item.PrizeID,
                        PrizeName=item.PrizeName,
                        Location = item.Location,
                        ImageUrl = item.ImageUrl

                    };
                    if (item.PrizeLocationID != null && item.PrizeLocationID.ToString() != "")
                    {
                        entity.PrizeLocationID = item.PrizeLocationID;
                        bllPrizeLocation.Update(entity);
                    }
                    else
                    {
                        bllPrizeLocation.Create(entity);
                    }
                }
            }

            var rd = new EmptyRD();
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        /// <summary>
        /// 获取奖品位置列表
        /// </summary>
        /// <param name="pRequest"></param>
        /// <returns></returns>
        public string GetPrizeLocationList(string pRequest)
        {
            var rp = pRequest.DeserializeJSONTo<APIRequest<PrizeLocationRP>>().Parameters;
            var rd = new PrizeLocationRD();

            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;

            var entityPrizeLocation = new LPrizeLocationEntity();
            var bllPrizeLocation = new LPrizeLocationBLL(loggingSessionInfo);

            if (string.IsNullOrEmpty(rp.EventID))
            {
                rd.ErrMsg = "EventID参数有误";
            }
            else
            {
                rd.EventID = rp.EventID;
                rd.PrizeLocationList = bllPrizeLocation.QueryByEntity(new LPrizeLocationEntity() { EventID = rp.EventID }, null).ToList();
            }
            var rsp = new SuccessResponse<IAPIResponseData>(rd);
            return rsp.ToJSON();
        }
        #endregion
    }
    public class PrizeLocation
    {
        public Guid? PrizeLocationID { get; set; }
        public string PrizeID { get; set; }
        public string PrizeName { get; set; }
        public int Location { get; set; }
        public string ImageUrl { get; set; }
    }
    public class PrizeLocationRP:IAPIRequestParameter
    {
        public string EventID { get; set; }
        public List<PrizeLocation> PrizeLocationList { get; set; }
        public void Validate()
        {
        }
    }

    public class PrizeLocationRD:IAPIResponseData
    {
        public string EventID { get; set; }
        public string ErrMsg { get; set; }
        public List<LPrizeLocationEntity> PrizeLocationList { get; set; }
    }
    public class WxCode
    {
        public bool success { get; set; }
        public string msg { get; set; }
        public string ImageUrl { get; set; }
        public int MaxWQRCod { get; set; }
    }
    public class EventRP : IAPIRequestParameter
    {/// <summary>
        /// 
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? EventLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParentEventID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BeginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String EndTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WeiXinID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CityID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImageURL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ApplyQuesID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PollQuesID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsSubEvent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? EventStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? DisplayIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? PersonCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDelete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ModelId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EventManagerUserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDefault { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsTop { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Organizer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EventFlag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EventTypeID { get; set; }

        public int DrawMethodId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? EventGenreId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? CanSignUpCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsTicketRequired { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? ReplyType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Distance { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsShare { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShareRemark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PosterImageUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OverRemark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BootURL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? MailSendInterval { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShareLogoUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsPointsLottery { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? PointsLottery { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? RewardPoints { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? BeginPersonCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? EventFee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsSignUpList { get; set; }

        public int VipCardType { get; set; }
        public int VipCardGrade { get; set; }

        public void Validate()
        {
        }
    }
    public class EventRD : IAPIResponseData
    {
        public string EventId { get; set; }
        public LEventsEntity EventInfo { get; set; }//活动列表   
    }
    public class GetStep3InfoRD : IAPIResponseData
    {
        public string EventId { get; set; }
        public LEventsInfo EventInfo { get; set; }//活动列表 
       public string MappingId { get; set; }
       public WMaterialTextEntity MaterialText { get; set; }
        public ModulePageInfo ModulePage { get; set; }
        
    }
    public class ModulePageInfo
    {
        public string PageID { get; set; }//String	模板Page标识
        public string ModuleName { get; set; }//String	Code
        public string PageCode { get; set; }//String	页面类别码,根据类别来确定前端页面显示如活动、系统功能等
        public string URLTemplate { get; set; }//URL模板
        public string PageKey { get; set; }//URL模板
        
    }
    public class LEventsInfo
    {
        public string EventID { get; set; }
        public int DrawMethodId { get; set; }
        public string DrawMethodName { get; set; }
        public string DrawMethodCode { get; set; }
        
        public string EventName { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public int EventStatus { get; set; }
        public string statusDESC { get; set; }
        
        public string EventTypeId { get; set; }
        public string EventTypeName { get; set; }
    }
    public class SaveEventStep3RP : IAPIRequestParameter
    {/// <summary>
        /// 
        /// </summary>
        public string EventId { get; set; }
        public string MappingId { get; set; }


        public MaterialTextInfo MaterialText { get; set; }


        public void Validate()
        {
        }
    }
    public class MaterialTextInfo
    {
        /// <summary>
        ///标识	为空时是增加,否则为修改
        /// </summary>  
        public string TextId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图片URL	
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 原文连接
        /// </summary>
        public string OriginalUrl { get; set; }
        /// <summary>
        /// 文本内容	
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 作为请求是不作为参数
        /// </summary>
        public int DisplayIndex { get; set; }
        /// <summary>
        /// 申请接口主标识
        /// </summary>
        // public string ApplicationId { get; set; }
        /// <summary>
        /// 图文类别	
        /// </summary>
        public string TypeId { get; set; }
        /// <summary>
        /// 链接类别ID
        /// </summary>
        public string UnionTypeId { get; set; }
        /// <summary>
        /// 模块ID
        /// </summary>
        public Guid? PageID { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 页面替换参数JSON	
        /// </summary>
        public string PageParamJson { get; set; }

    }
    public class ImageObjectRP : IAPIRequestParameter
    {
        public string EventId { get; set; }
        public int? RuleId { get; set; }
        public string RuleContent { get; set; }
        public List<ObjectImage> ItemImageList { get; set; }
        public void Validate()
        {
        }

    }
    public class SavePrizesRP : IAPIRequestParameter
    {
        public string EventId { get; set; }
        public string PrizesId { get; set; }
        /// <summary>
        /// 奖品名称
        /// </summary>
        public string PrizeName { get; set; }
        /// <summary>
        /// 奖品等级
        /// </summary>
        public int PrizeLevel { get; set; }
        /// <summary>
        /// 奖品数量
        /// </summary>
        public int CountTotal { get; set; }
        /// <summary>
        /// 奖品类型
        /// </summary>
        public string PrizeTypeId { get; set; }
        /// <summary>
        /// 奖品类型选择积分时 所需的积分，选择优惠券时记录优惠券id
        /// </summary>
        public int? Point { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CouponTypeID { get; set; }
        /// <summary>
        /// 中奖概率
        /// </summary>

        public decimal Probability { get; set; }

        public string ImageUrl { get; set; }
        public List<ObjectImage> ObjectImage { get; set; }
        public void Validate()
        {
        }
    }
    public class PrizeRD : IAPIResponseData
    {
        public int RuleId { get; set; }
        public string RuleContent { get; set; }

        public List<Prize> PrizeList { get; set; }
        public List<ObjectImagesEntity> ImageList { get; set; }
    }
    public class Prize
    {
        public string EventId { get; set; }
        public string PrizesID { get; set; }
        public string PrizeName { get; set; }
        public int PrizeLevel { get; set; }
        public string PrizeLevelName { get; set; }
        public string CouponTypeName { get; set; }
        public string CouponTypeID { get; set; }
        public int CountTotal { get; set; }
        public int IssuedQty { get; set; }
        public decimal Probability { get; set; }
        public string ImageUrl { get; set; }
        //剩余数量
        public int RemainCount { get; set; }
    }
    public class ObjectImage
    {
        public string ImageURL { get; set; }
        public string BatId { get; set; }

    }
    public class SavePrizesRD : IAPIResponseData
    {
        public string EventId { get; set; }

    }
    public class AppendPrizeRP : IAPIRequestParameter
    {

        public string PrizesId { get; set; }

        public int CountTotal { get; set; }
        public string EventId { get; set; }
        public string PrizeType { get; set; }

        public void Validate()
        {
        }
    }
    public class GetDrawMethodByTypeRP : IAPIRequestParameter
    {
        public string EventTypeId { get; set; }//活动类型标识
        public void Validate()
        {
        }
    }
    public class GetDrawMethodByTypeRD : IAPIResponseData
    {
        public IList<LEventDrawMethodEntity> LEventDrawMethodList { get; set; }//活动方式列表       

    }
    public class GetCouponTypeListRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
    }
    public class GetCouponTypeListRD : IAPIResponseData
    {
        public List<CouponType> CouponTypeList { get; set; }
        public List<CouponTypeCount> CouponTypeCount { get; set; }
    }
    public class CouponType
    {
        public Guid CouponTypeID { get; set; }
        public string CouponTypeName { get; set; }

        public int IssuedQty { get; set; }
        public int CountTotal { get; set; }
    }
    public class CouponTypeCount
    {
        public Guid CouponTypeID { get; set; }
        public int CountTotal { get; set; }
    }
    public class GetVipCardTypeListRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
    }
    public class GetVipCardTypeListRD : IAPIResponseData
    {
        public VipCardTypeInfo[] VipCardTypeList { get; set; }
    }
    public class GetPrizeTypeRD : IAPIResponseData
    {
        public PrizeTypeInfo[] PrizeTypeList { get; set; }
    }
    public class VipCardTypeInfo
    {
        #region 属性集
        /// <summary>
        /// 
        /// </summary>
        public Int32? VipCardTypeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String VipCardTypeCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String VipCardTypeName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CreateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String LastUpdateBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsDelete { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String CustomerID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? AddUpAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? IsExpandVip { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String PreferentialAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String SalesPreferentiaAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public String IntegralMultiples { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Decimal? YearAmount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32? Expires { get; set; }


        #endregion
    }
    public class GetVipCardGradeListRP : IAPIRequestParameter
    {
        public void Validate()
        {
        }
    }
    public class GetVipCardGradeListRD : IAPIResponseData
    {
        public SysVipCardGradeEntity[] VipCardGradeList { get; set; }
    }
    #region 1.1 查询数据表 Options
    /// <summary>
    /// 接口请求参数
    /// </summary>
    class getOptionListByNameRP : IAPIRequestParameter
    {
        /// <summary>
        /// 校验请求参数
        /// </summary>
        public void Validate()
        {
            if (string.IsNullOrEmpty(OptionName))
            {
                throw new ArgumentNullException("标志名称不能为空！");
            }
        }
        /// <summary>
        /// 标志名称
        /// </summary>
        public string OptionName { get; set; }
        /// <summary>
        /// 是否排序
        /// </summary>
        public bool? IsSort { get; set; }
    }
    /// <summary>
    /// 接口响应参数
    /// </summary>
    class getOptionListByNameRD : IAPIResponseData
    {
        /// <summary>
        /// 接口响应数据
        /// </summary>
        public Option[] Options { get; set; }


    }

    public class Option
    {
        /// <summary>
        /// ID
        /// </summary>
        public int? OptionID { get; set; }
        /// <summary>
        /// 名称值
        /// </summary>
        public string OptionText { get; set; }
    }


    public class PrizeTypeInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public string PrizeTypeCode { get; set; }
        /// <summary>
        /// 名称值
        /// </summary>
        public string PrizeTypeName { get; set; }
    }


    public class GetEventBaseDataRD : IAPIResponseData
    {
        public SysVipCardGradeEntity[] VipCardGradeList { get; set; }
        public VipCardTypeInfo[] VipCardTypeList { get; set; }
        public LEventsTypeEntity[] EventsTypeList { get; set; }
        public PersonCountEntity[] PersonCountList { get; set; }

    }
    #endregion
}