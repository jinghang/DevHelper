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
    
    public partial class Shop_OrderRoom
    {
        public string ID { get; set; }
        public string OrderCode { get; set; }
        public string UserName { get; set; }
        public string Sex { get; set; }
        public Nullable<int> PeopleNum { get; set; }
        public string RoomType { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> ToStoreTime { get; set; }
        public Nullable<int> IsForOrther { get; set; }
        public Nullable<int> PayType { get; set; }
        public Nullable<decimal> OldPrice { get; set; }
        public Nullable<decimal> NowPrice { get; set; }
        public string Invoice { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> AddTime { get; set; }
        public string UserId { get; set; }
        public string CompanyId { get; set; }
        public Nullable<int> ClientType { get; set; }
        public string Phone { get; set; }
        public string ClientMobileType { get; set; }
        public int PrintCount { get; set; }
    }
}
