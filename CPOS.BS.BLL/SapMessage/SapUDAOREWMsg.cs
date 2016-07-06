using JIT.CPOS.BS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL.SapMessage
{
    public class SapUDAOREWMsg : BaseSapMsg
    {
        public T_TypeBLL typeBll { get; set; }

        public t_unitBLL unitBll { get; set; }

        public bool IsExits { get; set; }
        public SapUDAOREWMsg()
            : base()
        {
            typeBll = new T_TypeBLL(loggingSessionInfo);
            unitBll = new t_unitBLL(loggingSessionInfo);
        }

        #region 增加多利发货区域
        /// <summary>
        /// 增加多利发货区域
        /// </summary>
        public override bool Add()
        {
            if (string.IsNullOrEmpty(MsgObjRD.Msg1.Content) && MsgObjRD.Omsg.Status > 0)
            {
                Msg = "Content 为空，无消息,status=" + MsgObjRD.Omsg.Status;
                return true;
            }

            if (string.IsNullOrEmpty(MsgObjRD.Msg1.Content))
            {
                Msg = "Content 为空，无消息";
                return false;
            }

            t_unitEntity unit = GetUnitEntity();
            if (IsExits)
            {
                return true;
            }

            var typeCenterEntity = typeBll.QueryByEntity(new T_TypeEntity() { type_code = "tonySendCenterArea" }, null).FirstOrDefault();
            var typeLeafEntity = typeBll.QueryByEntity(new T_TypeEntity() { type_code = "tonySendLeafArea" }, null).FirstOrDefault();

            if (typeCenterEntity == null)
            {
                typeCenterEntity = GetCenterArea();
                typeBll.Create(typeCenterEntity);
            }
            if (typeLeafEntity == null)
            {
                typeLeafEntity = GetLeafArea();
                typeBll.Create(typeLeafEntity);
            }
            if (unit.unit_code == "A01")
            {
                unit.type_id = typeCenterEntity.type_id;
            }
            else
            {
                unit.type_id = typeLeafEntity.type_id;
            }
            unit.unit_id = Guid.NewGuid().ToString().Replace("-", "");
            unitBll.Create(unit);

            string path = "BOM/BO/UDAOREW/row/";
            string sql = @"INSERT dbo.T_Unit_Relation
                        ( unit_relation_id ,
                          src_unit_id ,
                          dst_unit_id ,
                          status ,
                          create_time ,
                          modify_time
                        )
                VALUES  ( N'{0}' , -- unit_relation_id - nvarchar(50)
                          N'{1}' , -- src_unit_id - nvarchar(50)
                          N'{2}' , -- dst_unit_id - nvarchar(50)
                          0 , -- status - int
                          GETDATE() , -- create_time - datetime
                          GETDATE()  -- modify_time - datetime
                        )";
            var fathercode = ReadXml(path + "U_FatherCode");
            var tempUnitId = string.Empty;
            if (fathercode == "-1")
            {
                tempUnitId = unit.unit_id;
            }
            else
            {
                tempUnitId = unitBll.QueryByEntity(new t_unitEntity() { unit_code = fathercode }, null).FirstOrDefault().unit_id;
            }
            sql = string.Format(sql, Guid.NewGuid().ToString().Replace("-", ""), unit.unit_id, tempUnitId);
            unitBll.CreateUnitRelation(sql);
            return true;
        }
        #endregion

        #region 获取发货分区域
        /// <summary>
        /// 多利发货分区域
        /// </summary>
        /// <returns></returns>
        private T_TypeEntity GetLeafArea()
        {
            var typeCenterEntity = new T_TypeEntity();
            typeCenterEntity.type_id = Guid.NewGuid().ToString().Replace("-", "");
            typeCenterEntity.type_code = "tonySendLeafArea";
            typeCenterEntity.type_name = "多利发货分区域";
            typeCenterEntity.type_name_en = "tonySendLeafArea";
            typeCenterEntity.type_domain = "UnitType";
            typeCenterEntity.type_system_flag = 1;
            typeCenterEntity.type_Level = 1;
            typeCenterEntity.status = 1;
            typeCenterEntity.customer_id = "7e144bf108b94505a890ec3a7820db8d";
            return typeCenterEntity;
        }
        #endregion

        #region 获取发货总区域
        /// <summary>
        /// 获取发货总区域
        /// </summary>
        /// <returns></returns>
        private T_TypeEntity GetCenterArea()
        {
            var typeCenterEntity = new T_TypeEntity();
            typeCenterEntity.type_id = Guid.NewGuid().ToString().Replace("-", "");
            typeCenterEntity.type_code = "tonySendCenterArea";
            typeCenterEntity.type_name = "多利发货总区域";
            typeCenterEntity.type_name_en = "tonySendCenterArea";
            typeCenterEntity.type_domain = "UnitType";
            typeCenterEntity.type_system_flag = 1;
            typeCenterEntity.type_Level = 1;
            typeCenterEntity.status = 1;
            typeCenterEntity.customer_id = "7e144bf108b94505a890ec3a7820db8d";
            return typeCenterEntity;
        }
        #endregion

        #region 获取销售区域信息
        /// <summary>
        /// 获取销售区域信息
        /// </summary>
        /// <returns></returns>
        private t_unitEntity GetUnitEntity()
        {
            string path = "BOM/BO/UDAOREW/row/";
            t_unitEntity unit = new t_unitEntity();
            unit.unit_code = ReadXml(path + "Code");

            var unitTemp = unitBll.QueryByEntity(new t_unitEntity() { unit_code = unit.unit_code }, null).FirstOrDefault();
            if (unitTemp != null && !string.IsNullOrEmpty(unitTemp.unit_id))
            {
                IsExits = true;
                return unit;
            }
            unit.unit_name = ReadXml(path + "Name");
            unit.customer_id = "7e144bf108b94505a890ec3a7820db8d";
            unit.unit_name_en = "";
            unit.Status = "1";
            unit.create_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            unit.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return unit;
        }
        #endregion

        public override bool Update()
        {
            if (string.IsNullOrEmpty(MsgObjRD.Msg1.Content) && MsgObjRD.Omsg.Status > 0)
            {
                Msg = "Content 为空，无消息,status=" + MsgObjRD.Omsg.Status;
                return true;
            }

            if (string.IsNullOrEmpty(MsgObjRD.Msg1.Content))
            {
                Msg = "Content 为空，无消息";
                return false;
            }

            string path = "BOM/BO/UDAOREW/row/";
            t_unitBLL unitBll = new t_unitBLL(loggingSessionInfo);
            string code = ReadXml(path + "Code");
            t_unitEntity unit = unitBll.QueryByEntity(new t_unitEntity() { unit_code = code }, null).FirstOrDefault();
            unit.unit_code = code;
            unit.unit_name = ReadXml(path + "Name");
            if (ReadXml(path + "Canceled") == "Y")
            {
                unit.Status = "0";
            }
            unit.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            unitBll.Update(unit);
            return true;
        }

        public override bool Delete()
        {
            t_unitBLL unitBll = new t_unitBLL(loggingSessionInfo);
            t_unitEntity unit = unitBll.QueryByEntity(new t_unitEntity() { unit_code = MsgObjRD.Omsg.FieldValues }, null).FirstOrDefault();
            if (unit != null)
            {

                unit.Status = "0";
                unitBll.Update(unit);

                return true;
            }
            Msg = " 未查询到区域：" + MsgObjRD.Omsg.FieldValues;
            return false;
        }
    }
}
