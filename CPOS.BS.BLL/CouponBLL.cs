/*
 * Author		:CodeGeneration
 * EMail		:
 * Company		:JIT
 * Create On	:2013-12-14 15:57
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
using System.Transactions;
using JIT.CPOS.Common;
using JIT.CPOS.BS.DataAccess.Base;
using JIT.Utility.Log;
using JIT.CPOS.DTO.Base;
using JIT.CPOS.BS.Entity.Interface;
using System.Collections;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// ҵ����  
    /// </summary>
    public partial class CouponBLL
    {
        #region ����ID��ȡ�Ż�ȯ�б�

        /// <summary>
        /// ����ID��ȡ�Ż�ȯ�б�
        /// </summary>
        /// <param name="vipId">�û�ID</param>
        /// <returns></returns>
        public DataSet GetMyCouponList(string vipId)
        {
            return this._currentDAO.GetMyCouponList(vipId);
        }

        #endregion

        #region ��ȡ����ʹ�õ��Ż�ȯ�ܼ�

        /// <summary>
        /// ��ȡ����ʹ�õ��Ż�ȯ�ܼ�
        /// </summary>
        /// <param name="vipId">�û�ID</param>
        /// <param name="orderId">����ID</param>
        /// <returns></returns>
        public DataSet OrderCouponSum(string vipId, string orderId)
        {
            return this._currentDAO.OrderCouponSum(vipId, orderId);
        }

        #endregion

        #region ����ʹ�õ��Ż�ȯ�б�

        /// <summary>
        /// ����ʹ�õ��Ż�ȯ�б�
        /// </summary>
        /// <param name="vipId">�û�ID</param>
        /// <param name="orderId">����ID</param>
        /// <returns></returns>
        public DataSet OrderCouponList(string vipId, string orderId)
        {
            return this._currentDAO.OrderCouponList(vipId, orderId);
        }

        #endregion

        #region ������ȡ��ʹ���Ż�ȯ

        /// <summary>
        /// ������ȡ��ʹ���Ż�ȯ
        /// </summary>
        /// <param name="orderId">����ID</param>
        /// <param name="couponId">�Ż�ȯID</param>
        /// <returns></returns>
        public void CancelCouponMapping(string orderId, string couponId)
        {
            this._currentDAO.CancelCouponMapping(orderId, couponId);
        }

        #endregion

        #region ���¶���������

        /// <summary>
        /// ���¶���������
        /// </summary>
        /// <param name="orderId">����ID</param>
        /// <returns></returns>
        public void CancelCouponOrder(string orderId)
        {
            this._currentDAO.CancelCouponOrder(orderId);
        }

        #endregion

        #region ���㶩��ʹ�õ��Ż�ȯ

        public DataSet CheckCouponForOrder(string vipId, string orderId, string couponId)
        {
            return this._currentDAO.CheckCouponForOrder(vipId, orderId, couponId);
        }

        #endregion

        #region �����Ż�ȯ�ν�ҵ��

        public void SetEventPrizes(string vipId, string eventId)
        {
            var tranHelper = new TransactionHelper(this.CurrentUserInfo);
            var tran = tranHelper.CreateTransaction();
            using (tran.Connection)
            {
                LLotteryLogBLL lotteryService = new LLotteryLogBLL(this.CurrentUserInfo);
                var lotterys = lotteryService.QueryByEntity(new LLotteryLogEntity() { EventId = eventId, VipId = vipId }, null);

                if (lotterys != null && lotterys.Length > 0)
                {
                    //����
                    var lotteryEntity = lotterys.FirstOrDefault();
                    lotteryEntity.LotteryCount += 1;
                    lotteryService.Update(lotteryEntity, tran);
                }
                else
                {
                    //����
                    var lotteryEntity = new LLotteryLogEntity()
                    {
                        LogId = CPOS.Common.Utils.NewGuid(),
                        VipId = vipId,
                        EventId = eventId,
                        LotteryCount = 1
                    };
                    lotteryService.Create(lotteryEntity, tran);
                }

                //1���ж��û��Ƿ��н�
                //2���û��н� ����Ż�ȯ��Coupon�����Ż�ȯ�û���ϵ��VipCouponMapping��
                //3��added by zhangwei�û��н�����Ϊ����������û����� 
                LPrizeWinnerBLL winnerService = new LPrizeWinnerBLL(this.CurrentUserInfo);
                var prize = winnerService.GetWinnerInfo(vipId, eventId);

                if (prize.Read())
                {
                    //ȡ�ý�Ʒ��Ϣ������Ʒ�����ǻ��ֻ����Ż�ȯ
                    var prizeMapping = winnerService.GetPrizeCouponTypeMapping(prize["PrizeWinnerID"].ToString(), tran);
                    if (prizeMapping.Tables[0].Rows.Count > 0)
                    {
                        //��Ʒ����ǻ��֣��������ֱ�
                        if (!string.IsNullOrEmpty(prizeMapping.Tables[0].Rows[0]["AwardPoints"].ToString()))
                        {
                            //�µĻ��ַ��� zhangwei2013-2-13
                            int IntegralSourceID = 16;
                            if (eventId.ToUpper() == "E5A304D716D14CD2B96560EBD2B6A29C")
                                IntegralSourceID = 16;
                            if (eventId.ToUpper() == "BFC41A8BF8564B6DB76AE8A8E43557BA")
                                IntegralSourceID = 17;
                            var objectID = prizeMapping.Tables[0].Rows[0]["PrizesID"].ToString();
                            if (!string.IsNullOrEmpty(objectID))
                            {
                                Loggers.DEFAULT.Debug(new DebugLogInfo
                                {
                                    Message = "�齱�ɹ�:+IntegralSourceID��" + IntegralSourceID + "ClientID:" + CurrentUserInfo.ClientID + "VipID:" + vipId + "ObjectID:" + objectID
                                });
                                new VipIntegralBLL(CurrentUserInfo).ProcessPoint(IntegralSourceID,
                                                                                 CurrentUserInfo.ClientID, vipId,
                                                                                 objectID);
                            }
                            else
                            {
                                Loggers.DEFAULT.Debug(new DebugLogInfo
                                {
                                    Message = "�齱ʧ��:+IntegralSourceID��" + IntegralSourceID + "ClientID:" + CurrentUserInfo.ClientID + "VipID:" + vipId
                                });
                            }
                            /*
                            //�������
                            VipIntegralBLL vipIntegralBLL = new VipIntegralBLL(CurrentUserInfo);
                            VipIntegralEntity vipIntegralEntity = new VipIntegralEntity();
                            decimal integralValue = decimal.Parse(prizeMapping.Tables[0].Rows[0]["AwardPoints"].ToString());
                            var vipIntegralDataList = vipIntegralBLL.QueryByEntity(
                       new VipIntegralEntity() { VipID = vipId }, null);
                            if (vipIntegralDataList == null || vipIntegralDataList.Length == 0 ||
                                vipIntegralDataList[0] == null)
                            {

                                vipIntegralEntity.VipID = vipId;
                                vipIntegralEntity.InIntegral = integralValue; //�ۼƻ���
                                vipIntegralEntity.EndIntegral = integralValue; //�������
                                vipIntegralEntity.ValidIntegral = integralValue; // ��ǰ��Ч����
                                vipIntegralBLL.Create(vipIntegralEntity, tran);
                            }
                            else
                            {
                                vipIntegralEntity = vipIntegralDataList[0];
                                vipIntegralEntity.VipID = vipId;
                                vipIntegralEntity.InIntegral = (vipIntegralEntity.InIntegral.HasValue ? vipIntegralEntity.InIntegral : 0) + integralValue; //�ۼƻ���
                                vipIntegralEntity.EndIntegral = (vipIntegralEntity.EndIntegral.HasValue ? vipIntegralEntity.EndIntegral : 0) + integralValue; //�������
                                vipIntegralEntity.ValidIntegral = (vipIntegralEntity.ValidIntegral.HasValue ? vipIntegralEntity.ValidIntegral : 0) + integralValue; // ��ǰ��Ч����
                                vipIntegralBLL.Update(vipIntegralEntity, false, tran);
                            }

                            //����vip����ּ�¼
                            VipBLL vipBll = new VipBLL(CurrentUserInfo);
                            VipEntity vipEntity = vipBll.GetByID(vipId);
                            if (vipEntity != null)
                            {
                                vipEntity.Integration = (vipEntity.Integration.HasValue ? vipEntity.Integration.Value : 0) + integralValue;
                                vipBll.Update(vipEntity);
                            }


                            VipIntegralDetailBLL vipIntegralDetailBLL = new VipIntegralDetailBLL(CurrentUserInfo);
                            VipIntegralDetailEntity vipIntegralDetailEntity = new VipIntegralDetailEntity();
                            vipIntegralDetailEntity.VipIntegralDetailID = CPOS.Common.Utils.NewGuid();
                            vipIntegralDetailEntity.VIPID = vipId;
                            vipIntegralDetailEntity.FromVipID = vipId;
                            vipIntegralDetailEntity.Integral = integralValue;
                            if (eventId.ToUpper() == "E5A304D716D14CD2B96560EBD2B6A29C")
                                vipIntegralDetailEntity.IntegralSourceID = "16";
                            if (eventId.ToUpper() == "BFC41A8BF8564B6DB76AE8A8E43557BA")
                                vipIntegralDetailEntity.IntegralSourceID = "17";

                            vipIntegralDetailBLL.Create(vipIntegralDetailEntity);
                             */

                        }//��Ʒ������Ż�ȯ
                        else if (!string.IsNullOrEmpty(prizeMapping.Tables[0].Rows[0]["CouponTypeID"].ToString()))
                        {

                            var couponId = Utils.NewGuid();

                            //�����Ż�ȯ
                            this._currentDAO.CreateCoupon(vipId, eventId, couponId, tran);

                            //����Ż�ȯ�û���ϵ
                            VipCouponMappingBLL mappingService = new VipCouponMappingBLL(this.CurrentUserInfo);
                            mappingService.Create(new VipCouponMappingEntity
                            {
                                VipCouponMapping = Utils.NewGuid(),
                                VIPID = vipId,
                                CouponID = couponId,
                                UrlInfo = ""
                            }, tran);
                        }

                    }
                    //���ý�ƷΪ�Ѷһ�
                    var prizeWinner = winnerService.Query(new IWhereCondition[]
                            {
                                new EqualsCondition()
                                    {
                                        FieldName = "PrizeWinnerID",
                                        Value = prize["PrizeWinnerID"].ToString()
                                    }
                            }, null).FirstOrDefault();

                    if (prizeWinner != null)
                    {
                        prizeWinner.HasConvert = 1;
                        winnerService.Update(prizeWinner, false, tran);
                    }
                }

                tran.Commit();
            }
        }

        #endregion

        #region �Ƽ����˽���
        public void RecommenderPrize(string VipID, string EventId)
        {
            _currentDAO.RecommenderPrize(VipID, EventId);
        }
        #endregion

        #region ��ȡ�Ƽ����а��б�

        /// <summary>
        /// ��ȡ�Ƽ����а��б�
        /// </summary>
        /// <returns></returns>
        public DataSet GetRecommendList()
        {
            return this._currentDAO.GetRecommendList();
        }

        #endregion

        #region ��ȡ�Ƽ�ս��

        /// <summary>
        /// ��ȡ�Ƽ�ս��
        /// </summary>
        /// <returns></returns>
        public DataSet GetRecommendRecord(string userId)
        {
            return this._currentDAO.GetRecommendRecord(userId);
        }

        #endregion

        #region ��ȡ�Ƽ�ս����Ա�б�

        /// <summary>
        /// ��ȡ�Ƽ�ս����Ա�б�
        /// </summary>
        /// <returns></returns>
        public DataSet GetRecommendRecordList(string userId)
        {
            return this._currentDAO.GetRecommendRecordList(userId);
        }

        #endregion

        public void UpdateVipRecommandTrace(string vipID, string higherVipId)
        {
            _currentDAO.UpdateVipRecommandTrace(vipID, higherVipId);
        }

        #region �ж��Ƿ��Ѽ�¼�Ƽ���ϵ
        public bool IfRecordedRecommendTrace(string vipID, string reCommandId)
        {
            bool result = true;
            object row = _currentDAO.IfRecordedRecommendTrace(vipID, reCommandId);
            if (row == null)
                result = false;

            Loggers.Debug(new DebugLogInfo() { Message = "result = " + result });
            return result;
        }
        #endregion

        #region ��ȡ��Ա�Ż�ȯ���б�
        /// <summary>
        /// ��ȡ��Ա�Ż�ȯ���б�
        /// </summary>
        /// <param name="vipID"></param>
        /// <param name="CouponTypeID"></param>
        /// <returns></returns>
        public DataSet GetCouponList(string vipID, string CouponTypeID)
        {
            return this._currentDAO.GetCouponList(vipID, CouponTypeID);
        }
        #endregion

        #region ��ȡ��Ա��Ч�Ż�ȯ���б�
        public CouponManagePagedSearchRD GetCouponList(GetCouponListRP getCouponListRP)
        {
            CouponManagePagedSearchRD rd = new CouponManagePagedSearchRD();

            DataSet dataSet = new DataSet();
            dataSet = this.GetCouponList(getCouponListRP.VipID, "");
            if (Utils.IsDataSetValid(dataSet))
            {
                var couponList = (from d in dataSet.Tables[0].AsEnumerable()
                                  where d["Status"].ToString() == "0"
                                  select new CouponManageEntity
                                  {
                                      CouponID = d["CouponID"].ToString(),
                                      CouponTypeName = d["CouponTypeName"].ToString(),
                                      CouponName = d["CouponName"].ToString(),
                                      CouponCode = d["CouponCode"].ToString(),
                                      BeginTime = d["BeginDate"].ToString() != "" ? Convert.ToDateTime(d["BeginDate"].ToString()).ToLongDateString().ToString() : "",
                                      EndTime = d["EndDate"].ToString() != "" ? Convert.ToDateTime(d["EndDate"].ToString()).ToLongDateString().ToString() : ""
                                  });

                rd.CouponList = couponList.ToArray();
            }
            return rd;
        }
        #endregion

        #region ��ȡ������Ϣ
        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="couponID"></param>
        /// <returns></returns>
        public DataSet GetCouponDetail(string couponID, string userID)
        {
            return this._currentDAO.GetCouponDetail(couponID, userID);
        }
        #endregion

        #region ʹ���Ż݄�
        /// <summary>
        /// ʹ���Ż݄�
        /// </summary>
        /// <param name="couponID"></param>
        /// <returns></returns>
        public int BestowCoupon(string couponID, string doorID)
        {
            return this._currentDAO.BestowCoupon(couponID, doorID);
        }
        #endregion

        /// <summary>
        /// ����ȯ����ƷID��ȡ��Ч�Ż�ȯ
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public string GetCouponByItemId(string itemId)
        {
            return this._currentDAO.GetCouponByItemId(itemId);
        }

        #region �����Ż�ȯ
        public EmptyRD GenerateCoupon(GenerateCouponRP generateCouponRP)
        {
            EmptyRD rd = new EmptyRD();
            string endTime = generateCouponRP.EndTime != null ? Convert.ToDateTime(generateCouponRP.EndTime).ToShortDateString() + " 23:59:59" : null;
            string result = this._currentDAO.GenerateCoupon(generateCouponRP.CouponTypeID, generateCouponRP.CouponName, generateCouponRP.BeginTime, endTime, generateCouponRP.Description, generateCouponRP.Qty);

            return rd;
        }
        #endregion

        #region �����Ż�ȯ�б�
        public CouponManagePagedSearchRD ManageCouponPagedSearch(CouponManagePagedSearchRP manageCouponSearchRP)
        {
            CouponManagePagedSearchRD rd = new CouponManagePagedSearchRD();
            DataSet dataSet = new DataSet();

            dataSet = _currentDAO.ManageCouponPagedSearch(manageCouponSearchRP.CouponTypeID, manageCouponSearchRP.CouponName, manageCouponSearchRP.CouponUseStatus, manageCouponSearchRP.CouponStatus, manageCouponSearchRP.BeginTime, manageCouponSearchRP.EndTime, manageCouponSearchRP.CouponCode, manageCouponSearchRP.Comment, manageCouponSearchRP.UseTime, manageCouponSearchRP.CreateByName, manageCouponSearchRP.UseEndTime, int.Parse(manageCouponSearchRP.PageIndex), int.Parse(manageCouponSearchRP.PageSize));
            if (dataSet.Tables.Count == 2)
            {
                var couponList = (from d in dataSet.Tables[0].AsEnumerable()
                                  select new CouponManageEntity
                                  {
                                      CouponID = d["CouponID"].ToString(),
                                      CouponTypeName = d["CouponTypeName"].ToString(),
                                      CouponName = d["CouponName"].ToString(),
                                      CouponCode = d["CouponCode"].ToString(),
                                      CouponUseStatus = d["CouponUseStatus"].ToString(),
                                      BeginTime = d["BeginDate"].ToString() != "" ? Convert.ToDateTime(d["BeginDate"].ToString()).ToLongDateString().ToString() : "",
                                      EndTime = d["EndDate"].ToString() != "" ? Convert.ToDateTime(d["EndDate"].ToString()).ToLongDateString().ToString() : "",
                                       CouponStatus = d["Status"].ToString(),
                                      IsDelete = d["IsDelete"].ToString(),
                                      CreateByName = d["CreateByName"].ToString(),
                                      Comment = d["Comment"].ToString(),
                                      UseTime = d["UseTime"].ToString() != "" ? Convert.ToDateTime(d["UseTime"].ToString()).ToLongDateString().ToString() : "" 
                                  });

                rd.CouponList = couponList.ToArray();
                rd.TotalPage = int.Parse(dataSet.Tables[1].Rows[0][0].ToString());
                rd.TotalCount = int.Parse(dataSet.Tables[1].Rows[0][1].ToString());
            }

            return rd;
        }
        #endregion

        #region �����ַ���¼ 2014-10-10
        public DataTable GetExportData(CouponManagePagedSearchRP manageCouponSearchRP)
        {
            CouponManagePagedSearchRD rd = new CouponManagePagedSearchRD();
            DataSet dataSet = new DataSet();
            int pageSize = 999999999;   //int.Parse(manageCouponSearchRP.PageSize)
            int pageIndex = 0;  //int.Parse(manageCouponSearchRP.PageIndex)
            dataSet = _currentDAO.ManageCouponPagedSearch(manageCouponSearchRP.CouponTypeID, manageCouponSearchRP.CouponName, manageCouponSearchRP.CouponUseStatus, manageCouponSearchRP.CouponStatus, manageCouponSearchRP.BeginTime, manageCouponSearchRP.EndTime, manageCouponSearchRP.CouponCode, manageCouponSearchRP.Comment, manageCouponSearchRP.UseTime, manageCouponSearchRP.CreateByName, manageCouponSearchRP.UseEndTime, pageIndex, pageSize);

            DataTable dataTable = dataSet.Tables[0];

            //ɾ������Ҫ����
            dataTable.Columns.Remove("CouponID");
            dataTable.Columns.Remove("CouponDesc");
            dataTable.Columns.Remove("CouponTypeID");
            dataTable.Columns.Remove("BeginDate");
            dataTable.Columns.Remove("EndDate");
            dataTable.Columns.Remove("CouponUrl");
            dataTable.Columns.Remove("ImageUrl");
            dataTable.Columns.Remove("Status");

            dataTable.Columns.Remove("CreateTime");
            dataTable.Columns.Remove("CreateBy");
            dataTable.Columns.Remove("LastUpdateTime");
            dataTable.Columns.Remove("LastUpdateBy");
            dataTable.Columns.Remove("IsDelete");
            dataTable.Columns.Remove("CouponPwd");
            dataTable.Columns.Remove("CollarCardMode");
            dataTable.Columns.Remove("CustomerID");

            dataTable.Columns.Remove("IsDel");
            dataTable.Columns.Remove("CouponUseStatus");
            dataTable.Columns.Remove("CoupnName");
            dataTable.Columns.Remove("DoorID");

            for (int i = 1; i <= 50; i++)
            {
                dataTable.Columns.Remove("Col" + i);
            }

            dataTable.Columns["RowNo"].ColumnName = "���";
            dataTable.Columns["CouponCode"].ColumnName = "�Ż�ȯ��";
            dataTable.Columns["CouponName"].ColumnName = "�Ż�ȯ����";
            dataTable.Columns["CreateByName"].ColumnName = "������";
            dataTable.Columns["Comment"].ColumnName = "�鵥��";
            dataTable.Columns["UseTime"].ColumnName = "��������";
            dataTable.Columns["CouponTypeName"].ColumnName = "�Ż�ȯ����";

            if (dataSet != null)
            {
                return dataTable;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region �����ַ���¼ 2014-10-10
        public DataTable GetExportBindData(CouponBindLogPagedSearchRP couponBindLogPagedSearchRP)
        {
            DataSet dataSet = new DataSet();
            string  pageSize = "999999999";   //int.Parse(manageCouponSearchRP.PageSize)
            string pageIndex = "0";  //int.Parse(manageCouponSearchRP.PageIndex)
            dataSet = this._currentDAO.BindCouponLog(couponBindLogPagedSearchRP.CouponTypeID, couponBindLogPagedSearchRP.CouponName, couponBindLogPagedSearchRP.CouponCode, couponBindLogPagedSearchRP.VipCriteria, couponBindLogPagedSearchRP.BindingBeginTime, couponBindLogPagedSearchRP.BindingEndTime, couponBindLogPagedSearchRP.Operator, pageIndex, pageSize);

            DataTable dataTable = dataSet.Tables[0];

            //ɾ������Ҫ����
            dataTable.Columns.Remove("VipCouponMapping");
            dataTable.Columns.Remove("VIPID");
            dataTable.Columns.Remove("CouponID");
            dataTable.Columns.Remove("UrlInfo");
            dataTable.Columns.Remove("IsDelete");
            dataTable.Columns.Remove("LastUpdateBy");
            dataTable.Columns.Remove("LastUpdateTime");
            dataTable.Columns.Remove("CreateBy");

            //��������
            dataTable.Columns["RowNo"].ColumnName = "���";
            dataTable.Columns["CouponCode"].ColumnName = "�Ż�ȯ��";
            dataTable.Columns["CouponName"].ColumnName = "�Ż�ȯ����";
            dataTable.Columns["Operator"].ColumnName = "������";
            dataTable.Columns["VipName"].ColumnName = "��Ա��";
            dataTable.Columns["CreateTime"].ColumnName = "�ַ�����";
            dataTable.Columns["CouponTypeName"].ColumnName = "�Ż�ȯ����";

            if (dataSet != null)
            {
                return dataTable;
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region �޸��Ż�ȯ��� 2014-9-24 update
        public SuccessResponse<IAPIResponseData> SetCouponCode(SetCouponCodeRP setCouponCodeRP)
        {
            var coupon = _currentDAO.Query(
                new IWhereCondition[] { 
                    new EqualsCondition() { FieldName = "CouponCode", Value = setCouponCodeRP.CouponCode }
                    , new EqualsCondition() { FieldName = "CustomerID", Value = CurrentUserInfo.ClientID}
                }, null);
            SuccessResponse<IAPIResponseData> sr = new SuccessResponse<IAPIResponseData>();
            if (coupon.Length == 0)
            {
                this._currentDAO.Update(new CouponEntity() { CouponID = setCouponCodeRP.CouponID, CouponCode = setCouponCodeRP.CouponCode }, false);
                sr.Message = "���³ɹ���";
            }
            else
            {
                sr.Message = "ϵͳ�Ѿ������˸ñ�ţ�";
            }

            return sr;
        }
        #endregion

        #region �޸��Ż�ȯ״̬Ϊɾ�� 2014-9-25 update
        public SuccessResponse<IAPIResponseData> SetCouponStates(SetCouponCodeRP setCouponCodeRP)
        {
            var coupon = _currentDAO.Query(
                new IWhereCondition[] { 
                    new EqualsCondition() { FieldName = "CouponID", Value = setCouponCodeRP.CouponIDs[0] }
                    , new EqualsCondition() { FieldName = "IsDelete", Value = setCouponCodeRP.IsDelete}
                }, null);
            SuccessResponse<IAPIResponseData> sr = new SuccessResponse<IAPIResponseData>();
            if (coupon.Length == 0)
            {
                //�߼�ɾ����ָ��Ż�ȯ
                this._currentDAO.DeleteNew(setCouponCodeRP.CouponIDs[0], null, Convert.ToInt32(setCouponCodeRP.IsDelete));

                sr.Message = "���³ɹ���";
            }
            else
            {
                sr.Message = "����ʧ�ܣ�";
            }

            return sr;
        }
        #endregion

        #region �ַ��Ż�ȯ
        public object BindCoupon(BindCouponRP bindCouponRP)
        {
            var rd = new EmptyRD();
            ErrorResponse er = new ErrorResponse();
            SuccessResponse<IAPIResponseData> sr = new SuccessResponse<IAPIResponseData>();

            //If coupon exists
            var coupon = _currentDAO.Query(
                new IWhereCondition[] { 
                    new EqualsCondition() { FieldName = "CouponCode", Value = bindCouponRP.CouponCode }
                    , new EqualsCondition() { FieldName = "CustomerID", Value = CurrentUserInfo.ClientID}
                    , new EqualsCondition() { FieldName = "Status", Value = "0"}
                    , new EqualsCondition() { FieldName = "IsDelete", Value = "0"}
                }, null);

            if (coupon.Length == 0)
            {
                er.Message = "���Ż�ȯ��Ų����ڻ��ѱ�ʹ�ã�";
            }
            else
            {
                //if coupon bound
                VipCouponMappingBLL vipCouponMappingBLL = new VipCouponMappingBLL(CurrentUserInfo);
                var mapping = vipCouponMappingBLL.Query(
                    new IWhereCondition[] {
                        new EqualsCondition() { FieldName = "CouponID", Value = coupon[0].CouponID}
                        , new EqualsCondition() { FieldName = "IsDelete", Value = "0"}
                    }, null);

                if (mapping.Length > 0)
                {
                    er.Message = "���Ż�ȯ�ѷַ���";
                }
                else
                {
                    //bind coupon
                    vipCouponMappingBLL.Create(
                        new VipCouponMappingEntity()
                        {
                            VipCouponMapping = Guid.NewGuid().ToString(),
                            VIPID = bindCouponRP.VipID,
                            CouponID = coupon[0].CouponID,
                            IsDelete = 0,
                            CreateBy = CurrentUserInfo.UserID,
                            CreateTime = DateTime.Now
                        });

                    sr.Message = "�ַ��ɹ���";
                }
            }

            object result;
            if (!string.IsNullOrEmpty(er.Message))
                result = er;
            else
                result = sr;

            return result;
        }
        #endregion

        #region �����Ż�ȯ
        public object WriteOffCoupon(WriteOffCouponRP writeOffCouponRP)
        {
            ErrorResponse er = new ErrorResponse();
            SuccessResponse<IAPIResponseData> sr = new SuccessResponse<IAPIResponseData>();

            //If coupon exists
            var coupon = _currentDAO.Query(
                new IWhereCondition[] { 
                    new EqualsCondition() { FieldName = "CouponID", Value = writeOffCouponRP.CouponID }
                    , new EqualsCondition() { FieldName = "CustomerID", Value = CurrentUserInfo.ClientID}
                    , new EqualsCondition() { FieldName = "Status", Value = "0"}
                    , new EqualsCondition() { FieldName = "IsDelete", Value = "0"}
                }, null);

            if (coupon.Length == 0)
            {
                er.Message = "���Ż�ȯ�����ڻ��ѱ�ʹ�ã�";
            }
            else
            {
                //�����Ż�ȯ״̬Ϊ��ʹ��
                //write off coupon
                this._currentDAO.Update(
                     new CouponEntity()
                     {
                         CouponID = writeOffCouponRP.CouponID,
                         Status = 1,
                         LastUpdateBy = CurrentUserInfo.UserID,
                         //Col1 = writeOffCouponRP.Comment,
                         //Col2 = DateTime.Now.ToString(),
                         //Col3 = CurrentUserInfo.UserID,
                         LastUpdateTime = DateTime.Now
                     }, false);
                //����֮��ȥ��������
                writeOffCouponRP.Comment = string.IsNullOrEmpty(writeOffCouponRP.Comment) ? "��̨��������ȯ" : writeOffCouponRP.Comment;
                var vipcouponMappingBll = new VipCouponMappingBLL(CurrentUserInfo);
                var vipcouponmappingList = vipcouponMappingBll.QueryByEntity(new VipCouponMappingEntity()
                {
                    CouponID = writeOffCouponRP.CouponID
                }, null);
                this._currentDAO.UpdateCouponUse(writeOffCouponRP.CouponID, writeOffCouponRP.Comment, vipcouponmappingList[0].VIPID, CurrentUserInfo.UserID, CurrentUserInfo.CurrentUserRole.UnitId, CurrentUserInfo.ClientID);
                sr.Message = "�����ɹ���";
            }

            object result;
            if (!string.IsNullOrEmpty(er.Message))
                result = er;
            else
                result = sr;

            return result;
        }
        #endregion

        #region �ַ��Ż�ȯ��¼
        public CouponBindLogPagedSearchRD BindCouponLog(CouponBindLogPagedSearchRP couponBindLogPagedSearchRP)
        {
            CouponBindLogPagedSearchRD couponBindLogPagedSearchRD = new CouponBindLogPagedSearchRD();

            DataSet dataSet = this._currentDAO.BindCouponLog(couponBindLogPagedSearchRP.CouponTypeID, couponBindLogPagedSearchRP.CouponName, couponBindLogPagedSearchRP.CouponCode, couponBindLogPagedSearchRP.VipCriteria, couponBindLogPagedSearchRP.BindingBeginTime, couponBindLogPagedSearchRP.BindingEndTime, couponBindLogPagedSearchRP.Operator, couponBindLogPagedSearchRP.PageIndex, couponBindLogPagedSearchRP.PageSize);

            if (Utils.IsDataSetValid(dataSet))
            {
                var list = DataTableToObject.ConvertToList<BindCouponEntity>(dataSet.Tables[0]);
                couponBindLogPagedSearchRD.CouponList = list.ToArray();

                couponBindLogPagedSearchRD.TotalPage = int.Parse(dataSet.Tables[1].Rows[0][0].ToString());
                couponBindLogPagedSearchRD.TotalCount = int.Parse(dataSet.Tables[1].Rows[0][1].ToString());
            }

            if (couponBindLogPagedSearchRD.CouponList != null)
            {
                foreach (BindCouponEntity item in couponBindLogPagedSearchRD.CouponList)
                {
                    item.CreateTime = item.CreateTime != "" ? Convert.ToDateTime(item.CreateTime).ToLongDateString().ToString() : "";
                }
            }

            return couponBindLogPagedSearchRD;
        }
        #endregion

        /// <summary>
        /// ���������Ż�ȯ
        /// </summary>
        /// <param name="htCouponInfo"></param>
        /// <returns></returns>
        public void GenerateCoupon(Hashtable htCouponInfo)
        {
           this._currentDAO.GenerateCoupon(htCouponInfo);
        }
    }


    #region RequestParameter

    public class GenerateCouponRP : IAPIRequestParameter
    {
        public string CouponTypeID { get; set; }
        public string CouponName { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Description { get; set; }
        public string Qty { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(CouponTypeID))
                throw new APIException(201, "�Ż�ȯ���Ͳ���Ϊ�գ�");

            if (string.IsNullOrEmpty(CouponName))
                throw new APIException(202, "�Ż�ȯ���Ʋ���Ϊ�գ�");

            if (string.IsNullOrEmpty(Qty) || int.Parse(Qty) <= 0)
                throw new APIException(203, "�����������0��");
        }
    }

    public class CouponManagePagedSearchRP : IAPIRequestParameter
    {
        public string CouponTypeID { get; set; }
        public string CouponName { get; set; }
        public string CouponUseStatus { get; set; }
        public string CouponStatus { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public string PageIndex { get; set; }
        public string PageSize { get; set; }
        public string CouponCode { get; set; }
        public string Comment { get; set; } //�鵥��
        public string CreateByName { get; set; } //������
        public string UseTime { get; set; } //����ʱ��
        public string UseEndTime { get; set; } //��������ʱ��

        //public 
        public void Validate()
        {
            if (!string.IsNullOrEmpty(BeginTime))
            {
                try
                {
                    DateTime.Parse(BeginTime);
                }
                catch (Exception)
                {
                    throw new APIException(201, "��ʼ���ڸ�ʽ����");
                }
            }

            if (!string.IsNullOrEmpty(EndTime))
            {
                try
                {
                    DateTime.Parse(EndTime);
                }
                catch (Exception)
                {
                    throw new APIException(202, "�������ڸ�ʽ����");
                }
            }

            try
            {
                int.Parse(PageIndex);
            }
            catch (Exception)
            {
                throw new APIException(203, "ҳ���ʽ����");
            }

            try
            {
                int.Parse(PageSize);
            }
            catch (Exception)
            {
                throw new APIException(204, "ҳ��С��ʽ����");
            }
        }
    }

    public class CouponBindLogPagedSearchRP : CouponManagePagedSearchRP
    {
        public string BindingBeginTime { get; set; }
        public string BindingEndTime { get; set; }
        public string Operator { get; set; }
        public string VipCriteria { get; set; }

        public string CouponTypeID { get; set; }
        public string CouponName { get; set; }
        public string CouponUseStatus { get; set; }
        public string CouponStatus { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public string PageIndex { get; set; }
        public string PageSize { get; set; }
        public string CouponCode { get; set; }
        public string Comment { get; set; } //�鵥��
        public string CreateByName { get; set; } //������
        public string UseTime { get; set; } //����ʱ��
        public string UseEndTime { get; set; } //��������ʱ��



        new public void Validate()
        {
            //base.Validate();

            if (!string.IsNullOrEmpty(BindingBeginTime))
            {
                try
                {
                    DateTime.Parse(BindingBeginTime);
                }
                catch (Exception)
                {
                    throw new APIException(301, "��ʼ���ڸ�ʽ����");
                }
            }

            if (!string.IsNullOrEmpty(BindingEndTime))
            {
                try
                {
                    DateTime.Parse(BindingEndTime);
                }
                catch (Exception)
                {
                    throw new APIException(302, "�������ڸ�ʽ����");
                }
            }
        }
    }

    public class SetCouponCodeRP : IAPIRequestParameter
    {
        public string CouponID { get; set; }
        public string CouponCode { get; set; }
        public string IsDelete { get; set; }  //��Ƭ�Ƿ�Ϊ����״̬���Ƿ���״̬
        public string[] CouponIDs { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(CouponID))
            {
                throw new APIException(201, "�Ż�ȯID����Ϊ�գ�");
            }

            if (string.IsNullOrEmpty(CouponCode))
            {
                throw new APIException(202, "�Ż�ȯ��Ų���Ϊ�գ���");
            }
        }
    }

    public class BindCouponRP : IAPIRequestParameter
    {
        public string VipID { get; set; }
        public string CouponCode { get; set; }
        //public 
        public void Validate()
        {
            if (string.IsNullOrEmpty(VipID))
            {
                throw new APIException(201, "��ԱID����Ϊ�գ�");
            }

            if (string.IsNullOrEmpty(CouponCode))
            {
                throw new APIException(202, "�Ż�ȯ��Ų���Ϊ�գ���");
            }
        }
    }

    public class GetCouponListRP : IAPIRequestParameter
    {
        public string VipID { get; set; }
        public void Validate()
        {
            if (string.IsNullOrEmpty(VipID))
            {
                throw new APIException(201, "��ԱID����Ϊ�գ�");
            }
        }
    }

    public class WriteOffCouponRP : IAPIRequestParameter
    {
        public string CouponID { get; set; }
        /// <summary>
        /// �鵥��
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// �ŵ�ID
        /// </summary>
        //public string UnitID { get; set; }
        /// <summary>
        /// ������ԱID
        /// </summary>
        //public string WriteOffUserID { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(CouponID))
            {
                throw new APIException(201, "�Ż�ȯID����Ϊ�գ�");
            }
        }
    }

    #endregion

    #region ResponseData

    public class CouponManagePagedSearchRD : IAPIResponseData
    {
        public CouponManageEntity[] CouponList { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }
    }

    public class CouponManageEntity
    {
        public string CouponID { get; set; }
        public string CouponTypeName { get; set; }
        public string CouponName { get; set; }
        public string CouponCode { get; set; }
        public string CouponUseStatus { get; set; }
        public string CouponStatus { get; set; }
        public string BeginTime { get; set; }
        public string EndTime { get; set; }
        public string IsDelete { get; set; }
        public string CreateByName { get; set; }
        public string Comment { get; set; }
        public string UseTime { get; set; }

    }

    public class CouponBindLogPagedSearchRD : IAPIResponseData
    {
        public BindCouponEntity[] CouponList { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }
    }

    public class BindCouponEntity : CouponManageEntity
    {
        public string VipName { get; set; }
        public string Operator { get; set; }
        public string CreateTime { get; set; }
        public string PageIndex { get; set; }
        public string PageSize { get; set; }
    }

    #endregion

    #region ����ʱ����������
    /// <summary>
    /// ���ض���
    /// </summary>
    public class setOrderInfoRespData
    {
        public setOrderInfoRespContentData content { get; set; }
    }

    /// <summary>
    /// ���صľ���ҵ�����ݶ���
    /// </summary>
    public class setOrderInfoRespContentData
    {
        public string orderId { get; set; }
    }

    #endregion
}