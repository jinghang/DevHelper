//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ADH.DevHelperWeb
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sys_PushCityInfo
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string CreatorId { get; set; }
        public string CreatorName { get; set; }
        public string PushInfo { get; set; }
        public int PushType { get; set; }
        public Nullable<int> ActiveType { get; set; }
        public Nullable<short> PushStatus { get; set; }
        public Nullable<int> UserCount { get; set; }
        public string CompanyId { get; set; }
        public Nullable<System.DateTime> SendTime { get; set; }
    }
}
