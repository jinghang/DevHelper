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
    
    public partial class Shop_AttributeValues
    {
        public string ValueId { get; set; }
        public string AttributeId { get; set; }
        public Nullable<int> DisplaySequence { get; set; }
        public string ValueStr { get; set; }
        public string ImageUrl { get; set; }
    
        public virtual Shop_ProductTypesAttributes Shop_ProductTypesAttributes { get; set; }
    }
}
