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
    
    public partial class Sys_SearchTable
    {
        public string ID { get; set; }
        public string TagCategoryID { get; set; }
        public string ParentID { get; set; }
        public Nullable<bool> isTag { get; set; }
        public int Sort { get; set; }
        public string CompanyID { get; set; }
        public string Path { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<bool> IsChild { get; set; }
        public string SortNum { get; set; }
    }
}
