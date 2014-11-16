using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIT.CPOS.BS.Entity;
using JIT.CPOS.BS.Entity.User;
using JIT.CPOS.BS.BLL;
using JIT.CPOS.BS.DataAccess;
using JIT.CPOS.Common;
using System.Configuration;
using System.Transactions;

using JIT.Utility.ExtensionMethod;
using JIT.Utility.Reflection;
using JIT.Utility.Web;
using JIT.Utility.Log;
using JIT.Utility;

namespace JIT.CPOS.BS.BLL
{
    /// <summary>
    /// 初始化
    /// </summary>
    public class InitialService
    {
        #region 业务平台初始化 20131021
        /// <summary>
        /// 设置业务平台客户开户初始化
        /// </summary>
        /// <param name="strCustomerInfo">客户信息字符窜</param>
        /// <param name="strUnitInfo">门店信息字符窜</param>
        /// <param name="typeId">处理类型typeId=1(总部与门店一起处理);typeId=2（只处理总部，不处理门店）;typeId=3（只处理门店，不处理总部）</param>
        /// <returns></returns>
        public bool SetBSInitialInfo(string strCustomerInfo, string strUnitInfo, string strMenu, string typeId)
        {
            CustomerInfo customerInfo = new CustomerInfo();
            #region 获取客户信息
            if (!strCustomerInfo.Equals(""))
            {
                customerInfo = (JIT.CPOS.BS.Entity.CustomerInfo)cXMLService.Deserialize(strCustomerInfo, typeof(JIT.CPOS.BS.Entity.CustomerInfo));
                if (customerInfo == null || customerInfo.ID.Equals(""))
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetBSInitialInfo:{0}", "客户不存在或者没有获取合法客户信息")
                    });
                    throw new Exception("客户不存在或者没有获取合法客户信息");
                }
            }
            else
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetBSInitialInfo:{0}", "客户信息不存在")
                });
                throw new Exception("客户信息不存在.");
            }
            #endregion
            //获取连接数据库信息
            JIT.CPOS.BS.Entity.User.UserInfo userInfo = new JIT.CPOS.BS.Entity.User.UserInfo();
            LoggingManager loggingManager = new cLoggingManager().GetLoggingManager(customerInfo.ID);
            LoggingSessionInfo loggingSessionInfo = new LoggingSessionInfo();
            loggingSessionInfo.CurrentLoggingManager = loggingManager;
            loggingSessionInfo.CurrentUser = userInfo;


            #region 判断客户是否已经建立总部
            typeId = "1";
            bool bReturn = false;
            string strError = string.Empty;
            string strReturn = string.Empty;
            UnitInfo storeInfo = new UnitInfo();

            cUserService userServer = new cUserService(loggingSessionInfo);

            #region 处理用户信息
            userInfo.User_Id = BaseService.NewGuidPub(); //用户标识
            userInfo.customer_id = customerInfo.ID;//客户标识
            userInfo.User_Code = "admin";
            userInfo.User_Name = "管理员";
            userInfo.User_Gender = "1";
            userInfo.User_Password = "jit15d!";
            userInfo.User_Status = "1";
            userInfo.User_Status_Desc = "正常";
            userInfo.strDo = "Create";
            #endregion

            #endregion

            using (TransactionScope scope = new TransactionScope())
            {
                #region 门店
                JIT.CPOS.BS.Entity.UnitInfo unitShopInfo = new JIT.CPOS.BS.Entity.UnitInfo();//门店
                if (!strUnitInfo.Equals(""))
                {
                    unitShopInfo = (JIT.CPOS.BS.Entity.UnitInfo)cXMLService.Deserialize(strUnitInfo, typeof(JIT.CPOS.BS.Entity.UnitInfo));        
                }
                else
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetBSInitialInfo:{0}", "门店信息不存在")
                    });
                    throw new Exception("门店信息不存在.");
                }

                #region 门店是否存在
                JIT.CPOS.BS.Entity.UnitInfo unitStoreInfo2 = new JIT.CPOS.BS.Entity.UnitInfo();
                try
                {
                    unitStoreInfo2 = new UnitService(loggingSessionInfo).GetUnitById(unitShopInfo.Id);
                }
                catch (Exception ex)
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetBSInitialInfo:{0}", "获取门店失败")
                    });
                    throw new Exception("获取门店失败：" + ex.ToString());
                }
                if (unitStoreInfo2 == null || !string.IsNullOrEmpty(unitStoreInfo2.Id))
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetBSInitialInfo:{0}", "门店信息已经存在")
                    });
                    throw new Exception("门店信息已经存在.");
                }
                #endregion
                #endregion

                #region 新建角色

                RoleModel roleInfo = new RoleModel();
                if (typeId.Equals("1"))
                {
                    roleInfo.Role_Id = BaseService.NewGuidPub();
                    roleInfo.Def_App_Id = "D8C5FF6041AA4EA19D83F924DBF56F93";
                    roleInfo.Role_Code = "Admin";
                    roleInfo.Role_Name = "管理员";
                    roleInfo.Role_Eng_Name = "administrator";
                    roleInfo.Is_Sys = 1;
                    roleInfo.Status = 1;
                    roleInfo.customer_id = customerInfo.ID;
                    roleInfo.Create_User_Id = userInfo.User_Id;
                    roleInfo.Create_Time = new BaseService().GetCurrentDateTime();
                    strReturn = new RoleService(loggingSessionInfo).SetRoleInfo(roleInfo,false);
                    if (!strReturn.Equals("成功"))
                    {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("SetBSInitialInfo:{0}", "新建角色失败")
                        });
                        throw new Exception("新建角色失败");
                    }
                    else {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("SetBSInitialInfo:{0}", "新建角色成功")
                        });
                    }
                }
                #endregion

                #region 角色与流程关系
                if (typeId.Equals("1"))
                {
                    bReturn = new cBillService(loggingSessionInfo).SetBatBillActionRole(roleInfo.Role_Id);
                    if (!bReturn)
                    {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("SetBSInitialInfo:{0}", "创建角色与流程关系失败")
                        });
                        throw new Exception("创建角色与流程关系失败");
                    }
                    else {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("SetBSInitialInfo:{0}", "角色与流程关系成功")
                        });
                    }
                }
                #endregion

                #region 处理用户信息

                if (typeId.Equals("1"))
                {
                    IList<UserRoleInfo> userRoleInfos = new List<UserRoleInfo>();
                    UserRoleInfo userRoleInfo = new UserRoleInfo();
                    userRoleInfo.Id = BaseService.NewGuidPub();
                    userRoleInfo.RoleId = roleInfo.Role_Id;
                    userRoleInfo.UserId = userInfo.User_Id;
                    userRoleInfo.UnitId = unitShopInfo.Id;
                    userRoleInfos.Add(userRoleInfo);
                    loggingSessionInfo.CurrentUserRole = userRoleInfo;
                    bReturn = userServer.SetUserInfo(userInfo, null, out strError,false);
                    if (!bReturn) {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("SetBSInitialInfo:{0}", "保存用户失败")
                        });
                        throw new Exception("保存用户失败"); }
                    else {
                        Loggers.Debug(new DebugLogInfo()
                        {
                            Message = string.Format("SetBSInitialInfo:{0}", "保存用户成功")
                        });
                    }
                }

                #endregion

                #region 处理新建客户总部
                UnitInfo unitInfo = new UnitInfo();
                UnitService unitServer = new UnitService(loggingSessionInfo);
                if (typeId.Equals("1") || typeId.Equals("2"))
                {
                    unitInfo.Id = BaseService.NewGuidPub();
                    unitInfo.TypeId = "2F35F85CF7FF4DF087188A7FB05DED1D";
                    unitInfo.Code = customerInfo.Code + "总部";
                    unitInfo.Name = customerInfo.Name + "总部";
                    unitInfo.CityId = customerInfo.city_id;
                    unitInfo.Status = "1";
                    unitInfo.Status_Desc = "正常";
                    unitInfo.CustomerLevel = 1;
                    unitInfo.customer_id = customerInfo.ID;
                    unitInfo.Parent_Unit_Id = "-99";
                    unitInfo.strDo = "Create";
                    strReturn = unitServer.SetUnitInfo(loggingSessionInfo, unitInfo,false);
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetBSInitialInfo:{0}","提交总部：" + strReturn.ToString())
                    });
                }

                #endregion

                #region 处理门店
               
                storeInfo.Id = unitShopInfo.Id;
                storeInfo.TypeId = "EB58F1B053694283B2B7610C9AAD2742";
                storeInfo.Code = customerInfo.Code;
                storeInfo.Name = customerInfo.Name;
                storeInfo.CityId = customerInfo.city_id;
                storeInfo.Status = "1";
                storeInfo.Status_Desc = "正常";
                storeInfo.CustomerLevel = 1;
                storeInfo.customer_id = customerInfo.ID;
                storeInfo.Parent_Unit_Id = unitInfo.Id;
                storeInfo.strDo = "Create";
                strReturn = unitServer.SetUnitInfo(loggingSessionInfo, storeInfo,false);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetBSInitialInfo:{0}", "提交门店：" + strReturn.ToString())
                });
                #endregion

                #region 插入用户与角色与客户总部关系
                IList<UserRoleInfo> userRoleInfoList = new List<UserRoleInfo>();
                if (typeId.Equals("1") || typeId.Equals("2"))
                {
                    UserRoleInfo userRoleInfo = new UserRoleInfo();
                    userRoleInfo.Id = BaseService.NewGuidPub();
                    userRoleInfo.UserId = userInfo.User_Id;
                    userRoleInfo.RoleId = roleInfo.Role_Id;
                    userRoleInfo.UnitId = unitInfo.Id;
                    userRoleInfo.Status = "1";
                    userRoleInfo.DefaultFlag = 1;
                    userRoleInfoList.Add(userRoleInfo);
                }
                UserRoleInfo userRoleInfo1 = new UserRoleInfo();
                userRoleInfo1.Id = BaseService.NewGuidPub();
                userRoleInfo1.UserId = userInfo.User_Id;
                userRoleInfo1.RoleId = roleInfo.Role_Id;
                userRoleInfo1.UnitId = storeInfo.Id;
                userRoleInfo1.Status = "1";
                userRoleInfo1.DefaultFlag = 0;
                userRoleInfoList.Add(userRoleInfo1);
                bReturn = userServer.SetUserRoleTableInfo(loggingSessionInfo, userRoleInfoList, userInfo);
                if (!bReturn)
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetBSInitialInfo:{0}", "新建角色用户门店关系失败")
                    });
                    throw new Exception("新建角色用户门店关系失败");
                }
                else {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetBSInitialInfo:{0}", "新建角色用户门店关系成功")
                    });
                }
                #endregion

                #region 添加仓库以及仓库与门店关系
                JIT.CPOS.BS.Entity.Pos.WarehouseInfo warehouseInfo = new JIT.CPOS.BS.Entity.Pos.WarehouseInfo();
                warehouseInfo.warehouse_id = BaseService.NewGuidPub();
                warehouseInfo.wh_code = storeInfo.Code + "_wh";
                warehouseInfo.wh_name = storeInfo.Name + "仓库";
                warehouseInfo.is_default = 1;
                warehouseInfo.wh_status = 1;
                warehouseInfo.CreateUserID = userInfo.User_Id;
                warehouseInfo.CreateTime = Convert.ToDateTime(new BaseService().GetCurrentDateTime());
                warehouseInfo.CreateUserName = userInfo.User_Name;
                warehouseInfo.Unit = storeInfo;

                PosService posService = new PosService(loggingSessionInfo);
                bReturn = posService.InsertWarehouse(warehouseInfo,false);
                if (!bReturn)
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetBSInitialInfo:{0}", "新建仓库失败")
                    });
                    throw new Exception("新建仓库失败");
                }
                else {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetBSInitialInfo:{0}", "新建仓库成功")
                    });
                }
                #endregion

                #region 设置菜单信息
                if (typeId.Equals("1") || typeId.Equals("2"))
                {
                    if (!strMenu.Equals(""))
                    {
                        CMenuService menuServer = new CMenuService(loggingSessionInfo);
                        bReturn = new CMenuService(loggingSessionInfo).SetMenuInfo(strMenu, customerInfo.ID, false);
                        if (!bReturn) {
                            Loggers.Debug(new DebugLogInfo()
                            {
                                Message = string.Format("SetBSInitialInfo:{0}", "新建菜单失败")
                            });
                            throw new Exception("新建菜单失败"); }
                        else {
                            Loggers.Debug(new DebugLogInfo()
                            {
                                Message = string.Format("SetBSInitialInfo:{0}", "新建菜单成功")
                            });
                        }
                    }
                }
                #endregion

                #region 20131127设置固定标签
                TagsBLL tagsServer = new TagsBLL(loggingSessionInfo);
                bool bReturnTags = tagsServer.setCopyTag(customerInfo.ID);
                #endregion

                scope.Complete();
                scope.Dispose();    
            }
            
            #region 管理平台--插入客户下的用户信息
            if (typeId.Equals("1"))
            {
                if (!new cUserService(loggingSessionInfo).SetManagerExchangeUserInfo(loggingSessionInfo, userInfo, 1))
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetBSInitialInfo:{0}", "提交管理平台用户信息失败")
                    });
                    strError = "提交管理平台失败";
                    bReturn = false;
                    return bReturn;
                }
                else
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetBSInitialInfo:{0}", "提交管理平台用户信息成功")
                    });
                }
            }
            #endregion

            #region 管理平台--插入客户下的门店信息

            bReturn = new UnitService(loggingSessionInfo).SetManagerExchangeUnitInfo(loggingSessionInfo, storeInfo, 1);
            if (!bReturn)
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetBSInitialInfo:{0}", "门店插入管理平台失败")
                });
                throw new Exception("门店插入管理平台失败");
            }
            else
            {
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetBSInitialInfo:{0}", "门店插入管理平台成功")
                });
            }

            #endregion

            #region 中间层--插入用户，客户关系
            if (typeId.Equals("1"))
            {
                userInfo.customer_id = customerInfo.ID;
                string strResult = cUserService.SetDexUserCertificate(loggingSessionInfo, userInfo);
                if (!(strResult.Equals("True") || strResult.Equals("true")))
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetBSInitialInfo:{0}", "插入SSO失败")
                    });
                    strError = "插入SSO失败";
                    bReturn = false;
                    return bReturn;
                }
                else
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetBSInitialInfo:{0}", "插入SSO成功")
                    });
                }
            }
            #endregion

            return true;
        }



        #endregion
    }
}
