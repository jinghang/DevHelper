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
    
    public partial class Shop_PrinterSetting
    {
        public int Id { get; set; }
        public string CompanyId { get; set; }
        public Nullable<bool> IsAutoPrint { get; set; }
        public string SellType { get; set; }
        public Nullable<int> PrintCount { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public Nullable<int> Interval { get; set; }
        public Nullable<int> PrinterType { get; set; }
        public string MachineCode { get; set; }
        public string MachineKey { get; set; }
        public Nullable<short> Status { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> QueryTime { get; set; }
    }
}