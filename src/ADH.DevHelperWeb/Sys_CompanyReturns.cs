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
    
    public partial class Sys_CompanyReturns
    {
        public Sys_CompanyReturns()
        {
            this.Shop_Order = new HashSet<Shop_Order>();
        }
    
        public long Id { get; set; }
        public string Code { get; set; }
        public string CompanyId { get; set; }
        public System.DateTime CreateDateTime { get; set; }
        public System.DateTime ApplyDateTime { get; set; }
        public Nullable<System.DateTime> CompleteDateTime { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Rate { get; set; }
        public short Status { get; set; }
    
        public virtual ICollection<Shop_Order> Shop_Order { get; set; }
    }
}