using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JIT.CPOS.BS.DAL
{
    public static class ControlSqlMap
    {       
        //判断表中是否存在树形数据
        public const string SQL_GETTREEBYPARENTID =
            @"select count(ParentId) 
            from {0}
            where ParentID is not null 
            and ISNULL(ClientID,{1})={1} 
            and isdelete=0 {2}";


        //渠道查询，查询全部数据
        public const string SQL_GETCHANNELBYCLIENTID =
            @"Select ChannelID,ChannelName,ParentID,IsLeaf
            from  Channel 
            where (ClientID='12' or ClientID is null)                  
                  and isdelete=0 {1} 
            order by ChannelLevel,CreateTime asc";

        //连锁查询，查询全部数据
        public const string SQL_GETCHAINBYCLIENTID =
            @"Select ChainID,ChainName,ParentID,IsLeaf
            from  Chain 
            where (ClientID='12' or ClientID is null)                  
                  and isdelete=0 {1} 
            order by ChainLevel,CreateTime asc";

        //选项查询，查询相对应的数据，根据OptionName
        public const string SQL_GETOPTIONSBYCLIENTID =
            @" select OptionName,OptionValue,OptionText
            from Options 
            where  (ClientID='{0}' or ClientID is null)
                    and IsDelete=0 
                    and OptionName='{1}'  
            order by Sequence,OptionValue";

        public const string SQL_GETCLIENTPOSITION =
           @"  select ClientPositionID,PositionName,ParentID,IsLeaf
            from ClientPosition 
            where (ClientID='12' or ClientID is null) 
                  and IsDelete=0  {1}
            order by  PositionLevel asc,ClientPositionID";     

        //品类查询，查询全部数据
        public const string SQL_GETCATEGORYBYCLIENTID =
            @"select CategoryID,CategoryName,ParentID,IsLeaf
            from Category
            where (ClientID='12' or ClientID is null)                  
                  and isdelete=0 {1} 
            order by  CategoryLevel,CreateTime asc";


        //品牌查询，查询全部数据
        public const string SQL_GETBRANDBYCLIENTID =
            @"select BrandID,BrandName,ParentID,IsLeaf
            from Brand
            where (ClientID='12' or ClientID is null)                  
                  and isdelete=0 {1} 
            order by  BrandLevel asc,CreateTime asc";

        //组织架构查询，查询全部数据
        public const string SQL_GETCLIENTSTRUCTUREBYCLIENTID =
            @"select ClientStructureID,StructureName,ParentID 
            from ClientStructure
            where (ClientID='12' or ClientID is null)                  
                  and isdelete=0 {1} 
            order by  StructureType,CreateTime asc";

        //层系选择项
        public const string SQL_GETHIERARCHY =
            @"select HierarchyName,ClientHierarchyID 
            from ClientHierarchy
            where (ClientID='12' or ClientID is null) 
                    and IsDelete=0 {1}
            order by HierarchyType ";

        //层系级别选择项
        public const string SQL_GETHIERARCHYLEVEL =
            @"select LevelName,ClientHierarachyLevelID,ParentID
            from ClientHierarchyLevel
            where (ClientID='12' or ClientID is null)
                    and IsDelete=0 {1}
            order by HierarchyLevel ";

        //层系项下拉
        public const string SQL_GETHIERARCHYITEM =
                @"select chi.ClientHierarchyItemID,chi.ItemName ,chi.ItemValue,chi.IsLeaf,chi.ParentID,chl.LevelName,ch.HierarchyName,chim.ItemValue as  PItemValue
                from ClientHierarchyItem as chi 
                left join ClientHierarchyLevel as chl 
                        on  chi.ClientHierarachyLevelID=chl.ClientHierarachyLevelID
                left join ClientHierarchy  as ch 
                        on  ch.ClientHierarchyID=chl.ClientHierarchyID 
                left join ClientHierarchyItem  as chim 
                        on chi.ParentID=chim.ClientHierarchyItemID
                where ch.ClientHierarchyID='{1}' 
                        and  chi.IsDelete=0 and chl.IsDelete=0 and ch.IsDelete=0
                        and ISNULL(chi.ClientID,{0})={0} and ISNULL(chl.ClientID,{0})={0} and ISNULL(ch.ClientID,{0})={0}
                        {2}
                order by chi.CreateTime";

        //省的查询语句
        public const string SQL_GETPROVINCE = 
                @"select ProvinceID,Province,ProvinceEn 
                from province
                where IsDelete=0 
                order by ProvinceID";

        //市的查询语句
        public const string SQL_GETCITY = 
                @"select CityID,City,CityEn 
                from City
                where IsDelete=0
                      and ProvinceID  in({0})  
                order by ProvinceID,CityID";

        //市的查询，根据CityID查询
        public const string SQL_GetCITYBYCITYID =
                @"select CityID,City,CityEn 
                from City
                where IsDelete=0
                      and ProvinceID=(select Top 1 ProvinceID from City where CityID={0})  
                order by ProvinceID,CityID";

        //县的查询语句
        public const string SQL_GETDISTRICT =
                @"select DistrictID,District,DistrictEn
                from District
                where IsDelete=0
                      and CityID in({0}) 
                order by CityID,DistrictID";

        //县的查询,根据DistrictID查询
        public const string SQL_GETDISTRICTBYDISTRICTYID =
                @"select DistrictID,District,DistrictEn
                from District
                where IsDelete=0
                      and CityID =(select Top 1 CityID from District where DistrictID={0}) 
                order by CityID,DistrictID";
    }
}
