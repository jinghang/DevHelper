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
    
    public partial class Shop_OrderProcess
    {
        public string ID { get; set; }
        public string OrderCode { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string Opeartor { get; set; }
        public Nullable<int> Status { get; set; }
        public short OperatorType { get; set; }
        public short PaymentStatus { get; set; }
        public short RefundStatus { get; set; }
    }
}
