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
    
    public partial class Sys_CompanyCategory
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> OrderNum { get; set; }
        public string ParentCategoryId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public byte[] Tfs { get; set; }
        public Nullable<int> Depth { get; set; }
        public string Path { get; set; }
        public string PicUrl { get; set; }
        public Nullable<bool> HasChild { get; set; }
        public string Remark { get; set; }
        public string CreaterId { get; set; }
        public string CreaterName { get; set; }
        public bool IsSystem { get; set; }
        public string CompanyId { get; set; }
    }
}