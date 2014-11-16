/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013/9/23 17:47:45
 * Description	:
 * 1st Modified On	:
 * 1st Modified By	:
 * 1st Modified Desc:
 * 1st Modifier EMail	:
 * 2st Modified On	:
 * 2st Modified By	:
 * 2st Modifier EMail	:
 * 2st Modified Desc:
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using JIT.Utility;
using JIT.Utility.ExtensionMethod;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.Utility.Reflection;
using JIT.CPOS.BS.Entity.Interface;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 业务处理：  
    /// </summary>
    public partial class DynamicInterfaceBLL
    {
        private LoggingSessionInfo CurrentUserInfo;
        private DynamicInterfaceDAO _currentDAO;
        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public DynamicInterfaceBLL(LoggingSessionInfo pUserInfo)
        {
            this.CurrentUserInfo = pUserInfo;
            this._currentDAO = new DynamicInterfaceDAO(pUserInfo);
        }
        #endregion

        #region 老版接口
        #region getUserIDByOpenID
        public string getUserIDByOpenID(getUserIDByOpenIDEntity pEntity)
        {
            return this._currentDAO.getUserIDByOpenID(pEntity);
        }
        #endregion

        #region getCodeByPhone
        public int getCodeByPhone(getCodeByPhoneEntity pEntity)
        {
            return this._currentDAO.getCodeByPhone(pEntity);
        }
        #endregion

        #region getSign
        public string getSign(getCodeByPhoneEntity pEntity)
        {
            return this._currentDAO.getSign(pEntity);
        }
        #endregion

        #region getUserByPhoneAndCode
        public string getUserByPhoneAndCode(getUserByPhoneAndCodeEntity pEntity)
        {
            return this._currentDAO.getUserByPhoneAndCode(pEntity);
        }
        #endregion

        #region getUserDefinedByUserID
        public List<PageEntity> getUserDefinedByUserID(ReqData<getUserDefinedByUserIDEntity> pEntity)
        {
            DataSet ds = this._currentDAO.getUserDefinedByUserID(pEntity);
            List<PageEntity> lPageEntity = new List<PageEntity>();
            List<BlockEntity> lBlockEntity = new List<BlockEntity>();
            List<ControlEntity> lControlEntity = new List<ControlEntity>();
            List<ConOptionsEntity> lOptionEntity = new List<ConOptionsEntity>();
            if (ds != null && ds.Tables != null)
            {
                DataTable dtPage = null;
                DataTable dtControl = null;
                DataTable dtOption = null;
                DataTable dtVip = null;
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    dtPage = ds.Tables[0];
                }
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    dtControl = ds.Tables[1];
                }
                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    dtOption = ds.Tables[2];
                }
                if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                {
                    dtVip = ds.Tables[3];
                }
                if (dtPage != null)
                {
                    DataRow[] dtPageRow = dtPage.Select(" Type=1", " Sort asc");
                    if (dtPageRow != null && dtPageRow.Length > 0)
                    {
                        for (int i = 0; i < dtPageRow.Length; i++)
                        {
                            PageEntity pageEntity = new PageEntity();
                            pageEntity.PageNum = (i + 1);
                            pageEntity.PageName = dtPageRow[i]["title"].ToString();
                            DataRow[] dtBlockRow = dtPage.Select(" Type=2 and ParentID='" + dtPageRow[i]["MobilePageBlockID"] + "'", " Sort asc");
                            if (dtBlockRow != null && dtBlockRow.Length > 0)
                            {
                                lBlockEntity = new List<BlockEntity>();
                                for (int j = 0; j < dtBlockRow.Length; j++)
                                {
                                    BlockEntity blockEntity = new BlockEntity();
                                    blockEntity.BlockName = dtBlockRow[j]["title"].ToString();
                                    blockEntity.BlockSort = Convert.ToInt32(dtBlockRow[j]["Sort"]);
                                    DataRow[] drControl = dtControl.Select("MobilePageBlockID='" + dtBlockRow[j]["MobilePageBlockID"] + "'", " EditOrder asc");
                                    if (drControl != null && drControl.Length > 0)
                                    {
                                        lControlEntity = new List<ControlEntity>();
                                        for (int k = 0; k < drControl.Length; k++)
                                        {
                                            ControlEntity controlEntity = new ControlEntity();
                                            controlEntity.AuthType = drControl[k]["AuthType"].ToString();
                                            controlEntity.ColumnDesc = drControl[k]["ColumnDesc"].ToString();
                                            controlEntity.ColumnName = drControl[k]["ColumnName"].ToString();
                                            controlEntity.ColumnDescEN = drControl[k]["ColumnDescEN"].ToString();
                                            controlEntity.ControlID = drControl[k]["MobileBussinessDefinedID"].ToString();
                                            controlEntity.ControlType = drControl[k]["ControlType"].ToString();
                                            controlEntity.IsMustDo = drControl[k]["IsMustDo"].ToString() == "1" ? true : false;
                                            controlEntity.LinkageItem = drControl[k]["LinkageItem"].ToString();
                                            controlEntity.ExampleValue = drControl[k]["ExampleValue"].ToString();
                                            controlEntity.MaxLength = Convert.ToInt32(drControl[k]["MaxLength"] == DBNull.Value ? 0 : drControl[k]["MaxLength"]);
                                            controlEntity.MaxSelected = Convert.ToInt32(drControl[k]["MaxSelected"] == DBNull.Value ? 0 : drControl[k]["MaxSelected"]);
                                            controlEntity.MinLength = Convert.ToInt32(drControl[k]["MinLength"] == DBNull.Value ? 0 : drControl[k]["MinLength"]);
                                            controlEntity.MinSelected = Convert.ToInt32(drControl[k]["MinSelected"] == DBNull.Value ? 0 : drControl[k]["MinSelected"]);
                                            if (dtVip != null && dtVip.Rows.Count > 0)
                                            {
                                                if (dtVip.Rows[0]["" + controlEntity.ColumnName + ""] != null)
                                                {
                                                    controlEntity.Value = dtVip.Rows[0]["" + controlEntity.ColumnName + ""].ToString();
                                                }
                                                else
                                                {
                                                    controlEntity.Value = "";
                                                }
                                            }
                                            else
                                            {
                                                controlEntity.Value = "";
                                            }
                                            if (controlEntity.ControlType == "7" || controlEntity.ControlType == "8" || controlEntity.ControlType == "6")
                                            {
                                                DataRow[] drOption = dtOption.Select("OptionName='" + drControl[k]["CorrelationValue"] + "'", " Sequence asc");
                                                if (drOption != null && drOption.Length > 0)
                                                {
                                                    lOptionEntity = new List<ConOptionsEntity>();
                                                    for (int l = 0; l < drOption.Length; l++)
                                                    {
                                                        ConOptionsEntity optionsEntity = new ConOptionsEntity();
                                                        optionsEntity.IsSelected = false;
                                                        optionsEntity.OptionID = drOption[l]["OptionValue"].ToString();
                                                        optionsEntity.OptionText = drOption[l]["OptionText"].ToString();
                                                        lOptionEntity.Add(optionsEntity);
                                                    }
                                                    controlEntity.Options = lOptionEntity;
                                                }
                                            }
                                            lControlEntity.Add(controlEntity);
                                        }
                                        blockEntity.Control = lControlEntity;
                                    }
                                    lBlockEntity.Add(blockEntity);
                                }
                                pageEntity.Block = lBlockEntity;
                            }
                            lPageEntity.Add(pageEntity);
                        }
                    }
                }
            }
            return lPageEntity;
        }

        /// <summary>
        /// 获取报名表单 Add By Alan
        /// </summary>
        /// <param name="eventId">活动Id</param>
        /// <returns></returns>
        public List<PageEntity> GetSignUpForm(string eventId)
        {
            DataSet ds = this._currentDAO.GetSignUpForm(eventId);
            List<PageEntity> lPageEntity = new List<PageEntity>();
            List<BlockEntity> lBlockEntity = new List<BlockEntity>();
            List<ControlEntity> lControlEntity = new List<ControlEntity>();
            List<ConOptionsEntity> lOptionEntity = new List<ConOptionsEntity>();
            if (ds != null && ds.Tables != null)
            {
                DataTable dtPage = null;
                DataTable dtControl = null;
                DataTable dtOption = null;
                DataTable dtVip = null;
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    dtPage = ds.Tables[0];
                }
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    dtControl = ds.Tables[1];
                }
                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    dtOption = ds.Tables[2];
                }
                if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                {
                    dtVip = ds.Tables[3];
                }
                if (dtPage != null)
                {
                    DataRow[] dtPageRow = dtPage.Select(" Type=1", " Sort asc");
                    if (dtPageRow != null && dtPageRow.Length > 0)
                    {
                        for (int i = 0; i < dtPageRow.Length; i++)
                        {
                            PageEntity pageEntity = new PageEntity();
                            pageEntity.PageNum = (i + 1);
                            pageEntity.PageName = dtPageRow[i]["title"].ToString();
                            DataRow[] dtBlockRow = dtPage.Select(" Type=2 and ParentID='" + dtPageRow[i]["MobilePageBlockID"] + "'", " Sort asc");
                            if (dtBlockRow != null && dtBlockRow.Length > 0)
                            {
                                lBlockEntity = new List<BlockEntity>();
                                for (int j = 0; j < dtBlockRow.Length; j++)
                                {
                                    BlockEntity blockEntity = new BlockEntity();
                                    blockEntity.BlockName = dtBlockRow[j]["title"].ToString();
                                    blockEntity.BlockSort = Convert.ToInt32(dtBlockRow[j]["Sort"]);
                                    DataRow[] drControl = dtControl.Select("MobilePageBlockID='" + dtBlockRow[j]["MobilePageBlockID"] + "'", " EditOrder asc");
                                    if (drControl != null && drControl.Length > 0)
                                    {
                                        lControlEntity = new List<ControlEntity>();
                                        for (int k = 0; k < drControl.Length; k++)
                                        {
                                            ControlEntity controlEntity = new ControlEntity();
                                            controlEntity.AuthType = drControl[k]["AuthType"].ToString();
                                            controlEntity.ColumnDesc = drControl[k]["ColumnDesc"].ToString();
                                            controlEntity.ColumnName = drControl[k]["ColumnName"].ToString();
                                            controlEntity.ColumnDescEN = drControl[k]["ColumnDescEN"].ToString();
                                            controlEntity.ControlID = drControl[k]["MobileBussinessDefinedID"].ToString();
                                            controlEntity.ControlType = drControl[k]["ControlType"].ToString();
                                            controlEntity.IsMustDo = drControl[k]["IsMustDo"].ToString() == "1" ? true : false;
                                            controlEntity.LinkageItem = drControl[k]["LinkageItem"].ToString();
                                            controlEntity.ExampleValue = drControl[k]["ExampleValue"].ToString();
                                            controlEntity.MaxLength = Convert.ToInt32(drControl[k]["MaxLength"]);
                                            controlEntity.MaxSelected = Convert.ToInt32(drControl[k]["MaxSelected"]);
                                            controlEntity.MinLength = Convert.ToInt32(drControl[k]["MinLength"]);
                                            controlEntity.MinSelected = Convert.ToInt32(drControl[k]["MinSelected"]);
                                            if (dtVip != null && dtVip.Rows.Count > 0)
                                            {
                                                if (dtVip.Rows[0]["" + controlEntity.ColumnName + ""] != null)
                                                {
                                                    controlEntity.Value = dtVip.Rows[0]["" + controlEntity.ColumnName + ""].ToString();
                                                }
                                                else
                                                {
                                                    controlEntity.Value = "";
                                                }
                                            }
                                            else
                                            {
                                                controlEntity.Value = "";
                                            }
                                            if (controlEntity.ControlType == "7" || controlEntity.ControlType == "8" || controlEntity.ControlType == "6")
                                            {
                                                DataRow[] drOption = dtOption.Select("OptionName='" + drControl[k]["CorrelationValue"] + "'", " Sequence asc");
                                                if (drOption != null && drOption.Length > 0)
                                                {
                                                    lOptionEntity = new List<ConOptionsEntity>();
                                                    for (int l = 0; l < drOption.Length; l++)
                                                    {
                                                        ConOptionsEntity optionsEntity = new ConOptionsEntity();
                                                        optionsEntity.IsSelected = false;
                                                        optionsEntity.OptionID = drOption[l]["OptionValue"].ToString();
                                                        optionsEntity.OptionText = drOption[l]["OptionText"].ToString();
                                                        lOptionEntity.Add(optionsEntity);
                                                    }
                                                    controlEntity.Options = lOptionEntity;
                                                }
                                            }
                                            lControlEntity.Add(controlEntity);
                                        }
                                        blockEntity.Control = lControlEntity;
                                    }
                                    lBlockEntity.Add(blockEntity);
                                }
                                pageEntity.Block = lBlockEntity;
                            }
                            lPageEntity.Add(pageEntity);
                        }
                    }
                }
            }
            return lPageEntity;
        }
        #endregion

        #region submitUserInfo
        public int submitUserInfo(submitUserInfoEntity pEntity)
        {
            string pSql = "";
            if (string.IsNullOrEmpty(pEntity.common.userId))
            {
                string pUserID = Guid.NewGuid().ToString();
                pSql = @"insert vip ({0}VipID,CreateBy,CreateTime,ClientID,Status)values({1}'{2}','1',GETDATE(),'{3}','2');
                         insert VIPRoleMapping(VipId,RoleId,ClientID)values('{2}','{4}','{3}');
                        ";
                StringBuilder pColumn = new StringBuilder();
                StringBuilder pValues = new StringBuilder();
                if (pEntity != null && pEntity.special.Control != null && pEntity.special.Control.Count > 0)
                {
                    for (int i = 0; i < pEntity.special.Control.Count; i++)
                    {
                        pEntity.common.userId = pUserID;
                        ControlUpdateEntity cEntity = pEntity.special.Control[i];
                        if (cEntity != null)
                        {
                            if (!string.IsNullOrEmpty(cEntity.ColumnName))
                            {
                                pColumn.Append(cEntity.ColumnName + ",");
                                pValues.Append("'" + cEntity.Value + "',");
                            }
                        }
                    }
                    pSql = string.Format(pSql, pColumn.ToString(), pValues.ToString(), pUserID, pEntity.common.customerId, pEntity.common.roleid);
                }
            }
            else
            {
                pSql = "update VIP set {0}LastUpdateBy='{1}',LastUpdateTime=GETDATE() where VIPID='{1}'";
                StringBuilder pColumn = new StringBuilder();
                if (pEntity != null && pEntity.special.Control != null && pEntity.special.Control.Count > 0)
                {
                    for (int i = 0; i < pEntity.special.Control.Count; i++)
                    {
                        ControlUpdateEntity cEntity = pEntity.special.Control[i];
                        if (cEntity != null)
                        {
                            if (!string.IsNullOrEmpty(cEntity.ColumnName))
                            {
                                pColumn.Append(cEntity.ColumnName + "='" + cEntity.Value + "',");
                            }
                        }
                    }
                    pSql = string.Format(pSql, pColumn.ToString(), pEntity.common.userId);
                }
            }
            if (string.IsNullOrEmpty(pSql))
            {
                return 0;
            }
            return this._currentDAO.submitUserInfo(pSql);
        }
        #endregion

        #region checkUserEmail  验证Email是否存在
        public bool checkUserEmail(string email, string userId, string clientId)
        {
            return this._currentDAO.checkUserEmail(email, userId, clientId);
        }
        #endregion

        #region checkUserPhone  验证手机号是否存在
        public bool checkUserPhone(string phone, string userId, string clientId)
        {
            return this._currentDAO.checkUserPhone(phone, userId, clientId);
        }
        #endregion

        #region getNewsList
        public SearchListEntity<DataTable> getNewsList(ReqData<getNewsListEntity> pEntity)
        {
            SearchListEntity<DataTable> searchListEntity = new SearchListEntity<DataTable>();
            DataSet ds = this._currentDAO.getNewsList(pEntity);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                searchListEntity.ItemList = ds.Tables[0];
            }
            searchListEntity.IsNext = false;
            if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                if ((pEntity.special.pageSize * (pEntity.special.page)) < Convert.ToInt32(ds.Tables[1].Rows[0][0]))
                {
                    searchListEntity.IsNext = true;
                }
            }
            return searchListEntity;
        }
        #endregion

        #region getNewsDetailByNewsID
        public reqNewsEntity getNewsDetailByNewsID(ReqData<getNewsDetailByNewsIDEntity> pEntity)
        {
            reqNewsEntity pNewsEntity = new reqNewsEntity();
            DataSet ds = this._currentDAO.getNewsDetailByNewsID(pEntity);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                NewsDetailEntity[] entity = DataLoader.LoadFrom<NewsDetailEntity>(ds.Tables[0]);
                pNewsEntity.News = entity[0];
                //if (!string.IsNullOrEmpty(pNewsEntity.News.Content))
                //{
                //    pNewsEntity.News.Content = pNewsEntity.News.Content.Replace("\"", "'").Replace("&quot;", "'");
                //}

            }
            return pNewsEntity;
        }
        #endregion

        #region GetUserInfo
        public DataSet GetUserInfo(string pUserID)
        {
            return this._currentDAO.GetUserInfo(pUserID);
        }
        #endregion

        #region GetUserEmail
        public string GetUserEmail(string pUserID)
        {
            return this._currentDAO.GetUserEmail(pUserID);
        }
        #endregion

        #region getUserByLogin
        public string getUserByLogin(ReqData<getUserByLoginEntity> pEntity)
        {
            return this._currentDAO.getUserByLogin(pEntity);
        }
        #endregion

        #region getTopList
        public SearchListEntity<DataTable> getTopList(ReqData<getTopListEntity> pEntity)
        {
            SearchListEntity<DataTable> searchListEntity = new SearchListEntity<DataTable>();
            DataSet ds = this._currentDAO.getTopList(pEntity);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                searchListEntity.ItemList = ds.Tables[0];
            }
            searchListEntity.IsNext = false;
            return searchListEntity;
        }
        #endregion

        #region getActivityList
        public SearchListEntity<reqActivityListEntity[]> getActivityList(ReqData<getActivityListEntity> pEntity)
        {
            SearchListEntity<reqActivityListEntity[]> searchListEntity = new SearchListEntity<reqActivityListEntity[]>();
            DataSet ds = this._currentDAO.getActivityList(pEntity);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                searchListEntity.ItemList = DataLoader.LoadFrom<reqActivityListEntity>(ds.Tables[0]);
            }
            searchListEntity.IsNext = false;
            if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                if ((pEntity.special.pageSize * (pEntity.special.page)) < Convert.ToInt32(ds.Tables[1].Rows[0][0]))
                {
                    searchListEntity.IsNext = true;
                }
            }
            if (ds != null && ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
            {
                if (searchListEntity.ItemList != null && searchListEntity.ItemList.Length > 0)
                {
                    List<reqActivityUserItem> listEntity = new List<reqActivityUserItem>();

                    for (int i = 0; i < searchListEntity.ItemList.Length; i++)
                    {
                        listEntity = new List<reqActivityUserItem>();
                        DataRow[] dtPageRow = ds.Tables[2].Select(" EventID='" + searchListEntity.ItemList[i].ActivityID + "'");
                        if (dtPageRow != null && dtPageRow.Length > 0)
                        {
                            for (int j = 0; j < dtPageRow.Length; j++)
                            {
                                reqActivityUserItem uiEntity = new reqActivityUserItem();
                                uiEntity.UserHeadImg = dtPageRow[j]["VipName"].ToString();
                                uiEntity.UserID = dtPageRow[j]["VipID"].ToString();
                                listEntity.Add(uiEntity);
                            }
                            searchListEntity.ItemList[i].UserItem = listEntity;
                        }
                    }
                }
            }
            return searchListEntity;
        }
        #endregion

        #region getActivityByActivityID
        public reqActivityEntity getActivityByActivityID(ReqData<getActivityByActivityIDEntity> pEntity)
        {
            reqActivityEntity pNewsEntity = new reqActivityEntity();
            DataSet ds = this._currentDAO.getActivityByActivityID(pEntity);
            int? TicketSum = 0;
            int? VipSum = 0;
            int? VipSum2 = 0;


            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                retActivityByActivityIDEntity[] entity = DataLoader.LoadFrom<retActivityByActivityIDEntity>(ds.Tables[0]);
                pNewsEntity.Activity = entity[0];
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    reqTicketEntity[] ticketEntity = DataLoader.LoadFrom<reqTicketEntity>(ds.Tables[1]);
                    if (ticketEntity != null && ticketEntity.Length > 0)
                    {
                        pNewsEntity.Activity.Ticket = ticketEntity.ToList();
                        for (int i = 0; i < ticketEntity.Length; i++)
                        {
                            TicketSum = TicketSum + ticketEntity[i].TicketNum;
                        }
                    }
                }
                if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                {
                    VipSum = Convert.ToInt32(ds.Tables[2].Rows[0][0]);
                    VipSum2 = Convert.ToInt32(ds.Tables[2].Rows[0][1]);
                }
                if (ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
                {
                    pNewsEntity.Activity.TicketID = ds.Tables[3].Rows[0][0].ToString();
                }
                else
                {
                    pNewsEntity.Activity.TicketID = "";
                }
                //if (!string.IsNullOrEmpty(pNewsEntity.Activity.ActivityContent))
                //{
                //    pNewsEntity.Activity.ActivityContent =pNewsEntity.Activity.ActivityContent.Replace("\"", "'").Replace("&quot;","'");
                //}                
                pNewsEntity.Activity.ActivityUp = string.Format("限制{0}人（已报名{1}人,已缴费{2}人）", TicketSum, VipSum, VipSum2);
            }
            return pNewsEntity;
        }
        #endregion

        #region submitActivityInfo
        public string submitActivityInfo(ReqData<submitActivityInfoEntity> pEntity)
        {
            string pEventVipTicketID = Guid.NewGuid().ToString();
            string pVipID = Guid.NewGuid().ToString();
            string pSql = @"insert Vip ({0}VipID,CreateBy,CreateTime,Status,ClientID)values({1}'{6}','{6}',GETDATE(),3,'{5}'); 
                        insert EventVipTicket (EventVipTicketID,EventID,VipID,TicketID,Seat,SginVipID,CustomerID)
                        values('{4}','{7}','{6}','{2}','','{3}','{5}');";
            StringBuilder pColumn = new StringBuilder();
            StringBuilder pValues = new StringBuilder();
            if (pEntity != null && pEntity.special.Control != null && pEntity.special.Control.Count > 0)
            {
                for (int i = 0; i < pEntity.special.Control.Count; i++)
                {
                    ControlUpdateEntity cEntity = pEntity.special.Control[i];
                    if (cEntity != null)
                    {
                        if (!string.IsNullOrEmpty(cEntity.ColumnName))
                        {
                            pColumn.Append(cEntity.ColumnName + ",");
                            pValues.Append("'" + cEntity.Value + "',");
                        }
                    }
                }
                if (string.IsNullOrEmpty(pEntity.common.userId))
                {
                    pEntity.common.userId = pVipID;
                }
                pSql = string.Format(pSql, pColumn.ToString(), pValues.ToString(),
                    pEntity.special.TicketID, pEntity.common.userId, pEventVipTicketID,
                    pEntity.common.customerId, pVipID, pEntity.special.ActivityID);
            }
            if (!string.IsNullOrEmpty(pSql))
            {
                if (this._currentDAO.submitActivityInfo(pSql) > 0)
                {
                    return pEventVipTicketID;
                }
            }
            return "";
        }
        #endregion

        #region submintNewsCountByID
        public int submintNewsCountByID(ReqData<submintNewsCountByIDEntity> pEntity)
        {
            string pSql = @"insert NewsCount(CountType,NewsType,NewsID,VipID,CustomerID,CreateBy)
                            values('{0}','{1}','{2}','{3}','{4}','{3}');";
            string sql = @"";
            int BrowseCount = 0;
            int PraiseCount = 0;
            int CollCount = 0;
            if (pEntity.special.NewsType == "1")
            {
                sql = @"update LNews set 
                         BrowseCount=isnull(BrowseCount,0)+{0},PraiseCount=isnull(PraiseCount,0)+{1},CollCount=isnull(CollCount,0)+{2} 
                         where NewsID='{3}'";
                switch (pEntity.special.CountType)
                {
                    case "1":
                        BrowseCount = 1;
                        break;
                    case "2":
                        PraiseCount = 1;
                        break;
                    case "3":
                        CollCount = 1;
                        break;
                }
                sql = string.Format(sql, BrowseCount, PraiseCount, CollCount, pEntity.special.NewsID);
            }
            pSql = string.Format(pSql, pEntity.special.CountType, pEntity.special.NewsType, pEntity.special.NewsID, pEntity.common.userId, pEntity.common.customerId);
            pSql = pSql + sql;
            return this._currentDAO.ExecuteNonQuery(pSql);
        }
        #endregion

        #region 中欧活动相关接口
        #region getEventList
        /// <summary>
        /// 查询客户的活动信息
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public SearchListEntity<reqActivityListEntity[]> getEventList(ReqData<getActivityListEntity> pEntity)
        {
            SearchListEntity<reqActivityListEntity[]> searchListEntity = new SearchListEntity<reqActivityListEntity[]>();
            DataSet ds = this._currentDAO.getEventList(pEntity);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                searchListEntity.ItemList = DataLoader.LoadFrom<reqActivityListEntity>(ds.Tables[0]);
            }
            //判断是否有下一页
            searchListEntity.IsNext = false;
            if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
            {
                if ((pEntity.special.pageSize * (pEntity.special.page)) < Convert.ToInt32(ds.Tables[1].Rows[0][0]))
                {
                    searchListEntity.IsNext = true;
                }
            }
            if (ds != null && ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
            {
                if (searchListEntity.ItemList != null && searchListEntity.ItemList.Length > 0)
                {
                    List<reqActivityUserItem> listEntity = new List<reqActivityUserItem>();

                    //动态匹配活动的报名人员信息，这个地方用不到
                    for (int i = 0; i < searchListEntity.ItemList.Length; i++)
                    {
                        listEntity = new List<reqActivityUserItem>();
                        DataRow[] dtPageRow = ds.Tables[2].Select(" EventID='" + searchListEntity.ItemList[i].ActivityID + "'");
                        if (dtPageRow != null && dtPageRow.Length > 0)
                        {
                            for (int j = 0; j < dtPageRow.Length; j++)
                            {
                                reqActivityUserItem uiEntity = new reqActivityUserItem();
                                uiEntity.UserHeadImg = dtPageRow[j]["VipName"].ToString();
                                uiEntity.UserID = dtPageRow[j]["VipID"].ToString();
                                listEntity.Add(uiEntity);
                            }
                            searchListEntity.ItemList[i].UserItem = listEntity;
                        }
                    }
                }
            }
            return searchListEntity;
        }
        #endregion

        #region getEventByEventID
        /// <summary>
        /// 根据活动ID查询活动的详细信息，包含票务信息，报名信息
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public reqActivityEntity getEventByEventID(ReqData<getActivityByActivityIDEntity> pEntity)
        {
            reqActivityEntity pNewsEntity = new reqActivityEntity();
            DataSet ds = this._currentDAO.getEventByEventID(pEntity);
            int? TicketSum = 0;
            int? VipSum = 0;
            int? VipSum2 = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                retActivityByActivityIDEntity[] entity = DataLoader.LoadFrom<retActivityByActivityIDEntity>(ds.Tables[0]);
                pNewsEntity.Activity = entity[0];
                //计算票的总报名量
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    reqTicketEntity[] ticketEntity = DataLoader.LoadFrom<reqTicketEntity>(ds.Tables[1]);
                    if (ticketEntity != null && ticketEntity.Length > 0)
                    {
                        pNewsEntity.Activity.Ticket = ticketEntity.ToList();
                        for (int i = 0; i < ticketEntity.Length; i++)
                        {
                            TicketSum = TicketSum + ticketEntity[i].TicketNum;
                        }
                    }
                }
                //已报名人数和已缴费人数
                if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                {
                    VipSum = Convert.ToInt32(ds.Tables[2].Rows[0][0]);
                    VipSum2 = Convert.ToInt32(ds.Tables[2].Rows[0][1]);
                }
                if (ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
                {
                    pNewsEntity.Activity.TicketID = ds.Tables[3].Rows[0][0].ToString();
                }
                else
                {
                    pNewsEntity.Activity.TicketID = "";
                }
                if (pNewsEntity.Activity.IsTicketRequired != null && pNewsEntity.Activity.IsTicketRequired.Value == 1)//只有有门票的时候才显示限制人数
                {
                    pNewsEntity.Activity.ActivityUp = string.Format("限制{0}人（已报名{1}人,已缴费{2}人）", TicketSum, VipSum, VipSum2);
                }
                else
                {
                    pNewsEntity.Activity.ActivityUp = string.Format("已报名{0}人", VipSum);
                }

                pNewsEntity.Activity.TicketSum = TicketSum;
                pNewsEntity.Activity.VipSum = VipSum;
            }
            return pNewsEntity;
        }

        /// <summary>
        /// 根据活动ID获取活动信息，包含已报名和已缴费的会员信息，以及票的信息
        /// </summary>
        /// <param name="eventId">活动Id</param>
        /// <returns></returns>
        public reqActivityEntity GetEventDetail(string eventId)
        {
            reqActivityEntity pNewsEntity = new reqActivityEntity();
            DataSet ds = this._currentDAO.GetEventDetail(eventId);
            int? TicketSum = 0;
            int? VipSum = 0;
            int? VipSum2 = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                retActivityByActivityIDEntity[] entity = DataLoader.LoadFrom<retActivityByActivityIDEntity>(ds.Tables[0]);
                pNewsEntity.Activity = entity[0];
                //计算票的总报名量
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    reqTicketEntity[] ticketEntity = DataLoader.LoadFrom<reqTicketEntity>(ds.Tables[1]);
                    if (ticketEntity != null && ticketEntity.Length > 0)
                    {
                        pNewsEntity.Activity.Ticket = ticketEntity.ToList();
                        for (int i = 0; i < ticketEntity.Length; i++)
                        {
                            TicketSum = TicketSum + ticketEntity[i].TicketNum;
                        }
                    }
                }
                //已报名人数和已缴费人数
                if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                {
                    VipSum = Convert.ToInt32(ds.Tables[2].Rows[0][0]);
                    VipSum2 = Convert.ToInt32(ds.Tables[2].Rows[0][1]);
                }
                if (ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
                {
                    pNewsEntity.Activity.TicketID = ds.Tables[3].Rows[0][0].ToString();
                }
                else
                {
                    pNewsEntity.Activity.TicketID = "";
                }
                if (pNewsEntity.Activity.IsTicketRequired != null && pNewsEntity.Activity.IsTicketRequired.Value == 1)//只有有门票的时候才显示限制人数
                {
                    pNewsEntity.Activity.ActivityUp = string.Format("限制{0}人（已报名{1}人,已缴费{2}人）", TicketSum, VipSum, VipSum2);
                }
                else
                {
                    pNewsEntity.Activity.ActivityUp = string.Format("已报名{0}人", VipSum);
                }

                pNewsEntity.Activity.TicketSum = TicketSum;
                pNewsEntity.Activity.VipSum = VipSum;
            }
            return pNewsEntity;
        }
        #endregion

        #region submitEventInfo
        /// <summary>
        /// 活动报名
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public string submitEventInfo(ReqData<submitActivityInfoEntity> pEntity)
        {
            string phone = "";
            StringBuilder pColumn = new StringBuilder();
            StringBuilder pValues = new StringBuilder();
            string VipID = "";
            string pVipID = "";
            //判断是否登录
            if (string.IsNullOrEmpty(pEntity.common.userId))
            {
                if (pEntity != null && pEntity.special.Control != null && pEntity.special.Control.Count > 0)
                {
                    for (int i = 0; i < pEntity.special.Control.Count; i++)
                    {
                        ControlUpdateEntity cEntity = pEntity.special.Control[i];
                        if (cEntity != null)
                        {
                            if (!string.IsNullOrEmpty(cEntity.ColumnName))
                            {
                                if (cEntity.ColumnName.ToString().ToLower() == "phone")
                                {
                                    phone = cEntity.Value;
                                }
                                pColumn.Append(cEntity.ColumnName + ",");
                                pValues.Append("'" + cEntity.Value + "',");
                            }
                        }
                    }
                }
                string phoneSql = string.Format(@"select vipID from Vip where IsDelete=0  and ClientID='{0}' and Phone='{1}'", pEntity.common.customerId, phone); ;
                object userID = this._currentDAO.ExecuteScalar(phoneSql);
                //判断电话号码是否存在
                if (userID == null || string.IsNullOrEmpty(userID.ToString()))
                {
                    //不存在，添加
                    pVipID = Guid.NewGuid().ToString();
                    //添加用户信息
                    string pUserSql = @"insert vip ({0}VipID,CreateBy,CreateTime,ClientID,Status)values({1}'{2}','1',GETDATE(),'{3}','3');
                         insert VIPRoleMapping(VipId,RoleId,ClientID)values('{2}','{4}','{3}');";
                    pUserSql = string.Format(pUserSql, pColumn.ToString(), pValues.ToString(), pVipID, pEntity.common.customerId, pEntity.common.roleid);
                    this._currentDAO.ExecuteNonQuery(pUserSql).ToString();
                    VipID = pVipID;
                }
                else
                {
                    VipID = userID.ToString();
                }
            }
            else
            {
                //已登录
                VipID = pEntity.common.userId;
            }

            #region 添加订单信息
            string OrderID = "";

            LEventsEntity eventEntity = new LEventsBLL(this.CurrentUserInfo).GetByID(pEntity.special.ActivityID);
            if (eventEntity.IsTicketRequired.HasValue && eventEntity.IsTicketRequired.Value == 1)//如果需要问卷的才提交订单，不需要的不提交
            {
                OrderID = BaseService.NewGuidPub();
                InoutService inoutService = new InoutService(this.CurrentUserInfo);
                #region 设置参数
                InoutService service = new InoutService(this.CurrentUserInfo);
                SetOrderEntity orderInfo = new SetOrderEntity();
                orderInfo.TotalQty = 1; //商品数量 【必须】
                orderInfo.OrderId = OrderID;
                orderInfo.CreateBy = VipID;
                orderInfo.CustomerId = pEntity.common.customerId;
                if (pEntity.special.TicketPrice != null)
                {
                    orderInfo.TotalAmount = pEntity.special.TicketPrice.Value; ;//订单总价 【必须】
                    orderInfo.ActualAmount = pEntity.special.TicketPrice.Value; ;//订单总价 【必须】 
                }
                else
                {
                    orderInfo.TotalAmount = 0m;
                    orderInfo.ActualAmount = 0m;
                }
                orderInfo.Status = "100";
                orderInfo.StatusDesc = "未审核";
                //获取订单号
                TUnitExpandBLL serviceUnitExpand = new TUnitExpandBLL(this.CurrentUserInfo);
                orderInfo.OrderCode = serviceUnitExpand.GetUnitOrderNo(this.CurrentUserInfo, "1");
                orderInfo.OrderDetailInfoList = new List<InoutDetailInfo>();
                InoutDetailInfo orderDetailInfo = new InoutDetailInfo();
                orderDetailInfo.order_id = orderInfo.OrderId;
                orderDetailInfo.order_detail_id = BaseService.NewGuidPub();
                orderDetailInfo.sku_id = pEntity.special.TicketID;
                orderDetailInfo.std_price = orderInfo.TotalAmount;
                orderDetailInfo.enter_qty = 1;
                orderDetailInfo.order_qty = 1;
                orderDetailInfo.display_index = 1;
                orderDetailInfo.enter_price = orderDetailInfo.std_price;
                orderDetailInfo.enter_amount = orderDetailInfo.std_price;
                orderDetailInfo.retail_price = orderDetailInfo.std_price;
                orderDetailInfo.retail_amount = orderDetailInfo.std_price;
                orderDetailInfo.discount_rate = 100;
                orderInfo.OrderDetailInfoList.Add(orderDetailInfo);
                #endregion
                string strError = string.Empty;
                string strMsg = string.Empty;
                bool bReturn = service.SetOrderOnlineShoppingNew(orderInfo, out strError, out strMsg);
            }
            #endregion

            #region 添加订单关联信息
            string pSql = @"insert LEventSignUp({0}signUpID,EventID,VipID,IsDelete)
                            values({1}newID(),'{2}','{3}',0)
                            insert LEventsVipObject(mappingID,EventId,VipId,ObjectId)
                            values(newID(),'{2}','{3}','{4}');";
            pColumn = new StringBuilder();
            pValues = new StringBuilder();
            if (pEntity != null && pEntity.special.Control != null && pEntity.special.Control.Count > 0)
            {
                for (int i = 0; i < pEntity.special.Control.Count; i++)
                {
                    ControlUpdateEntity cEntity = pEntity.special.Control[i];
                    if (cEntity != null)
                    {
                        if (!string.IsNullOrEmpty(cEntity.ColumnName))
                        {
                            if (cEntity.ColumnName.ToString().ToLower() == "vipname")
                            {
                                cEntity.ColumnName = "UserName";
                            }
                            pColumn.Append(cEntity.ColumnName + ",");
                            pValues.Append("'" + cEntity.Value + "',");
                        }
                    }
                }
                pSql = string.Format(pSql, pColumn.ToString(), pValues.ToString(), pEntity.special.ActivityID
                    , VipID, OrderID);
            }
            if (!string.IsNullOrEmpty(pSql))
            {
                if (this._currentDAO.submitEventInfo(pSql) > 0)
                {
                    return OrderID;
                }
            }
            #endregion
            return "";
        }
        #endregion
        #endregion

        #region getEventTypeList
        public EventsTypeEntity[] getEventTypeList()
        {
            return this._currentDAO.getEventTypeList();
        }
        #endregion

        #region getNewsType
        public LNewsTypeEntity[] getNewsType(int ChannelCode)
        {
            return this._currentDAO.getNewsType(ChannelCode);
        }
        #endregion

        #region getMyEventList
        public DataSet getMyEventList()
        {
            return this._currentDAO.getMyEventList();
        }
        #endregion

        #region register
        public int register(submitUserInfoEntity pEntity)
        {
            string pSql = "";
            if (string.IsNullOrEmpty(pEntity.common.userId))
            {
                string pUserID = Guid.NewGuid().ToString();
                pSql = @"insert vip ({0}VipID,CreateBy,CreateTime,ClientID)values({1}'{2}','1',GETDATE(),'{3}');
                         insert VIPRoleMapping(VipId,RoleId,ClientID)values('{2}','{4}','{3}');
                        ";
                StringBuilder pColumn = new StringBuilder();
                StringBuilder pValues = new StringBuilder();
                if (pEntity != null && pEntity.special.Control != null && pEntity.special.Control.Count > 0)
                {
                    for (int i = 0; i < pEntity.special.Control.Count; i++)
                    {
                        pEntity.common.userId = pUserID;
                        ControlUpdateEntity cEntity = pEntity.special.Control[i];
                        if (cEntity != null)
                        {
                            if (!string.IsNullOrEmpty(cEntity.ColumnName))
                            {
                                pColumn.Append(cEntity.ColumnName + ",");
                                pValues.Append("'" + cEntity.Value + "',");
                            }
                        }
                    }
                    pSql = string.Format(pSql, pColumn.ToString(), pValues.ToString(), pUserID, pEntity.common.customerId, pEntity.common.roleid);
                }
            }
            else
            {
                pSql = "update VIP set {0}LastUpdateBy='{1}',LastUpdateTime=GETDATE() where VIPID='{1}'";
                StringBuilder pColumn = new StringBuilder();
                if (pEntity != null && pEntity.special.Control != null && pEntity.special.Control.Count > 0)
                {
                    for (int i = 0; i < pEntity.special.Control.Count; i++)
                    {
                        ControlUpdateEntity cEntity = pEntity.special.Control[i];
                        if (cEntity != null)
                        {
                            if (!string.IsNullOrEmpty(cEntity.ColumnName))
                            {
                                pColumn.Append(cEntity.ColumnName + "='" + cEntity.Value + "',");
                            }
                        }
                    }
                    pSql = string.Format(pSql, pColumn.ToString(), pEntity.common.userId);
                }
            }
            if (string.IsNullOrEmpty(pSql))
            {
                return 0;
            }
            return this._currentDAO.submitUserInfo(pSql);
        }
        #endregion

        #region GetPageBlockID
        /// <summary>
        /// 根据配置ID获取模块ID
        /// </summary>
        /// <param name="pDefindID"></param>
        /// <returns></returns>
        public List<BlockEntity> GetPageBlockID(string pDefindID)
        {
            DataSet ds = this._currentDAO.GetPageBlockID(pDefindID);
            List<BlockEntity> lBlockEntity = new List<BlockEntity>();
            if (ds != null && ds.Tables != null)
            {
                DataTable dtPage = null;
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    dtPage = ds.Tables[0];
                }
                if (dtPage != null)
                {
                    DataRow[] dtPageRow = dtPage.Select(" Type=2", " Sort asc");
                    if (dtPageRow != null && dtPageRow.Length > 0)
                    {
                        lBlockEntity = new List<BlockEntity>();
                        for (int j = 0; j < dtPageRow.Length; j++)
                        {
                            BlockEntity blockEntity = new BlockEntity();
                            blockEntity.BlockName = dtPageRow[j]["title"].ToString();
                            blockEntity.BlockSort = Convert.ToInt32(dtPageRow[j]["Sort"]);
                            blockEntity.Remark = dtPageRow[j]["remark"].ToString();

                            lBlockEntity.Add(blockEntity);
                        }
                    }
                }
            }
            return lBlockEntity;
        }
        #endregion

        #region submitEventsInfo
        //        /// <summary>
        //        /// 活动报名
        //        /// </summary>
        //        /// <param name="pEntity"></param>
        //        /// <returns></returns>
        //        public string submitEventsInfo(ReqData<submitActivityInfoEntity> pEntity)
        //        {
        //            string phone = "";
        //            StringBuilder pColumn = new StringBuilder();
        //            StringBuilder pValues = new StringBuilder();
        //            string VipID = "";
        //            string pVipID = "";
        //            //判断是否登录
        //            if (string.IsNullOrEmpty(pEntity.common.userId))
        //            {
        //                if (pEntity != null && pEntity.special.Control != null && pEntity.special.Control.Count > 0)
        //                {
        //                    for (int i = 0; i < pEntity.special.Control.Count; i++)
        //                    {
        //                        ControlUpdateEntity cEntity = pEntity.special.Control[i];
        //                        if (cEntity != null)
        //                        {
        //                            if (!string.IsNullOrEmpty(cEntity.ColumnName))
        //                            {
        //                                if (cEntity.ColumnName.ToString().ToLower() == "phone")
        //                                {
        //                                    phone = cEntity.Value;
        //                                }
        //                                pColumn.Append(cEntity.ColumnName + ",");
        //                                pValues.Append("'" + cEntity.Value + "',");
        //                            }
        //                        }
        //                    }
        //                }
        //                string phoneSql = string.Format(@"select vipID from Vip where IsDelete=0  and ClientID='{0}' and Phone='{1}'", pEntity.common.customerId, phone); ;
        //                object userID = this._currentDAO.ExecuteScalar(phoneSql);
        //                //判断电话号码是否存在
        //                if (userID == null || string.IsNullOrEmpty(userID.ToString()))
        //                {
        //                    //不存在，添加
        //                    pVipID = Guid.NewGuid().ToString();
        //                    //添加用户信息
        //                    string pUserSql = @"insert vip ({0}VipID,CreateBy,CreateTime,ClientID,Status)values({1}'{2}','1',GETDATE(),'{3}','3');
        //                         insert VIPRoleMapping(VipId,RoleId,ClientID)values('{2}','{4}','{3}');";
        //                    pUserSql = string.Format(pUserSql, pColumn.ToString(), pValues.ToString(), pVipID, pEntity.common.customerId, pEntity.common.roleid);
        //                    this._currentDAO.ExecuteNonQuery(pUserSql).ToString();
        //                    VipID = pVipID;
        //                }
        //                else
        //                {
        //                    VipID = userID.ToString();
        //                }
        //            }
        //            else
        //            {
        //                //已登录
        //                VipID = pEntity.common.userId;
        //            }

        //            #region 添加订单信息
        //            string OrderID = "";
        //            LEventsEntity eventEntity = new LEventsBLL(this.CurrentUserInfo).GetByID(pEntity.special.ActivityID);
        //            if (eventEntity.IsTicketRequired.HasValue && eventEntity.IsTicketRequired.Value == 1)//如果需要问卷的才提交订单，不需要的不提交
        //            {
        //                OrderID = BaseService.NewGuidPub();
        //                InoutService inoutService = new InoutService(this.CurrentUserInfo);
        //                #region 设置参数
        //                InoutService service = new InoutService(this.CurrentUserInfo);
        //                SetOrderEntity orderInfo = new SetOrderEntity();
        //                orderInfo.TotalQty = 1; //商品数量 【必须】
        //                orderInfo.OrderId = OrderID;
        //                orderInfo.CreateBy = VipID;
        //                orderInfo.CustomerId = pEntity.common.customerId;
        //                if (pEntity.special.TicketPrice != null)
        //                {
        //                    orderInfo.TotalAmount = pEntity.special.TicketPrice.Value;//订单总价 【必须】
        //                    orderInfo.ActualAmount = pEntity.special.TicketPrice.Value;//订单总价 【必须】 
        //                }
        //                else
        //                {
        //                    orderInfo.TotalAmount = 0m;
        //                    orderInfo.ActualAmount = 0m;
        //                }
        //                orderInfo.Status = "100";
        //                orderInfo.StatusDesc = "未审核";
        //                //获取订单号
        //                TUnitExpandBLL serviceUnitExpand = new TUnitExpandBLL(this.CurrentUserInfo);
        //                orderInfo.OrderCode = serviceUnitExpand.GetUnitOrderNo(this.CurrentUserInfo, "1");
        //                orderInfo.OrderDetailInfoList = new List<InoutDetailInfo>();
        //                InoutDetailInfo orderDetailInfo = new InoutDetailInfo();
        //                orderDetailInfo.order_id = orderInfo.OrderId;
        //                orderDetailInfo.order_detail_id = BaseService.NewGuidPub();
        //                orderDetailInfo.sku_id = pEntity.special.TicketID;
        //                orderDetailInfo.std_price = orderInfo.TotalAmount;
        //                orderDetailInfo.enter_qty = 1;
        //                orderDetailInfo.order_qty = 1;
        //                orderDetailInfo.display_index = 1;
        //                orderDetailInfo.enter_price = orderDetailInfo.std_price;
        //                orderDetailInfo.enter_amount = orderDetailInfo.std_price;
        //                orderDetailInfo.retail_price = orderDetailInfo.std_price;
        //                orderDetailInfo.retail_amount = orderDetailInfo.std_price;
        //                orderDetailInfo.discount_rate = 100;
        //                orderInfo.OrderDetailInfoList.Add(orderDetailInfo);
        //                #endregion
        //                string strError = string.Empty;
        //                string strMsg = string.Empty;
        //                bool bReturn = service.SetOrderOnlineShoppingNew(orderInfo, out strError, out strMsg);
        //            }
        //            #endregion


        //            #region 添加订单关联信息
        //            string pSql = @"insert LEventSignUp({0}signUpID,EventID,VipID,IsDelete)
        //                            values({1}newID(),'{2}','{3}',0)
        //                            insert LEventsVipObject(mappingID,EventId,VipId,ObjectId)
        //                            values(newID(),'{2}','{3}','{4}');";
        //            pColumn = new StringBuilder();
        //            pValues = new StringBuilder();
        //            if (pEntity != null && pEntity.special.Control != null && pEntity.special.Control.Count > 0)
        //            {
        //                for (int i = 0; i < pEntity.special.Control.Count; i++)
        //                {
        //                    ControlUpdateEntity cEntity = pEntity.special.Control[i];
        //                    if (cEntity != null)
        //                    {
        //                        if (!string.IsNullOrEmpty(cEntity.ColumnName))
        //                        {
        //                            if (cEntity.ColumnName.ToString().ToLower() == "vipname")
        //                            {
        //                                cEntity.ColumnName = "UserName";
        //                            }
        //                            pColumn.Append(cEntity.ColumnName + ",");
        //                            pValues.Append("'" + cEntity.Value + "',");
        //                        }
        //                    }
        //                }
        //                pSql = string.Format(pSql, pColumn.ToString(), pValues.ToString(), pEntity.special.ActivityID
        //                    , VipID, OrderID);
        //            }
        //            #endregion

        //            if (!string.IsNullOrEmpty(pSql))
        //            {
        //                if (this._currentDAO.submitEventInfo(pSql) > 0)
        //                {
        //                    return OrderID + "|" + VipID;
        //                }
        //            }

        //            return "";
        //        }
        #endregion

        #region submitEventsInfo
        /// <summary>
        /// 活动报名
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public int submitEventsInfo(ReqData<submitActivityInfoEntity> pEntity)
        {
            StringBuilder pColumn = new StringBuilder();
            StringBuilder pValues = new StringBuilder();
            string VipID = "";
            //判断是否登录
            if (!string.IsNullOrEmpty(pEntity.common.userId))
            {
                //已登录
                VipID = pEntity.common.userId;
            }

            string pSql = @"insert LEventSignUp({0}signUpID,EventID,VipID,IsDelete, CustomerId)
                            values({1}newID(),'{2}','{3}',0, '" + CurrentUserInfo.ClientID + "');";

            if (pEntity != null && pEntity.special.Control != null && pEntity.special.Control.Count > 0)
            {
                for (int i = 0; i < pEntity.special.Control.Count; i++)
                {
                    ControlUpdateEntity cEntity = pEntity.special.Control[i];
                    if (cEntity != null)
                    {
                        if (!string.IsNullOrEmpty(cEntity.ColumnName))
                        {
                            if (cEntity.ColumnName.ToString().ToLower() == "vipname")
                            {
                                cEntity.ColumnName = "UserName";
                            }
                            pColumn.Append(cEntity.ColumnName + ",");
                            pValues.Append("'" + cEntity.Value + "',");
                        }
                    }
                }
                pSql = string.Format(pSql, pColumn.ToString(), pValues.ToString(), pEntity.special.ActivityID
                    , VipID);
            }

            return this._currentDAO.submitEventInfo(pSql);
        }

        /// <summary>
        /// 活动报名 Add By Alan 2014-08-29
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        public int SignUpEvent(submitActivityInfoEntity pEntity)
        {
            StringBuilder pColumn = new StringBuilder();
            StringBuilder pValues = new StringBuilder();
            string VipID = CurrentUserInfo.UserID ?? string.Empty;

            string pSql = @"insert LEventSignUp({0}signUpID,EventID,VipID,IsDelete,CustomerID)
                            values({1}newID(),'{2}','{3}',0,'{4}');";

            if (pEntity != null && pEntity.Control != null && pEntity.Control.Count > 0)
            {
                for (int i = 0; i < pEntity.Control.Count; i++)
                {
                    ControlUpdateEntity cEntity = pEntity.Control[i];
                    if (cEntity != null)
                    {
                        if (!string.IsNullOrEmpty(cEntity.ColumnName))
                        {
                            if (cEntity.ColumnName.ToString().ToLower() == "vipname")
                            {
                                cEntity.ColumnName = "UserName";
                            }
                            pColumn.Append(cEntity.ColumnName + ",");
                            pValues.Append("'" + cEntity.Value + "',");
                        }
                    }
                }
                pSql = string.Format(pSql, pColumn.ToString(), pValues.ToString(), pEntity.ActivityID, VipID,CurrentUserInfo.ClientID);
            }
            return this._currentDAO.submitEventInfo(pSql);
        }
        #endregion

        #endregion
    }
}