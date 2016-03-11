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
            typeId = "1";//统一作为1来处理
            bool bReturn = false;
            string strError = string.Empty;
            string strReturn = string.Empty;
           // UnitInfo storeInfo = new UnitInfo();

            cUserService userServer = new cUserService(loggingSessionInfo);

            #region 处理用户信息
            userInfo.User_Id = BaseService.NewGuidPub(); //用户标识
            userInfo.customer_id = customerInfo.ID;//客户标识
            userInfo.User_Code = "admin";
            userInfo.User_Name = "管理员";
            userInfo.User_Gender = "1";
            userInfo.User_Password = "888888";
            userInfo.User_Status = "1";
            userInfo.User_Status_Desc = "正常";
            userInfo.strDo = "Create";
            #endregion

            #endregion
            UnitInfo unitInfo = new UnitInfo();
            using (TransactionScope scope = new TransactionScope())
            {
                #region 门店
                /**
                JIT.CPOS.BS.Entity.UnitInfo unitShopInfo = new JIT.CPOS.BS.Entity.UnitInfo();//门店
                if (!strUnitInfo.Equals(""))//把在运营商管理平台创建的门店(code就是商户的code)在这里反序列化
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
                 * **/

                #region 门店是否存在（在这之前门店并没有插入到运营商管理平台的数据库,只是序列化在字符串里）*****
               /**
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
                if (unitStoreInfo2 == null || !string.IsNullOrEmpty(unitStoreInfo2.Id))//***这里的判断是不是错了，应该是(unitStoreInfo2 != null || !string.IsNullOrEmpty(unitStoreInfo2.Id))
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetBSInitialInfo:{0}", "门店信息已经存在")
                    });
                    throw new Exception("门店信息已经存在.");
                }
                * **/
                #endregion
                #endregion

                #region
                //创建商户自己的门店类型
                T_TypeBLL t_TypeBLL = new T_TypeBLL(loggingSessionInfo);
                //创建总部类型
                T_TypeEntity typeGeneral = new T_TypeEntity();
                typeGeneral.type_id = BaseService.NewGuidPub();//去掉了中间的‘-’
                typeGeneral.type_code = "总部";
                typeGeneral.type_name = "总部";
                typeGeneral.type_name_en = "总部";
                typeGeneral.type_domain = "UnitType";
                typeGeneral.type_system_flag =1;
                typeGeneral.status = 1;
                typeGeneral.type_Level = 1;
                typeGeneral.customer_id = customerInfo.ID;
                t_TypeBLL.Create(typeGeneral);
                //创建门店类型
                T_TypeEntity typeShop = new T_TypeEntity();
                typeShop.type_id = BaseService.NewGuidPub();//去掉了中间的‘-’
                typeShop.type_code = "门店";
                typeShop.type_name = "门店";
                typeShop.type_name_en = "门店";
                typeShop.type_domain = "UnitType";
                typeShop.type_system_flag = 1;
                typeShop.status = 1;
                typeShop.type_Level = 99;//等到在页面上保存时在根据增加的层级来改变他的层级
                typeShop.customer_id = customerInfo.ID;
                t_TypeBLL.Create(typeShop);

                //创建在线商城类型
                T_TypeEntity typeOnlineShopping = new T_TypeEntity();
                typeOnlineShopping.type_id = BaseService.NewGuidPub();//去掉了中间的‘-’
                typeOnlineShopping.type_code = "OnlineShopping";
                typeOnlineShopping.type_name = "在线商城";
                typeOnlineShopping.type_name_en = "OnlineShopping";
                typeOnlineShopping.type_domain = "UnitType";
                typeOnlineShopping.type_system_flag = 1;
                typeOnlineShopping.status = 1;
                typeOnlineShopping.type_Level = -99;//在线商城类型不算在组织类型里，因此排在外面
                typeOnlineShopping.customer_id = customerInfo.ID;
                t_TypeBLL.Create(typeOnlineShopping);


                #endregion

                #region 新建角色
                
                RoleModel roleInfo = new RoleModel();
                if (typeId.Equals("1"))
                {
                    roleInfo.Role_Id = BaseService.NewGuidPub();
                    roleInfo.Def_App_Id = "D8C5FF6041AA4EA19D83F924DBF56F93";  //创建了一个o2o业务系统的角色
                    roleInfo.Role_Code = "Admin";
                    roleInfo.Role_Name = "管理员";
                    roleInfo.Role_Eng_Name = "Admin";
                    roleInfo.Is_Sys = 1;
                    roleInfo.Status = 1;
                    roleInfo.customer_id = customerInfo.ID;
                    roleInfo.Create_User_Id = userInfo.User_Id;
                    roleInfo.Create_Time = new BaseService().GetCurrentDateTime();
//新加上所处的层级
                    roleInfo.type_id = typeGeneral.type_id;
                    roleInfo.org_level = (int)typeGeneral.type_Level;//等级

                    string strerror = "";
                    strReturn = new RoleService(loggingSessionInfo).SetRoleInfo(roleInfo,out strerror, false);//这里没有创建他的菜单
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
                    bReturn = new cBillService(loggingSessionInfo).SetBatBillActionRole(roleInfo.Role_Id);//不知道有什么作用
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


               
                //UnitInfo unitInfo = new UnitInfo();
                UnitService unitServer = new UnitService(loggingSessionInfo);
                unitInfo.Id = BaseService.NewGuidPub();//先把总部的标识生成好

                #region 插入用户与角色与客户总部关系
                IList<UserRoleInfo> userRoleInfoList = new List<UserRoleInfo>();
                UserRoleInfo userRoleInfo = new UserRoleInfo();//后面要用到总部的信息
                if (typeId.Equals("1") || typeId.Equals("2"))
                {
                    userRoleInfo.Id = BaseService.NewGuidPub();
                    userRoleInfo.UserId = userInfo.User_Id;
                    userRoleInfo.RoleId = roleInfo.Role_Id; //admin角色*****
                    userRoleInfo.UnitId = unitInfo.Id;//总部下的*** 
                    userRoleInfo.Status = "1";
                    userRoleInfo.DefaultFlag = 1;
                    userRoleInfoList.Add(userRoleInfo);
                }
                loggingSessionInfo.CurrentUserRole = userRoleInfo;//主要是保存总部和员工信息时，会用到
                #region 处理新建客户总部
                if (typeId.Equals("1") || typeId.Equals("2"))
                {
                    
                    //   unitInfo.TypeId = "2F35F85CF7FF4DF087188A7FB05DED1D";//这里要替换成该商户自己的门店类型标识****
                    unitInfo.TypeId = typeGeneral.type_id;//***

                    unitInfo.Code = "总部";//customerInfo.Code +
                    unitInfo.Name = "总部";// customerInfo.Name +
                    unitInfo.CityId = customerInfo.city_id;
                    unitInfo.Status = "1";
                    unitInfo.Status_Desc = "正常";
                    unitInfo.CustomerLevel = 1;
                    unitInfo.customer_id = customerInfo.ID;
                    unitInfo.Parent_Unit_Id = "-99";
                    unitInfo.strDo = "Create";
                    strReturn = unitServer.SetUnitInfo(loggingSessionInfo, unitInfo, false);
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetBSInitialInfo:{0}", "提交总部：" + strReturn.ToString())
                    });
                }

                #endregion

                #region 处理门店，用的是商户的code和名称，id没有用*****
                /**
                storeInfo.Id = unitShopInfo.Id;//（运营商管理平台创建的门店(Shop代表门店***)
                if (string.IsNullOrEmpty(unitShopInfo.Id))
                {
                    storeInfo.Id = BaseService.NewGuidPub();
                }
               // storeInfo.TypeId = "EB58F1B053694283B2B7610C9AAD2742"; //整个平台的系统类型（门店类型）
                storeInfo.TypeId = typeShop.type_id;//上面创建的门店类型
                storeInfo.Code = customerInfo.Code;//商户的code
                storeInfo.Name = customerInfo.Name;//商户的name
                storeInfo.Name = customerInfo.Name;//商户的name

                storeInfo.CityId = customerInfo.city_id;
                storeInfo.Status = "1";
                storeInfo.Status_Desc = "正常";
                storeInfo.CustomerLevel = 1;
                storeInfo.customer_id = customerInfo.ID;
                storeInfo.Parent_Unit_Id = unitInfo.Id;//总部下面的一个门店
                storeInfo.strDo = "Create";
                storeInfo.StoreType = "DirectStore";//设置为直营店
                strReturn = unitServer.SetUnitInfo(loggingSessionInfo, storeInfo,false);
                Loggers.Debug(new DebugLogInfo()
                {
                    Message = string.Format("SetBSInitialInfo:{0}", "提交门店：" + strReturn.ToString())
                });
                 * **/
                #endregion


                //在线商城门店是在存储过程[spBasicSetting]里添加的

           
                //不建立下面的关系，就可以之保留admin的总部角色权限了
                /**
                UserRoleInfo userRoleInfo1 = new UserRoleInfo();
                userRoleInfo1.Id = BaseService.NewGuidPub();
                userRoleInfo1.UserId = userInfo.User_Id;
                userRoleInfo1.RoleId = roleInfo.Role_Id;
                userRoleInfo1.UnitId = storeInfo.Id;//还和子门店建立了关系（id为商户的id）
                userRoleInfo1.Status = "1";
                userRoleInfo1.DefaultFlag = 0;
                userRoleInfoList.Add(userRoleInfo1);
                 *  * **/
                bReturn = userServer.SetUserInfo(userInfo, null, out strError, false);//保存用户信息(这里会把之前建的会员的角色信息给删除掉)，所以要在后面再建立角色和会员的关系
                bReturn = userServer.SetUserRoleTableInfo(loggingSessionInfo, userRoleInfoList, userInfo);//只建立了用户和总部之间的关系
                if (!bReturn)
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetBSInitialInfo:{0}", "新建角色用户门店关系失败")
                    });
                    throw new Exception("新建角色用户门店关系失败");
                }
                else
                {
                    Loggers.Debug(new DebugLogInfo()
                    {
                        Message = string.Format("SetBSInitialInfo:{0}", "新建角色用户门店关系成功")
                    });
                }

                #endregion

                #region 处理用户信息

                if (typeId.Equals("1"))
                {
                    /**
                    IList<UserRoleInfo> userRoleInfos = new List<UserRoleInfo>();
                    UserRoleInfo userRoleInfo = new UserRoleInfo();
                    userRoleInfo.Id = BaseService.NewGuidPub();
                    userRoleInfo.RoleId = roleInfo.Role_Id;// 加上管理员权限
                    userRoleInfo.UserId = userInfo.User_Id;
                    userRoleInfo.UnitId = unitShopInfo.Id;//新建的门店标识（运营商管理平台创建的门店(Shop代表门店***)，code=customer_code,name=customer_name）
                    userRoleInfos.Add(userRoleInfo);
                    loggingSessionInfo.CurrentUserRole = userRoleInfo;
                     * **/
                  
                  
                    //先给admin键了一个门店的权限*****，可以给去掉，去掉上面一句话就可以了****

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

             


                #region 添加仓库以及仓库与门店关系
                /**
                JIT.CPOS.BS.Entity.Pos.WarehouseInfo warehouseInfo = new JIT.CPOS.BS.Entity.Pos.WarehouseInfo();
                warehouseInfo.warehouse_id = BaseService.NewGuidPub();
                warehouseInfo.wh_code = storeInfo.Code + "_wh";//建立了刚才见的子门点的仓库
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
                 * **/
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

            #region 管理平台--插入客户下的门店信息(只是提交门店级别的)
    
          //  bReturn = new UnitService(loggingSessionInfo).SetManagerExchangeUnitInfo(loggingSessionInfo, storeInfo, 1);
            bReturn = new UnitService(loggingSessionInfo).SetManagerExchangeUnitInfo(loggingSessionInfo, unitInfo, 1);
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
