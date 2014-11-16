using System.Collections.Generic;
using System.Web;
using System.Linq;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess.Query;
using System;
using System.Text;
using System.Data;
using JIT.Utility.Log;
using System.Configuration;
using JIT.CPOS.Common;
using System.Collections.Specialized;

namespace JIT.CPOS.BS.Web.Module.CustomerBasicSetting.Handler
{
    /// <summary>
    /// CustomerBasicSettingHander 的摘要说明
    /// </summary>
    public class CustomerBasicSettingHander : JIT.CPOS.BS.Web.PageBase.JITCPOSAjaxHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pContext"></param>
        protected override void AjaxRequest(HttpContext pContext)
        {
            string content = "";
            switch (pContext.Request.QueryString["method"])
            {
                case "GetCustomerList":
                    content = GetCustomerList();
                    break;

                case "SaveustomerBasicrInfo":
                    content = SaveustomerBasicrInfo(pContext.Request.Form);
                    break;

                case "GetCousCustomerType":
                    content = GetCousCustomerType();
                    break;
                case "IsAld":
                    content = IsAld();
                    break;
            }
            pContext.Response.Write(content);
            pContext.Response.End();
        }
        #region GetCustomerList
        /// <summary>
        /// 获取客户信息
        /// </summary>
        /// <returns></returns>
        public string GetCustomerList()
        {
            RequestCustomerBasicrInfo basicInfo = new RequestCustomerBasicrInfo();
            ResponseData res = new ResponseData();
            basicInfo.loadInfo = GetCustomerLoadInfo();
            try
            {
                //客户信息
                var customerBasicSettingBLL = new CustomerBasicSettingBLL(this.CurrentUserInfo);
                DataSet ds = customerBasicSettingBLL.GetCustomerBasicSettingByKey(this.CurrentUserInfo.ClientID);
                List<CustomerBasicCodeInfo> list = new List<CustomerBasicCodeInfo>();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        CustomerBasicCodeInfo codeinfo = new CustomerBasicCodeInfo();

                        codeinfo.SettingCode = item["SettingCode"].ToString();
                        codeinfo.SettingValue = item["SettingValue"].ToString();
                        list.Add(codeinfo);
                    }
                }
                #region 获取配送策略
                var deliveryStrategyBll = new CustomerDeliveryStrategyBLL(this.CurrentUserInfo);
                //查询是否已设置
                var deliverStrategayList = deliveryStrategyBll.Query(new IWhereCondition[] { 
                        new EqualsCondition() { FieldName = "CustomerID", Value = this.CurrentUserInfo.ClientID}
                    }, null);

                if (deliverStrategayList.Count() > 0)//已设置执行修改
                {
                    CustomerBasicCodeInfo codeinfo = new CustomerBasicCodeInfo()
                    {
                        SettingCode = "AmountEnd", //满多少免配送费
                        SettingValue = deliverStrategayList[0].AmountEnd.ToString()
                    };
                    CustomerBasicCodeInfo codeinfo1 = new CustomerBasicCodeInfo()
                    {
                        SettingCode = "DeliveryAmount",  //配送费
                        SettingValue = deliverStrategayList[0].DeliveryAmount.ToString()
                    };
                    list.Add(codeinfo);
                    list.Add(codeinfo1);
                }
                #endregion
                basicInfo.requset = list;
                res.success = true;
                basicInfo.resdata = res;
                return string.Format("{{\"data\":{0}}}", basicInfo.ToJSON());
            }
            catch (Exception)
            {
                //basicInfo.resdata.msg = "加载失败";
                //basicInfo.resdata.success = false;

                throw;
            }




            return "";
        }
        #endregion

        #region SaveustomerBasicrInfo
        /// <summary>
        /// 保存客户信息
        /// </summary>
        /// <param name="rParams"></param>
        /// <returns></returns>
        public string SaveustomerBasicrInfo(NameValueCollection rParams)
        {

            SttingCode entity = Request("form").DeserializeJSONTo<SttingCode>();
            SttingCode entity1 = Request("form1").DeserializeJSONTo<SttingCode>();
            SttingCode entity2 = Request("form2").DeserializeJSONTo<SttingCode>();
            SttingCode entity3 = Request("form3").DeserializeJSONTo<SttingCode>();
            var customerBasicSettingBLL = new CustomerBasicSettingBLL(this.CurrentUserInfo);
            ResponseData res = new ResponseData();
            List<CustomerBasicSettingEntity> list = new List<CustomerBasicSettingEntity>();
            #region
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "WebLogo",
                SettingValue = rParams["imageurl"].ToString()
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "CustomerType",
                SettingValue = entity.customerType
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "AboutUs",
                SettingValue = ImageHandler(Request("aboutUs"))
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "BrandStory",
                SettingValue = ImageHandler(Request("brandStory"))
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "BrandRelated",
                SettingValue = ImageHandler(Request("brandRelated"))
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "IntegralAmountPer",
                SettingValue = entity.IntegralAmountPer
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "SMSSign",
                SettingValue = entity.SMSSign
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "ForwardingMessageLogo",
                SettingValue = rParams["forwardingMessageLogourl"].ToString()
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "ForwardingMessageTitle",
                SettingValue = entity.ForwardingMessageTitle
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "ForwardingMessageSummary",
                SettingValue = entity.ForwardingMessageSummary
            });

            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "WhatCommonPoints",
                SettingValue = ImageHandler(Request("whatCommonPoints"))
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "GetPoints",
                SettingValue = ImageHandler(Request("getPoints"))
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "SetSalesPoints",
                SettingValue = ImageHandler(Request("setSalesPoints"))
            });

            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "VipCardLogo",
                SettingValue = rParams["imagecfurl"].ToString()
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "CustomerMobile",
                SettingValue = entity1.CustomerMobile
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "MemberBenefits",
                SettingValue = ImageHandler(Request("memberBenefits"))
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "RangeAccessoriesStores",
                SettingValue = entity2.RangeAccessoriesStores
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "IsSearchAccessoriesStores",
                SettingValue = entity2.IsSearchAccessoriesStores
            });
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "IsAllAccessoriesStores",
                SettingValue = entity2.IsAllAccessoriesStores
            });
            if (true)
            {

            }
            if (IsAld() == "1")
            {
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "AppLogo",
                    SettingValue = rParams["appLogo"]
                });
                list.Add(new CustomerBasicSettingEntity()
                {
                    SettingCode = "AppTopBackground",
                    SettingValue = rParams["appTopBackground"]
                });
            }
            list.Add(new CustomerBasicSettingEntity()
            {
                SettingCode = "DeliveryStrategy",
                SettingValue = entity3.DeliveryStrategy
            });
            #endregion
            int i = customerBasicSettingBLL.SaveustomerBasicrInfo(list);
            #region 配送费保存

            decimal AmountEnd =0;
            decimal DeliveryAmount = 0;

            decimal.TryParse(entity3.AmountEnd,out AmountEnd);
            decimal.TryParse(entity3.DeliveryAmount,out DeliveryAmount);

            if (AmountEnd != 0 && DeliveryAmount != 0)
            // 如果页面有配送费才做保存
            {
                var deliveryStrategyBll = new CustomerDeliveryStrategyBLL(this.CurrentUserInfo);
                //查询是否已设置
                var deliverStrategayList = deliveryStrategyBll.Query(new IWhereCondition[] { 
                        new EqualsCondition() { FieldName = "CustomerID", Value = this.CurrentUserInfo.ClientID}
                    }, null);

                if (deliverStrategayList.Count() > 0)//已设置执行修改
                {
                    deliverStrategayList[0].AmountEnd = AmountEnd;
                    deliverStrategayList[0].DeliveryAmount = DeliveryAmount;
                    deliveryStrategyBll.Update(deliverStrategayList[0]);
                }
                else//未设置执行创建
                {
                    CustomerDeliveryStrategyEntity deliveryStrategyEntity = new CustomerDeliveryStrategyEntity()
                    {
                        AmountBegin = 0,
                        AmountEnd = AmountEnd,
                        CustomerId = this.CurrentUserInfo.ClientID,
                        DeliveryAmount = DeliveryAmount,
                        Status = 1,
                        DeliveryId = "1"
                    };
                    deliveryStrategyBll.Create(deliveryStrategyEntity);
                }
            }
            
            #endregion
            if (i > 0)
            {
                res.msg = "操作成功";
                res.success = true;

            }
            return string.Format("{{\"ResponseData\":{0}}}", res.ToJSON());
        }
        #endregion

        #region GetCousCustomerType
        /// <summary>
        ///获取客户分类信息
        /// </summary>
        /// <returns></returns>
        public string GetCousCustomerType()
        {
            var customerBasicSettingBLL = new CustomerBasicSettingBLL(this.CurrentUserInfo);
            DataSet ds = customerBasicSettingBLL.GetCousCustomerType();
            if (ds != null && ds.Tables.Count > 0)
            {
                return string.Format("{0}", ds.Tables[0].ToJSON());
            }
            return string.Format("{{\"data\":''}}");
        }
        #endregion

        #region GetCustomerLoadInfo
        /// <summary>
        /// customerinfo赋值
        /// </summary>
        /// <returns></returns>
        private CustomerLoadInfo GetCustomerLoadInfo()
        {
            var customerBasicSettingBLL = new CustomerBasicSettingBLL(this.CurrentUserInfo);
            CustomerLoadInfo custome = new CustomerLoadInfo()
              {
                  customerID = this.CurrentUserInfo.ClientID,
                  customerName = this.CurrentUserInfo.ClientName
              };
            DataRow dr = customerBasicSettingBLL.GetCustomerInfo(CurrentUserInfo.ClientID).Tables[0].Rows[0];
            custome.customerCode = dr["customer_code"].ToString();
            custome.customerName = dr["customer_name"].ToString();
            return custome;
        }

        #endregion

        #region 是否阿拉丁
        public string IsAld()
        {
            var customerBasicSettingBLL = new CustomerBasicSettingBLL(this.CurrentUserInfo);
            string str = customerBasicSettingBLL.GetIsAld();
            return str;
        }

        #endregion

        #region 处理富文本编辑器中的图片
        /// <summary>
        /// 处理富文本编辑器中的图片
        /// </summary>
        /// <param name="Info">富文本内容</param>
        /// <returns></returns>
        public string ImageHandler(string Info)
        {

            return Info;
            //System.Text.RegularExpressions.MatchCollection matches = null;

            ////正则表达式获取<img src=>图片url  
            //if (!string.IsNullOrEmpty(Info))
            //    matches = System.Text.RegularExpressions.Regex.Matches(Info, @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //if (matches != null && matches.Count > 0)
            //{
            //    foreach (System.Text.RegularExpressions.Match match in matches)
            //    {
            //        string imgTag = match.Value;

            //        //去除已有的width
            //        System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("width=\"\\d+\"");
            //        imgTag = r.Replace(imgTag, "", 10);

            //        int srcStartIndex = match.Value.ToLower().IndexOf("src=\"");
            //        int srcEndIndex = match.Value.ToLower().IndexOf("\"", srcStartIndex + 5);

            //        if (!(imgTag.IndexOf("width=\"100%\"") > 0))
            //        {
            //            //add width='100%'
            //            imgTag = imgTag.Insert(srcEndIndex + 1, " width=\"100%\" ");
            //        }

            //        Info = Info.Replace(match.Value, imgTag);
            //    }
            //}
            //return Info;
        }
        #endregion



    }
    #region MyRegion

    #region customerinfo
    public class RequestCustomerBasicrInfo
    {
        public List<CustomerBasicCodeInfo> requset { set; get; }
        public CustomerLoadInfo loadInfo { set; get; }
        public ResponseData resdata { set; get; }

    }
    public class CustomerLoadInfo
    {
        //用户编号
        public string customerCode { set; get; }
        //用户名称
        public string customerName { set; get; }
        //用户标识
        public string customerID { set; get; }
    }
    public class CustomerBasicCodeInfo
    {
        public string SettingCode { set; get; }
        public string SettingValue { set; get; }

    }

    #endregion
    #region ResponseData
    public class ResponseData
    {
        public bool success { get; set; }
        public string msg { get; set; }
    }
    #endregion

    public class SttingCode
    {
        public string webLogo { set; get; }//上传logo
        public string customerType { set; get; }//客户类型
        public string AboutUs { set; get; }//关于我们
        public string BrandStory { set; get; }//品牌故事
        public string BrandRelated { set; get; }//品牌相关
        public string VipCardLogo { set; get; }//会员卡图片
        public string CustomerMobile { set; get; }

        public string IntegralAmountPer { set; get; }//积分抵用金额的比率
        public string SMSSign { set; get; }//手机短信签名
        public string ForwardingMessageTitle { set; get; }//转发消息默认标题
        public string ForwardingMessageSummary { set; get; }//转发消息默认摘要文字

        public string MemberBenefits { set; get; }
        public string RangeAccessoriesStores { set; get; }
        public string IsSearchAccessoriesStores { set; get; }
        public string IsAllAccessoriesStores { set; get; }
        public string DeliveryStrategy { set; get; }//配送费描述
        public string AmountEnd { set; get; }//大于等于AmountEnd，配送费
        public string DeliveryAmount { set;get; } //配送费  
    }
    #endregion
}
