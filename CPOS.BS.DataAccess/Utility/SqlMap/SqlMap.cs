using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.DataAccess
{
    public static partial class SqlMap
    {
        #region BasicData

        public const string SQL_GETCHAINLIST =
            @"select C.ChainID,C.ChainName,New_C.ChainName as NewChainName,C.ChainLevel,C.CreateTime,C.IsDelete from Chain C
            left join Chain New_C on C.ParentID=New_C.ChainID where C.IsDelete=0 ";

        public const string SQL_GETCHANNELLIST =
            @"select C.ChannelID,C.ChannelName,C.ChannelLevel,C.CreateTime,C.IsDelete,__NC.ChannelName as NewChannelName from Channel C
            left join Channel __NC on C.ParentID=__NC.ChannelID where C.IsDelete=0 ";

        public const string SQL_GETUSEROPRIGHT =
            @" select distinct D.*,E.ClientButtonID,E.ButtonText,E.ButtonTextEn,E.ButtonCode 
            from ClientUser A  
            inner join PositionMenuButtonMapping B on A.ClientPositionID=B.ClientPositionID 
            inner join ClientMenuButton C on B.ClientMenuButtonID=C.ClientMenuButtonID 
            left join ClientMenu D on D.ClientMenuID=C.ClientMenuID 
            left join ClientButton E on C.ClientButtonID=E.ClientButtonID
            where A.ClientUserID=1 and A.IsDelete=0 and B.IsDelete=0 and C.IsDelete=0 order by D.MenuOrder desc";
        
        
        public const string SQL_GETSKULIST =
            @"select S.SKUID,S.SKUName,S.SKUNo,S.PackSpec,S.Spec,B.BrandName as S_BrandName,C.CategoryName as S_CategoryName,S.CreateTime,S.IsDelete from SKU S
            left join Brand B on S.BrandID=B.BrandID 
            left join Category C on S.CategoryID=C.CategoryID where S.IsDelete=0 ";

                
        public const string SQL_DELETESKU =
            @" update SKU set IsDelete=1 where SKUID in ('{0}') ";

        public const string SQL_GETCATEGORYLIST =
            @"select C.CategoryID,C.CategoryName,C.CategoryNo,C.CategoryLevel,New_C.CategoryName as NewCategoryName,C.Remark,C.IsDelete,C.CreateTime from Category as C left join Category as New_C on C.ParentID=New_C.CategoryID where C.IsDelete=0";

        public const string SQL_DELETECATEGORY =
            @" update Category set IsDelete=1 where CategoryID in ('{0}') ";

        public const string SQL_GETBRANDLIST =
            @"select b.BrandID,b.BrandNo,b.BrandName,b.BrandLevel,b.IsOwner,b.Remark,b.IsDelete,b.CreateTime,bd.BrandName as 'ParentBrandName',
            bd.BrandName as 'BrandCompany' from Brand as b left join Brand as bd on b.ParentID=bd.BrandID where b.IsDelete=0 ";
        
        public const string SQL_DELETEBRAND =
            @" update Brand set IsDelete=1 where BrandID in ('{0}') ";


        public const string SQL_GETINOUTSTATUSLIST =
            @"select T.*, 
	          O1.OptionText as OrderStatusName, 
	          O2.OptionText as CheckResultName, 
	          O3.OptionText as PayMethodName, 
	          U.unit_name 
              from TInoutStatus T 
              left join Options O1 on T.OrderStatus=O1.OptionValue and O1.OptionName='TInOutStatus' and O1.IsDelete=0 and O1.CustomerID='{0}'  
              left join Options O2 on T.CheckResult=O2.OptionValue and O2.OptionName='CheckResult' and O2.IsDelete=0 
              left join Options O3 on T.PayMethod=O3.OptionValue and O3.OptionName='PayMethod' and O3.IsDelete=0 
              left join t_unit U on T.DeliverCompanyID=U.unit_id 
              where T.IsDelete=0 and T.CustomerID='{0}' {1}";


        public const string SQL_GETTICKETLIST =
            @"select T.*,L.Title from Ticket T inner join LEvents L 
                 on T.EventID=L.EventID  
                 where T.IsDelete=0 and T.CustomerID='{0}' {1}";


        public const string SQL_GETORDERINTEGRALLIST =
            @"SELECT oi.*,ti.item_name,ti.item_code,vp.VipName,vp.VipCode 
                FROM dbo.OrderIntegral oi 
                LEFT JOIN dbo.T_Item ti ON oi.ItemID=ti.item_id and ti.status='1' 
                LEFT JOIN dbo.Vip vp ON oi.VIPID=vp.VIPID and ti.status=1 
                WHERE oi.IsDelete=0 {0}";

        #endregion

        #region VisitingSetting
        public const string SQL_GETPARAMETERLIST =
            @" select *, 
            (select OptionText 
            from Options 
            where OptionName='ParameterType' and OptionValue= A.ParameterType and IsDelete=0) as ParameterTypeText,
            (select OptionText 
            from Options 
            where OptionName='ControlType' and OptionValue= A.ControlType and IsDelete=0) as ControlTypeText
            from VisitingParameter A
            where A.IsDelete=0 ";

        public const string SQL_GETTASKLIST =
            @" select * ,
            (select OptionText 
            from Options 
            where OptionName='POPType' and OptionValue=A.POPType and IsDelete=0) as POPTypeText,
            (select top 1 unit_Name 
            from t_unit where ClientPositionID=A.ClientPositionID and IsDelete=0 ) as ClientPositionText,
            (select COUNT(*) from VisitingPOPMapping where VisitingTaskID=A.VisitingTaskID and IsDelete=0) as POPCount
            from VisitingTask A where A.IsDelete=0";


        public const string SQL_GETSTEPLIST =
@"select *,
(select OptionText from Options where OptionName='StepType' and OptionValue=A.StepType) as StepTypeText,
(select VisitingTaskName from VisitingTask where VisitingTaskID=A.VisitingTaskID) as VisitingTaskName
from VisitingTaskStep A
where A.IsDelete=0 and A.VisitingTaskID='{0}'";

        public const string SQL_DELSTEP =
            @"update VisitingTaskStep set IsDelete=1
            where VisitingTaskStepID in ('{0}')
            update VisitingTaskStepObject set IsDelete=1
            where VisitingTaskStepID in ('{0}')
            update VisitingTaskParameterMapping set IsDelete=1
            where VisitingTaskStepID in ('{0}')";

        public const string SQL_GETSTEPPARALIST =
            @"select A.*,
            B.MappingID,
            B.ParameterOrder ,
(select OptionText 
            from Options 
            where OptionName='ParameterType' and OptionValue= A.ParameterType and IsDelete=0) as ParameterTypeText,
            (select OptionText 
            from Options 
            where OptionName='ControlType' and OptionValue= A.ControlType and IsDelete=0) as ControlTypeText
            from VisitingParameter A
            left join VisitingTaskParameterMapping B on A.VisitingParameterID=B.VisitingParameterID and B.IsDelete=0 and B.VisitingTaskStepID='{0}'
            where A.IsDelete=0 and A.ClientID='{1}'";

        public const string SQL_GETSTEPPOSITIONLIST =
            @"select A.*,B.ObjectID
            from ClientPosition A 
            left join VisitingTaskStepObject B on A.ClientPositionID=B.Target1ID and B.IsDelete=0 and B.VisitingTaskStepID='{0}'
            where A.IsDelete=0 and A.ClientID='{1}'";

        

        #endregion
    }
}