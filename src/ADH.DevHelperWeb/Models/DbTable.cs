using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADH.DevHelperWeb.Models
{
    public class DbTable
    {
        public string TableName { get; set; }
        public string 列名 { get; set; }
        public string 数据类型 { get; set; }
        public int 长度 { get; set; }
        public string 是否为空 { get; set; }
        public string 默认值 { get; set; }
        public string 说明 { get; set; }
    }
}