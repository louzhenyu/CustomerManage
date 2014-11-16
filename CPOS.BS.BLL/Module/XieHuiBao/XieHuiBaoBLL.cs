using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using JIT.Utility;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.Entity;
using JIT.Utility.Reflection;
using JIT.CPOS.BS.DataAccess;
using JIT.Utility.DataAccess;
using JIT.Utility.ExtensionMethod;
using JIT.Utility.DataAccess.Query;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// XieHuiBaoBLL 
    /// </summary>
    public partial class XieHuiBaoBLL
    {
        public LoggingSessionInfo CurrentUserInfo;
        public XieHuiBaoDAO _currentDAO;
        public string _pTableName;

        #region 构造函数
        /// <summary>
        /// 构造函数 
        /// </summary>
        public XieHuiBaoBLL(LoggingSessionInfo pUserInfo, string pTableName)
        {
            _pTableName = pTableName;
            this.CurrentUserInfo = pUserInfo;
            this._currentDAO = new XieHuiBaoDAO(pUserInfo, pTableName);
        }
        #endregion

        #region EMBA
        #region GetList
        /// <summary>
        /// 获取会员列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetList(List<DefindControlEntity> pSearch, int? pPageSize, int? pPageIndex)
        {
            return this._currentDAO.GetList(pSearch, pPageSize, pPageIndex);
        }
        #endregion

        #region GetVipDetail
        /// <summary>
        /// 通过Vipid获取vip信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet GetVipDetail(string id)
        {
            return this._currentDAO.GetVipDetail(id);
        }
        #endregion

        #region GetModuleColumn
        /// <summary>
        /// 获取显示列头
        /// </summary>
        /// <returns></returns>
        public DataSet GetModuleColumn()
        {
            return this._currentDAO.GetModuleColumn();
        }
        #endregion

        #region GetStatus
        /// <summary>
        /// 获取状态
        /// </summary>
        /// <returns></returns>
        public DataSet GetVipStatus()
        {
            return this._currentDAO.GetVipStatus();
        }
        #endregion

        #region VipApprove
        /// <summary>
        ///审核
        /// </summary>
        /// <param name="VipId">用户Id</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public int VipApprove(string VipId, string Status)
        {
            return this._currentDAO.VipApprove(VipId, Status);
        }
        #endregion

        #region NoVipApprove
        /// <summary>
        /// 审核不通过
        /// </summary>
        /// <param name="VipId">用户Id</param>
        /// <param name="Remarke">备注信息</param>
        /// <param name="Status">状态</param>
        /// <returns></returns>
        public int NoVipApprove(string VipId, string Remarke, string Status)
        {
            return this._currentDAO.NoVipApprove(VipId, Remarke, Status);
        }
        #endregion

        #region
        public DataSet GetVipStatusNum(List<DefindControlEntity> pSearch)
        {
            return this._currentDAO.GetVipStatusNum(pSearch);

        }
        #endregion

        #region GetPageData
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="pSearch">查询条件</param>
        /// <param name="pPageSize">分页数</param>
        /// <param name="pPageIndex">当前页</param>
        /// <returns>数据，记录数，页数</returns>
        public PageResultEntity GetPageData(List<DefindControlEntity> pSearch, int? pPageSize, int? pPageIndex, int? pType)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select ");
            sql.Append(this._currentDAO.GetVIPGridFildSQL(pType)); //获取字SQL
            sql.AppendLine("ROW_NUMBER() OVER( order by main.CreateTime desc) ROW_NUMBER,");
            sql.AppendLine("main.VIPID,t_status.OptionText Status into #outTemp");//4-16  ,main.Col14,main.Col15
            sql.AppendLine("from VIP main");
            sql.Append(this._currentDAO.GetVIPLeftGridJoinSQL(pType)); //获取联接SQL
            sql.AppendLine(string.Format("inner join options t_status on t_status.OptionName='VipStatus' and isnull( t_status.ClientID,'{0}')='{0}' and t_status.OptionValue=main.status and t_status.IsDelete=0", CurrentUserInfo.ClientID));
            sql.AppendLine("");
            sql.Append(this._currentDAO.GetVIPSearchJoinSQL(pSearch)); //获取条件联接SQL
            sql.AppendLine(string.Format("Where main.IsDelete=0 and main.ClientID='{0}'", CurrentUserInfo.ClientID));
            sql.Append(this._currentDAO.GetStroeGridSearchSQL(pSearch)); //获取条件
            sql.Append(this._currentDAO.GetPubPageSQL(pPageSize, pPageIndex));
            return new VIPDefindModuleBLL(CurrentUserInfo, "VIP").GetPageData(sql.ToString());
        }
        #endregion

        #region 获取定义
        public List<GridColumnModelEntity> GetGridDataModels(int? pType)
        {
            return this._currentDAO.GetGridDataModels(pType);
        }

        public List<GridColumnEntity> GetGridColumns(int? pType)
        {
            return this._currentDAO.GetGridColumns(pType);
        }
        #endregion

        #region 获取定义 2014-04-21 修改者:tiansheng.zhu

        #region GetGridDataModelsByEventID 根据活动获取列的定义  
        /// <summary>
        /// 获取列的定义
        /// </summary>
        /// <param name="pEventID">活动ID</param>
        /// <returns></returns>
        public List<GridColumnModelEntity> GetGridDataModelsByEventID(string pEventID)
        {
            return this._currentDAO.GetGridDataModelsByEventID(pEventID);
        }
        #endregion

        #region GetGridColumnsByEventID 根据活动获取配置文件字段
        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <param name="pEventID">活动ID</param>
        /// <returns></returns>
        public List<GridColumnEntity> GetGridColumnsByEventID(string pEventID)
        {
            return this._currentDAO.GetGridColumnsByEventID(pEventID);
        }
        #endregion

        #region GetPageDataByEventID 根据活动获取查询数据
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="pSearch">查询条件</param>
        /// <param name="pPageSize">分页数</param>
        /// <param name="pPageIndex">当前页</param>
        /// <returns>数据，记录数，页数</returns>
        public PageResultEntity GetPageDataByEventID(List<DefindControlEntity> pSearch, int? pPageSize, int? pPageIndex, string pEventID)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select main.SignUpID,");
            sql.Append(this._currentDAO.GetPubGridFildSQLByEventID(pEventID)); //获取字SQL
            sql.AppendLine(" ROW_NUMBER() OVER( order by main.CreateTime desc) ROW_NUMBER");
            sql.AppendLine( " into #outTemp ");//4-16  ,main.Col14,main.Col15
            sql.AppendLine(" from LEventSignUp main");
            sql.Append(this._currentDAO.GetPubLeftGridJoinSQLByEventID(pEventID)); //获取联接SQL
            sql.AppendLine("");
            sql.Append(this._currentDAO.GetVIPSearchJoinSQL(pSearch)); //获取条件联接SQL
            sql.AppendLine(string.Format("Where main.IsDelete=0  and main.EventID='{0}'",pEventID));
            sql.Append(this._currentDAO.GetStroeGridSearchSQL(pSearch)); //获取条件
            sql.Append(this._currentDAO.GetPubPageSQL(pPageSize, pPageIndex));
            return new VIPDefindModuleBLL(CurrentUserInfo, "VIP").GetPageData(sql.ToString());
        }
        #endregion
       
        #endregion

        #region GetEditControls
        /// <summary>
        /// 获取编辑页面的控件 
        /// </summary>
        /// <returns></returns>
        public List<DefindControlEntity> GetEditControls()
        {
            return this._currentDAO.GetEditControls();
        }
        #endregion

        #region GetEditData
        /// <summary>
        /// 获取编辑页面的值
        /// </summary>
        /// <returns></returns>
        public List<DefindControlEntity> GetEditData(string pKeyValue, int? pType)
        {
            return this._currentDAO.GetEditData(pKeyValue, pType);
        }
        #endregion

        #region GetUserList
        /// <summary>
        /// 获取活动人员列表
        /// </summary>
        /// <param name="pParems"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedQueryResult<VipViewEntity> GetUserList(Dictionary<string, string> pParems, int pageIndex, int pageSize)
        {
            return _currentDAO.GetUserList(pParems, pageIndex, pageSize);
        }
        #endregion

        #region ExportUserList
        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <param name="pParams"></param>
        /// <returns></returns>
        public DataSet ExportUserList(Dictionary<string, string> pParems)
        {
            return this._currentDAO.ExportUserList(pParems);
        }
        #endregion

        #region DeleteEventVipMapping
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pID"></param>
        public void DeleteEventVipMapping(string pID)
        {
            this._currentDAO.DeleteEventVipMapping(pID);
        }
        #endregion

        #region Update
        /// <summary>
        /// 修改终端
        /// </summary>
        /// <param name="pEditValue">传进来数据值</param>
        /// <param name="pKeyValue">主健值</param>
        /// <returns>成功/失败</returns>
        public bool Update(List<DefindControlEntity> pEditValue, string pKeyValue)
        {
            return this._currentDAO.Update(pEditValue, pKeyValue);
        }
        #endregion
        #endregion

        #region CEIBS
        #region EventStatsPageData
        public DataSet EventStatsPageData(string Type, string ObjectID, int? pPageSize, int? pPageIndex)
        {
            return this._currentDAO.EventStatsPageData(Type, ObjectID, pPageSize, pPageIndex);

        }
        #endregion

        #region GetOptionID
        /// <summary>
        ///  根据Option单据类型获取单据信息
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public DataSet GetOptionID(string objectType)
        {
            return this._currentDAO.GetOptionID(objectType);
        }
        #endregion

        #region EventStatsSave
        /// <summary>
        /// 保存最受关注信息
        /// </summary>
        /// <param name="eventStatsID">主键id</param>
        /// <param name="objectType">类型</param>
        /// <param name="objectID">类型id</param>
        /// <param name="sequence">排序</param>
        /// <returns>影响行数</returns>
        public int EventStatsSave(string id, string objectType, string objectID, string sequence)
        {
            return this._currentDAO.SaveEventStats(id, objectType, objectID, "0", "0", "0", sequence);
        }
        #endregion

        #region DelEventStats
        /// <summary>
        /// 删除最受关注信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DelEventStats(string id)
        {
            return this._currentDAO.DelEventStats(id);
        }
        #endregion

        #region GetEventStatsDetail
        /// <summary>
        /// 根据Id获取最受关注明细信息
        /// </summary>
        /// <param name="eventStatsID">主键id</param>
        /// <returns></returns>
        public DataSet GetEventStatsDetail(string eventStatsID)
        {
            return this._currentDAO.GetEventStatsDetail(eventStatsID);
        }
        #endregion

        #region GetAlbumList
        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <param name="pVipID">会员ID</param>
        /// <param name="rowCount">视频个数</param>
        /// <returns></returns>
        public List<LEventsAlbumViewEntity> GetAlbumList(string pVipID, int pageSize, int pageIndex, out int rowCount)
        {
            List<LEventsAlbumViewEntity> videoList = null;
            DataSet ds = new DataSet();
            ds = this._currentDAO.GetAlbumList(pVipID, pageSize, pageIndex);
            rowCount = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                videoList = DataTableToObject.ConvertToList<LEventsAlbumViewEntity>(ds.Tables[0]);
                int intout = 0;
                rowCount = int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out intout) == true ? intout : 0;
            }
            return videoList;
        }
        #endregion

        #region GetEventAlbumList
        /// <summary>
        /// 获取视频列表
        /// </summary>
        /// <param name="pVipID">会员ID</param>
        /// <param name="rowCount">视频个数</param>
        /// <returns></returns>
        public List<LEventsAlbumViewEntity> GetEventAlbumList(string pVipID, int pageSize, int pageIndex, out int rowCount)
        {
            List<LEventsAlbumViewEntity> videoList = null;
            DataSet ds = new DataSet();
            ds = this._currentDAO.GetEventAlbumList(pVipID, pageSize, pageIndex);
            rowCount = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                videoList = DataTableToObject.ConvertToList<LEventsAlbumViewEntity>(ds.Tables[0]);
                int intout = 0;
                rowCount = int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out intout) == true ? intout : 0;
            }
            return videoList;
        }
        #endregion

        #region AddNewsCount
        /// <summary>
        /// 浏览 收藏数据的操作
        /// </summary>
        /// <param name="pNewsID">数据ID</param>
        /// <param name="pVipID">用户ID</param>
        /// <param name="pCountType">操作类型 <1.BrowseCount(浏览数量) 2.PraiseCount(赞的数量) 3.KeepCount(收藏数量)> </param>
        /// <param name="pNewsType">数据类型 <1.咨询 2.视频 3.活动></param>
        /// <returns></returns>
        public int AddEventStats(string pNewsId, string pVipId, string pCountType, string pNewsType)
        {
            return this._currentDAO.AddEventStats(pNewsId, pVipId, pCountType, pNewsType);
        }
        #endregion

        #region GetMostConcern
        /// <summary>
        /// 最受关注列表
        /// </summary>
        /// <returns></returns>
        public DataSet GetEventStats(int? pPageSize, int? pPageIndex)
        {
            return this._currentDAO.GetEventStats(pPageSize,pPageIndex);
        }
        #endregion

        #region GetMostDetail
        /// <summary>
        ///获取点赞数量
        /// </summary>
        /// <param name="pEventsID"></param>
        /// <returns></returns>
        public int GetMostDetail(string pEventsID)
        {
            return this._currentDAO.GetMostDetail(pEventsID);
        }
        #endregion

        #region GetCourseInfo
        /// <summary>
        /// 获取课程详细 
        /// </summary>
        /// <param name="pCourseType">课程类型<1=MBA 2=EMBA 3=FMBA 4=高级经理课程></param>
        /// <returns></returns>
        public List<ZCourseEntity> GetCourseInfo(string pCourseType)
        {
            List<ZCourseEntity> courseList = null;
            DataSet ds = new DataSet();
            ds = this._currentDAO.GetCourseInfo(pCourseType);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                courseList = DataTableToObject.ConvertToList<ZCourseEntity>(ds.Tables[0]);
            }
            return courseList;
        }
        #endregion

        #region GetUserInfo
        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="pVipID"></param>
        /// <returns></returns>
        public List<VipViewEntity> GetUserInfo(string pVipID)
        {
            List<VipViewEntity> vipList = null;
            DataSet ds = new DataSet();
            ds = this._currentDAO.GetUserInfo(pVipID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                vipList = DataTableToObject.ConvertToList<VipViewEntity>(ds.Tables[0]);
            }
            return vipList;
        }
        #endregion

        #region GetUserInfo
        /// <summary>
        /// 根据用户ID获取用户信息
        /// </summary>
        /// <param name="pVipID"></param>
        /// <returns></returns>
        public List<VipViewEntity> GetUserList(string pVipName, string pClass, string pCompany, string pCity, string pVipID)
        {
            List<VipViewEntity> vipList = null;
            DataSet ds = new DataSet();
            ds = this._currentDAO.GetUserList(pVipName, pClass, pCompany, pCity, pVipID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                vipList = DataTableToObject.ConvertToList<VipViewEntity>(ds.Tables[0]);
            }
            return vipList;
        }
        #endregion

        #region getNewsDetailByNewsID
        public reqNewsEntity getEventstatsDetailByNewsID(ReqData<getNewsDetailByNewsIDEntity> pEntity)
        {
            reqNewsEntity pNewsEntity = new reqNewsEntity();
            DataSet ds = this._currentDAO.getEventstatsDetailByNewsID(pEntity);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                NewsDetailEntity[] entity = DataLoader.LoadFrom<NewsDetailEntity>(ds.Tables[0]);
                pNewsEntity.News = entity[0];
            }
            return pNewsEntity;
        }
        #endregion

        #region GetVipPayMent
        /// <summary>
        /// 根据客户获取会费
        /// </summary>
        /// <param name="pVipID"></param>
        /// <returns></returns>
        public VipPriceEntity GetVipPayMent(string pVipID)
        {
            DataSet ds = new DataSet();
            ds = this._currentDAO.GetVipPayMent(pVipID);
            VipPriceEntity pNewsEntity = new VipPriceEntity();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                VipPriceEntity[] entity = DataLoader.LoadFrom<VipPriceEntity>(ds.Tables[0]);
                return entity[0];
            }
            return null;
        }
        #endregion

        #region SubmitVipPayMent
        /// <summary>
        /// 缴会费，提交订单，增加表VipPayMent信息
        /// </summary>
        /// <param name="inoutEntity"></param>
        /// <param name="ItemId"></param>
        /// <param name="VipId"></param>
        /// <returns></returns>
        public int SubmitVipPayMent(string itemId, string vipId, string orderId, decimal price)
        {
            int i = this._currentDAO.SubmitVipPayMent(itemId, vipId, orderId, price);
            return i;
        }
        #endregion

        #region GetPriceByItemId
        /// <summary>
        /// 根据物资Id获取价格
        /// </summary>
        /// <param name="itemId">物资ID</param>
        /// <returns></returns>
        public decimal GetPriceByItemId(string itemId)
        {
            decimal price = this._currentDAO.GetPriceByItemId(itemId);
            return price;
        }
        #endregion

        #region GetSkuIdByItemId
       /// <summary>
        /// 根据itemId获取skuId
       /// </summary>
       /// <param name="itemId">物资Id</param>
       /// <returns></returns>
        public string GetSkuIdByItemId(string itemId)
        {
           return this._currentDAO.GetSkuIdByItemId(itemId);
        }
        #endregion
        #endregion
    }
}
