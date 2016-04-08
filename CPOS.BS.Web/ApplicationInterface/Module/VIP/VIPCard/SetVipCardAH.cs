using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JIT.CPOS.BS.Web.ApplicationInterface.Base;
using JIT.CPOS.BS.Web.Session;
using JIT.CPOS.DTO.Module.VIP.VIPCardManage.Request;
using JIT.CPOS.DTO.Module.VIP.VIPCardManage.Response;
using JIT.Utility.DataAccess.Query;
using JIT.CPOS.BS.BLL;
using JIT.Utility.DataAccess;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.DTO.Base;
namespace JIT.CPOS.BS.Web.ApplicationInterface.Module.VIP.VIPCard
{
    public class SetVipCardAH : BaseActionHandler<SetVipCardRP, SetVipCardRD>
    {
        protected override SetVipCardRD ProcessRequest(DTO.Base.APIRequest<SetVipCardRP> pRequest)
        {
            var rd = new SetVipCardRD();
            var para = pRequest.Parameters;
            var loggingSessionInfo = new SessionManager().CurrentUserLoginInfo;
            #region 业务对象
            //会员卡
            var VipCardBLL = new VipCardBLL(loggingSessionInfo);
            //余额变动记录
            var VipCardBalanceChangeBLL = new VipCardBalanceChangeBLL(loggingSessionInfo);
            //卡状态变更记录
            var VipCardStatusChangeLogBLL = new VipCardStatusChangeLogBLL(loggingSessionInfo);
            //卡关系
            var VipCardVipMappingBLL = new VipCardVipMappingBLL(loggingSessionInfo);
            //图片
            var objectImagesBLL = new ObjectImagesBLL(loggingSessionInfo);
            //会员
            var VipBLL = new VipBLL(loggingSessionInfo);

            var vipCardTransLogBLL = new VipCardTransLogBLL(loggingSessionInfo); //丰收日交易记录对象示例化
            var unitBLL = new TUnitBLL(loggingSessionInfo);//门店业务对象
            //事务
            var pTran = VipCardBLL.GetTran();
            #endregion



            using (pTran.Connection)
            {
                try
                {


                    if (string.IsNullOrWhiteSpace(para.VipCardID) || para.OperationType <= 0)
                    {
                        throw new APIException("卡ID或者操作类型参数不合法！") { ErrorCode = ERROR_CODES.INVALID_REQUEST_LACK_REQUEST_PARAMETER };
                    }

                    //定位当前卡业务对象
                    VipCardEntity changeEntity = VipCardBLL.GetByID(para.VipCardID);

                    if (changeEntity == null)
                    {
                        throw new APIException("当前卡信息对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                    }
                    #region 返回卡ID
                    rd.VipCardID = changeEntity.VipCardID;
                    #endregion
                    changeEntity.RechargeTotalAmount = changeEntity.RechargeTotalAmount ?? 0;
                    changeEntity.BalanceAmount = changeEntity.BalanceAmount ?? 0;
                    string OldVipCardCode = changeEntity.VipCardCode ?? "";//原卡号
                    string NewVipCardCode = "";//新卡号，获取新卡赋值
                    decimal OldMoney = changeEntity.BalanceAmount.Value;//原卡当前余额
                    decimal NewMoney = changeEntity.BalanceAmount.Value + para.BalanceMoney;//原卡当余额+调整金额

                    //原卡会员映射关系
                    VipCardVipMappingEntity OldVipCardVipMappingData = VipCardVipMappingBLL.QueryByEntity(new VipCardVipMappingEntity() { VipCardID = para.VipCardID }, null).FirstOrDefault();
                    if (OldVipCardVipMappingData == null)
                    {
                        throw new APIException("原卡会员关系映射对象为NULL！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                    }

                    //门店Entity
                    var UnitData = unitBLL.GetByID(loggingSessionInfo.CurrentUserRole.UnitId);
                    //会员
                    VipEntity VipData = VipBLL.GetByID(OldVipCardVipMappingData.VIPID);

                    #region 卡状态记录对象
                    VipCardStatusChangeLogEntity AddVCStatusEntity = new VipCardStatusChangeLogEntity();
                    AddVCStatusEntity.LogID = System.Guid.NewGuid().ToString();
                    AddVCStatusEntity.VipCardID = para.VipCardID;
                    AddVCStatusEntity.Reason = para.ChangeReason;
                    AddVCStatusEntity.OldStatusID = changeEntity.VipCardStatusId;
                    AddVCStatusEntity.CustomerID = loggingSessionInfo.ClientID;
                    AddVCStatusEntity.Remark = para.Remark;
                    AddVCStatusEntity.UnitID = loggingSessionInfo.CurrentUserRole.UnitId;
                    #endregion



                    #region 新卡
                    VipCardEntity NewChangeVipCardData = new VipCardEntity();
                    if (!string.IsNullOrWhiteSpace(para.NewCardCode))
                    {
                        //查询参数
                        List<IWhereCondition> complexCondition = new List<IWhereCondition> { };
                        if (!string.IsNullOrEmpty(para.NewCardCode))
                        {
                            complexCondition.Add(new DirectCondition("VipCardCode='" + para.NewCardCode + "' or VipCardISN='" + para.NewCardCode.ToString() + "' "));
                        }
                        //新卡对象
                        NewChangeVipCardData = VipCardBLL.Query(complexCondition.ToArray(), null).FirstOrDefault();
                        if (NewChangeVipCardData == null)
                        {
                            throw new APIException("新卡不存在！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                        }
                        //新卡号赋值
                        NewVipCardCode = NewChangeVipCardData.VipCardCode ?? "";
                        //新卡数据赋值
                        NewChangeVipCardData.VipCardGradeID = changeEntity.VipCardGradeID;
                        NewChangeVipCardData.BatchNo = changeEntity.BatchNo;
                        NewChangeVipCardData.BeginDate = changeEntity.BeginDate;
                        NewChangeVipCardData.EndDate = changeEntity.EndDate;
                        NewChangeVipCardData.TotalAmount = changeEntity.TotalAmount == null ? 0 : changeEntity.TotalAmount.Value;
                        NewChangeVipCardData.BalanceAmount = changeEntity.BalanceAmount == null ? 0 : changeEntity.BalanceAmount.Value;
                        NewChangeVipCardData.BalancePoints = changeEntity.BalancePoints == null ? 0 : changeEntity.BalancePoints.Value;
                        NewChangeVipCardData.BalanceBonus = changeEntity.BalanceBonus == null ? 0 : changeEntity.BalanceBonus.Value;
                        NewChangeVipCardData.CumulativeBonus = changeEntity.CumulativeBonus == null ? 0 : changeEntity.CumulativeBonus.Value;
                        NewChangeVipCardData.PurchaseTotalAmount = changeEntity.PurchaseTotalAmount == null ? 0 : changeEntity.PurchaseTotalAmount.Value;
                        NewChangeVipCardData.PurchaseTotalCount = changeEntity.PurchaseTotalCount == null ? 0 : changeEntity.PurchaseTotalCount.Value;
                        NewChangeVipCardData.CheckCode = changeEntity.CheckCode;
                        NewChangeVipCardData.SingleTransLimit = changeEntity.SingleTransLimit == null ? 0 : changeEntity.SingleTransLimit.Value;
                        NewChangeVipCardData.IsOverrunValid = changeEntity.IsOverrunValid == null ? 0 : changeEntity.IsOverrunValid.Value;
                        NewChangeVipCardData.RechargeTotalAmount = changeEntity.RechargeTotalAmount == null ? 0 : changeEntity.RechargeTotalAmount.Value;
                        NewChangeVipCardData.LastSalesTime = changeEntity.LastSalesTime;
                        NewChangeVipCardData.IsGift = changeEntity.IsGift == null ? 0 : changeEntity.IsGift.Value;
                        NewChangeVipCardData.SalesAmount = changeEntity.SalesAmount;
                        NewChangeVipCardData.SalesUserId = changeEntity.SalesUserId;
                        NewChangeVipCardData.CustomerID = changeEntity.CustomerID;
                        NewChangeVipCardData.MembershipTime = changeEntity.MembershipTime;
                        NewChangeVipCardData.SalesUserName = changeEntity.SalesUserName == null ? "" : changeEntity.SalesUserName;
                        NewChangeVipCardData.CreateBy = changeEntity.CreateBy;
                    }
                    #endregion


                    switch (para.OperationType)
                    {
                        case 1:
                            #region 调整卡余额
                            //卡更新

                            //卡内总金额
                            if (para.BalanceMoney > 0)
                            {
                                changeEntity.RechargeTotalAmount += para.BalanceMoney;
                                if (changeEntity.RechargeTotalAmount < 0)
                                {
                                    throw new APIException("调整后的余额小于0！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                                }
                            }
                            //changeEntity.BalanceAmount = changeEntity.BalanceAmount ?? 0;
                            //
                            changeEntity.BalanceAmount = NewMoney;
                            //执行更新
                            VipCardBLL.Update(changeEntity, pTran);

                            //新增余额变动记录
                            VipCardBalanceChangeEntity AddEntity = new VipCardBalanceChangeEntity();
                            AddEntity.ChangeID = System.Guid.NewGuid().ToString();
                            AddEntity.VipCardCode = changeEntity.VipCardCode;
                            AddEntity.ChangeAmount = para.BalanceMoney;
                            //变动前卡内余额
                            AddEntity.ChangeBeforeBalance = OldMoney;
                            //变动后卡内余额
                            AddEntity.ChangeAfterBalance = NewMoney;
                            AddEntity.ChangeReason = para.ChangeReason;
                            AddEntity.Status = 1;
                            AddEntity.Remark = para.Remark;
                            AddEntity.CustomerID = loggingSessionInfo.ClientID;
                            AddEntity.UnitID = loggingSessionInfo.CurrentUserRole.UnitId;
                            //执行新增
                            VipCardBalanceChangeBLL.Create(AddEntity, pTran);

                            //增加图片上传
                            if (!string.IsNullOrEmpty(para.ImageUrl))
                            {
                                var objectImagesEntity = new ObjectImagesEntity()
                                {
                                    ImageId = Guid.NewGuid().ToString(),
                                    ObjectId = AddEntity.ChangeID,
                                    ImageURL = para.ImageUrl
                                };
                                objectImagesBLL.Create(objectImagesEntity, pTran);
                            }
                            #region 充值记录
                            //读取最近一次积分变更记录
                            List<OrderBy> lstOrder = new List<OrderBy> { };  //排序参数
                            lstOrder.Add(new OrderBy() { FieldName = "TransTime", Direction = OrderByDirections.Desc });
                            var transLogInfo = vipCardTransLogBLL.QueryByEntity(new VipCardTransLogEntity() { VipCardCode = changeEntity.VipCardCode, TransType = "C" }, lstOrder.ToArray()).FirstOrDefault();
                            //期末积分
                            int newValue = transLogInfo != null ? (transLogInfo.NewValue ?? 0) : 0;
                            var vipCardTransLogEntity = new VipCardTransLogEntity()
                            {
                                VipCardCode = VipData == null ? "" : VipData.VipCode,
                                UnitCode = UnitData == null ? "" : UnitData.UnitCode,
                                TransContent = "余额",
                                TransType = "C",
                                TransTime = DateTime.Now,
                                TransAmount = para.BalanceMoney,
                                LastValue = newValue,        //期初金额
                                NewValue = Convert.ToInt32(para.BalanceMoney) + newValue, //期末金额
                                CustomerID = loggingSessionInfo.ClientID
                            };
                            vipCardTransLogBLL.Create(vipCardTransLogEntity, pTran);

                            #endregion


                            #endregion
                            break;
                        case 2:
                            #region 卡升级
       
                            //卡类型升级
                            if (para.VipCardTypeId != 0)
                            {
                                VipCardVipMappingBLL.updateVipCardByType(VipData.VIPID, para.VipCardTypeId, para.ChangeReason, para.Remark, VipData.VipCode, pTran);
                            }
                            //卡号升级
                            else
                            {
                                #region 原卡

                                #region 更新原卡
                                changeEntity.VipCardStatusId = 3;
                                //当前月，累计金额清0
                                //changeEntity.BalanceAmount = 0;
                                //changeEntity.TotalAmount = 0;
                                VipCardBLL.Update(changeEntity, pTran);//执行
                                #endregion

                                #region 新增原卡状态记录
                                AddVCStatusEntity.VipCardStatusID = 3;
                                AddVCStatusEntity.Action = "卡升级";
                                AddVCStatusEntity.Remark += "已升级为：" + NewChangeVipCardData.VipCardCode;
                                VipCardStatusChangeLogBLL.Create(AddVCStatusEntity, pTran);//执行
                                #endregion

                                #region 新增原卡余额变动记录
                                //if (OldMoney > 0)
                                //{
                                //    VipCardBalanceChangeEntity AddOldCardBalanceData = new VipCardBalanceChangeEntity();
                                //    AddOldCardBalanceData.ChangeID = System.Guid.NewGuid().ToString();
                                //    AddOldCardBalanceData.VipCardCode = changeEntity.VipCardCode;
                                //    AddOldCardBalanceData.ChangeAmount = -OldMoney;
                                //    //变动前卡内余额
                                //    AddOldCardBalanceData.ChangeBeforeBalance = OldMoney;
                                //    //变动后卡内余额
                                //    AddOldCardBalanceData.ChangeAfterBalance = 0;
                                //    AddOldCardBalanceData.ChangeReason = para.ChangeReason;
                                //    AddOldCardBalanceData.Status = 1;
                                //    AddOldCardBalanceData.Remark = para.Remark;
                                //    AddOldCardBalanceData.CustomerID = loggingSessionInfo.ClientID;
                                //    AddOldCardBalanceData.UnitID = loggingSessionInfo.CurrentUserRole.UnitId;
                                //    VipCardBalanceChangeBLL.Create(AddOldCardBalanceData, pTran);//执行
                                //    //增加图片上传
                                //    if (!string.IsNullOrEmpty(para.ImageUrl))
                                //    {
                                //        var objectImagesEntity = new ObjectImagesEntity()
                                //        {
                                //            ImageId = Guid.NewGuid().ToString(),
                                //            ObjectId = AddOldCardBalanceData.ChangeID,
                                //            ImageURL = para.ImageUrl
                                //        };
                                //        objectImagesBLL.Create(objectImagesEntity, pTran);
                                //    }
                                //}
                                #endregion

                                #endregion

                                #region 新卡

                                VipData.VipCode = para.NewCardCode;
                                VipBLL.Update(VipData, pTran);

                                #region 更新新卡
                                if (!string.IsNullOrEmpty(NewChangeVipCardData.MembershipUnit))
                                    throw new APIException("该会员卡已绑定会员！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                                if (NewChangeVipCardData.VipCardStatusId != 0)
                                    throw new APIException("该会员卡已激活！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                                if (NewChangeVipCardData.VipCardTypeID.Value == changeEntity.VipCardTypeID.Value)
                                    throw new APIException("该卡号与原卡等级相同，请更换卡号后重新尝试！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                                #region 返回新卡卡ID
                                rd.VipCardID = NewChangeVipCardData.VipCardID;
                                #endregion
                                NewChangeVipCardData.MembershipUnit = changeEntity.MembershipUnit;
                                NewChangeVipCardData.VipCardStatusId = 1;
                                VipCardBLL.Update(NewChangeVipCardData, pTran);//执行
                                #endregion

                                #region 新增新卡卡关系
                                VipCardVipMappingEntity AddVipCardVipMappingData = new VipCardVipMappingEntity();
                                AddVipCardVipMappingData.MappingID = System.Guid.NewGuid().ToString();
                                AddVipCardVipMappingData.VIPID = OldVipCardVipMappingData.VIPID;
                                AddVipCardVipMappingData.VipCardID = NewChangeVipCardData.VipCardID;
                                AddVipCardVipMappingData.CustomerID = loggingSessionInfo.ClientID;
                                VipCardVipMappingBLL.Create(AddVipCardVipMappingData, pTran);//执行
                                #endregion

                                #region 更新会员编号
                                //VipEntity SJ_VipData = VipBLL.GetByID(OldVipCardVipMappingData.VIPID);
                                //if (VipData == null)
                                //    throw new APIException("会员不存在！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                                //VipData.VipCode = NewChangeVipCardData.VipCardCode;
                                //VipBLL.Update(VipData, pTran);//执行
                                #endregion

                                #region 新增新卡状态记录
                                //新增新卡状态记录

                                VipCardStatusChangeLogEntity AddNewVCStatusEntity = new VipCardStatusChangeLogEntity();
                                AddNewVCStatusEntity.LogID = System.Guid.NewGuid().ToString();
                                AddNewVCStatusEntity.VipCardID = NewChangeVipCardData.VipCardID;
                                AddNewVCStatusEntity.VipCardStatusID = 1;
                                AddNewVCStatusEntity.Reason = para.ChangeReason;
                                AddNewVCStatusEntity.OldStatusID = 0;
                                AddNewVCStatusEntity.CustomerID = loggingSessionInfo.ClientID;
                                AddNewVCStatusEntity.Action = "卡升级";
                                AddNewVCStatusEntity.Remark = para.Remark + "由旧卡：" + changeEntity.VipCardCode + "升级";
                                AddNewVCStatusEntity.UnitID = loggingSessionInfo.CurrentUserRole.UnitId;
                                VipCardStatusChangeLogBLL.Create(AddNewVCStatusEntity, pTran);//执行

                                #endregion

                                #region 新增新卡余额记录
                                //新增余额记录
                                //if (OldMoney > 0)
                                //{
                                //    VipCardBalanceChangeEntity AddNewCardBalanceData = new VipCardBalanceChangeEntity();
                                //    AddNewCardBalanceData.ChangeID = System.Guid.NewGuid().ToString();
                                //    AddNewCardBalanceData.VipCardCode = NewChangeVipCardData.VipCardCode;
                                //    AddNewCardBalanceData.ChangeAmount = OldMoney;
                                //    //变动前卡内余额
                                //    AddNewCardBalanceData.ChangeBeforeBalance = 0;
                                //    //变动后卡内余额
                                //    AddNewCardBalanceData.ChangeAfterBalance = NewChangeVipCardData.BalanceAmount == null ? 0 : NewChangeVipCardData.BalanceAmount.Value;
                                //    AddNewCardBalanceData.ChangeReason = para.ChangeReason;
                                //    AddNewCardBalanceData.Status = 1;
                                //    AddNewCardBalanceData.Remark = para.Remark;
                                //    AddNewCardBalanceData.CustomerID = loggingSessionInfo.ClientID;
                                //    VipCardBalanceChangeBLL.Create(AddNewCardBalanceData, pTran);//执行
                                //    //增加图片上传
                                //    if (!string.IsNullOrEmpty(para.ImageUrl))
                                //    {
                                //        var objectImagesEntity = new ObjectImagesEntity()
                                //        {
                                //            ImageId = Guid.NewGuid().ToString(),
                                //            ObjectId = AddNewCardBalanceData.ChangeID,
                                //            ImageURL = para.ImageUrl
                                //        };
                                //        objectImagesBLL.Create(objectImagesEntity, pTran);
                                //    }
                                //}
                                #endregion

                                #endregion
                            }
                            #endregion
                            
                            break;
                        case 3:
                            #region 挂失
                            changeEntity.VipCardStatusId = 4;
                            //执行更新
                            VipCardBLL.Update(changeEntity, pTran);
                            //执行
                            AddVCStatusEntity.VipCardStatusID = 4;
                            AddVCStatusEntity.PicUrl = para.ImageUrl;
                            AddVCStatusEntity.Action = "挂失";
                            VipCardStatusChangeLogBLL.Create(AddVCStatusEntity, pTran);
                            #endregion

                            break;
                        case 4:

                            #region 冻结
                            changeEntity.VipCardStatusId = 2;
                            //执行更新
                            VipCardBLL.Update(changeEntity, pTran);
                            //执行新增
                            AddVCStatusEntity.VipCardStatusID = 2;
                            AddVCStatusEntity.Action = "冻结";
                            VipCardStatusChangeLogBLL.Create(AddVCStatusEntity, pTran);
                            #endregion
                            break;
                        case 5:
                            #region 转卡
                            #region 原卡
                            #region 更新原卡
                            changeEntity.VipCardStatusId = 3;
                            //当前月，累计金额清0
                            changeEntity.BalanceAmount = 0;
                            changeEntity.TotalAmount = 0;
                            VipCardBLL.Update(changeEntity, pTran);//执行
                            #endregion

                            #region 新增原卡状态记录
                            AddVCStatusEntity.VipCardStatusID = 3;
                            AddVCStatusEntity.Action = "转卡";
                            AddVCStatusEntity.Remark += "已转移为：" + NewChangeVipCardData.VipCardCode;
                            VipCardStatusChangeLogBLL.Create(AddVCStatusEntity, pTran);//执行
                            #endregion


                            #region 新增原卡余额变动记录
                            if (OldMoney > 0)
                            {
                                VipCardBalanceChangeEntity AddOldZKCardBalanceData = new VipCardBalanceChangeEntity();
                                AddOldZKCardBalanceData.ChangeID = System.Guid.NewGuid().ToString();
                                AddOldZKCardBalanceData.VipCardCode = changeEntity.VipCardCode;
                                AddOldZKCardBalanceData.ChangeAmount = -OldMoney;
                                //变动前卡内余额
                                AddOldZKCardBalanceData.ChangeBeforeBalance = OldMoney;
                                //变动后卡内余额
                                AddOldZKCardBalanceData.ChangeAfterBalance = 0;
                                AddOldZKCardBalanceData.ChangeReason = para.ChangeReason;
                                AddOldZKCardBalanceData.Status = 1;
                                AddOldZKCardBalanceData.Remark = para.Remark;
                                AddOldZKCardBalanceData.CustomerID = loggingSessionInfo.ClientID;
                                AddOldZKCardBalanceData.UnitID = loggingSessionInfo.CurrentUserRole.UnitId;
                                VipCardBalanceChangeBLL.Create(AddOldZKCardBalanceData, pTran); //执行
                                //增加图片上传
                                if (!string.IsNullOrEmpty(para.ImageUrl))
                                {
                                    var objectImagesEntity = new ObjectImagesEntity()
                                    {
                                        ImageId = Guid.NewGuid().ToString(),
                                        ObjectId = AddOldZKCardBalanceData.ChangeID,
                                        ImageURL = para.ImageUrl
                                    };
                                    objectImagesBLL.Create(objectImagesEntity, pTran);
                                }
                            }
                            #endregion

                            #endregion
                            #region 新卡

                            #region 更新新卡
                            if (!string.IsNullOrEmpty(NewChangeVipCardData.MembershipUnit))
                                throw new APIException("该会员卡已绑定会员！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                            if (NewChangeVipCardData.VipCardStatusId != 0)
                                throw new APIException("该会员卡已激活！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };
                            if (NewChangeVipCardData.VipCardTypeID.Value != changeEntity.VipCardTypeID.Value)
                                throw new APIException("该卡号与原卡等级不同，请更换卡号后重新尝试！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                            #region 返回新卡卡ID
                            rd.VipCardID = NewChangeVipCardData.VipCardID;
                            #endregion
                            NewChangeVipCardData.MembershipUnit = changeEntity.MembershipUnit;
                            NewChangeVipCardData.VipCardStatusId = 1;
                            VipCardBLL.Update(NewChangeVipCardData, pTran);//执行
                            #endregion



                            #region 新增新卡卡关系
                            VipCardVipMappingEntity AddZKVipCardVipMappingData = new VipCardVipMappingEntity();
                            AddZKVipCardVipMappingData.MappingID = System.Guid.NewGuid().ToString();
                            AddZKVipCardVipMappingData.VIPID = OldVipCardVipMappingData.VIPID;
                            AddZKVipCardVipMappingData.VipCardID = NewChangeVipCardData.VipCardID;
                            AddZKVipCardVipMappingData.CustomerID = loggingSessionInfo.ClientID;
                            VipCardVipMappingBLL.Create(AddZKVipCardVipMappingData, pTran);//执行
                            #endregion

                            #region 更新会员编号
                            //VipEntity ZK_VipData = VipBLL.GetByID(OldVipCardVipMappingData.VIPID);
                            if (VipData == null)
                                throw new APIException("会员不存在！") { ErrorCode = ERROR_CODES.INVALID_BUSINESS };

                            VipData.VipCode = NewChangeVipCardData.VipCardCode;
                            VipBLL.Update(VipData, pTran);//执行
                            #endregion

                            #region 新增新卡状态记录
                            VipCardStatusChangeLogEntity AddNewZKVCStatusEntity = new VipCardStatusChangeLogEntity();
                            AddNewZKVCStatusEntity.LogID = System.Guid.NewGuid().ToString();
                            AddNewZKVCStatusEntity.VipCardID = NewChangeVipCardData.VipCardID;
                            AddNewZKVCStatusEntity.VipCardStatusID = 1;
                            AddNewZKVCStatusEntity.Reason = para.ChangeReason;
                            AddNewZKVCStatusEntity.OldStatusID = 0;
                            AddNewZKVCStatusEntity.CustomerID = loggingSessionInfo.ClientID;
                            AddNewZKVCStatusEntity.Action = "转卡";
                            AddNewZKVCStatusEntity.Remark = para.Remark + para.Remark + "由旧卡：" + changeEntity.VipCardCode + "转移";
                            AddNewZKVCStatusEntity.UnitID = loggingSessionInfo.CurrentUserRole.UnitId;
                            VipCardStatusChangeLogBLL.Create(AddNewZKVCStatusEntity, pTran);//执行
                            #endregion

                            #region 新增新卡余额记录
                            if (OldMoney > 0)
                            {
                                VipCardBalanceChangeEntity AddNewZKCardBalanceData = new VipCardBalanceChangeEntity();
                                AddNewZKCardBalanceData.ChangeID = System.Guid.NewGuid().ToString();
                                AddNewZKCardBalanceData.VipCardCode = NewChangeVipCardData.VipCardCode;
                                AddNewZKCardBalanceData.ChangeAmount = OldMoney;
                                //变动前卡内余额
                                AddNewZKCardBalanceData.ChangeBeforeBalance = 0;
                                //变动后卡内余额
                                AddNewZKCardBalanceData.ChangeAfterBalance = NewChangeVipCardData.BalanceAmount == null ? 0 : NewChangeVipCardData.BalanceAmount.Value;
                                AddNewZKCardBalanceData.ChangeReason = para.ChangeReason;
                                AddNewZKCardBalanceData.Status = 1;
                                AddNewZKCardBalanceData.Remark = para.Remark;
                                AddNewZKCardBalanceData.CustomerID = loggingSessionInfo.ClientID;
                                AddNewZKCardBalanceData.UnitID = loggingSessionInfo.CurrentUserRole.UnitId;
                                VipCardBalanceChangeBLL.Create(AddNewZKCardBalanceData, pTran);//执行
                                //增加图片上传
                                if (!string.IsNullOrEmpty(para.ImageUrl))
                                {
                                    var objectImagesEntity = new ObjectImagesEntity()
                                    {
                                        ImageId = Guid.NewGuid().ToString(),
                                        ObjectId = AddNewZKCardBalanceData.ChangeID,
                                        ImageURL = para.ImageUrl
                                    };
                                    objectImagesBLL.Create(objectImagesEntity, pTran);
                                }
                            }
                            #endregion







                            #endregion
                            #endregion
                            break;
                        case 6:

                            #region 解挂
                            changeEntity.VipCardStatusId = 1;
                            //执行更新
                            VipCardBLL.Update(changeEntity, pTran);
                            //执行新增
                            AddVCStatusEntity.VipCardStatusID = 1;
                            AddVCStatusEntity.Action = "解挂";
                            VipCardStatusChangeLogBLL.Create(AddVCStatusEntity, pTran);
                            #endregion
                            break;
                        case 7:

                            #region 解冻
                            changeEntity.VipCardStatusId = 1;
                            //执行更新
                            VipCardBLL.Update(changeEntity, pTran);
                            //执行新增
                            AddVCStatusEntity.VipCardStatusID = 1;
                            AddVCStatusEntity.Action = "解冻";
                            VipCardStatusChangeLogBLL.Create(AddVCStatusEntity, pTran);
                            #endregion
                            break;
                        case 8:
                            #region 作废
                            changeEntity.VipCardStatusId = 3;
                            //执行更新
                            VipCardBLL.Update(changeEntity, pTran);


                            //执行新增
                            AddVCStatusEntity.VipCardStatusID = 3;
                            AddVCStatusEntity.Action = "作废";
                            VipCardStatusChangeLogBLL.Create(AddVCStatusEntity, pTran);
                            #endregion
                            break;
                        case 9:
                            #region 唤醒
                            changeEntity.VipCardStatusId = 1;
                            //执行更新
                            VipCardBLL.Update(changeEntity, pTran);
                            //执行新增
                            AddVCStatusEntity.VipCardStatusID = 1;
                            AddVCStatusEntity.Action = "唤醒";
                            VipCardStatusChangeLogBLL.Create(AddVCStatusEntity, pTran);
                            #endregion
                            break;
                        case 10:
                            #region 激活
                            changeEntity.VipCardStatusId = 1;
                            //执行更新
                            VipCardBLL.Update(changeEntity, pTran);
                            //执行新增
                            AddVCStatusEntity.VipCardStatusID = 1;
                            AddVCStatusEntity.Action = "激活";
                            VipCardStatusChangeLogBLL.Create(AddVCStatusEntity, pTran);
                            #endregion
                            break;
                        default:
                            throw new APIException("当前操作类型不匹配！") { ErrorCode = ERROR_CODES.INVALID_REQUEST_LACK_REQUEST_PARAMETER };
                            break;
                    };
                    pTran.Commit();
                    #region 卡升级，转卡操作转移消费记录表
                    if ((para.OperationType == 2 || para.OperationType == 5) && string.IsNullOrEmpty(para.VipCardTypeId.ToString())) //卡类型升级不执行此操作
                    {
                        string StrSql = string.Format("update VipCardTransLog set VipCardCode='{0}',OldVipCardCode='{1}' where VipCardCode='{1}'", NewVipCardCode,OldVipCardCode);   
                        vipCardTransLogBLL.UpdateVipCardTransLog(StrSql);//执行
                    }
                    #endregion
                }
                catch (APIException apiEx)
                {
                    pTran.Rollback();//回滚事物
                    throw new APIException(apiEx.ErrorCode, apiEx.Message);
                }
                catch (Exception ex)
                {
                    pTran.Rollback();//回滚事务
                    throw new APIException(ex.Message);
                }
            }
            return rd;
        }
    }
}