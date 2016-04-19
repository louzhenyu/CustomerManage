/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2015/5/17 17:27:33
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
using JIT.Utility.DataAccess;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.BLL.WX;
using JIT.CPOS.BS.DataAccess;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ���� �����̰����ŵ�͸��� 
    /// </summary>
    public partial class RetailTraderBLL
    {
        /// <summary>
        /// ��ȡ����ͷ��ͼƬUrl
        /// </summary>
        /// <param name="RetailTraderID"></param>
        /// <returns></returns>
        public string GetRetailHeadImage(string RetailTraderID)
        {
            return this._currentDAO.GetRetailHeadImage(RetailTraderID);
        }
        /// <summary>
        /// ��ȡ�������¸������ڵ�
        /// </summary>
        /// <param name="RetailTraderId">������ID</param>
        /// <returns></returns>
        public DataSet GetMultiLevelBeAddNode(string RetailTraderId)
        {
            return this._currentDAO.GetMultiLevelBeAddNode(RetailTraderId);
        }
        /// <summary>
        /// ��ȡ������ͳ�ơ��ȼ��б�
        /// </summary>
        /// <param name="RetailTraderId"></param>
        /// <returns></returns>
        public DataSet GetMultiLevelSalerQuery(string RetailTraderId)
        {
            return this._currentDAO.GetMultiLevelSalerQuery(RetailTraderId);
        }
        /// <summary>
        /// ����һ����ʵ����ap��
        /// </summary>
        /// <param name="pEntity">ʵ��ʵ��</param>
        public void Create2Ap(RetailTraderEntity pEntity)
        {
            _currentDAO.Create2Ap(pEntity, null);//û������
        }
        public void Update2Ap(RetailTraderEntity pEntity, IDbTransaction pTran, bool pIsUpdateNullField)
        {
            _currentDAO.Update2Ap(pEntity, pTran, pIsUpdateNullField);
        }


        public int getMaxRetailTraderCode(string CustomerID)
        {
            return this._currentDAO.getMaxRetailTraderCode(CustomerID);
        }
        public DataSet getRetailTraderInfoByLogin(string LoginName, string RetailTraderID, string CustomerID)
        {
            return this._currentDAO.getRetailTraderInfoByLogin(LoginName, RetailTraderID, CustomerID);
        }

        //��ap����ȡ��Ϣ������ͳһ�����������Ϣ�͵�½ʱȡcustomerID
        public DataSet getRetailTraderInfoByLogin2(string LoginName, string RetailTraderID, string CustomerID)
        {
            return this._currentDAO.getRetailTraderInfoByLogin2(LoginName, RetailTraderID, CustomerID);
        }

        public DataSet GetRetailTradersBySellUser(string RetailTraderName, string UserID, string CustomerID)
        {
            return this._currentDAO.GetRetailTradersBySellUser(RetailTraderName, UserID, CustomerID);
        }

        public int GetVipCountBySellUser(string UserID, string CustomerID)
        {
            return this._currentDAO.GetVipCountBySellUser(UserID, CustomerID);
        }


        public DataSet GetMonthVipList(string UserID, string CustomerID, int month, int year)
        {
            return this._currentDAO.GetMonthVipList(UserID, CustomerID, month, year);
        }


        public int GetMonthTradeCount(string UserID, string CustomerID, int month, int year)
        {
            return this._currentDAO.GetMonthTradeCount(UserID, CustomerID, month, year);
        }


        public DataSet MonthVipRiseTrand(string UserID, string CustomerID, int year)
        {
            return this._currentDAO.MonthVipRiseTrand(UserID, CustomerID, year);
        }


        public decimal RetailRewardByAmountSource(string UserID, string CustomerID, int year, int month, int day, string AmountSourceID)
        {
            return this._currentDAO.RetailRewardByAmountSource(UserID, CustomerID, year, month, day, AmountSourceID);
        }


        public DataSet MonthRewards(string UserID, string CustomerID, int year)
        {
            return this._currentDAO.MonthRewards(UserID, CustomerID, year);
        }

        public DataSet MonthDayRewards(string UserID, string CustomerID, int year, int month)
        {
            return this._currentDAO.MonthDayRewards(UserID, CustomerID, year, month);
        }

        public DataSet GetRewardsDayRiseList(string UserID, string CustomerID, DateTime beginDate, DateTime endDate)
        {
            return this._currentDAO.GetRewardsDayRiseList(UserID, CustomerID, beginDate, endDate);
        }

        public DataSet GetVipDayRiseList(string RetailTraderID, string CustomerID, DateTime beginDate, DateTime endDate)
        {
            return this._currentDAO.GetVipDayRiseList(RetailTraderID, CustomerID, beginDate, endDate);
        }



        public DataSet GetRetailVipInfos(string RetailTraderID, string CustomerID, int year, int month, int day)
        {
            return this._currentDAO.GetRetailVipInfos(RetailTraderID, CustomerID, year, month, day);

        }

        public DataSet GetRetailTraders(string RetailTraderName, string RetailTraderAddress
, string RetailTraderMan, string Status, string CooperateType, string UnitID, string UserID, string CustomerID, string longinUserID, int pageIndex, int pageSize, string OrderBy, string sortType)
        {
            return this._currentDAO.GetRetailTraders(RetailTraderName, RetailTraderAddress
, RetailTraderMan, Status, CooperateType, UnitID, UserID, CustomerID, longinUserID, pageIndex, pageSize, OrderBy, sortType);
        }
        public DataSet GetSellerMonthRewardList(string UnitID, string SellerOrRetailName
, int Year, int Month, string CustomerID, string longinUserID, int pageIndex, int pageSize, string OrderBy, string sortType)
        {
            return this._currentDAO.GetSellerMonthRewardList(UnitID, SellerOrRetailName
, Year, Month, CustomerID, longinUserID, pageIndex, pageSize, OrderBy, sortType);
        }




        public DataSet GetRetailMonthRewardList(string UnitID, string SellerOrRetailName
, int Year, int Month, string CustomerID, string longinUserID, int pageIndex, int pageSize, string OrderBy, string sortType)
        {
            return this._currentDAO.GetRetailMonthRewardList(UnitID, SellerOrRetailName
, Year, Month, CustomerID, longinUserID, pageIndex, pageSize, OrderBy, sortType);
        }


        public DataSet GetRetailCoupon(string RetailTraderID, string CustomerID, int Status, int pageIndex, int pageSize, string OrderBy, string sortType)
        {
            return this._currentDAO.GetRetailCoupon(RetailTraderID, CustomerID
, Status, pageIndex, pageSize, OrderBy, sortType);
        }
        /// <summary>
        /// ���ط����̹�����Ʒ��ά��
        /// </summary>
        /// <param name="strRetailTraderId"></param>
        /// <returns></returns>
        public DataSet RetailTraderItemQRCode(string strRetailTraderId)
        {
            return this._currentDAO.RetailTraderItemQRCode(strRetailTraderId);
        }

        #region ����Ǳ�ڷ־�����
        /// <summary>
        /// ����Ǳ�ڷ־�����
        /// </summary>
        /// <param name="loggingSessionInfo">loggingSessionInfo</param>
        /// <param name="vip_no">vip_no</param>
        public void CreatePrepRetailTrader(LoggingSessionInfo loggingSessionInfo, string vip_no)
        {

            VipEntity vipEntity = new VipBLL(loggingSessionInfo).GetVipDetailByVipID(vip_no);
            RetailTraderDAO retailTraderDao = new RetailTraderDAO(loggingSessionInfo);
            if (vipEntity == null || string.IsNullOrWhiteSpace(vipEntity.Col20))
            {
                return;
            }

            /// �жϵ�ǰvip��Ա�ֻ����Ƿ���ھ�����¼
            var entiryList = this.QueryByEntity(new RetailTraderEntity() { RetailTraderLogin = vipEntity.Phone }, null);
            if (entiryList != null && entiryList.Length > 0)
            {
                return;
            }

            t_unitEntity unitEntity = new t_unitBLL(loggingSessionInfo).GetMainUnit(loggingSessionInfo.ClientID);

            int RetailTraderCode = getMaxRetailTraderCode(loggingSessionInfo.ClientID);

            RetailTraderEntity pEntity = new RetailTraderEntity();
            pEntity.RetailTraderID = Guid.NewGuid().ToString();
            pEntity.RetailTraderType = "MultiLevelSaler";
            pEntity.RetailTraderCode = RetailTraderCode + 1;
            pEntity.RetailTraderName = vipEntity.VipName;
            pEntity.RetailTraderLogin = vipEntity.Phone;
            pEntity.RetailTraderPass = MD5Helper.Encryption("888888");
            pEntity.SalesType = "";
            pEntity.RetailTraderMan = "";
            pEntity.RetailTraderPhone = vipEntity.Phone;
            pEntity.RetailTraderAddress = "";
            pEntity.CooperateType = "";
            pEntity.SellUserID = "";
            pEntity.UnitID = unitEntity.unit_id;

            pEntity.MultiLevelSalerFromVipId = vip_no;
            if (!string.IsNullOrEmpty(vipEntity.Col20))
            {
                pEntity.HigheRetailTraderID = vipEntity.Col20;
            }
            pEntity.CreateTime = DateTime.Now; ;
            pEntity.CreateBy = "sys";
            pEntity.LastUpdateBy = "sys";
            pEntity.LastUpdateTime = DateTime.Now;
            pEntity.IsDelete = 0;
            pEntity.CustomerId = loggingSessionInfo.ClientID;
            pEntity.Status = "2";
            retailTraderDao.Create(pEntity);
            this.Create2Ap(pEntity);//ap�����RetailTraderID���̻����RetailTraderID��һ����

            new ObjectImagesBLL(loggingSessionInfo).SaveRetailTraderHeadImg(vipEntity, pEntity);

            // todo
            CommonBLL commonBll = new CommonBLL();
            string content = "�����ʺ�:" + pEntity.RetailTraderLogin + ",����:888888,�Ѿ��������ƹ�ע��ɹ�,���ڵ�ַhttp://app.chainclouds.com/download/chengguo/����һ����APP,���������Ϊ������׬Ǯ";
            JIT.CPOS.BS.Entity.WX.SendMessageEntity messageEntity = new JIT.CPOS.BS.Entity.WX.SendMessageEntity();
            messageEntity.content = content;
            messageEntity.touser = vipEntity.WeiXinUserId;
            messageEntity.msgtype = "text";
            WApplicationInterfaceEntity[] wApplicationInterfaceEntities = new WApplicationInterfaceBLL(loggingSessionInfo).QueryByEntity(new WApplicationInterfaceEntity { CustomerId = loggingSessionInfo.ClientID }, null);
            commonBll.SendMessage(messageEntity, wApplicationInterfaceEntities[0].AppID, wApplicationInterfaceEntities[0].AppSecret, loggingSessionInfo);
        }
        #endregion

        public DataSet GetVMultiLevelSalerConfigByCId(string customerId)
        {
            // string sql = string.Format(@"SELECT Id,MustBuyAmount,CustomerId,col1 FROM dbo.T_VipMultiLevelSalerConfig WITH(NOLOCK) WHERE	 CustomerId='{0}' and IsDelete=0", customerId);
            string sql = string.Format(@"SELECT Id,MustBuyAmount,Agreement,CustomerId FROM dbo.T_VipMultiLevelSalerConfig WITH(NOLOCK) WHERE	 CustomerId='{0}' and IsDelete=0", customerId);
            var ds = this._currentDAO.GetVMultiLevelSalerConfig(sql);
            return ds;
        }

    }
}