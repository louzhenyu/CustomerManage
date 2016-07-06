using JIT.CPOS.BS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.BLL.SapMessage
{
    public class SapUDAOITCMsg : BaseSapMsg
    {
        public T_Item_CategoryBLL CategoryBll { get; set; }

        public bool isExits { get; set; }

        public SapUDAOITCMsg()
            : base()
        {
            CategoryBll = new T_Item_CategoryBLL(loggingSessionInfo);
        }

        public override bool Add()
        {
            T_Item_CategoryEntity categoryEntity = GetItemCategory();

            if (categoryEntity != null && !isExits)
            {
                CategoryBll.Create(categoryEntity);
                return true;
            }
            return false;
        }

        public override bool Update()
        {
            T_Item_CategoryEntity categoryEntity = GetItemCategory();
            if (categoryEntity != null && isExits)
            {
                CategoryBll.Update(categoryEntity);
                return true;
            }
            return false;
        }

        public override bool Delete()
        {
            T_Item_CategoryEntity categoryEntity = CategoryBll.QueryByEntity(new T_Item_CategoryEntity() { item_category_code = MsgObjRD.Omsg.FieldValues }, null).FirstOrDefault();
            if (categoryEntity != null)
            {
                categoryEntity.status = "0";
                categoryEntity.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                CategoryBll.Update(categoryEntity);
                return true;
            }
            Msg = "不存在该商品类别：" + MsgObjRD.Omsg.FieldValues;
            return false;
        }

        private T_Item_CategoryEntity GetItemCategory()
        {
            string path = "BOM/BO/UDAOITC/row/";
            string categoryCode = ReadXml(path + "Code");
            T_Item_CategoryEntity category = CategoryBll.QueryByEntity(new T_Item_CategoryEntity() { item_category_code = categoryCode }, null).FirstOrDefault();

            if (category == null)
            {
                isExits = false;
                category = new T_Item_CategoryEntity();
                category.item_category_id = Guid.NewGuid().ToString().Replace("-", "");
                category.item_category_code = categoryCode;

                category.CustomerID = "7e144bf108b94505a890ec3a7820db8d";
                category.DisplayIndex = 1;
                category.bat_id = "1";
                category.if_flag = "0";
                category.create_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                category.modify_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {

                isExits = true;
            }
            string fatherCode = ReadXml(path + "U_FatherCode");
            fatherCode = fatherCode == "-1" ? "all" : fatherCode;
            category.parent_id = GetItemFatherCode(fatherCode);
            category.item_category_name = ReadXml(path + "Name");
            string status = ReadXml(path + "Canceled");
            if (status == "N")
            {
                category.status = "1";
            }
            else
            {
                category.status = "0";
            }

            return category;
        }

        private string GetItemFatherCode(string fatherCode)
        {
            string fatherCodeId = "20DF4991-D1F9-46B8-8EF1-4AFE6AD75AF5";
            if (string.IsNullOrEmpty(fatherCode))
            {
                fatherCodeId = "20DF4991-D1F9-46B8-8EF1-4AFE6AD75AF5";
            }
            else
            {
                T_Item_CategoryEntity categoryFather = CategoryBll.QueryByEntity(new T_Item_CategoryEntity() { item_category_code = fatherCode }, null).FirstOrDefault();
                if (categoryFather != null)
                {
                    fatherCodeId = categoryFather.item_category_id;
                }
            }
            return fatherCodeId;
        }
    }
}
