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
    
    public partial class AutoMarKet_OrderRecord
    {
        public string ID { get; set; }
        public string PromptlyConfigId { get; set; }
        public string OrderId { get; set; }
        public string Url { get; set; }
        public Nullable<System.DateTime> CountdownStime { get; set; }
        public Nullable<System.DateTime> CountdownEtime { get; set; }
        public bool IsClick { get; set; }
        public Nullable<System.DateTime> ClickTime { get; set; }
        public string UserId { get; set; }
        public Nullable<int> GoodBuyNum { get; set; }
        public Nullable<decimal> AmountOrder { get; set; }
        public string CompanyId { get; set; }
        public Nullable<System.DateTime> AddTime { get; set; }
    }
}
